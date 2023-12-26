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

        public void Add(FriendEntity friendEntity)
        {

        }
    }
}
