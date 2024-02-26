using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro.OCRMax.Implementation.Internal;
using Sunny.UI;

namespace sunyvpp
{
    public partial class FrmCOM : UIForm
    {
        MySerialPort mySerialPort = new MySerialPort();
        public FrmCOM()
        {
            InitializeComponent();
        }
        private void btn_OpenCom_Click(object sender, EventArgs e)
        {
            mySerialPort.SerialPortName = cmbComName.Text.ToString();
            mySerialPort.BaudRate = int.Parse(cmbBaudRate.Text.ToString()) ;
            mySerialPort.DataBits = int.Parse(cmbDataBits.Text);
            mySerialPort.MyParity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
            mySerialPort.MyStopBits = (StopBits)int.Parse(cmbStopBits.Text);
            if (mySerialPort.InitMySerialPort())
            {
                mySerialPort.inforShowEvent += ShowReceiveMessage;
                ShowSuccessDialog("串口打开成功！");
                Globals.LogRecord("【串口打开成功！】", true);
                btn_Send.Enabled = true;
                btn_Send.Visible = true;
            }
            else
            {
                ShowErrorDialog("【串口打开失败！】");
                Globals.LogRecord("【串口打开失败！】", true);
            }
        }
        private string s = "";
        private void ShowReceiveMessage(object sender, string e)
        {
            s = "";
            s = e;
            BeginInvoke(new Action(() =>
            {
                txt_DataReceived.Text += s + "\r\n";
            }));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            mySerialPort.Close();
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                mySerialPort.SendMessage(txt_Send.Text);
            }
            catch (Exception exception)
            {
                ShowErrorTip("【串口发送失败！】");
                Globals.LogRecord("【串口发送失败！】", true);
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

        private void FrmCOM_Load(object sender, EventArgs e)
        {

            //波特率
            cmbBaudRate.Items.Add(1200);
            cmbBaudRate.Items.Add(2400);
            cmbBaudRate.Items.Add(4800);
            cmbBaudRate.Items.Add(9600);
            cmbBaudRate.Items.Add(19200);
            cmbBaudRate.Items.Add(38400);
            cmbBaudRate.Items.Add(43000);
            cmbBaudRate.Items.Add(56000);
            cmbBaudRate.SelectedIndex = 3;

            //初始化数据位
            cmbDataBits.Items.Add(8);
            cmbDataBits.SelectedIndex = 0;

            //初始化校验位
            cmbParity.Items.Add("None");
            cmbParity.Items.Add("Even");
            cmbParity.Items.Add("Odd");
            cmbParity.SelectedIndex = 0;

            //初始化停止位
            cmbStopBits.Items.Add(1);
            cmbStopBits.Items.Add(2);
            cmbStopBits.Items.Add(3);
            cmbStopBits.SelectedIndex = 0;

            cmbComName.SelectedIndex = 0;
        }
    }
}
