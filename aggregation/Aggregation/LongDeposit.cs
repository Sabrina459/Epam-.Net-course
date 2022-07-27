namespace Aggregation
{
    public class LongDeposit:Deposit
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
            for (int i = 7; i < Period+1; i++)
            {
                finalSum += finalSum * (decimal)0.15;
            }
            return decimal.Round(finalSum, 3) - Amount; 
        }
    }
}