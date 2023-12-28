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
        FriendService friendService;

        public UserAddFriendView() 
        { 
            friendService = new FriendService();
        }
        public void Show(User user)
        {
            var friendData = new FriendData();

            Console.Write("Введите Email пользователя для добавления в друзья: ");
            friendData.FriendEmail = Console.ReadLine();

            friendData.UserId = user.Id;

            try 
            {
                //Вызвать либо новые методы userService, либо сделать friendService с наследованием от userService, чтобы не дублировать код поиска
                //Остановил выбор на friendService
                string friendFullName = friendService.AddFriend(friendData);

                SuccessMessage.Show("Пользователь " + friendFullName + " успешно добавлен в друзья!");
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }

            catch (AddYourselfFriendException)
            {
                AlertMessage.Show("Нельзя добавить в друзья самого себя!");
            }

            catch (FriendAlreadyExist)
            {
                AlertMessage.Show("Вы уже друзья!");
            }

            catch (Exception ex)
            {
                AlertMessage.Show("Произошла ошибка при добавлении пользователя в друзья!" + Environment.NewLine + ex.Message);
            }
        }
    }
}
