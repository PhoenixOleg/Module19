using SocialNetwork.BLL.Models;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class UserIncomingMessageView
    {
        public void Show(IEnumerable <Message> incomingMessages)
        {
            if (incomingMessages.Count() == 0) 
            {
                SuccessMessage.Show("Входящих сообщений нет");
            }

            SuccessMessage.Show("Ваши входящие сообщения:");
            foreach (var message in incomingMessages) 
            {
                Console.WriteLine("Получено сообщение:\n От " + message.sender_id + "\n Текст сообщения:\n" + message.content);
            }
        }
    }
}
