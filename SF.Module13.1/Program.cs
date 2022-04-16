using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace SF.Module13_6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Разбиваем весь текст на слова и сохраняем их в массив words
            const string FILE_PATH = "../../data/Text1.txt";
            string text = File.ReadAllText(FILE_PATH);
            char[] delimiters = new char[] { ' ', '\r', '\n' };     // символы-разделители слов
            string[] words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine($"Слов в файле: {words.Length}");
            Console.WriteLine($"Результат для List, [мс]      : {TestInsert(ToList, words, 5)}");
            Console.WriteLine($"Результат для LinkedList, [мс]: {TestInsert(ToLinkedList, words, 10)}");
        }

        static void ToList(string[] wordsArray)
        {
            List<string> list = new List<string> { "один", "два", "три", "четыре", "пять" };
            foreach (string word in wordsArray)
            {
                list.Insert(2, word);
            }
        }

        static void ToLinkedList(string[] wordsArray)
        {
            string[] initList = { "один", "два", "три", "четыре", "пять" };
            LinkedList<string> list = new LinkedList<string>(initList);

            LinkedListNode<string> current = list.Find("три");

            foreach (string word in wordsArray)
            {
                list.AddBefore(current, word);
            }
        }

        /// <summary>
        /// Тестирование функции вставки слов в список
        /// </summary>
        /// <param name="funcInsert">Тестируемая функция</param>
        /// <param name="wordsArray">Массив слов</param>
        /// <param name="count">Количество тестов</param>
        /// <returns>Список результатов в мсек. через запятую</returns>
        static string TestInsert(Action<string[]> funcInsert, string[] wordsArray, int count)
        {
            List<long> result = new List<long>();
            Stopwatch clock = new Stopwatch();

            for (int i = 0; i < count; i++)
            {
                clock.Start();
                funcInsert(wordsArray);
                clock.Stop();
                result.Add(clock.ElapsedMilliseconds);
                clock.Reset();
            }
            
            return string.Join(", ", result.OrderBy(t => t));
        }
    }
}
