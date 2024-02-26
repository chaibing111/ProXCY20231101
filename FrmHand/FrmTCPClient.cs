using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro.OCRMax.Implementation.Internal;
using Sunny.UI;
using TouchSocket.Core;
using TouchSocket.Core.ByteManager;
using TouchSocket.Sockets;

namespace sunyvpp
{
    public partial class FrmTCPClient : UIForm
    {
        public static FrmTCPClient frmTCPClient = null;
        //1.构造器私有化
        public static FrmTCPClient GetInstance()
        {
            if (frmTCPClient == null || (frmTCPClient != null && frmTCPClient.IsDisposed))
            {
                frmTCPClient = new FrmTCPClient();
            }

            return frmTCPClient;
        }
        private FrmTCPClient()
        {
            InitializeComponent();
        }
        private MyTcpHelper m1 = null;
        private void btn_Send_Click(object sender, EventArgs e)
        {
            //tcpClient.Send(txt_Send.Text.Trim());
            string strSend = null;
            byte[] sendClientData = new byte[1024 * 1024];
            //是否是十六进制发送
            if (chb_0x_DataSend.Checked)
            {
                strSend = txt_Send.Text.Replace(" ", "");
                string strsend1 = strSend.Replace(",", "");
                string strsend2 = strsend1.Replace("0x", "");
                string strsend3 = strsend2.Replace("0X", "");
                //将字符串转换为十六进制的字节数组
                sendClientData = strToToHexByte(strsend3); 
                m1.SendByte(sendClientData);
            }
            else
            {
                strSend = txt_Send.Text.Replace(" ", "");
                //sendClientData = Encoding.Default.GetBytes(strSend);
                m1.sendStr(strSend);
            }
        }
        private static byte[] strToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
            return returnBytes;
        }
        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            try
            {
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Replace(" ", ""), 16);
            }
            catch
            {
                MessageBox.Show("十六进制发送字符异常");
            }
            return returnBytes;
        }
        bool b_Connect = true;
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            int ang;
            if (!int.TryParse(txt_ServerPort.Text, out ang) && txt_ServerPort.Text != "")
            {
                ShowErrorDialog("请输入正确格式的端口号");
                txt_ServerPort.Text = ""; //textbox 重置
            }
            else
            {
                m1 = new MyTcpHelper(txt_ServerIp.Text.ToString(), 60000, 10);
                 m1.ConnectServer();
                m1.tcpClient.Received = ReceivedHandler1;
            }
            b_Connect = !b_Connect;
            if (b_Connect)
            {
                btn_Connect.Text = "连接";
                btn_Send.Enabled = true;
                ShowSuccessDialog("TCP连接成功！");
                Globals.LogRecord("【TCP连接成功！】", true);
            }
            else
            {
                m1.Close();
                btn_Connect.Text = "断开";
                btn_Send.Enabled = true;
                ShowErrorDialog("【TCP连接断开！】");
                Globals.LogRecord("【TCP连接断开！】", true);
            }
        }

        private void btn_DataReceivedClear_Click(object sender, EventArgs e)
        {
            txt_DataReceived.Clear();
        }

        private void btn_DataSendClear_Click(object sender, EventArgs e)
        {
            txt_Send.Clear();
        }

        private void FrmTCPClient_Load(object sender, EventArgs e)
        {
            btn_Send.Enabled = true;
        }
        public string a="";
        public void ReceivedHandler1<ITcpClient>(ITcpClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
        {
            a = "";
            string LastReciveTime = DateTime.Now.ToString();
            string LastReciveMessage = "";
            if (chb_0x_DataReceived.Checked)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < byteBlock.Buffer.Length; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", byteBlock.Buffer[i]);
                }
                a = sb.ToString() + "\r\n";
            }
            else
            {
                a = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
                a = a + "\r\n";
            }
            LastReciveMessage = m1.tcpClient.RemoteIPHost + ": "  + a;
            this.BeginInvoke(new Action(() =>
            {
                txt_DataReceived.Text += LastReciveMessage;
            }));
        }

        private void FrmTCPClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                m1.Close();
            }
            catch (Exception exception)
            {
                ;
            }
        }
    }
}
