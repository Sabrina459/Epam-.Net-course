namespace Aggregation
{
    public abstract class Deposit
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
        public abstract decimal Income();
    }
}