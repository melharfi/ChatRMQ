using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ChatUsingRMQ
{
    public class ProducerService
    {
        public ProducerService()
        {
            
        }
        public void Publish(MessageReceived model)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

                channel.BasicPublish(exchange: "chat-fanout-exchange",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            }

        }
    }
}
