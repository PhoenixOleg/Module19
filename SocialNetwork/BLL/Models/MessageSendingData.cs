using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Models
{
    public class MessageSendingData
    {
        public int IdSender { get; set; }
        public string Message { get; set; }
        public string RecipientEmail {  get; set; }
    }
}
