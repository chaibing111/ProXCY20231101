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
using TouchSocket.Core.ByteManager;
using TouchSocket.Sockets;

namespace sunyvpp
{
    public partial class FrmTcpServer : UIForm
    {
        public static FrmTcpServer frmTcpServer = null;
        //1.构造器私有化
        public static FrmTcpServer GetInstance()
        {
            if (frmTcpServer == null || (frmTcpServer != null && frmTcpServer.IsDisposed))
            {
                frmTcpServer = new FrmTcpServer();
            }

            return frmTcpServer;
        }
        public FrmTcpServer()
        {
            InitializeComponent();
        }

        public MyTCPServer server;
        public string[] strip;

        private void btnLisen_Click(object sender, EventArgs e)
        {
            int ang;
            cmbIP.Clear();
            server = new MyTCPServer();
            if (!int.TryParse(txtPort.Text, out ang) && txtPort.Text != "")
            {
                server.InitMyTCPService(ang);
            }
            else
            {
                server.InitMyTCPService(ang);
            }
            server.TcpService.Received += ReceivedHandler1<SocketClient>;


            server.Start();
            strip = server.GetIDs();
            for (int i = 0; i < strip.Length; i++)
            {

                if (server.TcpService.TryGetSocketClient(strip[i], out SocketClient socketClient))
                {
                    if (!cmbIP.Items.Contains(strip[i]))
                    {
                        cmbIP.Items.Add(strip[i] + ":" + socketClient.IP + ":" + socketClient.Port);
                    }
                    cmbIP.SelectedIndex = 0;
                }
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            btnLis_Click(null,null);
            string strSend = null;
            byte[] sendClientData = new byte[1024 * 1024];
            //是否是十六进制发送
            if (chbSend.Checked)
            {
                strSend = txtSend.Text.Replace(" ", "");
                string strsend1 = strSend.Replace(",", "");
                string strsend2 = strsend1.Replace("0x", "");
                string strsend3 = strsend2.Replace("0X", "");
                //将字符串转换为十六进制的字节数组
                sendClientData = strToToHexByte(strsend3);
                if (cmbIP.Text != "" || strip.Length != 0)
                {
                    try
                    {
                        string a = strip[0];
                        server.SendByte(strip[cmbIP.SelectedIndex], sendClientData);
                    }
                    catch (Exception exception)
                    {
                        ;
                    }
                }
            }
            else
            {
                if (cmbIP.Text != "" || strip.Length != 0)
                {
                    try
                    {
                        string a = strip[0];
                        server.SendStr(strip[cmbIP.SelectedIndex], txtSend.Text.ToString());
                    }
                    catch (Exception exception)
                    {
                        ;
                    }
                }
                //strSend = textBox4.Text.Replace(" ", "");
                //m1.SendByte(sendClientData);
            }
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
        private void FrmTcpServer_Load(object sender, EventArgs e)
        {
       
        }

        private string a = "";
        public void ReceivedHandler1<SocketClient>(TouchSocket.Sockets.SocketClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
        {
            a = "";
            string Receive = "";
            string LastReciveMessage = "";
            string LastReciveTime = DateTime.Now.ToString();
            //string Receive = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);

            if (chbReceive.Checked)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < byteBlock.Len; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", byteBlock.Buffer[i]);
                }
                Receive = sb.ToString() ;
            }
            else
            {
                Receive = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
                Receive = Receive ;
            }
            LastReciveMessage = client.IP + ": " + client.Port + ": " + Receive;
            try
            {
                this.BeginInvoke(new Action(() =>
                {
                    txtReceive.Text += LastReciveTime + ": " + LastReciveMessage;
                }));
            }
            catch (Exception e)
            {
                ;
            }
  

            //txtReceive.Text += LastReciveTime + ": " + LastReciveMessage;
            //Globals.LogRecord($"【{client.IP}:{client.Port}:收到信息:{Receive}】");
        }

        private void btnLis_Click(object sender, EventArgs e)
        {
            cmbIP.Items.Clear();
            strip = server.GetIP();
            for (int i = 0; i < strip.Length; i++)
            {

                if (server.TcpService.TryGetSocketClient(strip[i], out SocketClient socketClient))
                {
                    if (!cmbIP.Items.Contains(strip[i] + ":" + socketClient.IP + ":" + socketClient.Port))
                    {
                        cmbIP.Items.Add(strip[i] + ":" + socketClient.IP + ":" + socketClient.Port);
                    }
                    cmbIP.SelectedIndex = 0;
                }
            }
        }

        private void FrmTcpServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            server.TcpService.Stop();
        }
        private void btn_DataReceivedClear_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
        }

        private void btn_DataSendClear_Click(object sender, EventArgs e)
        {
            txtSend.Clear();
        }
    }
}
