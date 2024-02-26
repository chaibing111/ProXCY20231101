using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
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
using Sunny.UI;
using Cognex.VisionPro.ImageFile;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading;
using Cognex.VisionPro.FGGigE.Implementation.Internal;
using Cognex.VisionPro.OCRMax.Implementation.Internal;
using Cognex.VisionPro.PMAlign;
using HalconDotNet;
using VisionProHelper;
using HslCommunication.ModBus;
using HslCommunication.Profinet;
using HslCommunication;
using HslCommunication.Profinet.Omron;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using TouchSocket.Sockets;
using TouchSocket.Core.ByteManager;
using EventMgrLib;
using MyMelsecHelper;
namespace sunyvpp
{
    public partial class MainForm : UIForm
    {
        public static readonly Lazy<MainForm> instance = new Lazy<MainForm>(() => new MainForm());
        public static MainForm Instance { get; } = instance.Value;
        private string path = AppDomain.CurrentDomain.BaseDirectory + "ToolBlock.vpp";
        private CogAcqFifoTool acq;
        public CogImage8Grey Image1 = new CogImage8Grey();
        AutoSizeFormClass asc = new AutoSizeFormClass();//实例化自动适应窗体类
        public MainForm()
        {
            InitializeComponent();
        }
        public static CogToolBlock tb;
        public static void LoadToolBlock()
        {
            VisionProHelper.MyVisionProMgr.ins.VisionItemUpdata();
            //comboBox_vppName.Items.Clear();
            foreach (string keys in VisionProHelper.MyVisionProMgr.ins.VisionItemDic.Keys)
            {
                Globals.ProjName.Add(keys);
                Globals.ProjNamePath.Add(MyVisionProMgr.ins.ProjectBasePath + keys);
            }

            if (Globals.ProjName.Count > 0)
            {

                tb = MyVisionProMgr.ins.CogToolBlockDic[Globals.ProjName[0]];
                Globals.tbSelect = MyVisionProMgr.ins.CogToolBlockDic[Globals.ProjName[0]];
                foreach (var item in Globals.ProjName)
                {
                    Globals.LogRecord("【视觉工具:" + item + "加载完成！】", true);
                }
                Globals.AutoToolblock = MyVisionProMgr.ins.CogToolBlockDic["TB_Down.vpp"];
                Globals.UpCameraInspectToolblock = MyVisionProMgr.ins.CogToolBlockDic["TB_Inspect.vpp"];

                Globals.UpCameraInsFlowToolblock = MyVisionProMgr.ins.CogToolBlockDic["InsFlow.vpp"];
                Globals.UpCameraInsMonitorToolblock = MyVisionProMgr.ins.CogToolBlockDic["InsMonitor.vpp"];

                Globals.UpCameraAssemToolblock = MyVisionProMgr.ins.CogToolBlockDic["TB_UP.vpp"];
                Globals.UpCameraAssemToolblockNew = MyVisionProMgr.ins.CogToolBlockDic["TB_UP2.vpp"];
                Globals.CalibToolblock = MyVisionProMgr.ins.CogToolBlockDic["TB_Calib.vpp"];
                Globals.CalibRotToolblock = MyVisionProMgr.ins.CogToolBlockDic["TB_CalibRot.vpp"];
                
                Globals.LogRecord("【视觉加载完成！】", true);
            }
            else
            {
                Globals.LogRecord("【视觉工具:" + "加载失败！】", true);
            }
        }
        #region 保存图片
        public ICogImage[] Image = new ICogImage[2];
        public static string ImageSavePath = Application.StartupPath + "\\ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + "OK" + "\\";
        public static string ImageSavePathNG = Application.StartupPath + "\\ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\" + "NG" + "\\";
        public static string ImageSavePathDelet = Application.StartupPath + "\\ResultImage\\";
        public void PictureSave(string Name)
        {
            if (!Directory.Exists(ImageSavePath))
            {
                Directory.CreateDirectory(ImageSavePath);
                Image[0].ToBitmap().Save(ImageSavePath + Name + DateTime.Now.ToString("yyyyMMddhhmmss") + ".Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                Image[0].ToBitmap().Save(ImageSavePath + Name + DateTime.Now.ToString("yyyyMMddhhmmss") + ".Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        #endregion
        Thread AutoRunThread;
        Thread AutoRunPLC1StateThread;

        Thread RobotRunThread;

        private void Form1_Load(object sender, EventArgs e)
        {
            Globals.LogInOK = true;
            //Frm_Welcome.Instance.lbl_companyName.Text = "HERO-LASER";
            Frm_Welcome.Instance.Show();
            System.Windows.Forms.Application.DoEvents();
            Frm_Welcome.Instance.bar_step.Value = 30;
            Globals.MachineStart = true;
            m_AcqFito = new CogAcqFifoTool();
            CsvServer.Instance.Start();
            m_ImageFileTool = new CogImageFileTool();
            Globals.inforShowEvent += ShowOnlstRunLog;

            Globals.inforShowState += ShowOnState;
            Globals.inforShowRecod += ShowRecodDisplay;
            Globals.inforShowActionState += ShowActionState;
            Globals.inforShowActionDisp += ShowActionDisp;

            Frm_Welcome.Instance.bar_step.Value = 40;
            //开启运行线程
            Globals.ProductOpt("LoadPath");
            MyVisionProMgr.ins.ProjectBasePath = ConfigureFilePath.ImageTaskPath + "\\";// ConfigureFilePath.projectItemPath;
            Globals.LoadVisionParam();
            LoadToolBlock();
            MainForm.Instance.Text = "主窗口" + ConfigureFilePath.projectItemPath;
            Frm_Welcome.Instance.bar_step.Value = 100;
            Globals.loadingProcess_Var = true;
            asc.controllInitializeSize(this);
            try
            {
                Globals.Initialglb();
            }
            catch (Exception exception)
            {
            }
            InitializeDevice();
            
            uiTabControl1.Visible = true;
            Frm_Welcome.Instance.Hide();

            EventMgr.Ins.GetEvent<Yeild>().Subscribe(ShowYeild);
            EventMgr.Ins.GetEvent<YeildResault>().Subscribe(ShowYeildReSault);
            uiTabControl_Ribbon.SelectedIndex = 2;
            #region 订阅事件
            //EventMgr.Ins.GetEvent<GlobalControlEvent>().Subscribe(GlobalControl);
            //Task1.LoadTask1();
            #endregion

            AutoRunThread = new Thread(Task1.Instance.RunReadRobot);
            AutoRunThread.IsBackground = true;
            AutoRunThread.Start();
            TCPInitial();
            AutoRunPLC1StateThread = new Thread(Robot);
            AutoRunPLC1StateThread.IsBackground = true;
            AutoRunPLC1StateThread.Start();

            //RobotRunThread = new Thread(Task1.Instance.RunPLC1State);
            //RobotRunThread.IsBackground = true;
            //RobotRunThread.Start();

            Globals.TaskStep = 10;
            MyMelsecHelper.HslUtils.formMelsecBinary.FormClosing += Form1_FormClosing;
           
            RS232Initial();
            Globals.LogPlcState("PLC1", Color.Yellow);
            Globals.LogPlcState("PLC2", Color.Yellow);
            Globals.InitialMelnetPLC();
            Globals.State = Controller.Stop;
        }

        public static bool RunServerFalg = true;
        private void Robot()
        {
            while (true)
            {
                try
                {
                    string[] strip = Globals.myTCPServer.GetIDs();

                    if (strip.Length != 0)
                    {
                        for (int i = 0; i < strip.Length; i++)
                        {

                            if (Globals.myTCPServer.TcpService.TryGetSocketClient(strip[i], out SocketClient socketClient))
                            {
                                if (RunServerFalg != true)
                                {
                                    Globals.LogRecord("【客户端:" + strip[i] + ":" + socketClient.IP + ":" + socketClient.Port + "连接成功！】", true);
                                    Globals.LogPlcState("TCPS", Color.Lime);
                                    RunServerFalg = true;
                                }
                                Globals.stripClient = strip[0];

                                //Globals.RobotStrName = "abc";
                                //Globals.RobotStrValue = "1";

                                //string B = Globals.SetRobot("GlobalStep", "30", "100");
                                //Globals.myTCPServer.SendStr(Globals.stripClient, B);
                            }
                        }
                    }
                    else
                    {
                        if (RunServerFalg != false)
                        {
                            Globals.LogRecord("【客户端连接失败！:" + "】", true);

                            Globals.LogPlcState("TCPS", Color.Red);
                            RunServerFalg = false;
                        }

                    }
                    Thread.Sleep(200);
                }
                catch (Exception e)
                {
                    if (RunServerFalg != false)
                    {
                        Globals.LogRecord("【客户端连接失败！" + e.ToString() + "】", true);
                        Globals.LogPlcState("TCPS", Color.Red);
                        RunServerFalg = false;
                    }

                }



            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
        /*上位机刷PLC状态
         *机器人走通讯，机器人主动
         *
         *
         */
        public static bool AutoRunFlagStation1 = true;
        //PLC,机器人复位完成信号。刷新PLC状态
        public static bool PLC_ResetDoneFlag = true;
        public static bool Robot_ResetDoneFlag = true;

        public void TCPInitial()
        {
            //Globals.MyTcp = new MyTcpHelper(Globals.SettingOption.RobotIPAddress, Globals.SettingOption.RobotTCPPort, 10);
            //Globals.MyTcp.ConnectServer();
            //Globals.MyTcp.tcpClient.Received =Task1.ReceivedHandler1;

            Globals.myTCPServer = new MyTCPServer();
            Globals.myTCPServer.InitMyTCPService(Globals.SettingOption.RobotTCPPort);
            Globals.myTCPServer.Start();

            EventMgr.Ins.GetEvent<CommunicationEventRev>().Subscribe(Task1.ReceiveCommunicationEventRev);
        }
        public void RS232Initial()
        {
            try
            {
                Globals.myLaserSerialPort.SerialPortName = Globals.SettingOption.LaserPortName;
                Globals.myLaserSerialPort.BaudRate = Globals.SettingOption.LaserBaudRate;

                Globals.myLaserSerialPort.DataBits = 8;//设置数据位
                Globals.myLaserSerialPort.MyStopBits = (StopBits)int.Parse("1");//设置停止位
                Globals.myLaserSerialPort.MyParity = Parity.None;//奇偶校验
                if (Globals.myLaserSerialPort.InitMySerialPort())
                {
                    //Globals.myLaserSerialPort.Open();//打开串口
                    Globals.myLaserSerialPort.inforShowEvent += Task1.ShowReceiveMessage;
                    EventMgr.Ins.GetEvent<CommunicationEventSerialRev>().Subscribe(Task1.ReceiveCommunicationSerialEventRev);
                    Globals.LogRecord("【串口初始化成功！】", true);
                    ShowSuccessDialog("串口打开成功！");
                    Globals.LogRecord("【串口打开成功！】", false);
                }
                else
                {
                    ShowErrorDialog("【串口打开失败！】");
                    Globals.LogRecord("【串口打开失败！】", true);
                }
            }
            catch (Exception e)
            {
                Globals.LogRecord("【串口初始化错误！】", true);
            }
        }

        public void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)//定义一个接受数据的函数
        {
            ReceivePowerRS232 = "";
            //将byte数据转换成字符串窗口显示
            int count = Globals.sp.BytesToRead;
            byte[] RecieveBuf = new byte[count];

            Globals.sp.Read(RecieveBuf, 0, count);

            //直接转换成字符串形式
            if (true)
            {
                string strRecieve3 = System.Text.Encoding.Default.GetString(RecieveBuf);
                Invoke(new MethodInvoker(delegate ()
                {
                    ReceivePowerRS232 += strRecieve3;

                    Globals.LogRecord("【串口接收数据" + ReceivePowerRS232 + "】", true);
                    //MessageBox.Show(ReceivePowerRS232);
                }));
            }
            //转换成16进制字符串
            //if (radioButton2.Checked)
            //{

            //    Invoke(new MethodInvoker(delegate ()
            //    {
            //        txtRecieve1.Text += byteToHexStr(RecieveBuf);
            //    }));
            //}
        }

        private void ProductMgr(object sender, EventArgs e)
        {
            var button = sender as UIHeaderButton;
            if (button == null) return;
            if (button == btnProductNew)
            {
                FrmProCreate.Instance(ConfigureFilePath.Path_Config_Pro).ShowDialog();
            }
            if (button == btnProductSaveAs)
            {
                FrmProSave.Instance(ConfigureFilePath.projectItemPath).ShowDialog();

            }
            if (button == btnProductOpen)
            {
                try
                {
                    FrmProOpen.Instance(ConfigureFilePath.Path_Config_Pro).ShowDialog();
                }
                catch (Exception exception)
                {
                    ShowErrorDialog("工程目录及项目文件夹不存在,打开失败，请检查");
                    Globals.LogRecord("【工程目录及项目文件夹不存在,打开失败，请检查！】", true);
                }

            }
            //发布事件
            EventMgr.Ins.GetEvent<ProductEvent>().Publish(ConfigureFilePath.Path_Config_ProFile);
            MainForm.Instance.Text = Globals.MainFormCaptain;
        }


        private void GlobalControl(object sender, EventArgs e)
        {
            DeleteFile(ConfigureFilePath.Dir_Record_TaskLog, Globals.SettingOption.LogSaveDay);
            DeleteFile(ConfigureFilePath.PictruePath, Globals.SettingOption.ImageSaveDay);
            //Globals.MySetRobotOffsetX("999");
            //Globals.MySetRobotOffsetY("999");
            //Globals.MySetRobotOffsetR("999");
            OperateResult<bool[]> alarm =
                Globals.melsec_net.ReadBool("M1201", 5);
            OperateResult<bool> alarm1 =
                Globals.melsec_net.ReadBool("M1201");

            Globals.melsec_net.Write("M6", true);
            OperateResult<bool> alarm3 = Globals.melsec_net.ReadBool("M6");


            Task1.PickAssembNum = 1;
            Globals.TaskStep = 10;
            //bool any = Globals.myTCPServer.ids.Any(x => string.IsNullOrEmpty(x));
            //string c = Globals.myTCPServer.ids[0];
            //if (!any)
            //{
            //    string aaa=Globals.myTCPServer.ids[0];
            //}
            var button = sender as UIHeaderButton;
            if (button == null) return;
            Controller controller = Controller.none;
            if (button == btnHome)
            {
                Globals.State = Controller.Home;
            }
            if (button == btnStart)
            {
                Globals.State = Controller.Start;
            }
            if (button == btnStop)
            {
                Globals.State = Controller.Stop;
            }
            EventMgr.Ins.GetEvent<GlobalControlEvent>().Publish(Globals.State);
        }
        public void InitializeDevice()
        {
            try
            {
                Globals.Camera.GetCameraSN(out Globals.cameraSNs);
                if (Globals.cameraSNs.Count > 0)
                {
                    string rev = Globals.cameraSNs[0];
                    string rev1 = Globals.cameraSNs[1];
                    if (false)
                    {
                        //硬触发
                        Globals.Camera.OpenDevice(rev, true);
                        Globals.Camera.ReadImageEvent += Camera_ReadImageEvent;
                        Globals.Camera.SetTriggerSource(0);
                        Globals.Camera.SetTrigerMode(true);
                        Globals.Camera.StartGrab();
                    }
                    else
                    {
                        Globals.Camera.OpenDevice(rev);
                        Globals.Camera.SetTriggerSource(7);
                        Globals.Camera.SetTrigerMode(true);
                        Globals.Camera.StartGrab();
                        //下相机
                        Globals.CameraDown.OpenDevice(rev1);
                        Globals.CameraDown.SetTriggerSource(7);
                        Globals.CameraDown.SetTrigerMode(true);
                        Globals.CameraDown.StartGrab();
                    }
                    Globals.LogRecord("【相机初始化成功！】", true);
                }
                else
                {
                    Globals.LogRecord("【相机打开失败！】");
                    Globals.LogRecord("【相机初始化失败！】", true);
                }

            }
            catch (Exception exception)
            {
                Globals.LogRecord("【相机初始化失败！】", true); ;
            }
        }

        private Queue<Bitmap> IamgeQueue = new Queue<Bitmap>();
        private Queue<Bitmap> IamgeSaveQueue = new Queue<Bitmap>();
        private Queue<string> BarCodeQueue = new Queue<string>();
        //private Queue<HObject> IamgeQueue = new Queue<HObject>();
        /// <summary>
        /// 相机回调接受事件
        /// </summary>
        /// <param name="ho_image"></param>
        private void Camera_ReadImageEvent(Bitmap ho_image)
        {
            IamgeQueue.Enqueue(ho_image);
        }

        /// <summary>
        /// 运行日志显示到界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOnlstRunLog(object sender, string e)
        {
            //if (this.IsHandleCreated)  IntPtr i = this.Handle
            try
            {
                this.Invoke(new Action(() =>
                {
                    if (lstRunLog.Items != null && lstRunLog.Items.Count > 0 &&
                        lstRunLog.Items[lstRunLog.Items.Count - 1].ToString().Contains(e)) return;
                    lstRunLog.Items.Add(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "  " + e);
                    lstRunLog.SelectedIndex = lstRunLog.Items.Count - 1;
                    if (lstRunLog.Items != null && lstRunLog.Items.Count > 50)
                    {
                        lstRunLog.Items.Clear();
                    }
                }));
            }
            catch (Exception exception)
            {
                ;
            }
        }
        private void ShowYeildReSault(bool _yeildReSault)
        {
            //if (this.IsHandleCreated)  IntPtr i = this.Handle
            try
            {
                if (_yeildReSault)
                {
                    this.Invoke(new Action(() =>
                    {
                        LabReSaultDIp.Text = "OK";
                        LabReSaultDIp.BackColor = Color.Lime;
                        labProDuctNum.Text = (Globals.varProDuctNumOK+ Globals.varProDuctNumNG).ToString();
                        //labProDuctNumOK.Text = Globals.varProDuctNumOK.ToString();
                        //Globals.varProDuctNum += 1;
                        //Globals.varProDuctNumNG += 1;
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        labProDuctNumNG.Text = Globals.varProDuctNumNG.ToString();
                        labProDuctNum.Text = (Globals.varProDuctNumOK + Globals.varProDuctNumNG).ToString();
                        LabReSaultDIp.Text = "NG";
                        LabReSaultDIp.BackColor = Color.Red;
                    }));
                }
            }
            catch (Exception exception)
            {
                ;
            }
        }
        private void ShowYeild(int _yeild)
        {
            //if (this.IsHandleCreated)  IntPtr i = this.Handle
            try
            {
                this.Invoke(new Action(() =>
                {
                    labProDuctNumOK.Text = _yeild.ToString();
                    labProDuctNum.Text = (Globals.varProDuctNumOK + Globals.varProDuctNumNG).ToString();
                }));
            }
            catch (Exception exception)
            {
                ;
            }
        }
        private void ShowOnState(object sender, Color e)
        {
            //IntPtr i = this.Handle;
            this.Invoke(new Action(() =>
            {
                lampPLC1.Color = e;
            }));
        }

        private void ShowActionState(string sender, Color e)
        {
            if (sender == "PLC1")
            {
                //IntPtr i = this.Handle;
                this.Invoke(new Action(() =>
                {
                    lampPLC1.Color = e;
                }));
            }
            if (sender == "TCPS")
            {
                //IntPtr i = this.Handle;
                this.Invoke(new Action(() =>
                {
                    lampPLC2.Color = e;
                }));
            }
        }
        private void ShowActionDisp(int PicNum, ICogRecord display)
        {
            switch (PicNum)
            {
                case 1:
                    this.Invoke(new Action(() =>
                    {
                        cogRecordDisplay1.Record = display;
                        cogRecordDisplay1.Fit();
                    }));
                    break;
                case 2:
                    this.Invoke(new Action(() =>
                    {
                        dsp2.Record = display;
                        dsp2.Fit();
                    }));
                    break;
                case 3:
                    this.Invoke(new Action(() =>
                    {
                        dsp3.Record = display;
                        dsp3.Fit();
                    }));
                    break;

                case 4:
                    this.Invoke(new Action(() =>
                    {
                        dsp4.Record = display;
                        dsp4.Fit();
                    }));
                    break;
            }



        }


        private void ShowRecodDisplay(object sender, ICogRecord display)
        {
            this.Invoke(new Action(() =>
            {
                cogRecordDisplay1.Record = display;
                cogRecordDisplay1.Fit();
            }));
        }


        public ICogImage MyOutPutImage;
        CogImage8Grey inputimage;
        private bool cross_disp;
        CogImageConvertTool CogImageConvertTool1;
        private CogImageFileTool m_ImageFileTool1;// m_ImageFileTool1;
        private CogImageFileTool m_ImageFileTool;
        public int calibnum = 0;
     
        /// <summary>
        /// 保存界面截图
        /// </summary>
        /// <param name="display"></param>
        /// <param name="path"></param>
        private void SaveScreenImage(CogRecordDisplay display, string path) //保存界面截图jpeg
        {
            try
            {
                //  path = path + "\\" + "Camera-" + index.ToString();
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                string filename = "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + "_" + ".jpg";
                Bitmap bmp = display.CreateContentBitmap(CogDisplayContentBitmapConstants.Image) as Bitmap;
                // Bitmap bmp = display.Image.ToBitmap();
                bmp.Save(path + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存处理图像出现错误，信息为" + ex.Message);
            }
        }

        private void SaveScreenImageInputImage(Bitmap bmp, string path) //保存界面截图jpeg
        {
            try
            {
                //  path = path + "\\" + "Camera-" + index.ToString();
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                string filename = "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + "_" + ".jpg";
                //Bitmap bmp = display.CreateContentBitmap(CogDisplayContentBitmapConstants.Image) as Bitmap;
                // Bitmap bmp = display.Image.ToBitmap();
                bmp.Save(path + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存处理图像出现错误，信息为" + ex.Message);
            }
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.MachineStart = true;

            Globals.Camera.StopGrab();
            Globals.Camera.CloseDevice();

            Globals.CameraDown.StopGrab();
            Globals.CameraDown.CloseDevice();
            //AutoRunThread.Abort();

            //if (!this.IsHandleCreated)
            //{
            //    this.CreateHandle();
            //}

            Environment.Exit(0);
        }


        public CogCalibNPointToNPointTool Calib_tbCamCalib;

        public CogCalibNPointToNPointTool Calib_tbSideUPACamCalib;

        #region 偏移标定
        public static double[] Calib_X = new double[9];
        public static double[] Calib_Y = new double[9];
        #endregion

        /// <summary>
        /// CogRecordDisplay控件画十字线
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="crd"></param>
        public static void DrawCross(int width, int height, ref CogRecordDisplay crd, bool isAdd)
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

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            this.uiTabControl_Ribbon.Height = 40;
            // 3.为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应
            asc.controlAutoSize(this);
        }
        public static CogAcqFifoTool m_AcqFito;


        private static bool liveImage(CogAcqFifoTool cogAcqFifoTool, CogRecordDisplay crd)
        {

            if (cogAcqFifoTool == null) return true;
            CogFrameGrabberGigE cogFrameGrabberGigE = cogAcqFifoTool.Operator.FrameGrabber as CogFrameGrabberGigE;
            if (cogFrameGrabberGigE == null) return true;
            crd.StartLiveDisplay(cogAcqFifoTool.Operator, true);
            return true;
        }
        public void LiveImage()
        {
            cogRecordDisplay1.StaticGraphics.Clear();
            cogRecordDisplay1.Record = null;
            if (sybDisp_Act.Tag.ToString() == "0")
            {
                sybDisp_Act.Tag = "1";
                liveImage(m_AcqFito, cogRecordDisplay1);
            }
            else
            {
                sybDisp_Act.Tag = "0";
                cogRecordDisplay1.StopLiveDisplay();
            }
        }
        private HDevelopExport b = new HDevelopExport();
        string pathHalcon = ImageSavePath + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bmp";

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(ImageSavePath))
            {
                Directory.CreateDirectory(ImageSavePath);
            }

            b.SaveImage(pathHalcon);
        }

        private void btnSaveImageHal_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(ImageSavePath))
            {
                Directory.CreateDirectory(ImageSavePath);
            }
            pathHalcon = ImageSavePath + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bmp";
            b.SaveImage(pathHalcon);

            HObject ho_Image;
            HOperatorSet.ReadImage(out ho_Image, "bottle2");
            HImage2Bitmap24(ho_Image, out Bitmap res);

            ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(res);
            inputimage = MyOutPutImage as CogImage8Grey;
            tb.Inputs["Input1"].Value = inputimage;
            tb.Run();
            cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[0];

        }
        [DllImport("kernel32.dll")]
        private static extern void CopyMemory(int Destination, int add, int Length);

        /// <summary>
        /// HObject转8位Bitmap(单通道)
        /// </summary>
        /// <param name="image"></param>
        /// <param name="res"></param>
        public static void HObject2Bpp8(HObject image, out Bitmap res)
        {
            try
            {
                HTuple hpoint, type, width, height;

                const int Alpha = 255;
                int[] ptr = new int[2];
                HOperatorSet.GetImagePointer1(image, out hpoint, out type, out width, out height);

                res = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
                ColorPalette pal = res.Palette;
                for (int i = 0; i <= 255; i++)
                {
                    pal.Entries[i] = Color.FromArgb(Alpha, i, i, i);
                }
                res.Palette = pal;
                Rectangle rect = new Rectangle(0, 0, width, height);
                BitmapData bitmapData = res.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;

                object kk = bitmapData.Scan0;
                ptr[0] = bitmapData.Scan0.ToInt32();
                ptr[1] = hpoint.I;
                if (width % 4 == 0)
                    CopyMemory(ptr[0], ptr[1], width * height * PixelSize);
                else
                {
                    for (int i = 0; i < height - 1; i++)
                    {
                        ptr[1] += width;
                        CopyMemory(ptr[0], ptr[1], width * PixelSize);
                        ptr[0] += bitmapData.Stride;
                    }
                }
                res.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                res = null;
                //throw ex;
            }
        }
        ///// <summary>
        /// HObject转24位Bitmap
        /// </summary>
        /// <param name="image"></param>
        /// <param name="res"></param>
        public static void HObject2Bpp24(HObject image, out Bitmap res)
        {
            try
            {
                HTuple hred, hgreen, hblue, type, width, height;

                HOperatorSet.GetImagePointer3(image, out hred, out hgreen, out hblue, out type, out width, out height);

                res = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                Rectangle rect = new Rectangle(0, 0, width, height);
                BitmapData bitmapData = res.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
                int imglength = width * height;
                unsafe
                {
                    byte* bptr = (byte*)bitmapData.Scan0;
                    byte* r = ((byte*)hred.I);
                    byte* g = ((byte*)hgreen.I);
                    byte* b = ((byte*)hblue.I);
                    for (int i = 0; i < imglength; i++)
                    {
                        bptr[i * 4] = (b)[i];
                        bptr[i * 4 + 1] = (g)[i];
                        bptr[i * 4 + 2] = (r)[i];
                        bptr[i * 4 + 3] = 255;
                    }
                }

                res.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                res = null;
                throw ex;
            }
        }

        public static bool Start = true;
        public object obj = new object();

        public static Bitmap HImage2Bitmap24(HObject image, out Bitmap mimage)
        {
            try
            {
                HTuple type, width, height, pointer;
                HObject haImage;
                // HOperatorSet.GetImagePointer3(image, out hred, out hgreen, out hblue, out type, out width, out height);
                HOperatorSet.GetImageSize(image, out width, out height);
                HOperatorSet.InterleaveChannels(image, out haImage, "rgb", 4 * width, 0);
                HOperatorSet.GetImagePointer1(haImage, out pointer, out type, out width, out height);

                IntPtr ptr = pointer;
                Bitmap res = new Bitmap(width / 4, height, width, System.Drawing.Imaging.PixelFormat.Format24bppRgb, ptr);
                //Bitmap mimage = new Bitmap(res);
                mimage = (Bitmap)res.Clone();
                return mimage;
            }
            catch
            {
                mimage = null;
                return null;
            }

        }

        private void btnCloseCam_Click(object sender, EventArgs e)
        {
            Globals.Camera.StopGrab();
            Globals.Camera.CloseDevice();
        }
        public double[] _laserPower = new double[3];
        public double _laserPowerUploadMes = 0;

        private void pLC测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PLCTest fr_PLCTest = PLCTest.GetInstance();
            //fr_PLCTest.Show();
            FormOmron fr_Omron = FormOmron.GetInstance();
            fr_Omron.Show();
        }
        HDevelopExport BusBar = new HDevelopExport();
        private void 设备初始化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //HTuple vectMatrix = new HTuple();
            //HTuple hv_Qx = new HTuple(), hv_Qy = new HTuple();
            //BusBar.RadCal(1, 1, 2, 2, 3, 3, 4, 4, out vectMatrix);
            //BusBar.VectorAngleCalc(vectMatrix, 1, 1, out hv_Qx, out hv_Qy);
            //MessageBox.Show(((double)hv_Qx).ToString() + "计算结果");
            List_SendX.Clear();
            List_SendY.Clear();
            //Calc_Num = 1;
            AutoNum = 1;


        }

