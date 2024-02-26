using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sunyvpp
{
    public partial class FrmLight : UIForm
    {

        public static FrmLight genmove = null;
        //1.构造器私有化
        public static FrmLight GetInstance()
        {
            if (genmove == null || (genmove != null && genmove.IsDisposed))
            {
                genmove = new FrmLight();
            }
            return genmove;
        }
        public FrmLight()
        {
            InitializeComponent();
        }

        private void btnOpenLight_Click(object sender, EventArgs e)
        {
            try
            {
                string sendMessage="SA0255#"; ;
                switch (cmbChannelChange.SelectedIndex)
                {
                    case 0:
                        sendMessage = "SA0255#";
                        break;
                    case 1:
                        sendMessage = "SB0255#";
                        break;
                    case 2:
                        sendMessage = "SC0255#";
                        break;
                    case 3:
                        sendMessage = "SD0255#";
                        break;
                }
                Globals.myLaserSerialPort.SendMessage(sendMessage);
            }
            catch (Exception exception)
            {
                ShowErrorTip("【光源打开失败！】");
                Globals.LogRecord("【光源打开失败！】", false);
            }
     
        }

        private void btnCloseLight_Click(object sender, EventArgs e)
        {

            try
            {
                string sendMessage = "SA0000#"; ;
                switch (cmbChannelChange.SelectedIndex)
                {
                    case 0:
                        sendMessage = "SA0000#";
                        break;
                    case 1:
                        sendMessage = "SB0000#";
                        break;
                    case 2:
                        sendMessage = "SC0000#";
                        break;
                    case 3:
                        sendMessage = "SD0000#";
                        break;
                }
                Globals.myLaserSerialPort.SendMessage(sendMessage);
            }
            catch (Exception exception)
            {
                ShowErrorTip("【光源关闭失败！】");
                Globals.LogRecord("【光源关闭失败！】", false);
            }

        }

        private void FrmLight_Load(object sender, EventArgs e)
        {
            cmbChannelChange.SelectedIndex = 0;
        }
    }
}
