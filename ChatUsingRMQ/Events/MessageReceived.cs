using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatUsingRMQ.Events
{
    public class MessageReceived : IEvent
    {
        public MessageReceived(string nickName, string message)
        {
            Nickname = nickName;
            Message = message;
            SendingMessageDateTime = DateTime.UtcNow;
        }
        public string Nickname { get; set; }
        public string Message { get; set; }
        public DateTime SendingMessageDateTime { get; }
    }
}
