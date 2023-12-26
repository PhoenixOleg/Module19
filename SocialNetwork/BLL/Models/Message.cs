using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    public class Message
    {
        public int id { get; set; }
        public string content { get; set; }
        public int sender_id { get; set; }
        public int recipient_id { get; set; }

        public Message (int id, string content, int sender_id, int recipient_id)
        {
            this.id = id;
            this.content = content;
            this.sender_id = sender_id;
            this.recipient_id = recipient_id;
        }
    }
}
