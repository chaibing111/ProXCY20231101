using Cognex.VisionPro.ToolBlock;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using VisionProHelper;

namespace sunyvpp
{
    public partial class FrmProjSelect : UIForm
    {


        public static FrmProjSelect frmProjSelect = null;
        public static FrmProjSelect GetInstance()
        {
            if (frmProjSelect == null || (frmProjSelect != null && frmProjSelect.IsDisposed))
            {
                frmProjSelect = new FrmProjSelect();
            }
            return frmProjSelect;
        }
        public FrmProjSelect()
        {
            InitializeComponent();
        }

        private void FrmProjSelect_Load(object sender, EventArgs e)
        {
            cmbCam.SelectedIndex = 0;
            cmbExposeSelect.SelectedIndex = 0;
            VisionProHelper.MyVisionProMgr.ins.VisionItemUpdata();
            cmbChangeProj.Items.Clear();
            Globals.ProjName.Clear();
            Globals.ProjNamePath.Clear();
            cmbChangeProj.Clear();

            foreach (string keys in VisionProHelper.MyVisionProMgr.ins.VisionItemDic.Keys)
            {
                Globals.ProjName.Add(keys);

                Globals.ProjNamePath.Add(MyVisionProMgr.ins.ProjectBasePath + keys);

                cmbChangeProj.Items.Add(keys);
            }
            cmbChangeProj.SelectedIndex = 0;

            string a = cmbChangeProj.SelectedItem.ToString();
        }
        private void btnChangeProj_Click(object sender, EventArgs e)
        {
            CogToolBlock tbSelected = MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()];
            Globals.tbSelect = tbSelected;
            ICogImage MyOutPutImage=null;
            int ExPose = 1000;

            if (cmbCam.SelectedIndex==0)
            {
                if (cmbExposeSelect.SelectedIndex == 0)
                {
                    ExPose = Globals.SettingOption.UpCamExpose1;
                }
                if (cmbExposeSelect.SelectedIndex == 1)
                {
                    ExPose = Globals.SettingOption.UpCamExpose2;
                }
                Globals.Camera.Capture_New(ExPose,out Bitmap bt);
                 MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
            }
            if (cmbCam.SelectedIndex == 1)
            {
                if (cmbExposeSelect.SelectedIndex == 0)
                {
                    ExPose = Globals.SettingOption.DownCamExpose1;
                }
                if (cmbExposeSelect.SelectedIndex == 1)
                {
                    ExPose = Globals.SettingOption.DownCamExpose2;
                }
                Globals.CameraDown.Capture_New(ExPose, out Bitmap bt);
                MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
            }
            Globals.tbSelect.Inputs["Input1"].Value = MyOutPutImage;
            Globals.tbSelect.Run();
            //if (Globals.tbSelect.RunStatus.Result == CogToolResultConstants.Accept)
            this.cogRecordDisplay1.Record = Globals.tbSelect.CreateLastRunRecord().SubRecords[0];
            ICogRecord DispalyRecord = Globals.tbSelect.CreateLastRunRecord().SubRecords[0];
            Globals.DispalyRecord(DispalyRecord);
            double valuex = Math.Round((double)Globals.tbSelect.Outputs["X"].Value, 3);
            Globals.tbSelect.Run();
            string b = cmbChangeProj.SelectedItem.ToString();
            ShowSuccessDialog(b + "视觉工程切换成功！");
            Globals.LogRecord("【视觉工程" + b + "切换成功！】");
        }
    }
}
