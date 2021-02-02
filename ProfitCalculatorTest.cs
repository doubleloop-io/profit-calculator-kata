using Xunit;
using static ProfitCalculatorKata.Currencies;

namespace ProfitCalculatorKata
{
    public class ProfitCalculatorTest
    {
        readonly ProfitCalculator gbpCalculator = new ProfitCalculator(GBP);
        readonly ProfitCalculator eurCalculator = new ProfitCalculator(EUR);

        [Fact]
        public void calculates_the_tax_at_20_percent()
        {
            gbpCalculator.Add(500, GBP, true);

            var profit = gbpCalculator.CalculateProfitM();
            var tax = gbpCalculator.CalculateTaxM();

            Assert.Equal(new Money(400), profit);
            Assert.Equal(new Money(100), tax);
        }

        [Fact]
        public void calculates_the_tax_of_multiple_amounts()
        {
            gbpCalculator.Add(120, GBP, true);
            gbpCalculator.Add(200, GBP, true);

            var profit = gbpCalculator.CalculateProfitM();
            var tax = gbpCalculator.CalculateTaxM();

            Assert.Equal(new Money(256), profit);
            Assert.Equal(new Money(64), tax);
        }

        [Fact]
        public void different_currencies_are_not_taxed()
        {
            gbpCalculator.Add(120, GBP, true);
            gbpCalculator.Add(200, USD, true);

            var profit = gbpCalculator.CalculateProfitM();
            var tax = gbpCalculator.CalculateTaxM();

            Assert.Equal(new Money(221), profit);
            Assert.Equal(new Money(24), tax);
        }

        [Fact]
        public void handle_outgoings()
        {
            gbpCalculator.Add(500, GBP, true);
            gbpCalculator.Add(80, USD, true);
            gbpCalculator.Add(360, EUR, false);

            var profit = gbpCalculator.CalculateProfitM();
            var tax = gbpCalculator.CalculateTaxM();

            Assert.Equal(new Money(150), profit);
            Assert.Equal(new Money(100), tax);
        }

        [Fact]
        public void a_negative_balance_results_in_no_tax()
        {
            gbpCalculator.Add(500, GBP, true);
            gbpCalculator.Add(200, GBP, false);
            gbpCalculator.Add(400, GBP, false);
            gbpCalculator.Add(20, GBP, false);

            var profit = gbpCalculator.CalculateProfitM();
            var tax = gbpCalculator.CalculateTaxM();

            Assert.Equal(new Money(-120), profit);
            Assert.Equal(new Money(0), tax);
        }

        [Fact]
        public void everything_is_reported_in_the_local_currency()
        {
            eurCalculator.Add(400, GBP, true);
            eurCalculator.Add(200, USD, false);
            eurCalculator.Add(200, EUR, true);

            var profit = eurCalculator.CalculateProfitM();
            var tax = eurCalculator.CalculateTaxM();

            Assert.Equal(new Money(491), profit);
            Assert.Equal(new Money(40), tax);
        }
    }
}