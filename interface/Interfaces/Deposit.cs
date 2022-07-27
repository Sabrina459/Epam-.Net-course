using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Interfaces
{
    public abstract class Deposit: IComparable<Deposit>
    {
        public decimal Amount
        {
            get;
        }
        public int Period
        {
            get;
        }

        public Deposit(decimal Amount, int Period)
        {
            this.Amount = Amount;
            this.Period = Period;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public abstract decimal Income();
        public abstract int CompareTo([DisallowNull] Deposit other);
    }




}
