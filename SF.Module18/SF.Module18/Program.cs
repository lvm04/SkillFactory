using System;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace SF.Module18
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            const string FFMPEG_DIR = @"d:\Install\ffmpeg-n4.4-latest-win64-lgpl-4.4\bin";  // эти два параметра должны быть в файле конфигурации
            const string OUTPUT_DIR = @"d:\temp\03";
            
            ConsoleCommand input;

            var youtube = new YoutubeLoader(OUTPUT_DIR, FFMPEG_DIR);        // Receiver
            string Url = null;                                              // @"https://www.youtube.com/watch?v=uzZ_QSNs8E8";
            ControlUnit sender = new ControlUnit();                         // Sender


            while (true)
            {
                if (string.IsNullOrWhiteSpace(Url))
                {
                    if (!EnterAddress(out Url))
                        continue;
                }

                input = ShowMenu(Url);

                switch (input)
                {
                    case ConsoleCommand.Show:
                        sender.SetCommand(new DisplayInfoCommand(youtube, Url));
                        await sender.Run();
                        break;
                    case ConsoleCommand.Load:
                        sender.SetCommand(new LoadFileCommand(youtube, Url));
                        await sender.Run();
                        break;
                    case ConsoleCommand.ChangeLink:
                        EnterAddress(out Url);
                        break;
                    case ConsoleCommand.Exit:
                        return;
                    case ConsoleCommand.None:
                        Console.WriteLine("Ошибка! Введена неверная команда");
                        break;
                }
            }
        }

        static ConsoleCommand ShowMenu(string url)
        {
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($" Текущий адрес: {url}");
            Console.WriteLine(new string('-', 60));
            Console.WriteLine("\r\n1. Информация о видео, 2. Загрузить видео, 3. Изменить адрес, 0. Выход\n");
            Console.Write("\n>Введите номер команды: ");

            string command = Console.ReadLine();

            if (ValidCommand(command))
                return (ConsoleCommand)int.Parse(command);
            else
                return ConsoleCommand.None;
        }

        static bool ValidCommand(string str)
        {
            List<string> cmdList = new() { "0", "1", "2", "3" };
            return cmdList.Contains(str) ? true : false;
        }

        static bool EnterAddress(out string address)
        {
            Console.Write("\n>Введите адрес видео на Youtube: ");
            address = Console.ReadLine();
            return string.IsNullOrWhiteSpace(address) ? false : true;
        }

    }
}
