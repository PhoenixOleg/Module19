using NUnit.Framework;
using SocialNetwork.BLL;
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
        public void AuthenticateMustBeValid()
        {
            UserAuthenticationData userAuthenticationData = new()
            {
                Email = "first@gmail.com",
                Password = "12345678"
            };

            Assert.That (userService.Authenticate(userAuthenticationData) != null);
        }

    }
}
