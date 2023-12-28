using NUnit.Framework;
using SocialNetwork.BLL;
using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;

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
        public void Authenticate_MustBeValid()
        {
            UserAuthenticationData userAuthenticationData = new()
            {
                Email = "first@gmail.com",
                Password = "12345678"
            };

            Assert.That (userService.Authenticate(userAuthenticationData) != null);
        }

        [Test]
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

        [Test]
        public void AddFriend_NotMustThrowAddYourselfFriendException()
        {
            //Этот тест выявил ощибку в коде
            FriendData friendData = new()
            {
                FriendEmail = "third@gmail.com", //Целевой пользователь для дружбы
                UserId = userService.FindByEmail("first@gmail.com").Id //ID пользователя, который добавляет к себе в друзья (вошедшего в систему)
                //Или просто подставить ID нашего (не друга!) существующего пользователя из БД
                //UserId = 1 
            };

            Assert.DoesNotThrow(() => friendService.AddFriend(friendData));
        }
    }
}
