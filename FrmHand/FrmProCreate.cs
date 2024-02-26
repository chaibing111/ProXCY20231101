using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventMgrLib;
using Sunny.UI;

namespace sunyvpp
{
    public partial class FrmProCreate : UIForm
    {
        public FrmProCreate(string projectPath)
        {
            this.projectPath = projectPath;
            InitializeComponent();
        }

        string projectPath = null;
        string projectdirPath = null;


        private static FrmProCreate _instance;
        public static FrmProCreate Instance(string projectPath)
        {
            if (_instance == null)
            {
                _instance = new FrmProCreate(projectPath);
            }
            return _instance;
        }

        /// <summary>
        /// 新建项目名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_define_Click(object sender, EventArgs e)
        {
        }

        private void frm_NewProject_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance.Dispose();
            _instance = null;
        }

        private void btnCreatePro_Click(object sender, EventArgs e)
        {

            projectdirPath = projectPath + "\\" + txtFileName.Text;
            if (!Directory.Exists(projectdirPath))
            {
                Directory.CreateDirectory(projectdirPath);
                MessageBox.Show(projectdirPath + "该项创建成功");
                using (StreamWriter sw = new StreamWriter(ConfigureFilePath.Path_Config_ProFile, false, Encoding.Default))
                {
                    sw.Write(projectdirPath);
                }
                //订阅事件
                EventMgr.Ins.GetEvent<ProductEvent>().Subscribe(Globals.ProductOpt);
                //Globals.ProductOpt("a");
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("该项目已经存在，请重新输入项目名称");
            }
        }
    }
}
