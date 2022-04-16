using System;

namespace SF.Module10
{
    class Program
    {
        static ILogger Logger { get; set; }
        static void Main(string[] args)
        {
            Logger = new Logger();
            Calculator calculator = new Calculator(Logger);

            Console.WriteLine(" --- Задание 1 ---");
            try
            {
                Summing(calculator);
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка! Введены некорректные данные");
            }

            Console.WriteLine("\n --- Задание 2 ---");
            try
            {
                Summing(calculator);
            }
            catch (Exception)
            {
                calculator.Logger.Error($"[{DateTime.Now}] Введены некорректные данные");
            }
            finally
            {
                calculator.Logger.Event($"Были введены cледующие данные: a = {calculator.aStr}, b = {calculator.bStr}");
            }
            
        }

        static void Summing(Calculator calc)
        {
            Console.Write("Введите первое слагаемое: ");
            calc.aStr = Console.ReadLine();
            Console.Write("Введите второе слагаемое: ");
            calc.bStr = Console.ReadLine();

            double a = Double.Parse(calc.aStr);
            double b = Double.Parse(calc.bStr);

            Console.WriteLine("Результат: {0:f6}", calc.Sum(a, b));
        }
    }
}
