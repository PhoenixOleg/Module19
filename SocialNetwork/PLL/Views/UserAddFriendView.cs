using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class UserAddFriendView
    {
        UserService userService;

        public UserAddFriendView(UserService userService) 
        { 
            this.userService = userService;
        }
        public void Show(User user)
        {
            var addFriendData = new AddFriendData();

            Console.Write("Введите Email пользователя для добавления в друзья");
            addFriendData.FriendEmail = Console.ReadLine();

            addFriendData.UserId = user.Id;

            try 
            {
                //Вызвать либо новые методы userService, либо сделать friendService с наследованием от userService, чтобы не дублировать код поиска
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }

            catch(Exception ex)
            {
                AlertMessage.Show("Произошла ошибка при при добавлении пользователя в друзья!" + Environment.NewLine + ex.Message);
            }
        }
    }
}
