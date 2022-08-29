using CongestionTaxCalculatorAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculatorAPI.Controllers
{
    [Route("[controller]")]
    public class TaxCalculatorController : ControllerBase
    {
        private ITaxService taxService;

        public TaxCalculatorController(ITaxService _taxService)
        {
            taxService = _taxService; 
        }

        [HttpGet(Name = "TaxCalculation")]
        public int Get(string vehicleType, string[] stringDates, string[] taxRules)
        {
            return taxService.GetTax(vehicleType, stringDates, taxRules);
        }
    }
}
