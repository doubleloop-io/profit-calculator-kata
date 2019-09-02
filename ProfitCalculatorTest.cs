using Xunit;

namespace ProfitCalculatorKata
{
    public class ProfitCalculatorTest {
        ProfitCalculator gbpCalculator = new ProfitCalculator("GBP");
        ProfitCalculator eurCalculator = new ProfitCalculator("EUR");

        [Fact]
        public void calculates_the_tax_at_20_percent() {
            gbpCalculator.add(500, "GBP", true);

            int profit = gbpCalculator.calculateProfit();
            int tax = gbpCalculator.calculateTax();

            Assert.Equal(400, profit);
            Assert.Equal(100, tax);
        }

        [Fact]
        public void calculates_the_tax_of_multiple_amounts() {
            gbpCalculator.add(120, "GBP", true);
            gbpCalculator.add(200, "GBP", true);

            int profit = gbpCalculator.calculateProfit();
            int tax = gbpCalculator.calculateTax();

            Assert.Equal(256, profit);
            Assert.Equal(64, tax);
        }

        [Fact]
        public void different_currencies_are_not_taxed() {
            gbpCalculator.add(120, "GBP", true);
            gbpCalculator.add(200, "USD", true);

            int profit = gbpCalculator.calculateProfit();
            int tax = gbpCalculator.calculateTax();

            Assert.Equal(221, profit);
            Assert.Equal(24, tax);
        }

        [Fact]
        public void handle_outgoings() {
            gbpCalculator.add(500, "GBP", true);
            gbpCalculator.add(80, "USD", true);
            gbpCalculator.add(360, "EUR", false);

            int profit = gbpCalculator.calculateProfit();
            int tax = gbpCalculator.calculateTax();

            Assert.Equal(150, profit);
            Assert.Equal(100, tax);
        }

        [Fact]
        public void a_negative_balance_results_in_no_tax() {
            gbpCalculator.add(500, "GBP", true);
            gbpCalculator.add(200, "GBP", false);
            gbpCalculator.add(400, "GBP", false);
            gbpCalculator.add(20, "GBP", false);

            int profit = gbpCalculator.calculateProfit();
            int tax = gbpCalculator.calculateTax();

            Assert.Equal(-120, profit);
            Assert.Equal(0, tax);
        }

        [Fact]
        public void everything_is_reported_in_the_local_currency() {
            eurCalculator.add(400, "GBP", true);
            eurCalculator.add(200, "USD", false);
            eurCalculator.add(200, "EUR", true);

            int profit = eurCalculator.calculateProfit();
            int tax = eurCalculator.calculateTax();

            Assert.Equal(491, profit);
            Assert.Equal(40, tax);
        }
    }
}