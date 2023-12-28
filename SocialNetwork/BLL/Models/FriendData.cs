using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    /// <summary>
    /// Класс с моделью данных для добавления в друзья
    /// 1. Надо знать ID текущего юзера, вошедшего в систему
    /// 2. Надо знать Email целевого полтзователя для добавления в друзья. По нему определим его ID
    /// </summary>
    public class FriendData
    {
        public int UserId { get; set; }
        public string FriendEmail { get; set; }
    }
}
