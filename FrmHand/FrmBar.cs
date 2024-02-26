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
using Sunny.UI;

namespace sunyvpp
{
    public partial class FrmBar : UIForm
    {
        Thread Loading = null;
        public FrmBar()
        {
            InitializeComponent();
        }

        //private void FrmBar_Load(object sender, EventArgs e)
        //{

        //}
        private void FrmBar_Load(object sender, EventArgs e)
        {
            Loading = new Thread(new ThreadStart(LoadingProcess));
            Loading.Start();
            progressBar1.Value = 0;
        }

        //update processbar pos
        public void SetProcess(int value)
        {
            progressBar1.Invoke((MethodInvoker)delegate
            {
                progressBar1.Value = value;
            });
        }
        //update loading message
        public void SetMessage(string message)
        {
            loadingtext.Invoke((MethodInvoker)delegate
            {
                loadingtext.Text = message;
            });
        }
        //end process text update
        public void EndProcess()
        {
            if (Loading != null)
                Loading.Abort();
        }


        int i = 0;
        private void LoadingProcess()
        {
            while (Globals.loadingProcess_Var)
            {
                switch (i)
                {
                    case 0:
                        {
                            label_process.Invoke((MethodInvoker)delegate
                            {
                                label_process.Text = "Loading";
                            });
                            i++;
                            break;
                        }
                    case 1:
                        {
                            label_process.Invoke((MethodInvoker)delegate
                            {
                                label_process.Text = "Loading>>";
                            });
                            i++;
                            break;
                        }
                    case 2:
                        {
                            label_process.Invoke((MethodInvoker)delegate
                            {
                                label_process.Text = "Loading>>>>";
                            });
                            i++;
                            break;
                        }
                    case 3:
                        {
                            label_process.Invoke((MethodInvoker)delegate
                            {
                                label_process.Text = "Loading>>>>>>>";
                            });
                            i = 0;
                            break;
                        }
                }
                Thread.Sleep(100);
            }
        }

    }
}
