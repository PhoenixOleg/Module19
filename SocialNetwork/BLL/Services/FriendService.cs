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

        public string AddFriend(FriendData friendData)
        {
            var findUserEntity = new UserService().FindByEmail(friendData.FriendEmail);

            if (findUserEntity == null ) 
                throw new UserNotFoundException();

            if (friendData.UserId == findUserEntity.Id)
                throw new AddYourselfFriendException();

            //Здесь была ошибка
            //var allFriend = new FriendRepository().FindAllByUserId(findUserEntity.Id);

            #region Проверка, существует ли пользователь с ID findUserEntity.Id в списке друзей
            int recordId = GetExistFriendID(friendData.UserId, findUserEntity.Id);
            
            if (recordId != -1)
            { 
                throw new FriendAlreadyExist();
            }
            #endregion

            FriendEntity friendEntity = new FriendEntity()
            {
                 user_id = friendData.UserId,
                 friend_id = findUserEntity.Id
            };

            if (friendRepository.Create(friendEntity) == 0)
                throw new Exception();

            return findUserEntity.FirstName + " " + findUserEntity.LastName;
        }

        public string DeleteFriend(FriendData friendData)
        {
            //Ищем удаляемого из друзей пользователя по его Email
            var findUserEntity = new UserService().FindByEmail(friendData.FriendEmail);

            //Не нашли
            if (findUserEntity == null)
                throw new UserNotFoundException();

            #region Проверка, существует ли пользователь с ID findUserEntity.Id в списке друзей
            int recordId = GetExistFriendID(friendData.UserId, findUserEntity.Id);

            if (recordId == -1)
                throw new FriendNotFoundException();
            #endregion

            if (friendRepository.Delete(recordId) == 0)
                throw new Exception();

            return findUserEntity.FirstName + " " + findUserEntity.LastName;
        }

        private int GetExistFriendID(int userId, int frinedIdForSearch)
        {
            //Ищем друзей по ID пользователя! Т. е. его друзей.
            //Получаем всех друзей пользователя, а затем селектим их по искомому ID уже друга
            int recordId = GetFriends(null, userId).ToList()
                .Where(x => x.friend_id == frinedIdForSearch)
                .Select(x => x.id)
                .DefaultIfEmpty(-1) //Значение по умолчанию, если в наборе нет данных.
                .First();
            return recordId;
        }

        public List<User> GetFriendsList(User user)
        {
            List<User> friendsList = new List<User>();
            UserService userService = new();

            var friends = GetFriends(user);

            if (friends.Count() == 0)
                throw new FriendNotFoundException();

            try
            {
                foreach (var friend in friends)
                {
                    friendsList.Add(userService.FindById(friend.friend_id)); //Вместе с паролем пользователя =)
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return friendsList;
        }
        
        public int FriendsCount(int userId)
        {
            return GetFriends(null, userId).Count();
        }

        private IEnumerable<FriendEntity> GetFriends(User user, int userId = -1)
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

            //AddFriend_NotMustThrowAddYourselfFriendException показал, что этот код здесь ошибочен
            //if (friends.Count() == 0)
            //    throw new FriendNotFoundException();

            return friends;
        }
    }
}
