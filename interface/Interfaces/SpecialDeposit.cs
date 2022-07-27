using System;
using System.Diagnostics.CodeAnalysis;


namespace Interfaces
{
    public class SpecialDeposit : Deposit, IProlongable
    {
        public new decimal Amount
        {
            get;
        }
        public new int Period
        {
            get;
        }
        public SpecialDeposit(decimal Amount, int Period) : base(Amount, Period)
        {
            this.Amount = Amount;
            this.Period = Period;

        }
        public override decimal Income()
        {
            decimal finalSum = Amount;
            for (int i = 1; i < Period + 1; i++)
            {
                finalSum += finalSum * i / 100;
            }
            return decimal.Round(finalSum, 3) - Amount;
        }

        public bool CanToProlong()
        {
            if (Amount >1000)
            {
                return true;
            }
            return false;

        }
        public override int CompareTo([DisallowNull] Deposit other)
        {
            return (Amount + Income()).CompareTo(other.Amount + other.Income());

        }
    }

}
