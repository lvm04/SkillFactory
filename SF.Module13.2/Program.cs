using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SF.Module13._6._2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Читаем файл
            const string FILE_PATH = "../../data/Text1.txt";
            string text = File.ReadAllText(FILE_PATH);

            // Создаем очищенный массив слов
            var noPunctuationText = new string(text.ToLower().Where(c => !char.IsPunctuation(c)).ToArray());
            char[] delimiters = new char[] { ' ', '\r', '\n' };     // символы-разделители слов
            string[] words = noPunctuationText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            // Считаем слова
            Dictionary<string, int> statistics = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (statistics.ContainsKey(word))
                    statistics[word]++;
                else
                    statistics.Add(word, 1);
            }

            // Выводим результат
            var top10 = (from word in statistics
                        orderby word.Value descending
                        select word).Take(10);

            Console.WriteLine("\nРейтинг слов:");
            int n = 1;
            foreach (var item in top10)
            {
                Console.WriteLine($"{n}. {item.Key} - {item.Value}");
                n++;
            }
        }
    }
}
