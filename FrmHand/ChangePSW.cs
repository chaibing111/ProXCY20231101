using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace sunyvpp
{
    public partial class ChangePSW : UIForm
    {

        public static ChangePSW genmove = null;
        //1.构造器私有化
        public static ChangePSW GetInstance()
        {
            if (genmove == null || (genmove != null && genmove.IsDisposed))
            {
                genmove = new ChangePSW();
            }
            
            return genmove;
        }
        private ChangePSW()
        {
            
            this.user = new List<UserInf>();
            InitializeComponent();
            //cmb_UserName.SelectedIndex = 0;
        }
        private List<UserInf> user;

        List<UserInf> User { get => user; set => user = value; }
        public void ExtractPSW()
        {
            FileStream fs = new FileStream("UserInf.obj", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            this.user = (List<UserInf>)bf.Deserialize(fs);

            fs.Close();
        }
        private void btn_Login_Click(object sender, EventArgs e)
        {
            SavePSW();
            MessageBox.Show("密码修改完成");
        }
        public void SavePSW()
        {
            ExtractPSW();
            //this.user.Clear();
            if (this.cmb_UserNameCP.SelectedIndex == 0)
            {
                if (txb_confirm_psw.Text == txb_psw.Text)
                {
                    this.user[0].Admin = "administrator";
                    this.user[0].ENG = "engineer";
                    this.user[0].OP = "operator";
                    this.user[0].Admin_PSW = txb_psw.Text;
                    this.user[0].OP_PSW    = this.user[0].OP_PSW;
                    this.user[0].ENG_PSW   = this.user[0].ENG_PSW;

                    //this.user.Add(new UserInf
                    //{
                    //    Admin = "administrator",
                    //    OP = "operator",
                    //    ENG = "engineer",
                    //    Admin_PSW = this.user[0].Admin_PSW = txb_psw.Text,
                    //    OP_PSW = this.user[0].OP_PSW,
                    //    ENG_PSW = this.user[0].ENG_PSW
                    //});
                }
            }
            //this.user[0].OP_PSW
            //    this.user[0].ENG_PSW
            //    this.user[0].Admin_PSW
            if (this.cmb_UserNameCP.SelectedIndex == 1)
            {
                if (txb_confirm_psw.Text == txb_psw.Text)
                {
                    this.user[0].Admin = "administrator";
                    this.user[0].ENG = "engineer";
                    this.user[0].OP = "operator";
                    this.user[0].Admin_PSW = this.user[0].Admin_PSW;
                    this.user[0].OP_PSW = this.user[0].OP_PSW;
                    this.user[0].ENG_PSW = txb_psw.Text;
                    //this.user.Add(new UserInf
                    //{
                    //    Admin = "administrator",
                    //    OP = "operator",
                    //    ENG = "engineer",
                    //    Admin_PSW = this.user[0].Admin_PSW,
                    //    OP_PSW = this.user[0].OP_PSW,
                    //    ENG_PSW = this.user[0].ENG_PSW = txb_psw.Text
                    //});
                }
            }
            if (this.cmb_UserNameCP.SelectedIndex == 2)
            {
                if (txb_confirm_psw.Text == txb_psw.Text)
                {
                    this.user[0].Admin = "administrator";
                    this.user[0].ENG = "engineer";
                    this.user[0].OP = "operator";
                    this.user[0].Admin_PSW = this.user[0].Admin_PSW;
                    this.user[0].OP_PSW = txb_psw.Text;
                    this.user[0].ENG_PSW = this.user[0].ENG_PSW;
                    //this.user.Add(new UserInf
                    //{
                    //    Admin = "administrator",
                    //    OP = "operator",
                    //    ENG = "engineer",
                    //    Admin_PSW = this.user[0].Admin_PSW,
                    //    OP_PSW = this.user[0].OP_PSW= txb_psw.Text,
                    //    ENG_PSW = this.user[0].ENG_PSW
                    //});
                }
            }
          
            FileStream fs = new FileStream("UserInf.obj", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this.user);//将当前文本文件中读取的数据对象，保存为集合对象，并以序列化方式存在
            fs.Close();

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void ChangePSW_Load(object sender, EventArgs e)
        {
            this.cmb_UserNameCP.SelectedIndex = 0;
        }
    }
}
