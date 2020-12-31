using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WebSocketApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isAppRunning = false;
            Mutex mutex = new Mutex(true, System.Diagnostics.Process.GetCurrentProcess().ProcessName, out isAppRunning);
            if (!isAppRunning)
            {
                MessageBox.Show("队列服务已经启动！");
                Environment.Exit(1);
            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-Hans");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
