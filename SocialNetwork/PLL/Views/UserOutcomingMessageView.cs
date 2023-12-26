using SocialNetwork.BLL.Models;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class UserOutcomingMessageView
    {
        public void Show(IEnumerable<Message> outcomingMessages)
        {
            if (outcomingMessages.Count() == 0)
            {
                SuccessMessage.Show("Исходящих сообщений нет");
            }

            SuccessMessage.Show("Ваши исходящие сообщения:");
            foreach (var message in outcomingMessages)
            {
                Console.WriteLine("Отпрвлено сообщение:\n Кому " + message.RecipientEmail + "\n Текст сообщения:\n\t" + message.Content);
            }
        }
    }
}
