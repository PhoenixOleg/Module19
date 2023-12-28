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
    public  class UserDeleteFriendView
    {
        FriendService friendService;

        public UserDeleteFriendView()
        {
            friendService = new FriendService();
        }

        public void Show(User user)
        {
            var addFriendData = new AddFriendData();

            Console.Write("Введите Email пользователя для удаления из друзей: ");
            addFriendData.FriendEmail = Console.ReadLine();

            addFriendData.UserId = user.Id;

            try
            {
                string friendFullName = friendService.DeleteFriend(addFriendData);

                SuccessMessage.Show("Пользователь " + friendFullName + " успешно удален из друзей");
            }


            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }

            catch (FriendNotFoundException)
            {
                AlertMessage.Show("Пользователь с указанным Email не найден среди Ваших друзей!");
            }

            catch (Exception ex) 
            {
                AlertMessage.Show("Произошла ошибка при добавлении пользователя в друзья!" + Environment.NewLine + ex.Message);
            }


        }
    }
}
