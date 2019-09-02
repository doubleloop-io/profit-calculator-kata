using System;
using System.Collections.Generic;

namespace ProfitCalculatorKata
{
    public class ProfitCalculator
    {
        static readonly Dictionary<string, double> ExchangeRates = new Dictionary<string, double>
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

            if (!ExchangeRates.ContainsKey(localCurrency))
            {
                throw new ArgumentException($"Invalid currency '{localCurrency}'");
            }
        }

        public void Add(int amount, string currency, bool incoming)
        {
            var realAmount = amount;

            if (!ExchangeRates.ContainsKey(currency))
            {
                throw new ArgumentException($"Invalid currency '{currency}''");
            }
            var exchangeRate = ExchangeRates[currency] / ExchangeRates[localCurrency];
            
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

        public int CalculateProfit()
        {
            return localAmount - CalculateTax() + foreignAmount;
        }

        public int CalculateTax()
        {
            if (localAmount < 0)
            {
                return 0;
            }

            return (int) (localAmount * 0.2);
        }
    }
}