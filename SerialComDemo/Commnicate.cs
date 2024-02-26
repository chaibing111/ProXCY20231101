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
    public partial class Commnicate : Form
    {
        public static Commnicate genmove = null;
        //1.构造器私有化
        public static Commnicate GetInstance()
        {
            if (genmove == null || (genmove != null && genmove.IsDisposed))
            {
                genmove = new Commnicate();
            }
            return genmove;
        }



        public Commnicate()
        {
            InitializeComponent();
        }
        private string serialPrmAllDic_path1 =
            " E:\\资料\\项目\\麦壳\\AHEROLS_Vision\\AHEROLS\\bin\\Debug\\SysData\\SerialPrmAllDic.json";
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
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
    }
}
