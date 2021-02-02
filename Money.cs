using System;

namespace ProfitCalculatorKata
{
    public class Money
    {
        public Int32 Amount { get; }

        public Money(in int amount)
        {
            Amount = amount;
        }

        public Money Sum(Money other) =>
            new Money(other.Amount + Amount);

        protected bool Equals(Money other)
        {
            return Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Money) obj);
        }

        public override int GetHashCode()
        {
            return Amount;
        }

        public static bool operator ==(Money left, Money right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !Equals(left, right);
        }
    }
}