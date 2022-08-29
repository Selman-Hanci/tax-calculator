namespace CongestionTaxCalculatorAPI.Services
{
    public interface ITaxService
    {
        int GetTax(string vehicleType, string[] stringDates, string[] taxRules);
    }
}