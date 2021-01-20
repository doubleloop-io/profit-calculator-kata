using System;

namespace ProfitCalculatorKata
{
    public sealed class Currency
    {
        public String Value { get; }

        public Currency(String value)
        {
            Value = value;
        }
        
        public Currency Copy(string value)=>
            new Currency(value ?? Value);

        public override string ToString()
        {
            return Value;
        }

        bool Equals(Currency other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Currency) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Currency left, Currency right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Currency left, Currency right)
        {
            return !Equals(left, right);
        }
    }
}