using Xunit;
using static ProfitCalculatorKata.Currencies;

namespace ProfitCalculatorKata
{
    public class ProfitCalculatorTest {
        ProfitCalculator gbpCalculator = new ProfitCalculator(GBP);
        ProfitCalculator eurCalculator = new ProfitCalculator(EUR);

        [Fact]
        public void calculates_the_tax_at_20_percent() {
            gbpCalculator.Add(500, GBP, true);

            int profit = gbpCalculator.CalculateProfit();
            int tax = gbpCalculator.CalculateTax();

            Assert.Equal(400, profit);
            Assert.Equal(100, tax);
        }

        [Fact]
        public void calculates_the_tax_of_multiple_amounts() {
            gbpCalculator.Add(120, GBP, true);
            gbpCalculator.Add(200, GBP, true);

            int profit = gbpCalculator.CalculateProfit();
            int tax = gbpCalculator.CalculateTax();

            Assert.Equal(256, profit);
            Assert.Equal(64, tax);
        }

        [Fact]
        public void different_currencies_are_not_taxed() {
            gbpCalculator.Add(120, GBP, true);
            gbpCalculator.Add(200, USD, true);

            int profit = gbpCalculator.CalculateProfit();
            int tax = gbpCalculator.CalculateTax();

            Assert.Equal(221, profit);
            Assert.Equal(24, tax);
        }

        [Fact]
        public void handle_outgoings() {
            gbpCalculator.Add(500, GBP, true);
            gbpCalculator.Add(80, USD, true);
            gbpCalculator.Add(360, EUR, false);

            int profit = gbpCalculator.CalculateProfit();
            int tax = gbpCalculator.CalculateTax();

            Assert.Equal(150, profit);
            Assert.Equal(100, tax);
        }

        [Fact]
        public void a_negative_balance_results_in_no_tax() {
            gbpCalculator.Add(500, GBP, true);
            gbpCalculator.Add(200, GBP, false);
            gbpCalculator.Add(400, GBP, false);
            gbpCalculator.Add(20, GBP, false);

            int profit = gbpCalculator.CalculateProfit();
            int tax = gbpCalculator.CalculateTax();

            Assert.Equal(-120, profit);
            Assert.Equal(0, tax);
        }

        [Fact]
        public void everything_is_reported_in_the_local_currency() {
            eurCalculator.Add(400, GBP, true);
            eurCalculator.Add(200, USD, false);
            eurCalculator.Add(200, EUR, true);

            int profit = eurCalculator.CalculateProfit();
            int tax = eurCalculator.CalculateTax();

            Assert.Equal(491, profit);
            Assert.Equal(40, tax);
        }
    }
}