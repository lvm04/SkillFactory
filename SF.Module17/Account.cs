namespace SF.Module17
{
    /// <summary>
    /// Обычный счет
    /// </summary>
    public class Account : AbstractAccount, IInterest
    {
        public Account(string accId, double initSum) : base(accId, initSum)
        {
            
        }

        public void CalculateInterest()
        {
            Interest = Balance * 0.01;
        }
    }
}