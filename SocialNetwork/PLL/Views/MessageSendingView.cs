using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class MessageSendingView
    {
        MessageService messageService;
        UserService userService;

        public MessageSendingView(MessageService messageService, UserService userService)
        {
            this.messageService = messageService;
            this.userService = userService; 
        }

        public void Show(User user)
        {
            var messageSendingData = new MessageSendingData();

            Console.Write("Введите Email получателя сообщения");
            messageSendingData.RecipientEmail = Console.ReadLine();

            Console.Write("Введите сообщение (не более 5000 символов");
            messageSendingData.Message = Console.ReadLine();

            messageSendingData.IdSender = user.Id;
        }
    }
}
