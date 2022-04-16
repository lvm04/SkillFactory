using System;
using System.Collections.Generic;
using System.IO;
using SF.WorkWithFiles;

namespace SF.WorkWithFiles.Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"d:\temp\02\";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            List<FileSystemInfo> delList = new List<FileSystemInfo>();

            if (dirInfo.Exists)
            {
                FileExtensions.SearchOldFiles(dirInfo, 0, delList, 30);
            }
            else
            {
                Console.WriteLine($"Каталог {dirInfo} не найден");
                return;
            }

            Console.Write("Удалить отмеченные файлы и каталоги? (y/n) ");
            string response = Console.ReadLine();
            long sizeBefore = dirInfo.DirSize();

            if (response.ToLower() == "y")
            {
                FileExtensions.DeleteFiles(delList);
            }
            long sizeAfter = dirInfo.DirSize();

            Console.WriteLine();
            Console.WriteLine("Исходный размер папки: {0}", sizeBefore);
            Console.WriteLine("Освобождено          : {0}", sizeBefore - sizeAfter);
            Console.WriteLine("Текущий размер папки : {0}", sizeAfter);
            
        }
    }
}
