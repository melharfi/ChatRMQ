
namespace ChatUsingRMQ
{
    partial class Chat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.TbNickname = new System.Windows.Forms.TextBox();
            this.RtbMessages = new System.Windows.Forms.RichTextBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TbMessage = new System.Windows.Forms.TextBox();
            this.BtnClearChat = new System.Windows.Forms.Button();
            this.PlChat = new System.Windows.Forms.Panel();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.PlChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nickname :";
            // 
            // TbNickname
            // 
            this.TbNickname.Location = new System.Drawing.Point(78, 9);
            this.TbNickname.Name = "TbNickname";
            this.TbNickname.Size = new System.Drawing.Size(141, 20);
            this.TbNickname.TabIndex = 1;
            // 
            // RtbMessages
            // 
            this.RtbMessages.AcceptsTab = true;
            this.RtbMessages.Location = new System.Drawing.Point(10, 61);
            this.RtbMessages.Name = "RtbMessages";
            this.RtbMessages.Size = new System.Drawing.Size(458, 231);
            this.RtbMessages.TabIndex = 2;
            this.RtbMessages.Text = "";
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(359, 6);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(111, 39);
            this.BtnSend.TabIndex = 3;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Message :";
            // 
            // TbMessage
            // 
            this.TbMessage.Location = new System.Drawing.Point(74, 8);
            this.TbMessage.Multiline = true;
            this.TbMessage.Name = "TbMessage";
            this.TbMessage.Size = new System.Drawing.Size(279, 37);
            this.TbMessage.TabIndex = 1;
            // 
            // BtnClearChat
            // 
            this.BtnClearChat.Location = new System.Drawing.Point(8, 294);
            this.BtnClearChat.Name = "BtnClearChat";
            this.BtnClearChat.Size = new System.Drawing.Size(67, 23);
            this.BtnClearChat.TabIndex = 4;
            this.BtnClearChat.Text = "Clear Chat";
            this.BtnClearChat.UseVisualStyleBackColor = true;
            this.BtnClearChat.Click += new System.EventHandler(this.BtnClearChat_Click);
            // 
            // PlChat
            // 
            this.PlChat.Controls.Add(this.label4);
            this.PlChat.Controls.Add(this.BtnClearChat);
            this.PlChat.Controls.Add(this.TbMessage);
            this.PlChat.Controls.Add(this.BtnSend);
            this.PlChat.Controls.Add(this.RtbMessages);
            this.PlChat.Location = new System.Drawing.Point(12, 37);
            this.PlChat.Name = "PlChat";
            this.PlChat.Size = new System.Drawing.Size(479, 322);
            this.PlChat.TabIndex = 5;
            this.PlChat.Visible = false;
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(225, 7);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(75, 23);
            this.BtnConnect.TabIndex = 5;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(20, 373);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(188, 13);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/melharfi/ChatRMQ";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 395);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TbNickname);
            this.Controls.Add(this.PlChat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Chat";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chat_FormClosing);
            this.PlChat.ResumeLayout(false);
            this.PlChat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbNickname;
        private System.Windows.Forms.RichTextBox RtbMessages;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TbMessage;
        private System.Windows.Forms.Button BtnClearChat;
        private System.Windows.Forms.Panel PlChat;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}