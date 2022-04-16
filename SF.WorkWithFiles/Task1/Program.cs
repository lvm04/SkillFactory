using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace SF.WorkWithFiles.Task1
{
    class Program
    {
        public const int THRESHOLD = 30;

        static void Main(string[] args)
        {

            string path = @"d:\temp\02\";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            List<FileSystemInfo> delList = new List<FileSystemInfo>();

            if (dirInfo.Exists)
            {
                SearchOldFiles(dirInfo, 0, delList);
            }
            else
            {
                Console.WriteLine($"Каталог {dirInfo} не найден");
                return;
            }

            Console.Write("Удалить отмеченные файлы и каталоги? (y/n) ");
            string response = Console.ReadLine();

            if (response.ToLower() == "y")
            {
                // Сначала удаляем файлы
                foreach (FileInfo item in delList.Where(f => f.GetType() == typeof(FileInfo)))
                {
                    try
                    {
                        item.Delete();
                        Console.WriteLine($"Удален файл {item.FullName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                // Потом каталоги
                foreach (DirectoryInfo item in delList.Where(f => f.GetType() == typeof(DirectoryInfo)))
                {
                    try
                    {
                        item.Delete(true);
                        Console.WriteLine($"Удален каталог {item.FullName}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public static void SearchOldFiles(DirectoryInfo dir, int level, List<FileSystemInfo> delList)
        {
            try
            {
                FileInfo[] fiList = dir.GetFiles();
                foreach (FileInfo fi in fiList)
                {
                    WriteMarkedString(fi, delList, level);
                }

                DirectoryInfo[] diList = dir.GetDirectories();
                foreach (DirectoryInfo di in diList)
                {
                    WriteMarkedString(di, delList, level);
                    SearchOldFiles(di, level + 1, delList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void WriteMarkedString(FileSystemInfo fsInfo, List<FileSystemInfo> delList, int level)
        {
            DateTime now = DateTime.Now;
            TimeSpan interval = now - fsInfo.LastWriteTime;

            string str = $"{new string(' ', level)}" +
                (fsInfo.GetType() == typeof(DirectoryInfo) ? $"[{ fsInfo.Name}] " : $"{fsInfo.Name} ") +
                    $"\t{fsInfo.LastWriteTime}\t{(int)interval.TotalMinutes} минут";

            if (interval.TotalMinutes > THRESHOLD)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(str);
                Console.ForegroundColor = ConsoleColor.Gray;
                delList.Add(fsInfo);
            }
            else
                Console.WriteLine(str);
        }
    }
}

