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
    public class UserGetFriendsView
    {
        FriendService friendService;

        public UserGetFriendsView()
        {
            friendService = new FriendService();
        }

        public void Show(User user) 
        {      
            try
            {
                int idx = 0;

                List<User> friends = friendService.GetFriendsList(user);

                SuccessMessage.Show("Список ваших друзей:");

                foreach (var friend in friends)
                {
                    Console.WriteLine("{0}. {1} {2} (Email - {3})", ++idx, friend.FirstName, friend.LastName, friend.Email);
                }
            }

            catch (FriendNotFoundException)
            {
                AlertMessage.Show("Вы еще не добавили друзей!");
            }
            
            catch (Exception ex)
            {
                AlertMessage.Show("Произошла ошибка при получении списка друзей!" + Environment.NewLine + ex.Message);
            }
        }
    }
}
