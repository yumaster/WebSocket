using System;
using Microsoft.Win32;
using WebSocketSharp.Server;
using System.Windows.Forms;

namespace WebSocketApp
{
    public partial class FrmMain : Form
    {
        public static FrmMain instance;
        private WebSocketServer wss;
        public FrmMain()
        {
            InitializeComponent();
            instance = this;
            this.ShowInTaskbar = false;
            this.Load += FrmMain_Load;
            this.FormClosing += FrmMain_FormClosing;
            this.Resize += FrmMain_Resize;
        }

        private void FrmMain_Load(object sender, System.EventArgs e)
        {
            this.mNotifyIcon.Icon = WebSocketApp.Properties.Resources.queue;

            wss = new WebSocketServer(7799);
            wss.AddWebSocketService<QueueHandler>("/queue");
            wss.Start();
            if (!wss.IsListening)
            {
                MessageBox.Show("端口被占用");
            }
            
        }
        void FrmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.mNotifyIcon.Visible = true;
            }
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您将退出系统,是否继续？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
                if (wss != null && wss.IsListening) wss.Stop();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void mNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }
    }
}
