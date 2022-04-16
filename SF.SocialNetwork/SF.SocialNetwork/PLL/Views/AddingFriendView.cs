using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class AddingFriendView
    {
        UserService userService;
        public AddingFriendView(UserService userService)
        {
            this.userService = userService;
        }
        public void Show(User user)
        {
            try
            {
                var userAddingFriendData = new UserAddingFriendData();
                var emailList = userService.GetAllUser().Select(u => u.Email).ToArray();

                Console.WriteLine("Введите почтовый адрес пользователя которого хотите добавить в друзья: ");

                //userAddingFriendData.FriendEmail = Console.ReadLine();
                userAddingFriendData.FriendEmail = ConsoleUtil.ReadLineAutoComplete(emailList);
                userAddingFriendData.UserId = user.Id;

                this.userService.AddFriend(userAddingFriendData);

                SuccessMessage.Show("Вы успешно добавили пользователя в друзья!");
            }

            catch(UserNotFoundException)
            {
                AlertMessage.Show("Пользователя с указанным почтовым адресом не существует!");
            }

            catch (HimselfFriendException)
            {
                AlertMessage.Show("Нельзя добавить в друзья самого себя!");
            }

            catch (FriendAlreadyExistsException)
            {
                AlertMessage.Show("Данный пользователь уже находится в друзьях!");
            }

            catch (Exception)
            {
                AlertMessage.Show("Произоша ошибка при добавлении пользотваеля в друзья!");
            }
 
        }
    }
}
