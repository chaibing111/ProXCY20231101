using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace sunyvpp
{
    public partial class PLCTest : UIForm
    {

        public static PLCTest frmPLCTest = null;
        AutoSizeFormClass ascFrmPLCTest = new AutoSizeFormClass();//实例化自动适应窗体类
        //1.构造器私有化
        //MVS des = new MVS();
        //private FrmCam()
        //{
        //    InitializeComponent();
        //    Globals.Initialglb();
        //    //Globals.SettingOption.CamExpose;


        //    if (File.Exists("1.json"))
        //    {
        //        Globals.mSettingOptionParm = (mSettingOption)JsonHelper.Instance.JsonFileToObject("1.json", Globals.mSettingOptionParm);
        //    }

        //    //uiTextBox1.Text = Globals.mSettingOptionParm.工单;
        //}
        public static PLCTest GetInstance()
        {
            if (frmPLCTest == null || (frmPLCTest != null && frmPLCTest.IsDisposed))
            {
                frmPLCTest = new PLCTest();
            }
            return frmPLCTest;
        }
        private PLCTest()
        {
            InitializeComponent();
        }

        private void PLCTest_Load(object sender, EventArgs e)
        {
            ascFrmPLCTest.controllInitializeSize(this);
        }

        private void PLCTest_SizeChanged(object sender, EventArgs e)
        {

            // 3.为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应
            ascFrmPLCTest.controlAutoSize(this);
        }

        private void btnReadValue_Click(object sender, EventArgs e)
        {
            try
            {
                var camTriX = Globals.omronFinsUdp.ReadInt32(txbReadAddress.Text);
                if (camTriX.IsSuccess)
                {
                    txbReadValue.Text = camTriX.Content.ToString();
                    ShowInfoDialog("数据读取", "数据读取成功！", UIStyle.Green);
                    ShowSuccessDialog("数据读取成功！");
                }
                else
                {
                    ShowErrorDialog("数据读取失败!");
                }
            }
            catch (Exception exception)
            {
                ShowInfoDialog("数据读取", "数据读取成功！", UIStyle.Green);
                ShowSuccessDialog("数据读取成功！");
                ShowErrorDialog("您输入了错误的数据格式！请输入D0，D100类型字符串" + exception.ToString());
            }
        }

        private void btnWriteValue_Click(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(txbWriteValue.Text, out value))
            {
                txbWriteValue.Text = "";
                ShowErrorDialog("您输入了错误的数据格式！请输入数字0,1,2,3类型格式"); return;
            }
            try
            {
                var camTriX=     Globals.omronFinsUdp.Write(txbWriteAddress.Text, value);
                if (camTriX.IsSuccess)
                {
                    ShowSuccessDialog("数据写入成功！");
                    Globals.LogRecord("【数据写入成功！】", true);
                }
                else
                {
                    ShowErrorDialog("数据写入失败");
                }
            }
            catch (Exception exception)
            {
                ShowErrorDialog("您输入了错误的数据格式！请输入D0，D100类型字符串"+ exception.ToString());
            }
        }
    }
}
