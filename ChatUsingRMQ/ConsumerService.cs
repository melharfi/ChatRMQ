using ChatUsingRMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatUsingRMQ
{
    public class ConsumerService
    {
        bool _isRunning = true;
        public void Receive(Chat.GetMessage getMessage)
        {
            // example
            //https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
            // In .net core better use DI rather than instanciate each time a new instance of factory with its connection
            var factory = new ConnectionFactory(){ HostName = "localhost", UserName = "guest", Password = "guest" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("",
                                    durable: true,
                                    exclusive: true,
                                    autoDelete: true,                       // make queues volatil
                                    arguments: null);

                // keep queue name empty to generate random queue name
                channel.QueueBind("", "chat-fanout-exchange", "");
                channel.BasicQos(0, 10, false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = Encoding.UTF8.GetString(e.Body.ToArray());
                    var headerType = e.BasicProperties.Headers["type"];

                    // Check if header exist and with type Byte[]
                    if (headerType == null || headerType.GetType() != typeof(Byte[]))
                        return;

                    var messageType = Encoding.UTF8.GetString(headerType as Byte[]);
                    switch (messageType)
                    {
                        case nameof(MessageReceived):
                            {
                                MessageReceived message = JsonConvert.DeserializeObject<MessageReceived>(body);
                                getMessage(message);
                            }
                            break;
                        case nameof(UserLoggedIn):
                            {
                                UserLoggedIn message = JsonConvert.DeserializeObject<UserLoggedIn>(body);
                                getMessage(message);
                            }
                            break;
                        case nameof(UserLoggedOut):
                            {
                                UserLoggedOut message = JsonConvert.DeserializeObject<UserLoggedOut>(body);
                                getMessage(message);
                            }
                            break;
                    }
                };

                while (_isRunning & channel.IsOpen)
                {
                    Thread.Sleep(10);
                    channel.BasicConsume("", true, consumer);
                }
            }
        }

        public void Disconnect()
        {
            _isRunning = false;
        }
    }
}
