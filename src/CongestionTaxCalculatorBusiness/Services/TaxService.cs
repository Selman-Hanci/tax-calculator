namespace CongestionTaxCalculatorBusiness.Services
{
    public class TaxService : ITaxService
    {
        public ICongestionTaxCalculatorService congestionTaxCalculatorService;

        public TaxService(ICongestionTaxCalculatorService _congestionTaxCalculatorService)
        {
            congestionTaxCalculatorService = _congestionTaxCalculatorService;
        }

        public int GetTax(string vehicleType, string[] stringDates, string[] taxRules)
        {
            return congestionTaxCalculatorService.GetTax(vehicleType, stringDates, taxRules);
        }
    }
}
