using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using CongestionTaxCalculatorBusiness.Services;

public class TaxRule
{
    public int startInterval;
    public int endInterval;
    public int taxValue;

    public TaxRule(int _startInterval, int _endInterval, int _taxValue)
    {
        startInterval = _startInterval;
        endInterval = _endInterval;
        taxValue = _taxValue;
    }
}

public class CongestionTaxCalculatorService : ICongestionTaxCalculatorService
{
    List<TaxRule> cityTaxRules = new List<TaxRule>();

    /**
    * Set tax rules that are given via parameters
    *
    * @param taxRules - string formatted tax rules for city, needs to be in strict rule schema
    */
    private void SetTaxRules(string[] taxRules)
    {
        // Example: 06:00–06:29	SEK 8
        for (int i = 0; i < taxRules.Length; i++)
        {
            string row = (string)taxRules.GetValue(i);              // 06:00–06:29	SEK 8
            row = row.Replace("\t", " ");
            string[] splittedRow = row.Split(" ");
            string interval = splittedRow[0];                       // 06:00–06:29
            string[] splittedInterval = interval.Split("–");
            string firstInterval = splittedInterval[0];             // 06:00
            string secondInterval = splittedInterval[1];            // 06:29

            int firstIntervalValue = GetNumberOfSecond(firstInterval);
            int secondIntervalValue = GetNumberOfSecond(secondInterval);

            TaxRule taxRule = new TaxRule(firstIntervalValue, secondIntervalValue,
                Convert.ToInt32(splittedRow[2]));

            cityTaxRules.Add(taxRule);
        }
    }

    /**
    * Calculate the total toll fee for one day
    *
    * @param vehicle - the vehicle
    * @param dates   - date and time of all passes on one day
    * @return - the total congestion tax for that day
    */
    public int GetTax(string vehicleType, string[] stringDates, string[] taxRules)
    {
        SetTaxRules(taxRules);

        DateTime[] dates = stringDates.Select(s => DateTime.ParseExact(s, "yyyy-MM-dd HH:mm:ss",
                                  CultureInfo.InvariantCulture)).ToArray();

        // Sorting dates chronologically
        var datesList = dates.ToList();
        datesList.Sort((a, b) => a.CompareTo(b));

        int totalFee = 0;

        var dailyGroupedDates = datesList.GroupBy(d => d.Date.DayOfYear);

        foreach (var dailyDates in dailyGroupedDates)
        {
            int dailyTotalFee = 0;
            int lastHourMaxValue = 0;
            DateTime lastHourDate = DateTime.MinValue;

            foreach (var currentDate in dailyDates)
            {
                // set the first toll fee of current hour
                if (currentDate > lastHourDate.AddHours(1))
                {
                    lastHourDate = currentDate;
                    lastHourMaxValue = GetTollFee(currentDate, vehicleType);
                    dailyTotalFee += lastHourMaxValue;
                    continue;
                }
                else
                {
                    int tollFee = GetTollFee(currentDate, vehicleType);

                    // set maximum value of current hour
                    if (tollFee > lastHourMaxValue)
                    {
                        dailyTotalFee = dailyTotalFee + tollFee - lastHourMaxValue;
                        lastHourMaxValue = tollFee;
                    }
                }

                if (dailyTotalFee >= 60)
                {
                    dailyTotalFee = 60;
                    break;
                }
            }

            totalFee += dailyTotalFee;
        }

        return totalFee;
    }

    /**
    * Checks if the vehicle type is categorized as toll free or not
    *
    * @param vehicleType - string vehicle type value
    * @return - boolean value that weather the vehicle is required to pay the toll value
    */
    private bool IsTollFreeVehicle(string vehicleType)
    {
        if (vehicleType == null) return false;

        return vehicleType.Equals(nameof(TollFreeVehicles.Motorcycle)) ||
               vehicleType.Equals(nameof(TollFreeVehicles.Tractor)) ||
               vehicleType.Equals(nameof(TollFreeVehicles.Emergency)) ||
               vehicleType.Equals(nameof(TollFreeVehicles.Diplomat)) ||
               vehicleType.Equals(nameof(TollFreeVehicles.Foreign)) ||
               vehicleType.Equals(nameof(TollFreeVehicles.Military));
    }

    /**
    * Handles calculating the total number of second passed on day until given time
    *
    * @param interval - hour and minute value
    * @return - total number of second in integer format
    */
    private int GetNumberOfSecond(string interval)
    {
        // Example: 06:29
        string[] splittedInterval = interval.Split(":");
        int intervalValue = Convert.ToInt32(splittedInterval[0]) * 3600 +
            Convert.ToInt32(splittedInterval[1]) * 60;

        return intervalValue;
    }

    /**
    * Checks if the vehicle is required to pay toll fee and calculates toll fee that needs to be payed
    *
    * @param date - Toll passing date
    * @param vehicleType - string vehicle type value
    * @return - toll fee that needs to be payed 
    */
    private int GetTollFee(DateTime date, string vehicleType)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicleType)) return 0;

        string timeOfDay = date.ToString("HH:mm:ss");
        int numberOfSecond = GetNumberOfSecond(timeOfDay);

        for (int i = 0; i < cityTaxRules.Count; i++)
        {
            var taxRule = cityTaxRules.ElementAt(i);
            if (taxRule.startInterval < numberOfSecond && taxRule.endInterval > numberOfSecond)
            {
                return taxRule.taxValue;
            }
        }

        return 0;
    }

    /**
    * Checks if the toll passing date is marked as toll free date or not
    *
    * @param date - Toll passing date
    * @return - boolean value if the date is toll free date or not 
    */
    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (year != 2013) return false;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
        {
            return true;
        }

        return false;
    }

    private enum TollFreeVehicles
    {
        Motorcycle = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}