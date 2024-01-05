using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CIT
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string proc_name = Process.GetCurrentProcess().ProcessName;
            Process[] procs = Process.GetProcessesByName(proc_name);
            if (procs.Length > 1)
            {
                // 使用了自定义的通知窗体。
                using (var msgbox = new MsgForm("提示", proc_name + " 已在运行，请勿重复启动。", true))
                {
                    msgbox.ShowDialog();
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
