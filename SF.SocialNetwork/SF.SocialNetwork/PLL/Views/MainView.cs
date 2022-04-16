using SocialNetwork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class MainView
    {
        public bool Show()
        {
            Console.WriteLine("1. Войти в профиль      (нажмите 1)");
            Console.WriteLine("2. Зарегистрироваться   (нажмите 2)");
            Console.WriteLine("3. Режим администратора (нажмите 3)");
            Console.WriteLine("0. Выйти                (нажмите 0)");

            switch (Console.ReadLine())
            {
                case "1":
                    {
                        Program.authenticationView.Show();
                        break;
                    }

                case "2":
                    {
                        Program.registrationView.Show();
                        break;
                    }
                case "3":
                    {
                        Program.adminMenuView.Show();
                        break;
                    }
                case "0":
                    return false;
            }

            return true;
        }
    }
}
