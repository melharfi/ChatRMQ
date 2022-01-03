//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ChatUsingRMQ
//{
//    public sealed class FactoryRMQ
//    {
//        private static readonly FactoryRMQ instance = new FactoryRMQ();
//        public IModel Channel;
//        // Explicit static constructor to tell C# compiler
//        // not to mark type as beforefieldinit
//        static FactoryRMQ()
//        {
//        }

//        private FactoryRMQ()
//        {
//            var factory = new ConnectionFactory
//            {
//                Uri = new Uri("amqp://chatuser:123456@localhost:5672")
//            };

//            var connection = factory.CreateConnection();
//            var channel = connection.CreateModel();
//            //channel.QueueDeclare("chat-fanout-queue",
//            //    durable: true,
//            //    exclusive: false,
//            //    autoDelete: false,
//            //    arguments: null);

//            Channel = channel;
//        }

//        public static FactoryRMQ Instance
//        {
//            get
//            {
//                return instance;
//            }
//        }
//    }
//}
