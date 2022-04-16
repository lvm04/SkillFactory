namespace SF.Module10
{
    public interface ISum
    {
        double Sum(double a, double b);
    }

    class Calculator : ISum
    {
        public string aStr;
        public string bStr;
        public ILogger Logger { get; }
        public Calculator(ILogger logger)
        {
            Logger = logger;
        }
        public double Sum(double a, double b)
        {
            return a + b;
        }
    }
}