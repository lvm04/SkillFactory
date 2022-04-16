using System;
using System.Linq;

namespace SF.Module9
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------ Задание 1 ------------
            CovidException covidException = new CovidException("Сертификат вакцинации от COVID-19 отсутствует");
            covidException.Data.Add("Дата создания исключения", DateTime.Now);
            covidException.HelpLink = "https://www.rospotrebnadzor.ru/";

            Exception[] exceptions = new Exception[5]
            {
                new ArgumentException(),
                new ArgumentOutOfRangeException(),
                new NullReferenceException(),
                new IndexOutOfRangeException(),
                covidException
            };

            foreach (var ex in exceptions)
            {
                try
                {
                    throw ex;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}: {1}", e.GetType(), e.Message);
                }
            }

            // ------------ Задание 2 ------------
            string[] persons = new string[5] { "Елена", "Светлана", "Яна", "Анна", "Мария" };
            NumberReader numberReader = new NumberReader();
            numberReader.NumberEnteredEvent += ShowPersons;

            while (true)
            {
                try
                {
                    numberReader.Read(persons);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введено некорректное значение");
                }
            }
        }

        static void ShowPersons(int number, string[] pList)
        {
            switch (number)
            {
                case 1:
                    Console.WriteLine(string.Join(", ", pList.OrderBy(p => p)));
                    break;
                case 2:
                    Console.WriteLine(string.Join(", ", pList.OrderByDescending(p => p)));
                    break;
            }
        }
    }

    public class CovidException : Exception
    {
        public CovidException()
        { }
        public CovidException(string message) : base(message)
        { }
    }

    class NumberReader
    {
        public delegate void NumberEnteredDelegate(int number, string[] list);
        public event NumberEnteredDelegate NumberEnteredEvent;

        public void Read(string[] list)
        {
            Console.WriteLine();
            Console.Write("Введите число 1 или 2: ");
            int number = Convert.ToInt32(Console.ReadLine());

            if (number != 1 && number != 2)
                throw new FormatException();

            NumberEnteredEvent?.Invoke(number, list);
        }
    }
}
