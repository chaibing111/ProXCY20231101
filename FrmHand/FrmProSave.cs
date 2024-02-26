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
    public partial class FrmProSave : UIForm
    {

        string itemProjectPath = null;
        public FrmProSave(string itemProjectPath)
        {
            this.itemProjectPath = itemProjectPath;
            InitializeComponent();
        }
        private static FrmProSave _instance;
        public static FrmProSave Instance(string itemProjectPath)
        {
            if (_instance == null)
            {
                _instance = new FrmProSave(itemProjectPath);
            }
            return _instance;
        }

        /// <summary>
        /// 复制文件夹及文件
        /// </summary>
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        /// <returns></returns>
        public static int CopyFolder(string sourceFolder, string destFolder)
        {
            try
            {
                //如果目标路径不存在,则创建目标路径
                if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }
                //得到原文件根目录下的所有文件
                string[] files = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = System.IO.Path.GetFileName(file);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    System.IO.File.Copy(file, dest);//复制文件
                }
                //得到原文件根目录下的所有文件夹
                string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = System.IO.Path.GetFileName(folder);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    CopyFolder(folder, dest);//构建目标路径,递归复制文件
                }
                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }
        private void FrmProSave_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance.Dispose();
            _instance = null;
        }
        private void btnConform_Click(object sender, EventArgs e)
        {
            string ProjectPath = Path.GetDirectoryName(itemProjectPath);
            string nameItemPath = ProjectPath + "\\"  + txtFileName.Text;
            if (CopyFolder(itemProjectPath, nameItemPath) != 1)
            {
                MessageBox.Show(nameItemPath + ":另存为失败");
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(ConfigureFilePath.Path_Config_ProFile, false, Encoding.Default))
                {
                    sw.Write(nameItemPath);
                }
                //del_Save_As_Item();
                EventMgr.Ins.GetEvent<ProductEvent>().Subscribe(Globals.ProductOpt);

                //Globals.ProductOpt("a");
                this.Close();
            }
        }

        private void FrmProSave_Load(object sender, EventArgs e)
        {
           
        }



    }
}
