using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class AdminMenuView
    {
        UserService userService;
        public AdminMenuView(UserService userService)
        {
            this.userService = userService;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("1. Список профилей  (нажмите 1)");
                Console.WriteLine("0. Выйти            (нажмите 0)");

                string keyValue = Console.ReadLine();

                if (keyValue == "0") break;

                switch (keyValue)
                {
                    case "1":
                        Program.userListView.Show(userService.GetAllUser());
                        break;
                }
            }
        }
    } 
}
