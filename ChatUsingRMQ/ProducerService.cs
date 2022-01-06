using ChatUsingRMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatUsingRMQ
{
    public class ProducerService
    {
        public ProducerService()
        {
            
        }
        public void Publish(IEvent myEvent)
        {
            try
            { 
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    var args = new Dictionary<string, object>();
                    args.Add("x-message-ttl", 60000);               // time of persisting message on the queue
                    args.Add("x-expires", 60000);                   // 
                    channel.QueueDeclare("",
                                         durable: false,            // is static even after reboot of server
                                         exclusive: true,           // is exclusive to one subscriber
                                         autoDelete: true,          // make channel autodeleted when no subscriber is found
                                         arguments: args);

                    // Enabling Publisher Confirms on a Channel which is not enabled by default
                    // this feature not used
                    //https://www.rabbitmq.com/tutorials/tutorial-seven-dotnet.html
                    //channel.ConfirmSelect();

                    IBasicProperties props = channel.CreateBasicProperties();
                    props.ContentType = "text/plain";
                    props.DeliveryMode = 2;
                    props.Expiration = "300000";            // channel expiration but not work

                    // add specific header to identify the type of packet "application logic"
                    props.Headers = new Dictionary<string, object>();
                    props.Headers.Add("type", myEvent.GetType().Name);

                    // crafted data
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(myEvent));

                    // send packet
                    channel.BasicPublish(exchange: "chat-fanout-exchange",
                                         routingKey: "",
                                         basicProperties: props,
                                         body: body);

                    // Enabling Publisher Confirms on a Channel which is not enabled by default
                    // this feature not used
                    //https://www.rabbitmq.com/tutorials/tutorial-seven-dotnet.html
                    //should come after channel.BasicPublish
                    //channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));
                }
            }
            catch (BrokerUnreachableException ex)
            {
                throw new BrokerUnreachableException(ex.InnerException);
            }

        }
    }
}
