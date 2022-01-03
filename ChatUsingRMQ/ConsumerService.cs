using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatUsingRMQ
{
    public class ConsumerService
    {
        public bool _isRunning = true;
        public void Receive(Chat.GetMessage getMessage)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                    // keep queue name empty to generate random queue name
                    channel.QueueBind("", "chat-fanout-exchange", "");
                    channel.BasicQos(0, 10, false);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) =>
                    {
                        var body = e.Body.ToArray();
                        MessageReceived message = JsonConvert.DeserializeObject<MessageReceived>(Encoding.UTF8.GetString(body));
                        getMessage(message);
                        Console.WriteLine("Message received " + message.Message + " by " + message.Nickname);
                    };

                    while (_isRunning & channel.IsOpen)
                    {
                        Thread.Sleep(10);
                        channel.BasicConsume("", true, consumer);
                    }
                }
            }
            catch(BrokerUnreachableException ex)
            {
                MessageBox.Show("Server RabbitMQ unreachable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Chat chat = (Chat)Application.OpenForms["Chat"];
                if(chat != null)
                chat.BeginInvoke((Action)(() =>
                {
                    chat.Close();
                }));
            }
        }
    }
}
