using System;

namespace ChatUsingRMQ.Events
{
    public class UserLoggedIn : IEvent
    {
        public UserLoggedIn(string nickname)
        {
            Nickname = nickname;
            ConnectionDateTime = DateTime.UtcNow;
        }
        public string Nickname { get; set; }
        public DateTime ConnectionDateTime { get; set; }
    }
}
