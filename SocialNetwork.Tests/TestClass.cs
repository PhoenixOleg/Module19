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

        public TestClass() 
        { 
            userService = new UserService();
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
        public void AuthenticateMust_MustThrowWrongPasswordException()
        {
            UserAuthenticationData userAuthenticationData = new()
            {
                Email = "first@gmail.com",
                Password = "WrongPassword"
            };

            Assert.Throws<WrongPasswordException>(() => userService.Authenticate(userAuthenticationData));

        }
    }
}
