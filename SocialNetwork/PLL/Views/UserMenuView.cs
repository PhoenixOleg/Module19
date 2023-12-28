using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class UserMenuView
    {
        UserService userService;
        public UserMenuView(UserService userService)
        {
            this.userService = userService;
        }

        public void Show(User user)
        {
            while (true)
            {
                Console.WriteLine("Входящие сообщения: {0}", user.IncomingMessages.Count());
                Console.WriteLine("Исходящие сообщения: {0}", user.OutgoingMessages.Count());
                Console.WriteLine("У Вас {0} друзей\n", user.FriendsCount);

                Console.WriteLine("Просмотреть информацию о моём профиле (нажмите 1)");
                Console.WriteLine("Редактировать мой профиль (нажмите 2)");

                Console.WriteLine("Просмотреть список друзей (нажмите 3)");
                Console.WriteLine("Добавить в друзья (нажмите 4)");
                Console.WriteLine("Удалить пользователя из друзей (нажмите 5)");
               
                Console.WriteLine("Написать сообщение (нажмите 6)");
                Console.WriteLine("Просмотреть входящие сообщения (нажмите 7)");
                Console.WriteLine("Просмотреть исходящие сообщения (нажмите 8)");
                Console.WriteLine("Выйти из профиля (нажмите 9)");

                string keyValue = Console.ReadLine();

                if (keyValue == "9") break;

                switch (keyValue)
                {
                    case "1":
                        {
                            Program.userInfoView.Show(user);
                            break;
                        }

                    case "2":
                        {
                            Program.userDataUpdateView.Show(user);
                            break;
                        }

                    case "3":
                        {
                            Program.userGetFriendsView.Show(user);
                            break;
                        }

                    case "4":
                        {
                            Program.userAddFriendView.Show(user);
                            break;
                        }

                    case "5":
                        {
                            Program.userDeleteFriendView.Show(user);
                            break;
                        }

                    case "6":
                        {
                            Program.messageSendingView.Show(user);
                            break;
                        }

                    case "7":
                        {
                            Program.userIncomingMessageView.Show(user.IncomingMessages);
                            break;
                        }

                    case "8":
                        {
                            Program.userOutcomingMessageView.Show(user.OutgoingMessages);
                            break;
                        }
                }
            }
        }
    }
}
