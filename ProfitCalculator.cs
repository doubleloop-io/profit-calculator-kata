using System;
using System.Collections.Generic;

namespace ProfitCalculatorKata
{
    public class ProfitCalculator
    {
        static Dictionary<string, double> EXCHANGE_RATES = new Dictionary<string, double>
        {
            {"GBP", 1.0},
            {"USD", 1.6},
            {"EUR", 1.2}
        };

        string localCurrency;
        int localAmount = 0;
        int foreignAmount = 0;

        public ProfitCalculator(string localCurrency)
        {
            this.localCurrency = localCurrency;

            if (!EXCHANGE_RATES.ContainsKey(localCurrency))
            {
                throw new ArgumentException($"Invalid currency '{localCurrency}'");
            }
        }

        public void add(int amount, string currency, bool incoming)
        {
            var realAmount = amount;

            if (!EXCHANGE_RATES.ContainsKey(currency))
            {
                throw new ArgumentException($"Invalid currency '{currency}''");
            }
            var exchangeRate = EXCHANGE_RATES[currency] / EXCHANGE_RATES[localCurrency];
            
            realAmount = (int) (realAmount / exchangeRate);
            
            if (!incoming)
            {
                realAmount = -realAmount;
            }

            if (localCurrency == currency)
            {
                localAmount += realAmount;
            }
            else
            {
                foreignAmount += realAmount;
            }
        }

        public int calculateProfit()
        {
            return localAmount - calculateTax() + foreignAmount;
        }

        public int calculateTax()
        {
            if (localAmount < 0)
            {
                return 0;
            }

            return (int) (localAmount * 0.2);
        }
    }
}