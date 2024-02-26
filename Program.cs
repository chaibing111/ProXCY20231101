using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sunyvpp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            //禁止窗口多开
            Mutex instance = new Mutex(true, "FrmMain", out bool IsCreateNew);
            if (!IsCreateNew)
            {
                UIMessageBox.ShowError("FrmMain程序只允许开一个");
                Application.Exit();
                return;
            }
            Globals.InitMainFrm();
            //Application.Run(new MainForm());
        }
    }
}
