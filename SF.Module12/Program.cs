using System;
using System.Collections.Generic;
using System.Threading;

namespace SF.Module12
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<User> userList = new List<User>()
            {
                new User { Login = "ivan_S", Name = "Иван", IsPremium = false },
                new User { Login = "anna18", Name = "Анна", IsPremium = true },
                new User { Login = "den43",  Name = "Денис", IsPremium = true },
                new User { Login = "sam007", Name = "Серега", IsPremium = false }
            };

            foreach (var user in userList)
            {
                Console.WriteLine($"Привет, {user.Name}!");
                if (!user.IsPremium)
                    ShowAds();
            }
        }

        static void ShowAds()
        {
            Console.WriteLine("Посетите наш новый сайт с бесплатными играми free.games.for.a.fool.com");
            Thread.Sleep(1000);     // Остановка на 1 с

            Console.WriteLine("Купите подписку на МыКомбо и слушайте музыку везде и всегда.");
            Thread.Sleep(2000);     // Остановка на 2 с

            Console.WriteLine("Оформите премиум-подписку на наш сервис, чтобы не видеть рекламу.");
            Thread.Sleep(3000);     // Остановка на 3 с
        }
    }
}
