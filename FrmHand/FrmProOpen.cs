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
    public partial class FrmProOpen : UIForm
    {
        public FrmProOpen(string projectPath)
        {
            this.projectPath = projectPath;
            InitializeComponent();
        }


        string projectPath = null;

        private static FrmProOpen _instance;
        public static FrmProOpen Instance(string projectPath)
        {
            if (_instance == null)
            {
                _instance = new FrmProOpen(projectPath);
            }
            return _instance;
        }

        private void frm_ItemDir_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance.Dispose();
            _instance = null;
        }

        private void Load_ItemDir()
        {
            DirectoryInfo floder = new DirectoryInfo(projectPath);
            //FileSystemInfo[] files = floder.GetFileSystemInfos();
            //FileInfo[] fileInfos = floder.GetFiles();
            cmbPro.Items.Clear();
            FileSystemInfo[] dir = floder.GetDirectories();
            foreach (var item in dir)
            {
                //如果是文件夹 递归遍历此文件夹
                cmbPro.Items.Add(item);
            }
            if (cmbPro.Items.Count > 0)
            {
                cmbPro.SelectedIndex = 0;
            }
            else
            {
                ShowInfoDialog("请打开项目文件！");
            }
        }


        private void btnOpenPro_Click(object sender, EventArgs e)
        {
            if (cmbPro.Text != "")
            {
                string projectdirPath = projectPath + "\\" + cmbPro.Text;

                using (StreamWriter sw = new StreamWriter(ConfigureFilePath.Path_Config_ProFile, false, Encoding.Default))
                {
                    sw.Write(projectdirPath);
                }
                ShowSuccessDialog(cmbPro.Text + ":项目切换成功");
                Globals.LogRecord("【项目切换成功！】", true);
                //del_Open_Item();
                EventMgr.Ins.GetEvent<ProductEvent>().Subscribe(Globals.ProductOpt);
                Globals.ProductOpt("a");
                this.Close();
            }
            else
            {
                ShowErrorDialog("项目切换失败");
                Globals.LogRecord("【项目切换失败！】", true);
            }
        }

        private void FrmProOpen_Load(object sender, EventArgs e)
        {
            Load_ItemDir();
        }
    }
}
