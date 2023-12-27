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
    public class FriendService : UserService
    {
        IFriendRepository friendRepository;

        public FriendService() 
        { 
            friendRepository = new FriendRepository();
        }

        public string AddFriend(AddFriendData addFriendData)
        {
            var findUserEntity = new UserService().FindByEmail(addFriendData.FriendEmail);

            if (findUserEntity == null ) 
                throw new UserNotFoundException();

            if (addFriendData.UserId == findUserEntity.Id)
                throw new AddYourselfFriendException();

            var allFriend = new FriendRepository().FindAllByUserId(findUserEntity.Id);

            if (allFriend.Count() > 0)
            { 
                throw new FriendAlreadyExist();
            }

            FriendEntity friendEntity = new FriendEntity()
            {
                 user_id = addFriendData.UserId,
                 friend_id = findUserEntity.Id
            };

            if (friendRepository.Create(friendEntity) == 0)
                throw new Exception();

            return findUserEntity.FirstName + " " + findUserEntity.LastName;
        }

        public string DeleteFriend(string friendEmail)
        {
            var findUserEntity = new UserService().FindByEmail(friendEmail);

            if (findUserEntity == null)
                throw new UserNotFoundException();

            if (friendRepository.FindAllByUserId(findUserEntity.Id).Count() == 0)
                throw new FriendNotFoundException();

            if (friendRepository.Delete(findUserEntity.Id) == 0)
                throw new Exception();

            return findUserEntity.FirstName + " " + findUserEntity.LastName;
        }
    }
}
