namespace SF.Module17
{
    /// <summary>
    /// Депозитный счет
    /// </summary>
    public class DepositAccount : AbstractAccount, IInterest
    {
        public DepositAccount(string accId, double initSum) : base(accId, initSum)
        {

        }

        public void CalculateInterest()
        {
            if (Balance < 1000)
                Interest = Balance * 0.01;
            else if (Balance >= 1000 && Balance < 10000)
                Interest = Balance * 0.02;
            else
                Interest = Balance * 0.03;
        }
    }
}