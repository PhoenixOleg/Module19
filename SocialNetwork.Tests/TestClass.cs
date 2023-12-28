using Moq;
using NUnit.Framework;
using SocialNetwork.BLL;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.Tests
{
    [TestFixture]
    public class TestClass
    {
        private UserService userService;
        private FriendService friendService;

        public TestClass()
        {
            userService = new UserService();
            friendService = new FriendService();
        }

        [Test]
        // Аутентификация пройдена на существующем пользователе с корректными реквизитами 
        public void Authenticate_MustBeValid()
        {
            UserAuthenticationData userAuthenticationData = new()
            {
                Email = "first@gmail.com",
                Password = "12345678"
            };

            Assert.That(userService.Authenticate(userAuthenticationData) != null);
        }

        [Test]
        //Ошибка входа из-за неправильного пароля при существующем логине
        public void Authenticate_MustThrowWrongPasswordException()
        {
            UserAuthenticationData userAuthenticationData = new()
            {
                Email = "first@gmail.com",
                Password = "WrongPassword"
            };

            Assert.Throws<WrongPasswordException>(() => userService.Authenticate(userAuthenticationData));
        }

        [Test]
        //Ошибка входа с несуществующем логином (пользователем)
        public void Authenticate_MustThrowUserNotFoundException()
        {
            UserAuthenticationData userAuthenticationData = new()
            {
                Email = "NotExistUser@gmail.com",
                Password = "12345678"
            };

            Assert.Throws<UserNotFoundException>(() => userService.Authenticate(userAuthenticationData));
        }

        [Test]
        //Ошибка добавления в друзья несуществующего пользователя
        public void AddFriend_MustThrowUserNotFoundException()
        {
            FriendData friendData = new()
            {
                FriendEmail = "NotExistUser@gmail.com", //Целевой пользователь для дружбы
                UserId = userService.FindByEmail("first@gmail.com").Id //ID пользователя, который добавляет к себе в друзья (вошедшего в систему)
                //Или просто подставить ID нашего (не друга!) существующего пользователя из БД
                //UserId = 1 
            };

            Assert.Throws<UserNotFoundException>(() => friendService.AddFriend(friendData));
        }

        [Test]
        //Ошибка добавления в друзья самого себя
        public void AddFriend_MustThrowAddYourselfFriendException()
        {
            FriendData friendData = new()
            {
                FriendEmail = "first@gmail.com", //Целевой пользователь для дружбы
                UserId = userService.FindByEmail("first@gmail.com").Id //ID пользователя, который добавляет к себе в друзья (вошедшего в систему)
                //Или просто подставить ID нашего (не друга!) существующего пользователя из БД
                //UserId = 1 
            };

            Assert.Throws<AddYourselfFriendException>(() => friendService.AddFriend(friendData));
        }

        //[Test]
        //public void AddFriend_NotMustThrowAddYourselfFriendException()
        //{
        //    //Тесты на модификацию БД реально ее модифицируют! Т. е. надо применять Moq
        //    //Но походу на делоать конструктор friendService, который примет FriendRepository...
        //    //var mock = new Mock<IFriendRepository>();
        //    //mock.Setup(a => a.Create(new FriendEntity())).Returns(1);
        //    //На данный момент после теста надо чистить БД            

        //    //Этот тест выявил ошибку в коде
        //    FriendData friendData = new()
        //    {
        //        FriendEmail = "third@gmail.com", //Целевой пользователь для дружбы
        //        //UserId = userService.FindByEmail("first@gmail.com").Id //ID пользователя, который добавляет к себе в друзья (вошедшего в систему)
        //        //Или просто подставить ID нашего (не друга!) существующего пользователя из БД
        //        UserId = 1
        //    };

        //    Assert.DoesNotThrow(() => friendService.AddFriend(friendData));
        //}

        [Test]
        //Ошибка добавления в друзья добавленного ранее пользователя
        public void AddFriend_MustThrowFriendAlreadyExist()
        {
            FriendData friendData = new()
            {
                FriendEmail = "second@gmail.com", //Целевой пользователь для дружбы
                UserId = userService.FindByEmail("first@gmail.com").Id //ID пользователя, который добавляет к себе в друзья (вошедшего в систему)
                //Или просто подставить ID нашего (не друга!) существующего пользователя из БД
                //UserId = 1 
            };

            Assert.Throws<FriendAlreadyExist>(() => friendService.AddFriend(friendData));
        }

        [Test]
        //Ошибка удаления из друзей несуществующего в друзьях пользователя. В соцсети он существует
        public void DeleteFriend_MustThrowFriendNotFoundException()
        {
            FriendData friendData = new()
            {
                FriendEmail = "third@gmail.com", //Целевой пользователь для дружбы
                UserId = userService.FindByEmail("first@gmail.com").Id //ID пользователя, который добавляет к себе в друзья (вошедшего в систему)
                //Или просто подставить ID нашего (не друга!) существующего пользователя из БД
                //UserId = 1 
            };

            Assert.Throws<FriendNotFoundException>(() => friendService.DeleteFriend(friendData));
        }
    }
}
