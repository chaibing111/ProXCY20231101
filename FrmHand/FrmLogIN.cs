using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace sunyvpp
{
    public partial class Form1 : UIForm
    {

        public static Form1 genmove = null;
        //1.构造器私有化
        public static Form1 GetInstance()
        {
            if (genmove == null || (genmove != null && genmove.IsDisposed))
            {
                genmove = new Form1();
            }

            return genmove;
        }
        public Form1()
        {
            this.user = new List<UserInf>();
            InitializeComponent();
        }
        private List<UserInf> user;

        List<UserInf> User { get => user; set => user = value; }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                this.progressBar1.Value = i;
                //Thread.Sleep(1);
            }
            string C;
            string cccc = " a b c d ";
            string ddd = " 1 b 3 d ";
            if (cccc.Contains(" "))
            {
                 C = cccc.Replace(" ", "");
            }
            string rrr=Regex.Replace(ddd, @"\s", "");
            //string C = cccc;
            ExtractPSW();

            if (this.cmb_UserName.SelectedIndex==0)
            {
                if (this.user[0].Admin_PSW == txbPsw.Text)
                {
                    txbPsw.Text = "";
                    //FrmMain fr = FrmMain.GetInstance();
                    //fr.Show();

                    //CsvServer.Instance.Start();
                    //btnChangPSW.Visible = true;
                    btnChangPSW.Enabled = true;
                    Globals.LogInOK = true;
                    ShowSuccessDialog("管理员用户登录成功！");
                    this.Close();
                    //MessageBox.Show("OK!");
                    return;
                }
            }
            if (this.cmb_UserName.SelectedIndex == 1)
            {
                if (this.user[0].ENG_PSW == txbPsw.Text)
                {
                    txbPsw.Text = "";
                    ShowSuccessDialog("工程师登录成功!");
                    this.Close();
                    return;
                }
            }
            if (this.cmb_UserName.SelectedIndex == 2)
            {
                if (this.user[0].OP_PSW == txbPsw.Text)
                {
                    txbPsw.Text = "";
                    ShowSuccessDialog("作业员登录成功!");
                    this.Close();
                    return;
                }
            }

            txbPsw.Text = "";
            ShowErrorDialog("登录失败！");
            //       this.user[0].Admin
            //       this.user[0].Admin_PSW
            //       this.user[0].ENG
            //       this.user[0].ENG_PSW
            //       this.user[0].OP
            //       this.user[0].OP_PSW

        }
        public void ExtractPSW()
        {
            FileStream fs = new FileStream("UserInf.obj", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            this.user = (List<UserInf>)bf.Deserialize(fs);
            
            fs.Close();
        }
        private void btn_Logout_Click(object sender, EventArgs e)
        {
            if (Globals.LogInOK)
            {
                ChangePSW fr = ChangePSW.GetInstance();
                fr.Show();
            }
            else
            {
                ShowErrorDialog("请使用管理员权限登录账号！" );
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ExtractPSW();
            this.cmb_UserName.SelectedIndex = 0;

            if (Globals.LogInOK)
            {
                btnChangPSW.Visible = true;
                btnChangPSW.Enabled = true;
            }
            //this.user.Clear();
            //this.user[0].Admin = "administrator";
            //this.user[0].ENG = "engineer";
            //this.user[0].OP = "operator";
            //this.user[0].Admin_PSW = "111";// this.user[0].Admin_PSW;
            //this.user[0].OP_PSW = "111";// this.user[0].Admin_PSW;
            //this.user[0].ENG_PSW = "111";// this.user[0].Admin_PSW;
            //this.user.Add(new UserInf
            //{
            //    Admin = "administrator",
            //    OP = "operator",
            //    ENG = "engineer",
            //    //Admin_PSW = this.user[0].Admin_PSW,
            //    //OP_PSW = this.user[0].OP_PSW,
            //    //ENG_PSW = this.user[0].ENG_PSW

            //    Admin_PSW = "111",
            //    OP_PSW = "111",
            //    ENG_PSW = "111"
            //});

            //FileStream fs = new FileStream("UserInf.obj", FileMode.Create);
            //BinaryFormatter bf = new BinaryFormatter();
            //bf.Serialize(fs, this.user);//将当前文本文件中读取的数据对象，保存为集合对象，并以序列化方式存在
            //fs.Close();
    
        }

        private void btn_Logout_Click_1(object sender, EventArgs e)
        {
            btnChangPSW.Enabled = false;
            Globals.LogInOK = false;
            btnChangPSW.Visible = false;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            txbPsw.Focus();
        }
    }
}
