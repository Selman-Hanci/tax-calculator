using System;
using System.Linq;

namespace CongestionTaxCalculatorBusiness.Services
{
    public interface ICongestionTaxCalculatorService
    {
        int GetTax(string vehicleType, string[] stringDates, string[] taxRules);
    }
}