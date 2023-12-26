using SocialNetwork.BLL.Exceptions;
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
        MessageRepository messageRepository;

        public MessageService()
        {
            userRepository = new UserRepository();
            messageRepository = new MessageRepository();
        }

        public IEnumerable<Message> GetIncomingMessagesByUserId(int recipientId)
        {
            var messages = new List<Message>();

            messageRepository.FindByRecipientId(recipientId).ToList().ForEach(m =>
            {
                var senderUserEntity = userRepository.FindById(m.sender_id);
                var recipientUserEntity = userRepository.FindById(m.recipient_id);

                messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });

            return messages;
        }

        public IEnumerable<Message> GetOutcomingMessagesByUserId(int senderId)
        {
            var messages = new List<Message>();

            messageRepository.FindBySenderId(senderId).ToList().ForEach(m =>
            {
                var senderUserEntity = userRepository.FindById(m.sender_id);
                var recipientUserEntity = userRepository.FindById(m.recipient_id);

                messages.Add(new Message(m.id, m.content, senderUserEntity.email, recipientUserEntity.email));
            });

            return messages;
        }

        public void SendMessage(MessageSendingData messageSendingData)
        {

            if (string.IsNullOrEmpty(messageSendingData.Message))
                throw new ArgumentNullException();

            if (messageSendingData.Message.Length > 5000)
                throw new ArgumentOutOfRangeException();

            UserEntity recipientUserEntity = userRepository.FindByEmail(messageSendingData.RecipientEmail);

            if (recipientUserEntity == null)
                throw new UserNotFoundException();


            var messageEntity = new MessageEntity()
            {
                sender_id = messageSendingData.IdSender,
                recipient_id = recipientUserEntity.id,
                content = messageSendingData.Message
            };

            if (messageRepository.Create(messageEntity) == 0)
                throw new Exception();
        }
    }
}
