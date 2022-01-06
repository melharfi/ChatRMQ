using ChatUsingRMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
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
        public delegate void GetMessage(IEvent model);
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
            producerService = new ProducerService(); 
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            MessageReceived messageReceived = new MessageReceived(TbNickname.Text, TbMessage.Text);
            producerService.Publish(messageReceived);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnConnect.Text == "Connect")
                {
                    BtnConnect.Text = "Disconnect";
                    PlChat.Visible = true;
                    TbNickname.Enabled = false;

                    new Thread(new ThreadStart(() =>
                    {
                        try
                        {
                            #region RabbitMQ Consumer service
                            consumerService.Receive(new GetMessage(HandleMessages));
                            #endregion
                        }
                        catch (BrokerUnreachableException ex)
                        {
                            Chat chat = (Chat)Application.OpenForms["Chat"];
                            if (chat != null)
                                chat.BeginInvoke((Action)(() =>
                                {
                                    MessageBox.Show("Server RabbitMQ unreachable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    chat.Close();
                                }));
                        }
                    })).Start();

                    #region RabbitMQ Producer service
                    UserLoggedIn userLoggedIn = new UserLoggedIn(TbNickname.Text);
                    producerService.Publish(userLoggedIn);
                    #endregion
                }
                else
                {
                    consumerService.Disconnect();
                    BtnConnect.Text = "Connect";
                    PlChat.Visible = false;
                    TbNickname.Enabled = true;

                    UserLoggedOut userLoggedOut = new UserLoggedOut(TbNickname.Text);
                    producerService.Publish(userLoggedOut);
                }
            }
            catch (BrokerUnreachableException)
            {
                Chat chat = (Chat)Application.OpenForms["Chat"];
                if (chat != null)
                    chat.BeginInvoke((Action)(() =>
                    {
                        MessageBox.Show("Server RabbitMQ unreachable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        chat.Close();
                    }));
            }
        }

        public void HandleMessages(IEvent myEvent)
        {
            RtbMessages.BeginInvoke((Action)(() =>
            {
                RtbMessages.AppendText((RtbMessages.Text == "") ? "" : "\n");
                switch (myEvent.GetType().Name)
                {
                    case nameof(MessageReceived):
                        MessageReceived messageReceived = (MessageReceived)myEvent;
                        AppendText("[" + messageReceived.SendingMessageDateTime.ToString("g") + "]", Color.Gray);

                        if(messageReceived.Nickname == TbNickname.Text)
                        {
                            AppendText("[" + messageReceived.Nickname + "]", Color.Gray);
                            AppendText("You said : ", Color.Gray);
                            AppendText("\n\t " + messageReceived.Message, Color.Gray);
                        }
                        else
                        {
                            AppendText("[" + messageReceived.Nickname + "]", Color.Blue);
                            AppendText(" Said : ", Color.Blue);
                            AppendText("\n\t " + messageReceived.Message, Color.Blue);
                        }
                        break;
                    case nameof(UserLoggedIn):
                        UserLoggedIn userLoggedIn = (UserLoggedIn)myEvent;
                        AppendText("[" + userLoggedIn.ConnectionDateTime.ToString("g") + "]", Color.Gray);
                        AppendText("User [" + userLoggedIn.Nickname + "] just logged in", Color.Green);
                        break;
                    case nameof(UserLoggedOut):
                        UserLoggedOut userLoggedOut = (UserLoggedOut)myEvent;
                        AppendText("[" + userLoggedOut.DiconnectionDateTime.ToString("g") + "]", Color.Gray);
                        AppendText("User [" + userLoggedOut.Nickname + "] just logged out", Color.DarkRed);
                        break;
                }
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
            consumerService.Disconnect();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/melharfi/ChatRMQ");
        }
    }

}
