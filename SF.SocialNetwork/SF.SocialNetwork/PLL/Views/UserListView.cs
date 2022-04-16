using SocialNetwork.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class UserListView
    {
        public void Show(IEnumerable<User> users)
        {
            Console.WriteLine("\r\nСписок пользователей: ");


            if (users.Count() == 0)
            {
                Console.WriteLine("Пользователей нет");
                return;
            }

            users.ToList().ForEach(user =>
            {
                Console.WriteLine($"Имя     : {user.FirstName}");
                Console.WriteLine($"Фамилия : {user.LastName}");
                Console.WriteLine($"Email   : {user.Email}");
                Console.WriteLine("---------------------------------------");
            });

            }

        }
}

