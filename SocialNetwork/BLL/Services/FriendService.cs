﻿using Dapper;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public string DeleteFriend(AddFriendData addFriendData)
        {
            //int recordID;
            //Ищем удаляемого из друзей пользователя по его Email
            var findUserEntity = new UserService().FindByEmail(addFriendData.FriendEmail);

            //Не нашли
            if (findUserEntity == null) 
                throw new UserNotFoundException();

            //Ищем друзей по ID пользователя! Т. е. его друзей.
            //Получаем всех друзей пользователя, а затем селектим их по искомому ID уже друга
            int recordId = GetFriends(null, addFriendData.UserId).ToList()
                .Where(x => x.friend_id == findUserEntity.Id)
                .Select(x => x.id)
                .DefaultIfEmpty(-1) //Значение по умолчанию, если в наборе нет данных.
                .First();

            if (recordId == -1)
                throw new FriendNotFoundException();

            if (friendRepository.Delete(recordId) == 0)
                throw new Exception();

            return findUserEntity.FirstName + " " + findUserEntity.LastName;
        }

        public IEnumerable<FriendEntity> GetFriends(User user, int userId = -1)
        {
            int _userId;
            if (user == null && userId == -1)
                throw new ArgumentException();

            if (user == null && userId != -1)
                _userId = userId;
            else
                _userId = user.Id;

            var friends = new List<FriendEntity>();
            friends = friendRepository.FindAllByUserId(_userId).ToList();

            if (friends.Count() == 0)
                throw new FriendNotFoundException();

            return friends;
        }
    }
}
