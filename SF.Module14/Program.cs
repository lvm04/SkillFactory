using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var phoneBook = new List<Contact>();

            phoneBook.Add(new Contact("Игорь", "Николаев", 79990000001, "igor@example.com"));
            phoneBook.Add(new Contact("Игорь", "Ермалаев", 79990000002, "igor2@example.com"));
            phoneBook.Add(new Contact("Сергей", "Магнатов", 79990000010, "sergey@example.com"));
            phoneBook.Add(new Contact("Сергей", "Довлатов", 79980000011, "sergey2@example.com"));

            phoneBook.Add(new Contact("Анатолий", "Карпов", 79990000011, "anatoly@example.com"));
            phoneBook.Add(new Contact("Анатолий", "Окунев", 79990000012, "anatoly2@example.com"));
            phoneBook.Add(new Contact("Валерий", "Терентьев", 79990000012, "valera@example.com"));
            phoneBook.Add(new Contact("Валерий", "Леонтьев", 79990000083, "valera2@example.com"));

            phoneBook.Add(new Contact("Сергей", "Брин", 799900000013, "serg@example.com"));
            phoneBook.Add(new Contact("Сергей", "Грин", 799900000014, "serg2@example.com"));
            phoneBook.Add(new Contact("Иннокентий", "Смоктуновский", 799900000013, "innokentii@example.com"));
            phoneBook.Add(new Contact("Иннокентий", "Дубровский", 799900000017, "innokentiy5@example.com"));

            while (true)
            {
                Console.Write("Введите номер страницы: ");
                string input = Console.ReadLine();

                var parsed = Int32.TryParse(input, out int pageNumber);

                if (!parsed || pageNumber < 1 || pageNumber > 3)
                {
                    Console.WriteLine();
                    Console.WriteLine("Страницы не существует");
                }
                else
                {
                    var pageContent = phoneBook.OrderBy(c => c.Name).ThenBy(c => c.LastName)
                                        .Skip((pageNumber - 1) * 4).Take(4);

                    Console.WriteLine();

                    // выводим результат
                    foreach (var entry in pageContent)
                    {
                        Console.WriteLine(entry.Name + " " + entry.LastName + ": " + entry.PhoneNumber);
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
