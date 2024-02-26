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

namespace SerialComDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string serialPrmAllDic_path1 = " E:\\资料\\项目\\麦壳\\AHEROLS_Vision\\AHEROLS\\bin\\Debug\\SysData\\SerialPrmAllDic.json";
        private void btnCom_Click(object sender, EventArgs e)
        {
            //this.Hide();
            if (MyComHelper.MySerialClass.formSerialPort.Visible)
            {
                MyComHelper.MySerialClass.formSerialPort.Activate();
                return;
            }
            else
            {
                MyComHelper.MySerialClass.formSerialPort.Show();
            }
            new Thread(o =>
            {
                while (MyComHelper.MySerialClass.formSerialPort.Visible)
                {
                    Application.DoEvents(); System.Threading.Thread.Sleep(1);
                }
                //MyComHelper.MySerialClass.ins.SerialPrmAllDic = FileHelper.InfoRead<Dictionary<string, MyComHelper.SerialParameter>>(serialPrmAllDic_path);

                FileHelper.InfoSave(MyComHelper.MySerialClass.SerialPrmAllDic, serialPrmAllDic_path1);
                //foreach (var item in MyComHelper.MySerialClass.List_LogEdit)
                //{

                //    PublicLog.MsgShow(item.Number, true, true, false, item.Content);
                //}
                //MyComHelper.MySerialClass.List_LogEdit = new List<MyComHelper.MySerialClass.Log_Class>();
                // PublicLog.MsgShow(6037, true, true, false);     //消息处理
            }).Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
           string c= Run("Scan1", "a");
           textBox1.Text = c;
        }

        public void  DateReceive()
        {

        }
    
        public string   Run(string comName,string msg)
        {
            string dataVal = "";

            try
            {
                MyComHelper.MySerialClass.SerialDic[comName].ReplyData = null;

                MyComHelper.MySerialClass.SerialDic[comName].Write(msg);
                TimeSpan ts = new TimeSpan(0);
                DateTime start = DateTime.Now;
               
                MyComHelper.ByteData tmpData = MyComHelper.MySerialClass.SerialDic[comName].ReplyData;
                while (tmpData == null && dataVal.Length <= 0)
                {
                    tmpData = MyComHelper.MySerialClass.SerialDic[comName].ReplyData;
                    if (tmpData != null)
                    {
                        if (!string.IsNullOrEmpty(MyComHelper.MySerialClass.SerialPrmAllDic[comName].STXstr))
                        {

                        }

                        dataVal = tmpData.ToString().Trim().Replace("\r", "").Replace("\n", "");
                        break;
                    }

                    ts = DateTime.Now - start;
                    System.Windows.Forms.Application.DoEvents(); System.Threading.Thread.Sleep(1);
                    if (ts.TotalSeconds > 3)
                    {
                        return dataVal;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return dataVal;
        }
        private string serialPrmAllDic_path =
            " E:\\资料\\项目\\麦壳\\AHEROLS_Vision\\AHEROLS\\bin\\Debug\\SysData\\SerialPrmAllDic.json";
        //private string serialPrmAllDic_path =AppDomain.CurrentDomain.BaseDirectory + "ParmBackup\\" + " SerialPrmAllDic.json";
        private void Form1_Load(object sender, EventArgs e)
        {
            //串口参数加载
            MyComHelper.MySerialClass.SerialPrmAllDic = FileHelper.InfoRead<Dictionary<string, MyComHelper.SerialParameter>>(serialPrmAllDic_path);
            if (MyComHelper.MySerialClass.SerialPrmAllDic.Count > 0)
            {
                if (MyComHelper.MySerialClass.MySerialOpenAll())
                {
                    textBox1.Text= "串口参数加载成功"+"\r\n";     //消息处理
                }
                else
                {
                    textBox1.Text = "串口参数加载失败" + "\r\n";     //消息处理
                }
            }
            MyComHelper.MySerialClass.formSerialPort.FormClosing += Form_FormClosing;
        }
        public static Form1 form_main = null;
        private static void Form_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            ((System.Windows.Forms.Form)sender).Hide();
            ((System.Windows.Forms.Form)sender).Visible = false;
            //if (((System.Windows.Forms.Form)sender).DialogResult == System.Windows.Forms.DialogResult.Cancel)
            //    e.Cancel = true;
            //if (((System.Windows.Forms.Form)sender).DialogResult == System.Windows.Forms.DialogResult.None)
            //    e.Cancel = true;
            //form_main.Activate();
            e.Cancel = true;
        }
    }
}
