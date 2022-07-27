namespace Aggregation
{
    public class SpecialDeposit : Deposit
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
                finalSum += finalSum * i/100;
            }
            return decimal.Round(finalSum, 3) - Amount;
        }
    }
}