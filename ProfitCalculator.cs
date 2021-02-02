using System;
using System.Collections.Generic;

namespace ProfitCalculatorKata
{
    public class ProfitCalculator
    {
        static readonly Dictionary<Currency, double> ExchangeRates = new Dictionary<Currency, double>
        {
            {Currencies.GBP, 1.0},
            {Currencies.USD, 1.6},
            {Currencies.EUR, 1.2}
        };

        int localAmount = 0;
        int foreignAmount = 0;
        readonly Currency localCurrency;

        public ProfitCalculator(Currency currency)
        {
            localCurrency = currency;

            if (!ExchangeRates.ContainsKey(localCurrency))
            {
                throw new ArgumentException($"Invalid currency '{localCurrency}'");
            }
        }

        public void Add(Int32 amount, Currency currency, Boolean incoming)
        {
            var amountM = new Money(amount);
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
                var xxx = amountM.Sum(new Money(localAmount));
                localAmount = localAmount + realAmount;
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

    public class Money
    {
        public Int32 Amount { get; }

        public Money(in int amount)
        {
            Amount = amount;
        }

        public Money Sum(Money other) => 
            new Money(other.Amount + Amount);
    }
}