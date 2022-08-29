namespace CongestionTaxCalculatorAPI.Services
{
    public class TaxService : ITaxService
    {
        public int GetTax(string vehicleType, string[] stringDates, string[] taxRules)
        {
            CongestionTaxCalculator congestionTaxCalculator = new CongestionTaxCalculator();

            return congestionTaxCalculator.GetTax(vehicleType, stringDates, taxRules);
        }
    }
}
