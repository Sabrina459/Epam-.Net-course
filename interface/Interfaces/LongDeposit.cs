using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Interfaces
{
    public class LongDeposit : Deposit, IProlongable
    {
        public new decimal Amount
        {
            get;
        }
        public new int Period
        {
            get;
        }
        public LongDeposit(decimal Amount, int Period) : base(Amount, Period)
        {
            this.Amount = Amount;
            this.Period = Period;

        }
        public override decimal Income()
        {
            decimal finalSum = Amount;
            for (int i = 7; i < Period + 1; i++)
            {
                finalSum += finalSum * (decimal)0.15;
            }
            return decimal.Round(finalSum, 3) - Amount;
        }

        public bool CanToProlong()
        {
            if (Period <=36)
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
