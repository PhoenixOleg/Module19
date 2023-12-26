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

            Console.Write("Введите Email получателя сообщения: ");
            messageSendingData.RecipientEmail = Console.ReadLine();

            Console.Write("Введите сообщение (не более 5000 символов: ");
            messageSendingData.Message = Console.ReadLine();

            messageSendingData.IdSender = user.Id;

            try
            {
                messageService.SendMessage(messageSendingData);

                SuccessMessage.Show("Сообщение успешно отправлено пользователю с Email " + messageSendingData.RecipientEmail);
            }

            catch (ArgumentNullException)
            {
                AlertMessage.Show("Сообщение не может быть пустым!");
            }

            catch (ArgumentOutOfRangeException)
            {
                AlertMessage.Show("Сообщение не должно превышать 5000 символов!");
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь c Email " + messageSendingData.RecipientEmail + " не найден!");
            }

            catch (Exception ex)
            {
                AlertMessage.Show("Произошла ошибка при отправке сообщения " + Environment.NewLine + ex.Message + ".");
            }
        }
    }
}
