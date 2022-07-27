using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Interfaces
{
    public class BaseDeposit : Deposit
    {
        public new decimal Amount
        {
            get;
        }
        public new int Period
        {
            get;
        }
        public BaseDeposit(decimal Amount, int Period) : base(Amount, Period)
        {
            this.Amount = Amount;
            this.Period = Period;
        }
        public override decimal Income()
        {
            decimal finalSum = Amount;
            for (int i = 0; i < Period; i++)
            {

                finalSum += finalSum * (decimal)0.05;
            }
            return decimal.Round(finalSum, 3) - Amount;
        }

        public override int CompareTo([DisallowNull] Deposit other )
        {
            return (Amount + Income()).CompareTo(other.Amount + other.Income());


        }
    }

}
