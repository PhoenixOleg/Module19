using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services
{
    public class MessageService
    {
        UserRepository userRepository;

        public void SendMessage(MessageSendingData messageSendingData)
        {
            if (string.IsNullOrEmpty(messageSendingData.Message))
                throw new ArgumentNullException();

            if (messageSendingData.Message.Length > 5000)
                throw new ArgumentOutOfRangeException();

            if (userRepository.FindByEmail(messageSendingData.RecipientEmail) != null)
                throw new ArgumentException();

            UserEntity = userRepository.
            var messageEntity = new MessageEntity()
            {
                sender_id = messageSendingData.IdSender,
                recipient_id = messageSendingData.
            };
        }
    }
}
