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

            UserEntity recipientUserEntity = userRepository.FindByEmail(messageSendingData.RecipientEmail);

            if (recipientUserEntity == null)
                throw new ArgumentException();


            var messageEntity = new MessageEntity()
            {
                sender_id = messageSendingData.IdSender,
                recipient_id = recipientUserEntity.id,
                content = messageSendingData.Message
            };


        }
    }
}
