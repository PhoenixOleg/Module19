using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.PLL.Views
{
    public class UserAddFriendView
    {
        UserService userService;

        public UserAddFriendView(UserService userService) 
        { 
            this.userService = userService;
        }
        public void Show(User user)
        {

        }
    }
}
