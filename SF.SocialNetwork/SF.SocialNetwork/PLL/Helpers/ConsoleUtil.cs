using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SocialNetwork.PLL.Helpers
{
    public static class ConsoleUtil
    {
        /// <summary>
        /// Получает ввод с клавиатуры, а при нажатии на Tab ищет введенное слово в списке и дописывает его если находит
        /// </summary>
        /// <param name="data">Список слов для поиска</param>
        /// <returns></returns>
        public static string ReadLineAutoComplete(string[] data)
        {
            var builder = new StringBuilder();
            var input = Console.ReadKey(intercept: true);
            string currentInput = "";

            while (input.Key != ConsoleKey.Enter)
            {
                currentInput = builder.ToString();
                if (input.Key == ConsoleKey.Tab)
                {
                    var match = data.FirstOrDefault(item => item != currentInput && item.StartsWith(currentInput, true, CultureInfo.InvariantCulture));
                    if (string.IsNullOrEmpty(match))
                    {
                        input = Console.ReadKey(intercept: true);
                        continue;
                    }

                    ClearCurrentLine();
                    builder.Clear();

                    Console.Write(match);
                    builder.Append(match);
                }
                else
                {
                    if (input.Key == ConsoleKey.Backspace && currentInput.Length > 0)
                    {
                        builder.Remove(builder.Length - 1, 1);
                        ClearCurrentLine();

                        currentInput = currentInput.Remove(currentInput.Length - 1);
                        Console.Write(currentInput);
                    }
                    else
                    {
                        var key = input.KeyChar;
                        builder.Append(key);
                        Console.Write(key);
                    }
                }

                input = Console.ReadKey(intercept: true);
            }

            Console.Write(input.KeyChar);
            Console.WriteLine();
            return builder.ToString();

        }
        
        private static void ClearCurrentLine()
        {
            var currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
        }
    }
}
