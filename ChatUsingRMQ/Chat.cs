using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatUsingRMQ
{
    public partial class Chat : Form
    {
        readonly ProducerService producerService;
        readonly ConsumerService consumerService;
        public delegate void GetMessage(MessageReceived model);
        public Chat()
        {
            InitializeComponent();
            #region generate random Guestname
            {
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                string guestName = "Guest_" + new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
                TbNickname.Text = guestName;
            }
            #endregion

            consumerService = new ConsumerService();
            new Thread(new ThreadStart(() =>
            {
                consumerService.Receive(new GetMessage(HandleMessages));
            })).Start();

            producerService = new ProducerService(); 
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            MessageReceived userModel = new MessageReceived
            {
                Nickname = TbNickname.Text,
                Message = TbMessage.Text
            };

            producerService.Publish(userModel);
        }

        public void HandleMessages(MessageReceived model)
        {
            RtbMessages.BeginInvoke((Action)(() =>
            {
                if (RtbMessages.Text != "")
                    AppendText(Environment.NewLine + "[" + DateTime.Now.ToString("g") + "]", Color.Gray);
                else
                    AppendText("[" + DateTime.Now.ToString("g") + "]", Color.Gray);
                AppendText("[" + model.Nickname + "]", Color.Blue);
                AppendText(" said : ", Color.Gray);
                AppendText(model.Message, Color.Black);
            }));
        }

        public void AppendText(string text, Color color)
        {
            RtbMessages.SelectionStart = RtbMessages.TextLength;
            RtbMessages.SelectionLength = 0;
            RtbMessages.SelectionColor = color;
            RtbMessages.AppendText(text);
            RtbMessages.SelectionColor = RtbMessages.ForeColor;
        }

        private void BtnClearChat_Click(object sender, EventArgs e)
        {
            RtbMessages.Clear();
        }

        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            consumerService._isRunning = false;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if(BtnConnect.Text == "Connect")
            {
                BtnConnect.Text = "Disconnect";
                PlChat.Visible = true;
                TbNickname.Enabled = false;
            }
            else
            {
                BtnConnect.Text = "Connect";
                PlChat.Visible = false;
                TbNickname.Enabled = true;
            }
        }
    }
}
