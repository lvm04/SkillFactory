using System;

namespace SF.Module5
{
    class Program
    {
        static void Main(string[] args)
        {
            (string, string, int, string[], string[]) user = EnterUser();
            ShowUser(user);
        }

        // -----------------------------------------------------------------------------

        static (string, string, int, string[], string[]) EnterUser()
        {
            (string FirstName, string LastName, int Age, string[] Pets, string[] Colors) User;
            
            // 1. Имя
            Console.Write("Введите имя: ");
            User.FirstName = Console.ReadLine();
            
            // 2. Фамилия
            Console.Write("Введите фамилию: ");
            User.LastName = Console.ReadLine();
            
            // 3. Возраст
            User.Age = GetNumber(7, 120, "Введите возраст цифрами");

            // 4. Питомцы
            Console.Write("У Вас есть питомцы? (y/n) ");
            string petsResponse = Console.ReadLine();
            int petsAmount;

            if (petsResponse.ToLower() == "y")
            {
                petsAmount = GetNumber(1, 20, "Введите количество питомцев");
                User.Pets = CreateArrayString(petsAmount, "Питомец");
            }
            else
                User.Pets = null;
                
            // 5. Любимые цвета
            int colorsAmount = GetNumber(1, 10, "Введите количество цветов"); ;
            User.Colors = CreateArrayString(colorsAmount, "Цвет");

            return User;
        }

        static void ShowUser((string FirstName, string LastName, int Age, string[] Pets, string[] Colors) user)
        {
            Console.WriteLine("_________________INFO_________________");
            Console.WriteLine("Имя     : {0}", user.FirstName);
            Console.WriteLine("Фамилия : {0}", user.LastName);
            Console.WriteLine("Возраст : {0}", user.Age);

            if (user.Pets != null)
            {
                string pets = "";
                for (var i = 0; i < user.Pets.Length; i++)
                {
                    pets += user.Pets[i] + ", ";
                }
                Console.WriteLine("Питомцы : {0}", pets);
            }
            else
                Console.WriteLine("Питомцы : -");

            string colors = "";
            for (var i = 0; i < user.Colors.Length; i++)
            {
                colors += user.Colors[i] + ", ";
            }
            Console.WriteLine("Цвета   : {0}", colors);

            Console.WriteLine("______________________________________");

        }

        static int GetNumber(int min, int max, string phrase)
        {
            string str;
            int result;
            int i = 0;
            do
            {
                if (i > 0)
                    Console.WriteLine("*** Введено некорректное число");

                Console.Write("{0}: ", phrase);
                str = Console.ReadLine();
                i++;
            } while (!CheckNum(str, out result) || result < min || result > max);

            return result;
        }

        static string[] CreateArrayString(int num, string itemName)
        {
            var result = new string[num];

            for (int i = 0; i < num; i++)
            {
                Console.Write("{0} {1}: ", itemName, i + 1);
                result[i] = Console.ReadLine();
            }

            return result;
        }

        static bool CheckNum(string number, out int corrNumber)
        {
            if (int.TryParse(number, out int intNum))
            {
                if (intNum > 0)         // проверку можно бы убрать. Она есть в вызывающем методе
                {
                    corrNumber = intNum;
                    return true;
                }
            }
            
            corrNumber = 0;
            return false;
        }

    }
}
