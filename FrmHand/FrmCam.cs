using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvCamCtrl.NET;
using Sunny.UI;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ToolGroup;
using Cognex.VisionPro.QuickBuild;
using Cognex.VisionPro.Implementation;
using System.Collections;
using Cognex.VisionPro3D;
using Cognex.VisionPro.QuickBuild.Implementation.Internal;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.ImageFile;
using System.Diagnostics;
using System.Drawing.Imaging;
using Cognex.VisionPro.FGGigE.Implementation.Internal;
using HalconDotNet;
using HslCommunication.Profinet;
using HslCommunication;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Omron;
using VisionProHelper;

namespace sunyvpp
{
    public partial class FrmCam : UIForm
    {
        public static FrmCam frmVisionParam = null;
        AutoSizeFormClass ascFrmVisionParam = new AutoSizeFormClass();//实例化自动适应窗体类
        //1.构造器私有化
        private FrmCam()
        {
            InitializeComponent();
            JsonHelper.Instance.ObjectToJsonFile(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json", Globals.SettingOption);
            if (File.Exists(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json"))
            {
                Globals.SettingOption = (OptionSetting)JsonHelper.Instance.JsonFileToObject(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json", Globals.SettingOption);
            }
        }
        public static FrmCam GetInstance()
        {
            if (frmVisionParam == null || (frmVisionParam != null && frmVisionParam.IsDisposed))
            {
                frmVisionParam = new FrmCam();
            }
            return frmVisionParam;
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)
        {
        }
        private void FrmVisionParam_SizeChanged(object sender, EventArgs e)
        {
            // 3.为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应
            ascFrmVisionParam.controlAutoSize(this);
        }
        private Thread th;
        private void FrmCam_Load(object sender, EventArgs e)
        {
            this.rdbFile.Checked = true;
            cbDeviceList.SelectedIndex = 0;
            cmbExposeSelect.SelectedIndex = 0;
            chbUpCamPhotoPos.SelectedIndex = 0;
            intupdown1.Value = Globals.SettingOption.UpCamExpose1;
            intupdown2.Value = Globals.SettingOption.UpCamExpose2;
            ascFrmVisionParam.controllInitializeSize(this);
            th = new Thread(Run);
            th.IsBackground = true;
            th.Start();
            #region MyRegionLoadVPP
            cbDeviceList.SelectedIndex = 0;
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

            if (Globals.ProjName.Count > 0)
            {
                cmbChangeProj.SelectedIndex = 0;
                string a = cmbChangeProj.SelectedItem.ToString();
            }
            else
            {
                Globals.LogRecord("【视觉工具:" + "加载失败！】", true);
            }
            if (cbDeviceList.SelectedIndex == 0)
            {
                intupdown1.Value = Globals.SettingOption.UpCamExpose1;
                intupdown2.Value = Globals.SettingOption.UpCamExpose2;
            }
            if (cbDeviceList.SelectedIndex == 1)
            {
                intupdown1.Value = Globals.SettingOption.DownCamExpose1;
                intupdown2.Value = Globals.SettingOption.DownCamExpose2;
            }

            #endregion

            Globals.LoadVisionParam();
        }

        public static void DrawCrosscogDisplay(int width, int height, ref CogDisplay crd, bool isAdd)
        {
            try
            {
                if (isAdd)
                {
                    if (crd != null)
                    {
                        ICogImage image = crd.Image;

                        if (image == null)
                        {
                            image = new CogImage8Grey(width, height);
                        }
                        if (image != null)
                        {
                            int cw = image.Width / 2;
                            int ch = image.Height / 2;
                            double cx, cy;
                            image.GetTransform(".", "@").MapPoint(cw, ch, out cx, out cy);
                            CogPointMarker cross = new CogPointMarker();
                            cross.Color = CogColorConstants.Green;
                            cross.SetCenterRotationSize(cx, cy, 0, 2500);
                            crd.Image = image;
                            crd.StaticGraphics.Clear();
                            crd.StaticGraphics.Add(cross, "cross");
                        }
                    }
                }
                else
                {
                    crd.StaticGraphics.Remove("cross");
                }

            }
            catch
            {

            }
        }
        public static void DrawCrosscogDisplay(int width, int height, ref CogRecordDisplay crd, bool isAdd)
        {
            try
            {
                if (isAdd)
                {
                    if (crd != null)
                    {
                        ICogImage image = crd.Image;

                        if (image == null)
                        {
                            image = new CogImage8Grey(width, height);
                        }
                        if (image != null)
                        {
                            int cw = image.Width / 2;
                            int ch = image.Height / 2;
                            double cx, cy;
                            image.GetTransform(".", "@").MapPoint(cw, ch, out cx, out cy);
                            CogPointMarker cross = new CogPointMarker();
                            cross.Color = CogColorConstants.Green;
                            cross.SetCenterRotationSize(cx, cy, 0, 2500);
                            crd.Image = image;
                            crd.StaticGraphics.Clear();
                            crd.StaticGraphics.Add(cross, "cross");
                        }
                    }
                }
                else
                {
                    crd.StaticGraphics.Remove("cross");
                }

            }
            catch
            {

            }
        }
        Bitmap SaveBitmap = null;
        private void btnTriggerExec_Click(object sender, EventArgs e)
        {
            bool cross_disp = true;
            Bitmap bt = null;
            int ExPose = 1000;
            try
            {
                this.Invoke(new Action(
                        () =>
                        {

                            if (cbDeviceList.SelectedIndex == 0)
                            {
                                if (cmbExposeSelect.SelectedIndex == 0)
                                {
                                    ExPose = Globals.SettingOption.UpCamExpose1;
                                }
                                if (cmbExposeSelect.SelectedIndex == 1)
                                {
                                    ExPose = Globals.SettingOption.UpCamExpose2;
                                }
                                Globals.Camera.Capture_New(ExPose, out bt);
                                //Globals.LogRecord("【曝光值：" + ExPose + "】", true);
                            }
                            if (cbDeviceList.SelectedIndex == 1)
                            {
                                if (cmbExposeSelect.SelectedIndex == 0)
                                {
                                    ExPose = Globals.SettingOption.DownCamExpose1;
                                }
                                if (cmbExposeSelect.SelectedIndex == 1)
                                {
                                    ExPose = Globals.SettingOption.DownCamExpose2;
                                }
                                Globals.CameraDown.Capture_New(ExPose, out bt);
                            }

                            if (chbCross.Checked)
                            {
                                cross_disp = true;
                            }
                            else
                            {
                                cross_disp = false;
                            }

                        }
                ));
                ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                DrawCrosscogDisplay(2500, 2000, ref cogDisplay1, cross_disp);
                DrawCrosscogDisplay(2500, 2000, ref cogRecordDisplay1, cross_disp);
                this.cogRecordDisplay1.Record = null;
                this.cogRecordDisplay1.Image = null;
                this.cogRecordDisplay1.Image = MyOutPutImage;
                this.cogRecordDisplay1.Fit();
                this.cogRecordDisplay1.Visible = false;
                this.cogDisplay1.Image = MyOutPutImage;
                this.cogDisplay1.Fit();

            }
            catch (Exception exception)
            {
                Globals.LogRecord("【相机打开失败！】", true);
            }
        }
        private static readonly object lockCam = new object();
        bool IsContinueAcq = false;
        public void Run()
        {
            while (true)
            {
                lock (lockCam)
                {
                    if (IsContinueAcq)
                    {
                        Thread.Sleep(1);
                        btnTriggerExec_Click(null, null);
                        Thread.Sleep(1);
                        Application.DoEvents();
                    }
                }
            }

        }
        private void btnContinueAcq_Click(object sender, EventArgs e)
        {
            if (!IsContinueAcq)
            {
                btnContinueAcq.Text = "连续采集";
                IsContinueAcq = true;
            }
            else
            {
                btnContinueAcq.Text = "停止采集";
                IsContinueAcq = false;
            }
        }
        private void FrmCam_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsContinueAcq = false;
            Thread.Sleep(500);
            btnTriggerExec_Click(null, null);
        }
        private void cbDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDeviceList.SelectedIndex == 0)
            {
                intupdown1.Value = Globals.SettingOption.UpCamExpose1;
                intupdown2.Value = Globals.SettingOption.UpCamExpose2;
            }
            if (cbDeviceList.SelectedIndex == 1)
            {
                intupdown1.Value = Globals.SettingOption.DownCamExpose1;
                intupdown2.Value = Globals.SettingOption.DownCamExpose2;
            }
        }

        public void SaveExPose()
        {


            if (cbDeviceList.SelectedIndex == 0)
            {
                Globals.SettingOption.UpCamExpose1 = intupdown1.Value;
                Globals.SettingOption.UpCamExpose2 = intupdown2.Value;
            }
            if (cbDeviceList.SelectedIndex == 1)
            {
                Globals.SettingOption.DownCamExpose1 = intupdown1.Value;
                Globals.SettingOption.DownCamExpose2 = intupdown2.Value;
            }
            JsonHelper.Instance.ObjectToJsonFile(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json", Globals.SettingOption);
            Globals.LogRecord("【曝光保存成功！】");
            ShowInfoDialog("曝光参数保存", "曝光参数保存成功！", UIStyle.Green);
        }
        private void btnSetExpose_Click(object sender, EventArgs e)
        {
            SaveExPose();
            if (File.Exists(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json"))
            {
                Globals.SettingOption = (OptionSetting)JsonHelper.Instance.JsonFileToObject(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json", Globals.SettingOption);
            }
        }

        private void btnSetExpose2_Click(object sender, EventArgs e)
        {
            SaveExPose();
        }

        private CogImageFileTool mImageFileTool = new CogImageFileTool();
        private string a = null;
        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            bool cross_disp = false;

            if (chbCross.Checked)
            {
                cross_disp = true;
            }
            else
            {
                cross_disp = false;
            }

            string logPath1 = System.Environment.CurrentDirectory.ToString();///*获取软件运行时的路径*/ + @"\log\log1\";//如果需要设置特定的路径logPath1=@"C:\Program Files\";
            //string path = @"E:\visionPro\新建文件夹 (4)";
            //Process.Start(path);
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "打开文件";
            openfile.Filter = "所有文件(*.*)|*.*";
            openfile.InitialDirectory = logPath1;//设置打开路径的目录
            openfile.ShowDialog();
            a = openfile.FileName;
            if (a != "")
            {
                try
                {
                    mImageFileTool.Operator.Open(a, CogImageFileModeConstants.Read);
                    mImageFileTool.Run();
                    this.cogRecordDisplay1.Image = mImageFileTool.OutputImage;
                    this.cogDisplay1.Image = mImageFileTool.OutputImage;
                    this.cogRecordDisplay1.Fit();
                    DrawCrosscogDisplay(2500, 2000, ref cogDisplay1, cross_disp);

                }
                catch (Exception exception)
                {
                    return;
                }
            }
            else
            {
                return;
            }
            DrawCrosscogDisplay(2500, 2000, ref cogRecordDisplay1, cross_disp);
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(ConfigureFilePath.DebugStationSavePath))
            {
                System.IO.Directory.CreateDirectory(ConfigureFilePath.DebugStationSavePath);
            }
            string fileName = ConfigureFilePath.DebugStationSavePath + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".bmp";
            this.cogRecordDisplay1.Image.ToBitmap().Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        public static double ModleX = 0, ModleY = 0, ModleR = 0;
        private void btnChangeRunProj_Click(object sender, EventArgs e)
        {
            string value;
            ICogImage MyOutPutImage = null;
            Bitmap bt = null;
            int ExPose = 2000;
            bool cross_disp = true;
            foreach (Control ctr in gpbCamRes.Controls)
            {
                if (ctr is RadioButton && (ctr as RadioButton).Checked)
                {
                    value = ctr.Text;
                }
            }
            if (rdbCam.Checked)
            {
                if (cbDeviceList.SelectedIndex == 0)
                {
                    if (cmbExposeSelect.SelectedIndex == 0)
                    {
                        ExPose = Globals.SettingOption.UpCamExpose1;
                    }
                    if (cmbExposeSelect.SelectedIndex == 1)
                    {
                        ExPose = Globals.SettingOption.UpCamExpose2;
                    }
                    Globals.Camera.Capture_New(ExPose, out bt);
                }
                if (cbDeviceList.SelectedIndex == 1)
                {
                    if (cmbExposeSelect.SelectedIndex == 0)
                    {
                        ExPose = Globals.SettingOption.DownCamExpose1;
                    }
                    if (cmbExposeSelect.SelectedIndex == 1)
                    {
                        ExPose = Globals.SettingOption.DownCamExpose2;
                    }
                    Globals.CameraDown.Capture_New(ExPose, out bt);
                }

                try
                {
                    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                }
                catch (Exception exception)
                {
                    ShowErrorDialog("相机打开失败！");
                    Globals.LogRecord("【相机打开失败！】");
                    return;
                }
            }
            else
            {
                if (a != null)
                {
                    mImageFileTool.Operator.Open(a, CogImageFileModeConstants.Read);
                    mImageFileTool.Run();
                    MyOutPutImage = mImageFileTool.OutputImage;
                }
                else
                {
                    ShowErrorDialog("请选择一个图片文件！");
                    Globals.LogRecord("【图片文件未选择！】");
                    return;
                }

            }
            #region MyRegionRunVPP
            CogToolBlock tbSelected = MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()];
            //Globals.tbSelect = tbSelected;
            try
            {
                //Globals.tbSelect.Inputs["Input1"].Value = MyOutPutImage;
                //Globals.tbSelect.Run();
                MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].Inputs["Input1"].Value = MyOutPutImage;
                MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].Run();

                double valuex = Math.Round((double)MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].Outputs["X"].Value, 4);
                double valuey = Math.Round((double)MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].Outputs["Y"].Value, 4);
                double valuer = Math.Round((double)MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].Outputs["R"].Value, 4);
                bool valuere = (bool)MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].Outputs["Re"].Value;
                ModleX = valuex;
                ModleY = valuey;
                ModleR = valuer;
                txbModleX.Text = ModleX.ToString();
                txbModleY.Text = ModleY.ToString();
                txbModleR.Text = ModleR.ToString();
                if (valuere)
                {
                    //ShowSuccessDialog("视觉工程运行成功！");
                    //Globals.LogRecord("【视觉工程运行成功！】");
                }
                else
                {
                    //ShowErrorDialog("视觉工程运行失败！");
                    //Globals.LogRecord("【视觉工程运行失败！】");
                }
                Globals.LogRecord("【X：  " + txbModleX.Text + "    Y：" + txbModleY.Text + "   R： " + txbModleR.Text + "   】");


            }
            catch (Exception exception)
            {
                //ShowErrorDialog("视觉工程运行失败！");
                //Globals.LogRecord("【视觉工程运行失败！】");
            }
            //if (Globals.tbSelect.RunStatus.Result == CogToolResultConstants.Accept)
            //{

            //}
            //if (Globals.tbSelect.CreateLastRunRecord().SubRecords.Count > 0)
            this.cogRecordDisplay1.Visible = true;
            if (MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].CreateLastRunRecord().SubRecords.Count > 0)
            {
                //ICogRecord DispalyRecord1 = Globals.tbSelect.CreateLastRunRecord().SubRecords[0];
                //ICogRecord DispalyRecord = Globals.tbSelect.CreateLastRunRecord().SubRecords[Globals.tbSelect.CreateLastRunRecord().SubRecords.Count - 1];

                ICogRecord DispalyRecord1 = MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].CreateLastRunRecord().SubRecords[0];
                ICogRecord DispalyRecord = MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].CreateLastRunRecord().SubRecords[MyVisionProMgr.ins.CogToolBlockDic[cmbChangeProj.SelectedItem.ToString()].CreateLastRunRecord().SubRecords.Count - 1];


                if (rdbCam.Checked)
                {
                    Globals.SaveScreenImageInputImage(bt, ConfigureFilePath.Path_Image);
                }
                Globals.DispalyRecord(DispalyRecord1);
                Globals.DispalyRecord(DispalyRecord);
                Globals.DispalyRecordNum(1, DispalyRecord);
                Globals.DispalyRecordNum(2, DispalyRecord);
                Globals.DispalyRecordNum(3, DispalyRecord);
                Globals.DispalyRecordNum(4, DispalyRecord);

                this.cogRecordDisplay1.Record = DispalyRecord1;
                if (rdbCam.Checked)
                {
                    Globals.SaveScreenImage(cogRecordDisplay1, ConfigureFilePath.Path_Image);
                }
                this.cogRecordDisplay1.Record = DispalyRecord;
                if (rdbCam.Checked)
                {
                    Globals.SaveScreenImage(cogRecordDisplay1, ConfigureFilePath.Path_Image);
                }

            }
            this.cogDisplay1.Image = MyOutPutImage;
            if (chbCross.Checked)
            {
                cross_disp = true;
            }
            else
            {
                cross_disp = false;
            }
            DrawCrosscogDisplay(2500, 2000, ref cogDisplay1, cross_disp);
            DrawCrosscogDisplay(2500, 2000, ref cogRecordDisplay1, cross_disp);
            this.cogRecordDisplay1.Fit();
            string b = cmbChangeProj.SelectedItem.ToString();
            //ShowSuccessDialog(b + "视觉工程切换成功！");
            //Globals.LogRecord("【视觉工程" + b + "切换成功！】");
            #endregion
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            VisionProHelper.Form_MyVision form_MyVision = new VisionProHelper.Form_MyVision();
            form_MyVision.Show();
            Globals.LogRecord("【视觉工程打开完成！】", true);
        }

        private void btnUpCamModle_Click(object sender, EventArgs e)
        {
            Globals.LoadVisionParam();

            switch (chbUpCamPhotoPos.SelectedIndex)
            {
                case 0:
                    Globals.store.Value.UpCamFhotoPos1[0] = ModleX;
                    Globals.store.Value.UpCamFhotoPos1[1] = ModleY;
                    Globals.store.Value.UpCamFhotoPos1[2] = ModleR;
                    break;
                case 1:
                    Globals.store.Value.UpCamFhotoPos2[0] = ModleX;
                    Globals.store.Value.UpCamFhotoPos2[1] = ModleY;
                    Globals.store.Value.UpCamFhotoPos2[2] = ModleR;
                    break;

                case 2:
                    Globals.store.Value.UpCamFhotoPos3[0] = ModleX;
                    Globals.store.Value.UpCamFhotoPos3[1] = ModleY;
                    Globals.store.Value.UpCamFhotoPos3[2] = ModleR;
                    break;
                case 3:
                    Globals.store.Value.UpCamFhotoPos4[0] = ModleX;
                    Globals.store.Value.UpCamFhotoPos4[1] = ModleY;
                    Globals.store.Value.UpCamFhotoPos4[2] = ModleR;
                    break;
            }
            Globals.store.Save();
            ShowSuccessDialog("视觉模板参数数据保存成功！");
        }

        private void btnDownCamModle_Click(object sender, EventArgs e)
        {
            Globals.LoadVisionParam();
            Globals.store.Value.DownCamModel[0] = ModleX;
            Globals.store.Value.DownCamModel[1] = ModleX;
            Globals.store.Value.DownCamModel[2] = ModleX;
            Globals.store.Save();
            ShowSuccessDialog("视觉模板参数数据保存成功！");

        }
    }
}
