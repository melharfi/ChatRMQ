using System;

namespace ChatUsingRMQ.Events
{
    public class UserLoggedOut : IEvent
    {
        public UserLoggedOut(string nickname)
        {
            Nickname = nickname;
            DiconnectionDateTime = DateTime.UtcNow;
        }
        public string Nickname { get; set; }
        public DateTime DiconnectionDateTime { get; set; }
    }
}
