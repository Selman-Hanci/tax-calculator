namespace CongestionTaxCalculatorTest;

public class TaxCalculatorTest
{
        [Theory]
        [InlineData("Car",
            new string[] {
            "2013-01-14 21:00:00",
            "2013-01-15 21:00:00",
            "2013-02-07 06:23:27",
            "2013-02-07 15:27:00",
            "2013-02-08 06:27:00",
            "2013-02-08 06:20:27",
            "2013-02-08 14:35:00",
            "2013-02-08 15:29:00",
            "2013-02-08 15:47:00",
            "2013-02-08 16:01:00",
            "2013-02-08 16:48:00",
            "2013-02-08 17:49:00",
            "2013-02-08 18:29:00",
            "2013-02-08 18:35:00",
            "2013-03-26 14:25:00",
            "2013-03-28 14:07:27"},
            new string[] {
            "06:00–06:29 SEK 8",
            "06:30–06:59 SEK 13",
            "07:00–07:59 SEK 18",
            "08:00–08:29 SEK 13",
            "08:30–14:59 SEK 8",
            "15:00–15:29 SEK 13",
            "15:30–16:59 SEK 18",
            "17:00–17:59 SEK 13",
            "18:00–18:29 SEK 8",
            "18:30–05:59 SEK 0"
            },
            89)]
        public void TestCar(string vehicleType, string[] stringDates, string[] taxRules, int expected)
        {
        CongestionTaxCalculatorService congestionTaxCalculator = new CongestionTaxCalculatorService();

            int actual = congestionTaxCalculator.GetTax(vehicleType, stringDates, taxRules);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Car",
            new string[] {
            "2013-01-15 21:00:00",
            "2013-02-27 09:23:27" },
            new string[] {
            "06:00–06:29 SEK 8",
            "06:30–06:59 SEK 13",
            "07:00–07:59 SEK 18",
            "08:00–08:29 SEK 13",
            "08:30–14:59 SEK 8",
            "15:00–15:29 SEK 13",
            "15:30–16:59 SEK 18",
            "17:00–17:59 SEK 13",
            "18:00–18:29 SEK 8",
            "18:30–05:59 SEK 0"
            },
            13)]
        public void TestCarFalsePositive(string vehicleType, string[] stringDates, string[] taxRules, int expected)
        {
        CongestionTaxCalculatorService congestionTaxCalculator = new CongestionTaxCalculatorService();

            int actual = congestionTaxCalculator.GetTax(vehicleType, stringDates, taxRules);

            Assert.NotEqual(expected, actual);
        }

        [Theory]
        [InlineData("Diplomat",
            new string[] { "2013-02-07 15:27:00" },
            new string[] {
            "06:00–06:29 SEK 8",
            "06:30–06:59 SEK 13",
            "07:00–07:59 SEK 18",
            "08:00–08:29 SEK 13",
            "08:30–14:59 SEK 8",
            "15:00–15:29 SEK 13",
            "15:30–16:59 SEK 18",
            "17:00–17:59 SEK 13",
            "18:00–18:29 SEK 8",
            "18:30–05:59 SEK 0"
            },
            0)]
        public void TestDiplomat(string vehicleType, string[] stringDates, string[] taxRules, int expected)
        {
        CongestionTaxCalculatorService congestionTaxCalculator = new CongestionTaxCalculatorService();

            int actual = congestionTaxCalculator.GetTax(vehicleType, stringDates, taxRules);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Motorcycle",
            new string[] { "2013-02-07 15:27:00" },
            new string[] {
            "06:00–06:29 SEK 8",
            "06:30–06:59 SEK 13",
            "07:00–07:59 SEK 18",
            "08:00–08:29 SEK 13",
            "08:30–14:59 SEK 8",
            "15:00–15:29 SEK 13",
            "15:30–16:59 SEK 18",
            "17:00–17:59 SEK 13",
            "18:00–18:29 SEK 8",
            "18:30–05:59 SEK 0"
            },
            0)]
        public void TestMotorcycle(string vehicleType, string[] stringDates, string[] taxRules, int expected)
        {
        CongestionTaxCalculatorService congestionTaxCalculator = new CongestionTaxCalculatorService();

            int actual = congestionTaxCalculator.GetTax(vehicleType, stringDates, taxRules);

            Assert.Equal(expected, actual);

        }
}
