using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask         // SF.WorkWithFiles.Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"d:\Projects\Skillfactory\SF.WorkWithFiles\Students.dat";
            Student[] students;

            if (!File.Exists(path))
            {
                Console.WriteLine($"Каталог {path} не найден");
                return;
            }

            // Создаем каталог на рабочем столе
            string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Students";
            try
            {
                if (!Directory.Exists(desktopFolder))
                    Directory.CreateDirectory(desktopFolder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось создать каталог {desktopFolder}");
                return;
            }


            // Десериализация
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                students = (Student[])formatter.Deserialize(fs);
            }

            // Создаем файлы с разбивкой по группам
            var groupStudents = from student in students
                                group student by student.Group;
            string fileName, text;
            foreach (var grp in groupStudents.OrderBy(g => g.Key))
            {
                try
                {
                    Console.WriteLine(grp.Key);
                    fileName = $"{desktopFolder}\\Группа {grp.Key}.txt";
                    using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.UTF8))
                    {
                        foreach (var std in grp.OrderBy(s => s.Name))
                        {
                            Console.WriteLine($"\tИмя: {std.Name},\tДата рожд.: {std.DateOfBirth.ToShortDateString()}");
                            text = $"{std.Name},{std.DateOfBirth.ToShortDateString()}";
                            sw.WriteLine(text);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