        public static string ReceiverRobotStr = "";


        public static string ReceivePowerRS232 = "";

        public void ImageProcess()
        {
            string a = "aaa";
            //Bitmap bmp = IamgeQueue.Dequeue();
            //MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bmp);
            try
            {
                if (a != null)
                {
                    m_ImageFileTool.Operator.Open(a, CogImageFileModeConstants.Read);
                    m_ImageFileTool.Run();
                    inputimage = m_ImageFileTool.OutputImage as CogImage8Grey;
                    tb.Inputs["Image"].Value = inputimage;// m_ImageFileTool.OutputImage as CogImage8Grey;
                    //tb.Inputs["Input1"].Value = CogImageConvertTool1.OutputImage as CogImage8Grey;
                    //m_ImageFileTool.Dispose();
                    //CogPMAlignTool PMA = tb.Tools["CogPMAlignTool1"] as CogPMAlignTool;
                    //if (PMA.RU)
                    //{
                    //}
                    tb.Run();
                    //cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[1];
                    ICogRecord DispalyRecord = tb.CreateLastRunRecord().SubRecords[0];
                    Globals.DispalyRecord(DispalyRecord);
                    #region cogRecordDisplay1_cogDisplay1
                    //Bitmap bmp = this.cogRecordDisplay1.CreateContentBitmap(CogDisplayContentBitmapConstants.Image) as Bitmap;
                    //ICogImage MyOutPutImage2 = (ICogImage)new CogImage24PlanarColor(bmp);
                    //cogDisplay1.Image = MyOutPutImage2;
                    #endregion
                    //CogPMAlignTool PMA = tb.Tools["CogPMAlignTool1"] as CogPMAlignTool;
                    //if (PMA.Results.Count >= 1)
                    //{

                    //}
                    //tb.Outputs["ress"].Value = true;
                    //CogPMAlignTool PMA = mToolBlock.Tools["CogPMAlignTool1"] as CogPMAlignTool;
                    Thread.Sleep(10);
                    Task.Run(() => { SaveScreenImage(cogRecordDisplay1, ImageSavePath); });

                    //if (Globals.mSettingOptionParm.ACC)
                    //{

                    //}
                    //cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[1];

                    //double Pos_X = (double)tb.Outputs["X"].Value;
                    //double Pos_Y = (double)tb.Outputs["Y"].Value;
                    //double Pos_R = (double)tb.Outputs["R"].Value;
                    if (chbCross.Checked)
                    {
                        cross_disp = true;
                    }
                    else
                    {
                        cross_disp = true;
                    }
                    DrawCross(2500, 2000, ref cogRecordDisplay1, cross_disp);
                    //txb_X.Text = x.ToString("f3");
                    //txb_Y.Text = y.ToString("f3");
                    //txb_R.Text = r.ToString("f3");
                    cogRecordDisplay1.Fit();
                    //Globals.LogRecord("【定位数据X:" + Pos_X + "定位数据Y:" + Pos_Y + "】");
                    //SaveScreenImage(cogRecordDisplay1, ImageSavePath);
                    #region MyRegion_Calib
                    //运行九次拍照工具得到九个像素坐标，完成后再进行标定。

                    //tbCamCalib.Inputs["tbCamCalibImage"].Value = inputimage;

                    //Calib_tbCamCalib = tbCamCalib.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;

                    //Calib_tbCamCalib.Calibration.NumPoints = 9;
                    ////像素坐标
                    //Calib_tbCamCalib.Calibration.SetUncalibratedPoint(calibnum, Pos_X, Pos_Y);
                    ////机械手坐标
                    //Calib_tbCamCalib.Calibration.SetRawCalibratedPoint(calibnum, 0, -100);
                    ////标定
                    ////Calib_tbCamCalib.Calibration.Calibrate();

                    //tbCamCalib.Run();
                    //Calib_tbCamCalib = tb.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
                    //Calib_tbCamCalib.Calibration.NumPoints = 9;
                    //for (int i = 0; i <= 8; i++)
                    //{

                    //    //像素坐标
                    //    Calib_tbCamCalib.Calibration.SetUncalibratedPoint(i, Pos_X, Pos_Y);
                    //    //机械手坐标
                    //    //Calib_tbCamCalib.Calibration.SetRawCalibratedPoint(i, 0, -100);
                    //    //Calib_tbCamCalib.Calibration.Calibrate();
                    //}
                    //tbCamCalib.Run();
                    //if (calibnum==9)
                    //{
                    //    calibnum = 0;
                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Globals.LogRecord("【定位数据X:0" + "【定位数据Y:0", true);
                ShowErrorDialog("工具运行失败！" + ex.ToString()); return;
                MessageBox.Show(ex.ToString());
            }
            Globals.LogRecord("运行完成！", true);

            ToolBlockEdit edit = new ToolBlockEdit(Globals.ProjNamePath[0], tb);
            edit.cogToolBlockEditV21.Subject = tb;
            tb.Inputs["Image"].Value = inputimage;
            tb.Run();
            edit.ShowDialog();
            Task.Run(() =>
            {
                while (Start)
                {
                    lock (obj)
                    {
                        if (IamgeQueue.Count > 0)
                        {

                            IamgeQueue.Peek();//保存图像
                            IamgeQueue.Dequeue();
                        }
                    }
                }
            });

        }

        public void ImageSave()
        {

        }



        private void btnTest_Click(object sender, EventArgs e)
        {
            //Camera.GetCameraSN(out cameraSNs);
            //Camera.OpenDevice(cameraSNs[0]);
            //Camera.SetTriggerSource(7);
            //Camera.StartGrab();
            //InitializeDevice();
            //Camera.CaptureCalib(out Bitmap bt);
            Globals.Camera.Capture_New(out Bitmap bt);
            //Globals.modbusTcp.ReadInt32("D0");
            //string path = @"E:\visionPro\新建文件夹 (4)";
            //Process.Start(path);
            //OpenFileDialog openfile = new OpenFileDialog();
            //openfile.Filter = "所有文件(*.*)|*.*";
            //openfile.ShowDialog();mImageFileTool.Operator.Open(directoryInfo.GetFiles()[i].FullName, CogImageFileModeConstants.Read);
            //string a =
            //openfile.FileName;
            //m_ImageFileTool = tb.Tools["m_ImageFileTool1"] as CogImageFileTool;
            CogImageConvertTool1 = new CogImageConvertTool();
            ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
            //cogDisplay1.Image = MyOutPutImage;
            //this.pictureBox1.Image = bt;
            //Bitmap bmp = IamgeQueue.Dequeue();
            //MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bmp);
            try
            {
                //if (a != null)
                //{
                //m_ImageFileTool.Operator.Open(a, CogImageFileModeConstants.Read);
                //m_ImageFileTool.Run();
                //inputimage = m_ImageFileTool.OutputImage as CogImage8Grey;
                //tb.Inputs["Image"].Value = inputimage;// m_ImageFileTool.OutputImage as CogImage8Grey;
                //                                      //tb.Inputs["Input1"].Value = CogImageConvertTool1.OutputImage as CogImage8Grey;
                //                                      //m_ImageFileTool.Dispose();
                //inputimage = MyOutPutImage as CogImage8Grey;
                tb.Inputs["Image"].Value = MyOutPutImage;


                tb.Run();
                //cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[1];
                ICogRecord DispalyRecord = tb.CreateLastRunRecord().SubRecords[1];
                Globals.DispalyRecord(DispalyRecord);

                #region cogRecordDisplay1_cogDisplay1
                //Bitmap bmp = this.cogRecordDisplay1.CreateContentBitmap(CogDisplayContentBitmapConstants.Image) as Bitmap;
                //ICogImage MyOutPutImage2 = (ICogImage)new CogImage24PlanarColor(bmp);
                //cogDisplay1.Image = MyOutPutImage2;
                #endregion


                Thread.Sleep(10);
                SaveScreenImage(cogRecordDisplay1, ImageSavePath);

                //if (Globals.mSettingOptionParm.ACC)
                //{

                //}
                //cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[1];

                double Pos_X = (double)tb.Outputs["X"].Value;
                double Pos_Y = (double)tb.Outputs["Y"].Value;
                double Pos_R = (double)tb.Outputs["R"].Value;
                if (chbCross.Checked)
                {
                    cross_disp = true;
                }
                else
                {
                    cross_disp = true;
                }
                DrawCross(2500, 2000, ref cogRecordDisplay1, cross_disp);
                //txb_X.Text = x.ToString("f3");
                //txb_Y.Text = y.ToString("f3");
                //txb_R.Text = r.ToString("f3");
                cogRecordDisplay1.Fit();
                Globals.LogRecord("【定位数据X:" + Pos_X + "定位数据Y:" + Pos_Y + "】");
                //SaveScreenImage(cogRecordDisplay1, ImageSavePath);


                #region MyRegion_Calib
                //运行九次拍照工具得到九个像素坐标，完成后再进行标定。

                //tbCamCalib.Inputs["tbCamCalibImage"].Value = inputimage;

                //Calib_tbCamCalib = tbCamCalib.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
                //Calib_tbCamCalib.Calibration.NumPoints = 9;
                ////像素坐标
                //Calib_tbCamCalib.Calibration.SetUncalibratedPoint(calibnum, Pos_X, Pos_Y);
                ////机械手坐标
                //Calib_tbCamCalib.Calibration.SetRawCalibratedPoint(calibnum, 0, -100);
                ////标定
                ////Calib_tbCamCalib.Calibration.Calibrate();

                //tbCamCalib.Run();
                //Calib_tbCamCalib = tb.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
                //Calib_tbCamCalib.Calibration.NumPoints = 9;
                //for (int i = 0; i <= 8; i++)
                //{

                //    //像素坐标
                //    Calib_tbCamCalib.Calibration.SetUncalibratedPoint(i, Pos_X, Pos_Y);
                //    //机械手坐标
                //    //Calib_tbCamCalib.Calibration.SetRawCalibratedPoint(i, 0, -100);
                //    //Calib_tbCamCalib.Calibration.Calibrate();
                //}
                //tbCamCalib.Run();
                //if (calibnum==9)
                //{
                //    calibnum = 0;
                //}
                #endregion

                //}
            }
            catch (Exception ex)
            {
                Globals.LogRecord("【定位数据X:0" + "【定位数据Y:0", true);
                ShowErrorDialog("工具运行失败！" + ex.ToString()); return;
                MessageBox.Show(ex.ToString());
            }
            Globals.LogRecord("运行完成！", true);
            //LoadVisionParam();
            ToolBlockEdit edit = new ToolBlockEdit(Globals.ProjNamePath[0], tb);
            edit.cogToolBlockEditV21.Subject = tb;
            tb.Inputs["Image"].Value = MyOutPutImage;
            tb.Run();
            edit.ShowDialog();

        }
        //          D1512	=1工控机侧，=2流水线侧
        //          触发拍照 D1502   PLC触发拍照 D1502 = 1
        //          拍照完成 D1202   PC回复PLC
        //          定位偏移X:	D1204 PC写PLC定位偏移数据
        //          定位偏移Y D1206   PC写PLC定位偏移数据
        //          定位偏移R   D1208 PC写PLC定位偏移数据
        //          OK/NG D1210   PC写PLC定位结果OK/NG
        //          OK = 1; NG=2;

        //          PLC触发扫码 D1504 = 1
        //          扫码完成 D1204   PC回复PLC     
        /// <summary>
        /// PLC D1600 BusbarOrSide  Busbar=1;Side=2
        /// </summary>
        public static string PLC_BusbarOrSide_Adress = "D1600";
        /// <summary>
        /// PLC D1602 UP/DownSide UP=1,DOWN=2
        /// </summary>
        public static string PLC_UPDownSide_Adress = "D1602";
        /// <summary>
        /// PLC触发拍照D1502
        /// </summary>
        public static string PLC_Fhoto_Adress = "D1502";
        /// <summary>
        /// D1510  PLC触发拍照计数
        /// </summary>
        public static string PLC_FhotoNum_Adress = "D1510";
        /// <summary>
        /// PC拍照完成
        /// </summary>
        public static string CCD_Finish_Adress = "D1202";
        /// <summary>
        /// PLC触发扫码D1514
        /// </summary>
        public static string PLC_TrigCode_Adress = "D1514";
        /// <summary>
        /// PC扫码完成D1212
        /// </summary>
        public static string CCD_TrigCode_Finish_Adress = "D1212";
        /// <summary>
        /// D1512=1,工控机侧  ，流水线侧=2
        /// </summary>
        public static string CW_Adress = "D1512";
        /// <summary>
        /// D1204,定位结果X
        /// </summary>
        public static string PosX_Adress = "D1204";
        /// <summary>
        /// D1206,定位结果Y
        /// </summary>
        public static string PosY_Adress = "D1206";
        /// <summary>
        /// D1208,定位结果R
        /// </summary>
        public static string PosR_Adress = "D1208";
        /// <summary>
        /// D1210,测试结果
        /// </summary>
        public static string Resault_Adress = "D1210";
        /// <summary>
        /// 自动运行工具
        /// </summary>
        /// <param name="valuex"></param>
        /// <param name="valuey"></param>
        /// <returns></returns>
        private bool AutoRun()
        {

            double valuex = 999;
            double valuey = 999;
            try
            {
                bool isSuccess = true;
                //运行工具进行拍照
                Globals.Camera.Capture_New(out Bitmap bt);
                //
                //frmCam_MVS.CaptureCalib(out Bitmap bt);
                ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                //cogDisplay1.Image = MyOutPutImage;
                var camTri = Globals.omronFinsUdp.ReadInt32(PLC_Fhoto_Adress);

                //if (camTri.IsSuccess && camTri.Content == 1)
                if (camTri.Content == 1)
                {
                    tb.Inputs["Input1"].Value = MyOutPutImage;
                    tb.Run();

                }
                else if ((camTri.Content == 2))
                {
                    tb.Inputs["Input1"].Value = MyOutPutImage;
                    tb.Run();
                }

                if ((tb.RunStatus.Result == CogToolResultConstants.Accept) && (camTri.Content == 1))
                {
                    valuex = Math.Round((double)tb.Outputs["X"].Value, 3);
                    valuey = Math.Round((double)tb.Outputs["Y"].Value, 3);
                    #region CogRecord_CogDisplay1
                    //Bitmap bmp = this.cogRecordDisplay1.CreateContentBitmap(CogDisplayContentBitmapConstants.Image) as Bitmap;
                    //ICogImage MyOutPutImage2 = (ICogImage)new CogImage24PlanarColor(bmp);
                    //cogDisplay1.Image = MyOutPutImage2;

                    //Bitmap bmp = this.cogRecordDisplay1.CreateContentBitmap(CogDisplayContentBitmapConstants.Image) as Bitmap;
                    //ICogImage MyOutPutImage2 = (ICogImage)new CogImage24PlanarColor(bmp);
                    //cogDisplay1.Image = MyOutPutImage2;

                    //其他窗体传递图像
                    //ICogRecord DispalyRecord = tb.CreateLastRunRecord().SubRecords[1];
                    //Globals.DispalyRecord(DispalyRecord);
                    #endregion
                    this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[1];
                    Globals.omronFinsUdp.Write(PosX_Adress, valuex);
                    Globals.omronFinsUdp.Write(PosY_Adress, valuey);
                    Globals.omronFinsUdp.Write(Resault_Adress, 1);
                    //视觉拍照完成。
                    Globals.omronFinsUdp.Write(CCD_Finish_Adress, 1);

                    Stopwatch stopwatchFinish = new Stopwatch();
                    stopwatchFinish.Start();
                    while (stopwatchFinish.ElapsedMilliseconds < 5000)//超过3秒就超时了
                    {
                        var result = Globals.omronFinsUdp.ReadInt32(PLC_Fhoto_Adress);
                        if (result.IsSuccess && result.Content == 0)
                        {
                            Globals.omronFinsUdp.Write(CCD_Finish_Adress, 0);
                            isSuccess = true;
                            break;
                        }
                        Thread.Sleep(50);
                    }
                    if (isSuccess == true)
                    {
                        Globals.LogRecord("【未收到PLC拍照复位信号，通讯交互失败！】", true);
                        ShowErrorDialog("未收到PLC拍照复位信号，通讯交互失败！");
                        valuex = 0;
                        valuey = 0;
                        return true;
                    }
                    else
                    {
                        IsAutoRunFlag1 = true;
                        Globals.LogRecord("【视觉运行成功！】", true);
                    }
                }
                else
                {
                    Globals.LogRecord("【视觉运行失败，请检查视觉程序！】", true);
                    valuex = 0;
                    valuey = 0;
                    return true;
                }


                if (tb.RunStatus.Result == CogToolResultConstants.Accept && (camTri.Content == 2))
                {
                    valuex = Math.Round((double)tb.Outputs["X"].Value, 3);
                    valuey = Math.Round((double)tb.Outputs["Y"].Value, 3);
                    #region CogRecord_CogDisplay1
                    //Bitmap bmp = this.cogRecordDisplay1.CreateContentBitmap(CogDisplayContentBitmapConstants.Image) as Bitmap;
                    //ICogImage MyOutPutImage2 = (ICogImage)new CogImage24PlanarColor(bmp);
                    //cogDisplay1.Image = MyOutPutImage2;

                    //Bitmap bmp = this.cogRecordDisplay1.CreateContentBitmap(CogDisplayContentBitmapConstants.Image) as Bitmap;
                    //ICogImage MyOutPutImage2 = (ICogImage)new CogImage24PlanarColor(bmp);
                    //cogDisplay1.Image = MyOutPutImage2;

                    //其他窗体传递图像
                    //ICogRecord DispalyRecord = tb.CreateLastRunRecord().SubRecords[1];
                    //Globals.DispalyRecord(DispalyRecord);
                    #endregion
                    this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[1];

                    Globals.omronFinsUdp.Write(PosX_Adress, valuex);
                    Globals.omronFinsUdp.Write(PosY_Adress, valuey);
                    Globals.omronFinsUdp.Write(Resault_Adress, 1);
                    //视觉拍照完成。
                    Globals.omronFinsUdp.Write(CCD_Finish_Adress, 1);

                    Stopwatch stopwatchFinish = new Stopwatch();
                    stopwatchFinish.Start();
                    while (stopwatchFinish.ElapsedMilliseconds < 5000)//超过3秒就超时了
                    {
                        var result = Globals.omronFinsUdp.ReadInt32(PLC_Fhoto_Adress);
                        if (result.IsSuccess && result.Content == 0)
                        {
                            Globals.omronFinsUdp.Write(CCD_Finish_Adress, 0);
                            isSuccess = true;
                            break;
                        }
                        Thread.Sleep(50);
                    }
                    if (isSuccess == true)
                    {
                        Globals.LogRecord("【未收到PLC拍照复位信号，通讯交互失败！】", true);
                        ShowErrorDialog("未收到PLC拍照复位信号，通讯交互失败！");
                        valuex = 0;
                        valuey = 0;
                        return true;
                    }
                    else
                    {
                        IsAutoRunFlag1 = true;
                        Globals.LogRecord("【视觉运行成功！】", true);
                    }
                }
                else
                {
                    Globals.LogRecord("【视觉运行失败，请检查视觉程序！】", true);
                    valuex = 0;
                    valuey = 0;
                    return true;
                }



                //if (Globals.SettingOption.isSaveImage)
                //{
                //    Task.Run(() => { SaveScreenImage(cogRecordDisplay1, ImageSavePath); });
                //}
                Globals.LogRecord(" 【引导定位坐标 :" + "【X:" + valuex + "，Y:" + valuey + "!】");
                return true;
            }
            catch (Exception ex)
            {
                valuex = 0;
                valuey = 0;
                Globals.LogRecord("【程序运行异常,请检查！】" + ex.ToString(), true);
                return true;
            }

        }
        public static int AutoNum = 1;
        List<int> List_SendX = new List<int>();
        List<int> List_SendY = new List<int>();
        private bool AutoRunTest()
        {

            double valuex = 999.999;
            double valuey = 999.999;
            Bitmap bt = null;
            try
            {
                bool isSuccess = true;
                //运行工具进行拍照
                //Globals.Camera.Capture_New(out Bitmap bt);
                //
                //frmCam_MVS.CaptureCalib(out Bitmap bt);
                //ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                //cogDisplay1.Image = MyOutPutImage;
                var camTriFhotoNum = Globals.omronFinsUdp.ReadInt32(PLC_FhotoNum_Adress);
                if (camTriFhotoNum.IsSuccess && camTriFhotoNum.Content == 1)
                {
                    List_SendX.Clear();
                    List_SendY.Clear();
                    AutoNum = 1;
                    //Globals.LogRecord("【第一次触发拍照！】", true);
                }


                Globals.LogRecord("【触发拍照次数：" + camTriFhotoNum.Content + "】", true);

                var camTri = Globals.omronFinsUdp.ReadInt32(PLC_Fhoto_Adress);
                //if (camTri.IsSuccess && camTri.Content == 1)
                var camTriCWFhoto = Globals.omronFinsUdp.ReadInt32(CW_Adress);
                if (camTriCWFhoto.IsSuccess && camTriCWFhoto.Content == 2)
                {
                    Globals.Camera.Capture_New(out bt);
                }
                if (camTriCWFhoto.IsSuccess && camTriCWFhoto.Content == 1)
                {
                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose1, out bt);
                }

                //LuxShareCam
                //frmCam_MVS.CaptureCalib(out Bitmap bt);
                ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);

                //cogDisplay1.Image = MyOutPutImage;

                if (camTriCWFhoto.IsSuccess && camTriCWFhoto.Content == 2)
                {
                    #region MyRegion标定侧流水线
                    tb.Inputs["Input1"].Value = MyOutPutImage;
                    tb.Run();

                    this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[0];
                    //if (Globals.SettingOption.isSaveImage)
                    //{
                    //    Task.Run(() => { SaveScreenImage(cogRecordDisplay1, ImageSavePath); });
                    //}
                    this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[2];
                    if (chbCross.Checked)
                    {
                        cross_disp = true;
                    }
                    else
                    {
                        cross_disp = true;
                    }
                    DrawCross(2500, 2000, ref cogRecordDisplay1, cross_disp);
                    if (tb.RunStatus.Result == CogToolResultConstants.Accept)
                    {
                        valuex = Math.Round((double)tb.Outputs["X"].Value, 3);
                        valuey = Math.Round((double)tb.Outputs["Y"].Value, 3);
                    }
                    #endregion
                }
                //工控机侧
                if (camTriCWFhoto.IsSuccess && camTriCWFhoto.Content == 1)
                {
                    #region MyRegion标定侧流水线
                    tb.Inputs["Input1"].Value = MyOutPutImage;
                    tb.Run();

                    this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[0];
                    //if (Globals.SettingOption.isSaveImage)
                    //{
                    //    Task.Run(() => { SaveScreenImage(cogRecordDisplay1, ImageSavePath); });
                    //}
                    this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[2];
                    if (chbCross.Checked)
                    {
                        cross_disp = true;
                    }
                    else
                    {
                        cross_disp = true;
                    }
                    DrawCross(2500, 2000, ref cogRecordDisplay1, cross_disp);
                    if (tb.RunStatus.Result == CogToolResultConstants.Accept)
                    {
                        valuex = Math.Round((double)tb.Outputs["X"].Value, 3);
                        valuey = Math.Round((double)tb.Outputs["Y"].Value, 3);
                    }
                    #endregion
                }


                int xTemp = Convert.ToInt32(Math.Round(valuex, 3) * 1000);
                int yTemp = Convert.ToInt32(Math.Round(valuey, 3) * 1000);
                //var camTriCWFhoto = Globals.omronFinsUdp.ReadInt32(CW_Adress);
                if (camTriCWFhoto.IsSuccess && camTriCWFhoto.Content == 1)
                {
                    xTemp = xTemp * (-1);
                    //yTemp = yTemp * (-1);
                }
                List_SendX.Add(xTemp);
                List_SendY.Add(yTemp);
                if (camTriFhotoNum.IsSuccess && camTriFhotoNum.Content == 32)
                {
                    AutoNum = 1;
                    //Globals.LogRecord("【触发拍照次数：" + AutoNum + "】", true);
                    //Globals.LogRecord("【第32次触发拍照！】", true);
                    double a = List_SendX[0];
                    //批量发送D2000-2062  32个数据
                    for (int i = 2063, j = 0; i >= 2000; i--, j++) //(int i = 1999,j=0; i <= 2062; i++,j++)
                    {
                        i = i - 1;
                        string PLC_Adress_D_X = "D" + i.ToString();

                        Globals.omronFinsUdp.Write(PLC_Adress_D_X, List_SendY[j]);//* 1000

                    }
                    //批量发送D2000-2062  32个数据
                    for (int i = 2563, j = 0; i >= 2500; i--, j++)// (int i = 2499, j = 0; i <= 2562; i++, j++)
                    {
                        i = i - 1;
                        string PLC_Adress_D_Y = "D" + i.ToString();
                        Globals.omronFinsUdp.Write(PLC_Adress_D_Y, List_SendX[j]);
                    }
                    List_SendY.Clear();
                    List_SendX.Clear();
                }
                AutoNum++;
                Globals.omronFinsUdp.Write(PosX_Adress, valuex);
                Globals.omronFinsUdp.Write(PosY_Adress, valuey);
                Globals.omronFinsUdp.Write(Resault_Adress, 1);
                //视觉拍照完成。
                Globals.omronFinsUdp.Write(CCD_Finish_Adress, 1);
                Stopwatch stopwatchFinish = new Stopwatch();
                stopwatchFinish.Start();
                while (stopwatchFinish.ElapsedMilliseconds < 999999)//超过3秒就超时了
                {
                    var result = Globals.omronFinsUdp.ReadInt32(PLC_Fhoto_Adress);
                    if (result.IsSuccess && result.Content == 0)
                    {
                        Globals.omronFinsUdp.Write(CCD_Finish_Adress, 0);
                        isSuccess = true;
                        break;
                    }
                    Thread.Sleep(50);
                }
                if (isSuccess == true)
                {
                    Globals.LogRecord("【未收到PLC拍照复位信号，通讯交互失败！】", true);
                    //ShowErrorDialog("未收到PLC拍照复位信号，通讯交互失败！");
                    valuex = 0;
                    valuey = 0;
                    IsAutoRunFlag1 = true;
                    return true;
                }
                else
                {
                    IsAutoRunFlag1 = true;
                    Globals.LogRecord("【视觉运行成功！】", true);
                }
                Globals.LogRecord("【引导定位坐标 :" + "X:" + valuey + "，Y:" + valuex + "】");

                return true;
            }
            catch (Exception ex)
            {
                valuex = 0;
                valuey = 0;
                IsAutoRunFlag1 = true;
                Globals.LogRecord("【程序运行异常,请检查！】" + ex.ToString(), true);
                return true;
            }

        }
        private bool AutoRunSide()
        {
            double valuex = 999.999;
            double valuey = 999.999;
            bool resault = true;
            Bitmap bt = null;
            try
            {
                bool isSuccess = true;
                //运行工具进行拍照
                //Globals.Camera.Capture_New(out Bitmap bt);
                //LuxShareCam
                //frmCam_MVS.CaptureCalib(out Bitmap bt);
                //ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                //cogDisplay1.Image = MyOutPutImage;

                var camTriUPDownSide = Globals.omronFinsUdp.ReadInt32(PLC_UPDownSide_Adress);

                var camTriFhotoNum = Globals.omronFinsUdp.ReadInt32(PLC_FhotoNum_Adress);
                if (camTriFhotoNum.IsSuccess && camTriFhotoNum.Content == 1)
                {
                    List_SendX.Clear();
                    List_SendY.Clear();
                    AutoNum = 1;
                }
                AutoNum = camTriFhotoNum.Content;
                Globals.LogRecord("【触发拍照次数：" + camTriFhotoNum.Content + "】", true);

                //if (AutoNum == 8)
                //{
                //    Globals.LogRecord("【触发拍照次数：" + AutoNum + "】", true);
                //    AutoNum = 1;
                //}
                //else
                //{
                //    Globals.LogRecord("【触发拍照次数：" + AutoNum + "】", true);
                //}
                //AutoNum++;

                var camTriCWFhoto = Globals.omronFinsUdp.ReadInt32(CW_Adress);
                if (camTriCWFhoto.IsSuccess && camTriCWFhoto.Content == 2)
                {
                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose1, out bt);

                }
                if (camTriCWFhoto.IsSuccess && camTriCWFhoto.Content == 1)
                {
                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose1, out bt);
                }
                ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);

                if (camTriFhotoNum.IsSuccess)
                {
                    switch (camTriFhotoNum.Content)
                    {
                        case 1:
                            CalcCorid(tb, MyOutPutImage, ref valuex, ref valuey, ref resault);
                            break;
                        case 2:
                            CalcCorid(tb, MyOutPutImage, ref valuex, ref valuey, ref resault);
                            break;
                        case 3:
                            CalcCorid(tb, MyOutPutImage, ref valuex, ref valuey, ref resault);
                            break;
                        case 4:
                            CalcCorid(tb, MyOutPutImage, ref valuex, ref valuey, ref resault);
                            break;
                        case 5:
                            CalcCorid(tb, MyOutPutImage, ref valuex, ref valuey, ref resault);
                            break;
                        case 6:
                            CalcCorid(tb, MyOutPutImage, ref valuex, ref valuey, ref resault);
                            break;
                        case 7:
                            CalcCorid(tb, MyOutPutImage, ref valuex, ref valuey, ref resault);
                            break;
                        case 8:
                            CalcCorid(tb, MyOutPutImage, ref valuex, ref valuey, ref resault);
                            break;
                    }
                }
                if ((valuex >= 5) && (valuey >= 5))
                {
                    resault = true;
                }
                if (resault)
                {
                    Globals.omronFinsUdp.Write(Resault_Adress, 1);//OK
                    Globals.LogRecord("【视觉运行成功！】", true);
                }
                else
                {
                    Globals.omronFinsUdp.Write(Resault_Adress, 2);//NG
                    Globals.LogRecord("【视觉运行NG！】", true);
                }

                int xTemp = Convert.ToInt32(Math.Round(valuex, 3) * 1000);
                int yTemp = Convert.ToInt32(Math.Round(valuey, 3) * 1000);
                //var camTriCWFhoto = Globals.omronFinsUdp.ReadInt32(CW_Adress);
                if (camTriFhotoNum.IsSuccess && ((camTriFhotoNum.Content == 1) ||
                    (camTriFhotoNum.Content == 2) ||
                   (camTriFhotoNum.Content == 3) ||
                   (camTriFhotoNum.Content == 4)))
                {
                    xTemp = xTemp * (-1);
                    //yTemp = yTemp * (-1);
                }
                List_SendX.Add(xTemp);
                List_SendY.Add(yTemp);


                Globals.omronFinsUdp.Write(PosX_Adress, yTemp);//* 1000
                Globals.omronFinsUdp.Write(PosY_Adress, xTemp);//* 1000

                //视觉拍照完成。
                Globals.omronFinsUdp.Write(CCD_Finish_Adress, 1);
                Stopwatch stopwatchFinish = new Stopwatch();
                stopwatchFinish.Start();
                while (stopwatchFinish.ElapsedMilliseconds < 999999)//超过3秒就超时了
                {
                    var result = Globals.omronFinsUdp.ReadInt32(PLC_Fhoto_Adress);
                    if (result.IsSuccess && result.Content == 0)
                    {
                        Globals.omronFinsUdp.Write(CCD_Finish_Adress, 0);
                        isSuccess = true;
                        break;
                    }
                    Thread.Sleep(50);
                }
                if (isSuccess == true)
                {
                    Globals.LogRecord("【未收到PLC拍照复位信号，通讯交互失败！】", true);
                    //ShowErrorDialog("未收到PLC拍照复位信号，通讯交互失败！");
                    valuex = 0;
                    valuey = 0;
                    IsAutoRunFlag1 = true;
                    return true;
                }
                else
                {
                    IsAutoRunFlag1 = true;
                }
                Globals.LogRecord("【引导定位坐标 :" + "X:" + valuey + "，Y:" + valuex + "!】");
                return true;
            }
            catch (Exception ex)
            {
                valuex = 0;
                valuey = 0;
                IsAutoRunFlag1 = true;
                Globals.LogRecord("【程序运行异常,请检查！】" + ex.ToString(), true);
                return true;
            }

        }
        public void CalcCorid(CogToolBlock tbParams, ICogImage PictrueParam, ref double valuexParam, ref double valueyParam, ref bool resaultParam)
        {
            valuexParam = 0;
            valueyParam = 0;
            resaultParam = true;
            #region MyRegion_UP
            tbParams.Inputs["Input1"].Value = PictrueParam;
            tbParams.Run();

            this.cogRecordDisplay1.Record = tbParams.CreateLastRunRecord().SubRecords[0];
            //if (Globals.SettingOption.isSaveImage)
            //{
            //    //Task.Run(() => { SaveScreenImage(cogRecordDisplay1, ImageSavePath); });
            //}
            this.cogRecordDisplay1.Record = tbParams.CreateLastRunRecord().SubRecords[2];
            if (chbCross.Checked)
            {
                cross_disp = true;
            }
            else
            {
                cross_disp = true;
            }
            DrawCross(2500, 2000, ref cogRecordDisplay1, cross_disp);
            //if (tbParams.RunStatus.Result == CogToolResultConstants.Accept)
            //{
            valuexParam = Math.Round((double)tbParams.Outputs["X"].Value, 3);
            valueyParam = Math.Round((double)tbParams.Outputs["Y"].Value, 3);
            //    //resaultParam = (bool)tbParams.Outputs["Re"].Value;
            //}
            #endregion

        }
        private bool ManualRun()
        {
            double valuex = 999;
            double valuey = 999;
            try
            {
                //运行工具进行拍照
                Globals.Camera.Capture_New(out Bitmap bt);
                //
                //frmCam_MVS.CaptureCalib(out Bitmap bt);
                ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                //cogDisplay1.Image = MyOutPutImage;
                tb.Inputs["Input1"].Value = MyOutPutImage;
                tb.Run();
                this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[0];
                //if (Globals.SettingOption.isSaveImage)
                //{
                //    Task.Run(() => { SaveScreenImage(cogRecordDisplay1, ImageSavePath); SaveScreenImageInputImage(bt, ImageSavePath); });
                //}
                this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[1];
                if (chbCross.Checked)
                {
                    cross_disp = true;
                }
                else
                {
                    cross_disp = true;
                }
                DrawCross(2500, 2000, ref cogRecordDisplay1, cross_disp);
                if (tb.RunStatus.Result == CogToolResultConstants.Accept)
                {
                    valuex = Math.Round((double)tb.Outputs["X"].Value, 3);
                    valuey = Math.Round((double)tb.Outputs["Y"].Value, 3);

                    {
                        Globals.LogRecord("【视觉运行成功！】", true);
                    }
                }
                else
                {
                    Globals.LogRecord("【视觉运行失败，请检查视觉程序！】", true);
                    valuex = 0;
                    valuey = 0;
                    //ToolBlockEdit edit1 = new ToolBlockEdit(pathCamCalib, tb);
                    //edit1.cogToolBlockEditV21.Subject = tb;
                    //tb.Inputs["Input1"].Value = MyOutPutImage;
                    //tb.Run();
                    //edit1.ShowDialog();
                    return true;
                }

                Globals.LogRecord(" 【引导定位坐标 :" + "【X:" + valuey + "，Y:" + valuex + "!】");

                //ToolBlockEdit edit = new ToolBlockEdit(pathCamCalib, tb);
                //edit.cogToolBlockEditV21.Subject = tb;
                //tbCamCalib.Inputs["Input1"].Value = MyOutPutImage;
                //tb.Run();
                //edit.ShowDialog();

                return true;
            }
            catch (Exception ex)
            {
                //ToolBlockEdit edit3 = new ToolBlockEdit(pathCamCalib, tb);
                //edit3.cogToolBlockEditV21.Subject = tb;
                tb.Inputs["Input1"].Value = MyOutPutImage;
                tb.Run();
                //edit3.ShowDialog();
                valuex = 0;
                valuey = 0;
                Globals.LogRecord("【程序运行异常,请检查！】" + ex.ToString(), true);
                ShowErrorDialog("【程序运行异常, 请检查！】" + ex.ToString());
                return true;
            }
        }
        public static bool IsAutoRunFlag1 = true;
        /// <summary>
        /// 自动运行流程逻辑
        /// </summary>
        private void ScanPLC_Run()
        {
            #region MyRegion注释
            //Globals.LogRecord("【保存成功！】");
            //ShowInfoDialog("参数保存", "参数保存成功！", UIStyle.Green);
            //ShowErrorDialog("您输入了错误的数据格式！请输入D0，D100类型字符串" + ex.ToString());
            //ShowInfoDialog("数据读取", "数据读取成功！", UIStyle.Green);
            //ShowSuccessDialog("数据读取成功！");
            ////取地址
            //for (int i = 9999, j = 0; i <= 10126; i++, j++)
            //{
            //    i = i + 1;
            //    string a1 = "D" + i.ToString();
            //    PLC_Adress[j] = a1;
            //}
            ////MyTcpHelper tcpSever = new MyTcpHelper();
            //modbusTcp?.ConnectClose();
            //modbusTcp = new OmronFinsNet(DeviceParm.PLCAddress, DeviceParm.PLCPort);
            //modbusTcp.SA1 = (byte)DeviceParm.SA1;
            //modbusTcp.DA2 = 0;
            //OperateResult connect = modbusTcp.ConnectServer();
            //if (connect.IsSuccess)
            //{
            //    PlcState = "已连接";
            //    IsPlcConnect = true;
            //    AddMessage("PLC连接成功！");
            //}
            //else
            //{
            //    IsPlcConnect = true;
            //    PlcState = "未连接";
            //    AddMessage("PLC连接失败！");
            //}
            #endregion
            Task.Factory.StartNew(() =>
            {
                Globals.LogRecord("【启动PLC扫描！】");
                while (Globals.MachineStart)
                {
                    if (Globals.LogInOK)
                    {
                        this.BeginInvoke(new Action(() => { btnRunTool.Visible = true; }));
                    }
                    else
                    {
                        this.BeginInvoke(new Action(() => { btnRunTool.Visible = true; }));
                    }
                    if (Globals.IsPlcConnect)
                    {
                        if (Globals.OnLine)
                        {
                            var camTri = Globals.omronFinsUdp.ReadInt32(PLC_Fhoto_Adress);
                            if (camTri.IsSuccess && (IsAutoRunFlag1) && (camTri.Content == 1) || (camTri.Content == 2))
                            {
                                Globals.LogRecord("【收到PLC触发拍照信号！】");
                                IsAutoRunFlag1 = true;
                                Task.Factory.StartNew(() =>
                                {

                                    var camBusbarOrSide = Globals.omronFinsUdp.ReadInt32(PLC_BusbarOrSide_Adress);
                                    if (camBusbarOrSide.IsSuccess && (camBusbarOrSide.Content == 1))
                                    {
                                        AutoRunTest();
                                    }
                                    if (camBusbarOrSide.IsSuccess && (camBusbarOrSide.Content == 2))
                                    {
                                        AutoRunSide();
                                    }
                                    //Globals.LogRecord("【视觉处理完成！】");
                                });
                            }
                            var CodeTri = Globals.omronFinsUdp.ReadInt32(PLC_TrigCode_Adress);
                            if (CodeTri.IsSuccess && CodeTri.Content == 2)
                            {
                                Globals.LogRecord("【收到扫码触发信号！】");
                                Task.Factory.StartNew(() =>
                                {
                                    TestCode();
                                });
                            }

                        }
                    }
                    Thread.Sleep(20);
                }
            });

            //Task.Factory.StartNew(() =>
            //{
            //    short Heartbeat = 1;
            //    AddMessage("启动PLC心跳！");
            //    while (true)
            //    {

            //        if (IsPlcConnect)
            //        {
            //            if (Heartbeat == 1)
            //            {
            //                Heartbeat = 0;
            //            }
            //            else if (Heartbeat == 0)
            //            {
            //                Heartbeat = 1;
            //            }

            //            modbusTcp?.Write(m_Plc_HeartbeatAddress, Heartbeat);
            //        }
            //        Thread.Sleep(500);
            //    }
            //});
        }
        public static string QR_CodeModule = "";

        public void TestCode()
        {
            //运行工具进行拍照
            Globals.Camera.Capture_New(out Bitmap bt);
            ICogImage MyOutPutImageBarCode = (ICogImage)new CogImage24PlanarColor(bt);
            tb.Inputs["Input1"].Value = MyOutPutImageBarCode;
            tb.Run();
            this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[0];
            //测试Run完成
            if (tb.RunStatus.Result == Cognex.VisionPro.CogToolResultConstants.Accept)
            {
                QR_CodeModule = tb.Outputs["Code"].Value.ToString();
                BarCodeQueue.Enqueue(QR_CodeModule);

                Globals.LogRecord("【QR_CodeModule：" + BarCodeQueue.Peek() + "】");
            }
            else
            {
                Globals.LogRecord("【扫码失败！】");
                QR_CodeModule = "";
            }
        }
        public void ScanCode()
        {
            ////运行工具进行拍照
            //Globals.Camera.Capture_New(out Bitmap bt);
            //ICogImage MyOutPutImageBarCode = (ICogImage)new CogImage24PlanarColor(bt);
            //tbBarCode.Inputs["Input1"].Value = MyOutPutImageBarCode;
            //tbBarCode.Run();
            //this.cogRecordDisplay1.Record = tbBarCode.CreateLastRunRecord().SubRecords[1];
            //this.cogRecordDisplay1.Fit();
            //ShowTool(tbBarCode, pathBarCode);
            ////测试Run完成
            ////if (tbBarCode.RunStatus.Result == Cognex.VisionPro.CogToolResultConstants.Accept)
            ////{
            //string barcode = tbBarCode.Outputs["Code"].Value.ToString();
            //Globals.LogRecord("【QR_CodeModule：" + barcode + "】");
            ////}
            ////else
            ////{
            ////    Globals.LogRecord("【扫码失败！】");
            ////}
        }


        private void btnManual_Click(object sender, EventArgs e)
        {
            if (!Globals.OnLine)
            {
                ManualRun();
            }
            else
            {
                Globals.LogRecord("【联机自动运行中，禁止强制手动运行视觉代码！】");
            }
        }
        public static int Calc_Num = 0;
        private void Calib_Test_Click(object sender, EventArgs e)
        {
            /*
            *       5   <-    4   <-    3
            *       6         1   ->    2
            *       7   ->    8   ->    9
            *机械手Y轴对应代码X方向，右负方向（-）     流水线侧  右正方向 +
            *机械手Z轴对应代码Y方向，上正方向（+）     流水线侧  向上正方向 +    Z    +
            *       (1,1)      (0,1)      (-1,1)
            *       (1,0)      (0,0)      (-1,0)
            *       (1,-1)     (0,-1)     (-1,-1)
            */
            double valuex = 0;
            double valuey = 0;
            double calib_MoudleX = 0;
            double calib_MoudleY = 0;
            if (true)
            {
                calib_MoudleX = Globals.mSettingOptionParm.Calib_X;
                calib_MoudleY = Globals.mSettingOptionParm.Calib_Y;
                //中间列X
                Calib_X[7] = Calib_X[3] = Calib_X[0] = calib_MoudleX;// Globals.mSettingOptionParm.Calib_X;
                                                                     //左边列X
                Calib_X[6] = Calib_X[5] = Calib_X[4] = Calib_X[0] + 5;
                //右边列X
                Calib_X[8] = Calib_X[2] = Calib_X[1] = Calib_X[0] - 5;
                //中间行Y
                Calib_Y[5] = Calib_Y[1] = Calib_Y[0] = calib_MoudleY;// Globals.mSettingOptionParm.Calib_Y;
                                                                     //上边行Y
                Calib_Y[4] = Calib_Y[3] = Calib_Y[2] = Calib_Y[0] + 5;
                //下边行Y
                Calib_Y[8] = Calib_Y[7] = Calib_Y[6] = Calib_Y[0] - 5;

            }
            else
            {
                calib_MoudleX = Globals.mSettingOptionParm.Calib_ConvB_X;
                calib_MoudleY = Globals.mSettingOptionParm.Calib_ConvB_Y;

                //中间列X
                Calib_X[7] = Calib_X[3] = Calib_X[0] = calib_MoudleX;// Globals.mSettingOptionParm.Calib_X;
                                                                     //左边列X
                Calib_X[6] = Calib_X[5] = Calib_X[4] = Calib_X[0] - 5;
                //右边列X
                Calib_X[8] = Calib_X[2] = Calib_X[1] = Calib_X[0] + 5;
                //中间行Y
                Calib_Y[5] = Calib_Y[1] = Calib_Y[0] = calib_MoudleY;// Globals.mSettingOptionParm.Calib_Y;
                                                                     //上边行Y
                Calib_Y[4] = Calib_Y[3] = Calib_Y[2] = Calib_Y[0] + 5;
                //下边行Y
                Calib_Y[8] = Calib_Y[7] = Calib_Y[6] = Calib_Y[0] - 5;
            }



            Globals.Camera.Capture_New(out Bitmap bt);
            //
            //frmCam_MVS.CaptureCalib(out Bitmap bt);
            ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
            //cogDisplay1.Image = MyOutPutImage;

            tb.Inputs["Input1"].Value = MyOutPutImage;
            tb.Run();

            this.cogRecordDisplay1.Record = tb.CreateLastRunRecord().SubRecords[1];
            if (chbCross.Checked)
            {
                cross_disp = true;
            }
            else
            {
                cross_disp = true;
            }
            DrawCross(2500, 2000, ref cogRecordDisplay1, cross_disp);
            if (tb.RunStatus.Result == CogToolResultConstants.Accept)
            {
                valuex = Math.Round((double)tb.Outputs["X"].Value, 3);
                valuey = Math.Round((double)tb.Outputs["Y"].Value, 3);
            }
            Globals.LogRecord(" 【引导定位坐标 :" + "【X:" + valuex + "，Y:" + valuey + "!】");
            //像素坐标
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(Calc_Num, valuey, valuex);
            //机械手坐标
            Calib_tbCamCalib.Calibration.SetRawCalibratedPoint(Calc_Num, Calib_X[Calc_Num], Calib_Y[Calc_Num]);
            ToolBlockEdit editCur = new ToolBlockEdit("", tb);
            editCur.cogToolBlockEditV21.Subject = tb;
            tb.Inputs["Input1"].Value = MyOutPutImage;
            tb.Run();
            editCur.ShowDialog();
            Calc_Num++;
            if (Calc_Num == 8)
            {
                if (true)
                {
                    //标定
                    Calib_tbCamCalib.Calibration.Calibrate();
                    tb.Run();
                    ToolBlockEdit edit = new ToolBlockEdit("", tb);
                    edit.cogToolBlockEditV21.Subject = tb;
                    edit.ShowDialog();
                    Calc_Num = 0;
                }
                else
                {
                    //标定
                    Calib_tbCamCalib.Calibration.Calibrate();
                    tb.Run();
                    ToolBlockEdit edit = new ToolBlockEdit("", tb);
                    edit.cogToolBlockEditV21.Subject = tb;
                    edit.ShowDialog();
                    Calc_Num = 0;
                }
            }

            Globals.LogRecord(" 【标定计数坐标Calc_Num=】" + Calc_Num);
        }

        private void ManualCalibTest_Click(object sender, EventArgs e)
        {
            /*
            *       5   <-    4   <-    3
            *       6         1   ->    2
            *       7   ->    8   ->    9
            *机械手Y轴对应代码X方向，右负方向（-）
            *机械手Z轴对应代码Y方向，上正方向（+）
            *       (1,1)      (0,1)      (-1,1)
            *       (1,0)      (0,0)      (-1,0)
            *       (1,-1)     (0,-1)     (-1,-1)
            */
            //中间列X
            Calib_X[7] = Calib_X[3] = Calib_X[0] = Globals.mSettingOptionParm.Calib_X;
            //左边列X
            Calib_X[6] = Calib_X[5] = Calib_X[4] = Calib_X[0] + 1;
            //右边列X
            Calib_X[8] = Calib_X[2] = Calib_X[1] = Calib_X[0] - 1;
            //中间行Y
            Calib_Y[5] = Calib_Y[1] = Calib_Y[0] = Globals.mSettingOptionParm.Calib_Y;
            //上边行Y
            Calib_Y[4] = Calib_Y[3] = Calib_Y[2] = Calib_Y[0] + 1;
            //下边行Y
            Calib_Y[8] = Calib_Y[7] = Calib_Y[6] = Calib_Y[0] - 1;
            Calib_tbCamCalib = tb.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;

            Calib_tbCamCalib.Calibration.NumPoints = 9;
            //像素坐标
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(0, 0, 0);
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(1, -1, 0);
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(2, -1, 1);
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(3, 0, 1);
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(4, 1, 1);
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(5, 1, 0);
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(6, 1, -1);
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(7, 0, -1);
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(8, -1, -1);

            //机械手坐标
            for (int i = 0; i <= 8; i++)
            {
                Calib_tbCamCalib.Calibration.SetRawCalibratedPoint(i, Calib_X[i], Calib_Y[i]);
            }
            //标定
            Calib_tbCamCalib.Calibration.Calibrate();
            tb.Run();
            //ToolBlockEdit edit = new ToolBlockEdit(path1, tb);
            //edit.cogToolBlockEditV21.Subject = tb;
            //edit.ShowDialog();
        }

        private void uiSymbolButton2_Click(object sender, EventArgs e)

        {
            if (true)
            {
                Calib_tbCamCalib = tb.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
                //Calib_tbCamCalib.Calibration.NumPoints = 9;
            }
            else
            {
                Calib_tbCamCalib = tb.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            }
            Calib_tbCamCalib.Calibration.NumPoints = 9;


        }
        private void btnPLCTest_Click(object sender, EventArgs e)
        {
            //批量发送D2000-2062  32个数据
            for (int i = 1999, j = 0; i <= 2062; i++, j++)
            {
                i = i + 1;
                string PLC_Adress_D_X = "D" + i.ToString();
                Globals.omronFinsUdp.Write(PLC_Adress_D_X, j);

            }
            //批量发送D2000-2062  32个数据
            for (int i = 2499, j = 0; i <= 2562; i++, j++)
            {
                i = i + 1;
                string PLC_Adress_D_Y = "D" + i.ToString();
                Globals.omronFinsUdp.Write(PLC_Adress_D_Y, j);
            }
        }

        private void btnClearDate_Click(object sender, EventArgs e)
        {

        }
        private void timerDelete_Tick(object sender, EventArgs e)
        {
            DeleteFile(ConfigureFilePath.Dir_Record_TaskLog, Globals.SettingOption.LogSaveDay);
            DeleteFile(ImageSavePathDelet, Globals.SettingOption.ImageSaveDay);
        }
        public void DeleteFile(string fileDirect, int saveDay)
        {
            DateTime nowTime = DateTime.Now;
            DirectoryInfo root = new DirectoryInfo(fileDirect);
            DirectoryInfo[] dics = root.GetDirectories();

            FileAttributes attr = File.GetAttributes(fileDirect);
            if (attr == FileAttributes.Directory)
            {
                foreach (DirectoryInfo file in dics)
                {
                    TimeSpan t = nowTime - file.CreationTime;
                    int day = t.Days;
                    if (day > saveDay)
                    {
                        Directory.Delete(file.FullName, true);
                    }
                }
            }
        }
        public bool IsAutoRunFlagGRR = true;
        public bool RunFlagGRR = true;

        private void btnScanCode_Click(object sender, EventArgs e)
        {

            lampPLC1.Color = Color.Yellow;
            lampPLC1.Color = Color.Red;
            lampPLC1.Color = Color.DarkGreen;
            //ScanCode();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //InitialLaser();
            //MyLaser.Instance.GetLaserParam();
            string ret = DataUploadHelper.MyMes.MesSend(0, "modBusbarWeld", JsonConvert.SerializeObject(Globals.dicInfo));
            Form_Weld fr_Weld = Form_Weld.GetInstance();
            fr_Weld.Show();
        }

        #region 激光相关

        /// <summary>
        /// 初始化激光
        /// </summary>
        public void InitialLaser()
        {
            BslErrCode ret = MarkAPI.ins.ResetDevices(this.Handle);
            if (ret != BslErrCode.BSL_ERR_SUCCESS)
            {
                Globals.LogRecord("【初始化失败-->！" + MarkAPI.ins.MsgGet() + "】");
            }
            if (!MarkAPI.ins.GetDeviceList())
            {
                Globals.LogRecord("【初始化失败-->！" + MarkAPI.ins.MsgGet() + "】");
            }
            else
            {
                Globals.LogRecord("【初始化完成！" + MarkAPI.ins.MsgGet() + "】");
            }
        }

        public static bool IsAutoRunLaserFlag1 = true;
        public void ChangeLaserDocument()
        {
            // D1504切换文档1：A短2：A长，3:B短，4：B长。5：(Busbar_A两侧),6：(Busbar_A),7：(Busbar_B两侧),8：(Busbar_B)
            bool commnicateFalg = true;
            var camTri = Globals.omronFinsUdp.ReadInt32(MyLaser.Instance.ChangeLaserDocumentAddress);

            if (camTri.IsSuccess && (IsAutoRunLaserFlag1))
            {
                IsAutoRunLaserFlag1 = true;
                switch (camTri.Content)
                {
                    case 1:
                        LaserCommunicateMethod(1);
                        break;
                    case 2:
                        LaserCommunicateMethod(2);
                        break;
                    case 3:
                        LaserCommunicateMethod(3);
                        break;
                    case 4:
                        LaserCommunicateMethod(4);
                        break;
                    case 5:
                        LaserCommunicateMethod(5);
                        break;
                    case 6:
                        LaserCommunicateMethod(6);
                        break;
                    case 7:
                        LaserCommunicateMethod(7);
                        break;
                    case 8:
                        LaserCommunicateMethod(8);
                        break;
                }

            }
        }

        public void LaserCommunicateMethod(int Num, bool commnicateFalg = true)
        {
            Globals.LogRecord("【收到PLC触发焊接信号！】");
            if (MyLaser.Instance.AlterMethod(Num))
            {
                Globals.LogRecord("【焊接文档切换成功！】");
                //焊接文档切换完成。
                Globals.omronFinsUdp.Write(MyLaser.Instance.ChangeLaserOKAddress, 1);
                Stopwatch stopwatchFinish = new Stopwatch();
                stopwatchFinish.Start();
                while (stopwatchFinish.ElapsedMilliseconds < 999999) //超过3秒就超时了
                {
                    var resultOK = Globals.omronFinsUdp.ReadInt32(MyLaser.Instance.ChangeLaserDocumentAddress);
                    if (resultOK.IsSuccess && resultOK.Content == 0)
                    {
                        Globals.omronFinsUdp.Write(MyLaser.Instance.ChangeLaserOKAddress, 0);
                        commnicateFalg = true;
                        break;
                    }

                    Thread.Sleep(50);
                }

                if (commnicateFalg)
                {
                    IsAutoRunLaserFlag1 = true;
                    Globals.LogRecord("【PLC通讯交互完成！】");
                }
                else
                {
                    Globals.LogRecord("【PLC通讯交互失败！】");
                }
            }
            else
            {
                Globals.omronFinsUdp.Write(MyLaser.Instance.ChangeLaserOKAddress, 2);
                Globals.LogRecord("【焊接文档切换失败！】");
            }
        }

        #endregion
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void uiHeaderButton_Camera_Click(object sender, EventArgs e)
        {
            //FormMelsecAscii formMelsecAscii = FormMelsecAscii.GetInstance();
            //formMelsecAscii.Show();
            //FormMelsecBinary formMelsecBinary = FormMelsecBinary.GetInstance();
            //formMelsecBinary.Show();

            #region MyRegionMelsecLib
            //PLC初始化
            try
            {
                MyMelsecHelper.HslUtils.PlcsDic = FileHelper.InfoRead<Dictionary<string, MyMelsecHelper.PlcParm>>(ConfigureFilePath.projectItemPath + "\\" + "SysPLCParam.json");
                // //读取内容
                //bool flag = MyMelsecHelper.HslUtils.ReadResultRender("plcName", HslDataType.hslString,"m0" ,out int m000,out string aaaa);
                //bool flag = MyMelsecHelper.HslUtils.WriteResultRender("plcName", HslDataType.hslString,"D0","SSS",out string errMsg);

            }
            catch (Exception exception)
            {
                ;
            }
            //this.Hide();
            if (MyMelsecHelper.HslUtils.formMelsecBinary.Visible)
            {
                MyMelsecHelper.HslUtils.formMelsecBinary.Activate();
            }
            else
            {
                MyMelsecHelper.HslUtils.formMelsecBinary.Show();
            }

            new Thread(o =>
            {
                while (MyMelsecHelper.HslUtils.formMelsecBinary.Visible)
                {
                    Application.DoEvents(); System.Threading.Thread.Sleep(1);
                }
                FileHelper.InfoSave(MyMelsecHelper.HslUtils.PlcsDic, ConfigureFilePath.projectItemPath + "\\" + "SysPLCParam.json");
            }).Start();
            #endregion

            ShowSuccessDialog("PLC调试", "【PLC调试打开完成！】");
            Globals.LogRecord("【PLC调试打开完成！】", true);
        }

        private void uiTabControl_Ribbon_Selected(object sender, TabControlEventArgs e)
        {
            this.uiTabControl_Ribbon.Height = 40;
            if (Globals.LogInOK)
            {
                //获取当前选项卡的名称
                string TabName = e.TabPage.Text;
                if (TabName == "隐藏工具栏")
                {
                    this.uiTabControl_Ribbon.Height = 40;
                }
                else
                {
                    this.uiTabControl_Ribbon.Height = 150;
                }
            }
        }

        private void uiHeaderButton9_Click(object sender, EventArgs e)
        {
            Form1 fr = Form1.GetInstance();
            fr.Show();
        }

        private void uiHeaderButton_Config_Click(object sender, EventArgs e)
        {
            if (Globals.LogInOK)
            {
                FrmConfig fr_Setting = FrmConfig.GetInstance();
                fr_Setting.ShowDialog();
            }
        }
        private void uiHeaderButton_Communication_Click(object sender, EventArgs e)
        {
            FrmTCPClient frmTCPClient = FrmTCPClient.GetInstance();
            frmTCPClient.ShowDialog();
        }

        private void btnProSelect_Click(object sender, EventArgs e)
        {
            //FrmProjSelect fr_ProjSelect = FrmProjSelect.GetInstance();
            //fr_ProjSelect.Show();
        }

        private void 清除日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstRunLog.Items.Clear();
        }

        private void uiHeaderButton_Calibration_Click(object sender, EventArgs e)
        {
            FrmTcpServer frmTcpServer = FrmTcpServer.GetInstance();
            frmTcpServer.ShowDialog();
        }

        private void btnCamCapture_Click(object sender, EventArgs e)
        {
            //Task.Run(new Action(() =>
            //{
            FrmCam frmCam = FrmCam.GetInstance();
            frmCam.ShowDialog();
            //}));
        }

        private void btnSeries_Click(object sender, EventArgs e)
        {
            FrmCOM frmCOM = new FrmCOM();
            frmCOM.ShowDialog();
        }

        private void lstRunLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();
                Brush mybsh = Brushes.Black;
                // 判断是什么类型的标签
                if (lstRunLog.Items[e.Index].ToString().IndexOf("PLC1") != -1)
                {
                    mybsh = Brushes.Blue;
                }
                if (lstRunLog.Items[e.Index].ToString().IndexOf("PLC2") != -1)
                {
                    mybsh = Brushes.DarkOrange;
                }
                if ((lstRunLog.Items[e.Index].ToString().IndexOf("失败") != -1) ||
                    (lstRunLog.Items[e.Index].ToString().IndexOf("错误") != -1) ||
                    (lstRunLog.Items[e.Index].ToString().IndexOf("停止") != -1) ||
                    (lstRunLog.Items[e.Index].ToString().IndexOf("暂停") != -1) ||
                    (lstRunLog.Items[e.Index].ToString().IndexOf("报警") != -1) ||
                    (lstRunLog.Items[e.Index].ToString().IndexOf("超时") != -1)
                    )
                {
                    mybsh = Brushes.Red;
                }
                // 焦点框
                e.DrawFocusRectangle();
                //文本 
                e.Graphics.DrawString(lstRunLog.Items[e.Index].ToString(), e.Font, mybsh, e.Bounds, StringFormat.GenericDefault);
            }
        }

        private void btnCalibration_Click(object sender, EventArgs e)
        {
            FrmCalib frmCalib = FrmCalib.GetInstance();
            frmCalib.ShowDialog();
        }

        public static double[] dataPosX = new double[3];
        public static double[] dataPosY = new double[3];


        public static void RunTestRobot()
        {

            //Globals.MySetRobotGlobal("10");
            //if (Globals.WaitRobotValue("GlobalStep", "100", "11"))
            //{
            //    Globals.LogRecord("【Task1步序号10：运动到拍照检测位置！" + Globals.MyReciveStr + "】", true);
            //    Globals.MySetRobotGlobal("20");
            //}
            Globals.ReadGloablScope("POS_CalibY", "104");
            while (true)
            {
                if (Globals.ReceiveDone)
                {
                    double temp2 = Convert.ToDouble(Globals.MyReciveStr);
                    dataPosY[0] = Math.Round(temp2, 3);
                    Globals.ReceiveDone = false;
                    Globals.LogRecord(" 【机械手标定初始位置 :" + "【X:" + dataPosX[0].ToString() + "，Y:" + dataPosY[0].ToString() + "!】");
                    Globals.LogRecord(" 【数据POS_CalibY接收完成!】", false);

                    break;
                }
            }
            Globals.ReadGloablScope("POS_CalibX", "103");
            //Thread.Sleep(3000);
            while (true)
            {
                if (Globals.ReceiveDone)
                {
                    double temp1 = Convert.ToDouble(Globals.MyReciveStr);
                    dataPosX[0] = Math.Round(temp1, 3);
                    Globals.ReceiveDone = false;
                    Globals.LogRecord(" 【机械手标定初始位置 :" + "【X:" + dataPosX[0].ToString() + "，Y:" + dataPosY[0].ToString() + "!】");

                    Globals.LogRecord(" 【数据POS_CalibX接收完成!】", false);
                    break;
                }
            }



            //Globals.ReadGloablScope("POS_CalibY", "104");
            //while (true)
            //{
            //    if (Globals.ReceiveDone)
            //    {
            //        double temp2 = Convert.ToDouble(Globals.MyReciveStr);
            //        dataPosY[0] = Math.Round(temp2, 3);
            //        Globals.ReceiveDone = false;
            //        break;
            //    }
            //}
        }

        private void uiHeaderButton10_Click(object sender, EventArgs e)
        {
            Task.Run(
                () =>
                {
                    RunTestRobot();
                } );
        }

        private void hbtnLight_Click(object sender, EventArgs e)
        {
            FrmLight frmLight = FrmLight.GetInstance();
            frmLight.ShowDialog();
        }
    }
}
