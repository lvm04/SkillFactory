using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SF.WorkWithFiles
{
    /// <summary>
    /// Функции для работы с файлами
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Метод расширения для определения размера каталога
        /// </summary>
        /// <param name="dir"></param>
        /// <returns>Размер в байтах</returns>
        public static long DirSize(this DirectoryInfo dir)
        {
            long size = 0;
            FileInfo[] fiList = dir.GetFiles();
            foreach (FileInfo fi in fiList)
            {
                size += fi.Length;
            }

            DirectoryInfo[] diList = dir.GetDirectories();
            foreach (DirectoryInfo di in diList)
            {
                size += DirSize(di);
            }

            return size;
        }

        /// <summary>
        /// Поиск файлов и каталогов, время модификации которых старше threshold минут
        /// </summary>
        /// <param name="dir">Входной каталог</param>
        /// <param name="level">Уровень вложенности (для выводо в консоль)</param>
        /// <param name="delList">Результирующий список</param>
        /// <param name="threshold">Пороговое значение в минутах</param>
        public static void SearchOldFiles(DirectoryInfo dir, int level, List<FileSystemInfo> delList, int threshold)
        {
            try
            {
                FileInfo[] fiList = dir.GetFiles();
                foreach (FileInfo fi in fiList)
                {
                    WriteMarkedString(fi, delList, level, threshold);
                }

                DirectoryInfo[] diList = dir.GetDirectories();
                foreach (DirectoryInfo di in diList)
                {
                    WriteMarkedString(di, delList, level, threshold);
                    SearchOldFiles(di, level + 1, delList, threshold);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void WriteMarkedString(FileSystemInfo fsInfo, List<FileSystemInfo> delList, int level, int threshold)
        {
            DateTime now = DateTime.Now;
            TimeSpan interval = now - fsInfo.LastWriteTime;

            string str = $"{new string(' ', level)}" +
                (fsInfo.GetType() == typeof(DirectoryInfo) ? $"[{ fsInfo.Name}] " : $"{fsInfo.Name} ") +
                    $"\t{fsInfo.LastWriteTime}\t{(int)interval.TotalMinutes} минут";

            if (interval.TotalMinutes > threshold)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(str);
                Console.ForegroundColor = ConsoleColor.Gray;
                delList.Add(fsInfo);
            }
            else
                Console.WriteLine(str);
        }

        /// <summary>
        /// Удаление файлов и каталогов по списку delList
        /// </summary>
        /// <param name="delList">Список удаления</param>
        /// <param name="verbose">Логирование удаляемых файлов</param>
        public static void DeleteFiles(List<FileSystemInfo> delList, bool verbose = false)
        {
            // Сначала удаляем файлы
            foreach (FileInfo item in delList.Where(f => f.GetType() == typeof(FileInfo)))
            {
                try
                {
                    item.Delete();
                    if (verbose)
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
                    if (verbose)
                        Console.WriteLine($"Удален каталог {item.FullName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
