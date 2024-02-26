//using FinsLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading;
using Cognex.VisionPro.FGGigE.Implementation.Internal;
using HalconDotNet;
using HslCommunication.ModBus;
using HslCommunication.Profinet;
using HslCommunication;
using HslCommunication.Profinet.Omron;
using System.IO.Ports;
using ApeFree.DataStore;
using ApeFree.DataStore.Adapters;
using ApeFree.DataStore.Core;
using TouchSocket.Core;
using TouchSocket.Core.ByteManager;
using TouchSocket.Sockets;
using EventMgrLib;
using ApeFree.DataStore.Local;
using Newtonsoft.Json.Converters;
using VisionProHelper;
using HslCommunication.Profinet.Melsec;

namespace sunyvpp
{
    public class Globals
    {
        public static UIForm AAAA = new UIForm();
        #region 登录用户变量

        public static bool LogInOK = true;
        public bool loadComplite = true;
        public static string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        public static string currentUserName = "";
        public static bool loadingProcess_Var = true;
        public static bool Plc_IsSuccessUDP = true;
        public static bool MachineStart = true;
        public static OmronFinsNet modbusTcp;//PLC
        public static bool bAutoRun = true;
        public static int TaskStep;
        public static CogToolBlock tbSelect;
        /// <summary>
        /// HSL,PLC通讯实例
        /// </summary>
        public static OmronFinsUdp omronFinsUdp;
        public string plcIP = "192.168.100.100";
        public int plcPort = 9600;
        public byte SA1 = 123;
        public static Controller State = Controller.none;
        public static int SaveStep = 0;
        public static string PlcState;
        public static bool IsPlcConnect;
        public static object DataFormat = "CDBA";
        public static bool OnLine;
        #endregion
        #region 串口参数
        public static SerialPort sp = new SerialPort();
        public static MyTcpHelper MyTcp = null;

        public static MyTCPServer myTCPServer = null;

        public static string stripClient = "";

        public static string RobotStrName = "R";//"[CamSetVar_1_s("R","0",2,1);id=100]");
        public static string RobotStrValue = "1";

        public static string ReadRobotStrName = "R";

        public static string RobotStrID = "100";
        //    type:是类型:1 表示 int 型；2 表示是 real 型
        //    scope:域，0:表示系统，1 表示全局，2 表示工程，3 表示程序

        public static string SendToRobotStr = "[CamSetVar_1_s(\"" + RobotStrName + "\",\"" + RobotStrValue + "\",1,1);id=" + RobotStrID + "]";
        public static string ReadFromRobotStr = "[CamSetVar_1_s(\"" + ReadRobotStrName + "\"," + "1,2);id=" + RobotStrID + "]";


        public static MySerialPort myLaserSerialPort = new MySerialPort();
        #endregion
        public static Dictionary<string, string> dicInfo = new Dictionary<string, string>();
        public static List<string> ProjName = new List<string>();
        public static List<string> ProjNamePath = new List<string>();
        public static List<string> cameraSNs = new List<string>();
        public static List<string> PLC_Key = new List<string>();

        public static Dictionary<string, List<string>> ProCalibItemDic = new Dictionary<string, List<string>>();
        public static string ProjCalibPointNamePath;

        public static int varProDuctNum;
        public static int varProDuctNumOK;
        public static int varProDuctNumNG;
        //标定表格显示
        public static List<string[]> CalibRowList = new List<string[]>();
        public static List<string> ProjCalibPointNamePathList = new List<string>();

        public static event EventHandler<string> inforShowEvent;
        public static event Action<string, Color> inforShowActionState;
        public static event EventHandler<Color> inforShowState;

        public static event Action<int, ICogRecord> inforShowActionDisp;
        public static EventHandler<ICogRecord> inforShowRecod;

        #region MyVisionRegion
        public static CogToolBlock AutoToolblock;// = MyVisionProMgr.ins.CogToolBlockDic["TB_Down.vpp"];
        public static CogToolBlock UpCameraInspectToolblock;// = MyVisionProMgr.ins.CogToolBlockDic["TB_Inspect.vpp"];

        public static CogToolBlock UpCameraInsFlowToolblock;
        public static CogToolBlock UpCameraInsMonitorToolblock;

        public static CogToolBlock UpCameraAssemToolblock;// = MyVisionProMgr.ins.CogToolBlockDic["TB_UP.vpp"];
        public static CogToolBlock UpCameraAssemToolblockNew;
        public static CogToolBlock CalibToolblock;// = MyVisionProMgr.ins.CogToolBlockDic["TB_Calib.vpp"];
        public static CogToolBlock CalibRotToolblock;

        #region RunInsPectMyRegion
        public static void RunInsPect(ICogImage MyOutPutImage, CogToolBlock tbSelected, out double valuex, out double valuey, out double valuer, out bool valuere, int dipNum = 4)
        {
            tbSelected.Inputs["Input1"].Value = MyOutPutImage;
            tbSelected.Run();
            string resaultSend = "NG";
            if (tbSelected.RunStatus.Result == CogToolResultConstants.Accept)
            {
                if (tbSelected.CreateLastRunRecord().SubRecords.Count > 0)
                {
                    ICogRecord DispalyRecord1 = tbSelected.CreateLastRunRecord().SubRecords[0];
                    ICogRecord DispalyRecord = tbSelected.CreateLastRunRecord()
                        .SubRecords[tbSelected.CreateLastRunRecord().SubRecords.Count - 1];

                    Globals.DispalyRecordNum(dipNum, DispalyRecord);
                }
                valuex = Math.Round((double)tbSelected.Outputs["X"].Value, 3);
                valuey = Math.Round((double)tbSelected.Outputs["Y"].Value, 3);
                valuer = Math.Round((double)tbSelected.Outputs["R"].Value, 3);
                valuere = (bool)tbSelected.Outputs["Re"].Value;
                //valuex = Convert.ToInt32(Math.Round(valuex, 3));
                //valuey = Convert.ToInt32(Math.Round(valuey, 3));
                //valuer = Convert.ToInt32(Math.Round(valuey, 3));
                if (valuere)
                {
                    resaultSend = "OK";
                }
                else
                {
                    resaultSend = "NG";
                }
            }
            else
            {
                ICogRecord DispalyRecord1 = tbSelected.CreateLastRunRecord().SubRecords[0];
                ICogRecord DispalyRecord = tbSelected.CreateLastRunRecord()
                    .SubRecords[tbSelected.CreateLastRunRecord().SubRecords.Count - 1];
                Globals.DispalyRecordNum(dipNum, DispalyRecord1);
                Globals.DispalyRecordNum(dipNum + 1, DispalyRecord1);
                valuex = 999;
                valuey = 999;
                valuer = 999;
                valuere = false;
            }
        }


        #endregion


        public static void RunTool(ICogImage MyOutPutImage, CogToolBlock tbSelected, out double valuex, out double valuey, out double valuer, out bool valuere, int dipNum = 1)
        {
            tbSelected.Inputs["Input1"].Value = MyOutPutImage;
            tbSelected.Run();
            string resaultSend = "NG";
            if (tbSelected.RunStatus.Result == CogToolResultConstants.Accept)
            {
                if (tbSelected.CreateLastRunRecord().SubRecords.Count > 0)
                {
                    ICogRecord DispalyRecord1 = tbSelected.CreateLastRunRecord().SubRecords[0];
                    ICogRecord DispalyRecord = tbSelected.CreateLastRunRecord()
                        .SubRecords[tbSelected.CreateLastRunRecord().SubRecords.Count - 1];
                    Globals.DispalyRecordNum(dipNum, DispalyRecord1);
                }
                valuex = Math.Round((double)tbSelected.Outputs["X"].Value, 3);
                valuey = Math.Round((double)tbSelected.Outputs["Y"].Value, 3);
                valuer = Math.Round((double)tbSelected.Outputs["R"].Value, 3);
                valuere = (bool)tbSelected.Outputs["Re"].Value;
                //valuex = Convert.ToInt32(Math.Round(valuex, 3));
                //valuey = Convert.ToInt32(Math.Round(valuey, 3));
                //valuer = Convert.ToInt32(Math.Round(valuey, 3));
                if (valuere)
                {
                    resaultSend = "OK";
                }
                else
                {
                    resaultSend = "NG";
                }
            }
            else
            {
                ICogRecord DispalyRecord1 = tbSelected.CreateLastRunRecord().SubRecords[0];
                ICogRecord DispalyRecord = tbSelected.CreateLastRunRecord()
                    .SubRecords[tbSelected.CreateLastRunRecord().SubRecords.Count - 1];
                Globals.DispalyRecordNum(dipNum, DispalyRecord1);
                valuex = 999;
                valuey = 999;
                valuer = 999;
                valuere = false;
            }
        }

        /// <summary>
        /// 保存界面截图
        /// </summary>
        /// <param name="display"></param>
        /// <param name="path"></param>
        public static void SaveScreenImage(CogRecordDisplay display, string path) //保存界面截图jpeg
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

        public static void SaveScreenImageInputImage(Bitmap bmp, string path) //保存界面截图jpeg
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

        public static bool ReceiveDone = false;
        public static bool WaitRobotValue(string Name, string IdValue, string value, int timeOut = 20000)
        {
            Stopwatch swSign = new Stopwatch();
            swSign.Reset();
            swSign.Start();
            while (true)
            {
                //拍照检测位置运动到位
                Globals.MyReadRobotGlobal(Name, IdValue);
                if (MyReciveStr == value)
                {
                    break;
                }
                if (swSign.ElapsedMilliseconds >= timeOut)
                {
                    swSign.Stop();
                    swSign.Reset();
                    Thread.Sleep(10);
                    return false;
                    break;
                }
                Thread.Sleep(500);
            }
            return true;
        }
        #endregion
        /// <summary>
        /// 初始化PLC的TCP
        /// </summary>
        private void ScanPLC()
        {
            modbusTcp?.ConnectClose();
            modbusTcp = new OmronFinsNet("127.0.0.1", 9600);//(SettingOption.PLCIPAddress, SettingOption.Port);
            modbusTcp.SA1 = 201;// (byte)SettingOption.SA1;
            modbusTcp.DA2 = 0;
            OperateResult connect = modbusTcp.ConnectServer();

            string m_Plc_HeartbeatAddress = "D3000";
            if (connect.IsSuccess)
            {
                PlcState = "已连接";
                IsPlcConnect = true;
                Globals.LogRecord("PLC连接成功！");

            }
            else
            {
                IsPlcConnect = true;
                PlcState = "未连接";
                Globals.LogRecord("PLC连接失败！");

            }
            Task.Factory.StartNew(() =>
            {
                short Heartbeat = 1;
                Globals.LogRecord("启动PLC心跳！");
                while (true)
                {

                    if (IsPlcConnect)
                    {
                        if (Heartbeat == 1)
                        {
                            Heartbeat = 0;
                        }
                        else if (Heartbeat == 0)
                        {
                            Heartbeat = 1;
                        }

                        modbusTcp?.Write(m_Plc_HeartbeatAddress, Heartbeat);
                    }
                    Thread.Sleep(500);
                }

            });
        }

        /// <summary>
        /// 初始化PLC的TCP
        /// </summary>
        public static void ScanPLCUDP()
        {
            omronFinsUdp = new OmronFinsUdp("127.0.0.1", 9600);//(SettingOption.PLCIPAddress, SettingOption.Port);
            omronFinsUdp.SA1 = 201;// (byte)SettingOption.SA1;
            omronFinsUdp.DA2 = 0;
            //omronFinsUdp.ByteTransform.DataFormat = (HslCommunication.Core.DataFormat)(DataFormat);
            //  ABCD  BADC   CDAB  DCBA
            //omronFinsUdp.ByteTransform.IsStringReverseByteWord = true;

            OperateResult read = omronFinsUdp.ReadInt32("D1000");
            int InPlace = omronFinsUdp.ReadInt32("D1000").Content;
            omronFinsUdp.Write("D1000", 1);

            OperateResult oresurt = omronFinsUdp.ReadFloat("D1000");
            Plc_IsSuccessUDP = oresurt.IsSuccess;
            if (Plc_IsSuccessUDP)
            {
                PlcState = "已连接";
                IsPlcConnect = true;
                Globals.LogRecord("【PLC连接成功！】", true);
            }
            else
            {
                IsPlcConnect = true;
                PlcState = "未连接";
                Globals.LogRecord("【PLC连接失败！】", true);
            }

            //string m_Plc_HeartbeatAddress = "D3000";
            //Task.Factory.StartNew(() =>
            //{
            //    short Heartbeat = 1;
            //    Globals.LogRecord("启动PLC心跳！");
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
        public static double Max(double[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        double temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }

            return arr[2];
        }
        private void tsl_PLC_Click(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(() =>
            //{
            //    HslUtils.PlcsDic[plcName].PlcType = "UDP";
            //    HslUtils.PlcsDic[plcName].omronFinsUdp = new OmronFinsUdp(HslUtils.PlcsDic[plcName].IpAdr, plcPort);
            //    HslUtils.PlcsDic[plcName].omronFinsUdp.SA1 = SA1;
            //    HslUtils.PlcsDic[plcName].omronFinsUdp.ByteTransform.DataFormat = HslUtils.PlcsDic[plcName].ABCDtype;
            //    omronFinsUdp = HslUtils.PlcsDic[plcName].omronFinsUdp;
            //    OperateResult oresurt = omronFinsUdp.ReadFloat("D1430");
            //    Plc_IsSuccess = oresurt.IsSuccess;
            //    if (!Plc_IsSuccess)
            //        UpdateMessage("PLC连接失败!");
            //}).Wait(1 * 2000);
        }
        public static string MyReciveStr = "";
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

        #region Setting数据
        public static OptionSetting SettingOption = new OptionSetting();
        public static IStore<VisionDataParam> store;
        public static mSettingOption mSettingOptionParm = new mSettingOption();
        public static MVS Camera;
        public static MVS CameraDown;
        public static string MainFormCaptain = "";
        #endregion

        #region 加载参数文件
        public static void Initialglb()
        {
            Camera = new MVS();
            CameraDown = new MVS();
            //if (File.Exists("1.json"))
            //{
            //    Globals.mSettingOptionParm = (mSettingOption)JsonHelper.Instance.JsonFileToObject("1.json", Globals.mSettingOptionParm);
            //}
            if (File.Exists(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json"))
            {
                Globals.SettingOption = (OptionSetting)JsonHelper.Instance.JsonFileToObject(ConfigureFilePath.projectItemPath + "\\" + "SysParam.json", Globals.SettingOption);
                Globals.LogRecord("【系统参数加载成功！】");
                AAAA.ShowSuccessDialog("系统参数加载成功");
            }
            else
            {
                Globals.LogRecord("【系统参数加载失败！】");
                AAAA.ShowErrorDialog("系统参数加载失败");
            }

            Globals.PLC_Key.Clear();
            MyMelsecHelper.HslUtils.PlcsDic = FileHelper.InfoRead<Dictionary<string, MyMelsecHelper.PlcParm>>(ConfigureFilePath.projectItemPath + "\\" + "SysPLCParam.json");//ConfigureFilePath.PLC_Dic_path
            foreach (string plcName in MyMelsecHelper.HslUtils.PlcsDic.Keys)
            {
                Globals.PLC_Key.Add(plcName);
            }
            //加载OptionSetting参数
            //XMLMethod.ReadNodeAndInnerText(ConfigureFilePath.Path_Config_Setting_Option, "OptionSetting", out Globals.SettingOption);
            LoadVisionParam();
        }

        #endregion
        public static string SetRobot(string name, string value, string idValue)
        {
            return "[CamSetVar_1_s(\"" + name + "\",\"" + value + "\",1,1);id=" + idValue + "]";
        }
        public static string SetRobotReal(string name, string value, string idValue)
        {
            return "[CamSetVar_1_s(\"" + name + "\",\"" + value + "\",2,1);id=" + idValue + "]";
        }


        public static void MyReadRobotGlobal(string name = "GlobalStep", string idValue = "100")
        {
            string sendStr = ReadRobotGlobal(name, idValue);
            myTCPServer.SendStr(stripClient, sendStr);
        }

        public static void ReadGloablScope(string name = "GlobalStep", string idValue = "100")
        {
            string sendStr = ReadFinal(name, idValue);
            myTCPServer.SendStr(stripClient, sendStr);
        }
        public static string ReadFinal(string name, string idValue, string ZhengShi = "2", string QuanjuGong = "1")
        {
            return "[CamReadVar_s(\"" + name + "\"," + ZhengShi + "," + QuanjuGong + ");id=" + idValue + "]";
        }
        public static void MySetRobotGlobal(string value, string name = "GlobalStep", string idValue = "100")
        {
            string sendStr = SetRobot(name, value, idValue);
            myTCPServer.SendStr(stripClient, sendStr);
        }



        public static void MySetRobotOffsetX(string value, string name = "GlobOffset_X", string idValue = "105")
        {
            string sendStr = SetRobotReal(name, value, idValue);
            myTCPServer.SendStr(stripClient, sendStr);
        }
        public static void MySetRobotOffsetY(string value, string name = "GlobOffset_Y", string idValue = "106")
        {
            string sendStr = SetRobotReal(name, value, idValue);
            myTCPServer.SendStr(stripClient, sendStr);
        }
        public static void MySetRobotOffsetR(string value, string name = "GlobOffset_R", string idValue = "107")
        {
            string sendStr = SetRobotReal(name, value, idValue);
            myTCPServer.SendStr(stripClient, sendStr);
        }



        /// <summary>
        /// 1 整形/表示全局，2 表示工程
        /// </summary>
        /// <param name="name"></param>
        /// <param name="idValue"></param>
        /// <returns></returns>
        public static string ReadRobot(string name, string idValue)
        {
            return "[CamReadVar_s(\"" + name + "\"," + "1,2);id=" + idValue + "]";
        }
        public static string ReadRobotGlobal(string name, string idValue)
        {
            return "[CamReadVar_s(\"" + name + "\"," + "1,1);id=" + idValue + "]";
        }
        public static MelsecMcNet melsec_net = new MelsecMcNet();
        public static bool InitialMelnetPLC()
        {
            melsec_net.IpAddress = SettingOption.PLCIPAddress1;

            melsec_net.Port = SettingOption.PLCPort1;

            melsec_net.ConnectClose();

            try
            {
                OperateResult connect = melsec_net.ConnectServer();
                if (connect.IsSuccess)
                {
                    Globals.LogRecord("【PLC连接成功！】");
                    return true;
                }
                else
                {
                    Globals.LogRecord("【PLC连接失败！】");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Globals.LogRecord("【PLC连接失败！】");
                return true;
            }


        }

        public static void LoadVisionParam()
        {
            // 本地存储配置（默认使用Json格式）
            var settings = new LocalStoreAccessSettings(ConfigureFilePath.projectItemPath + "\\VisionParam.conf")
            {
                SerializationAdapter = new XmlSerializationAdapter()
            };
            // 本地存储器
            IStore<VisionDataParam> store = StoreFactory.Factory.CreateLocalStore<VisionDataParam>(settings);
            Globals.store = store;
            Globals.store.Load();
        }

        public static void ProductOpt(string fileOpt)
        {

            try
            {
                if (!Directory.Exists(ConfigureFilePath.Path_Config_Pro))
                {
                    Directory.CreateDirectory(ConfigureFilePath.Path_Config_Pro);
                }
                string projectItempath = Path.Combine(ConfigureFilePath.Path_Config_Pro, ConfigureFilePath.Path_Config_ProFile);
                if (!File.Exists(projectItempath))
                {
                    File.Create(projectItempath).Dispose();
                }
                using (StreamReader camRead = new StreamReader(projectItempath, Encoding.Default))
                {
                    if ((ConfigureFilePath.projectItemPath = camRead.ReadLine()) != null)
                    {
                        int lastIndex = ConfigureFilePath.projectItemPath.LastIndexOf("\\");
                        MainFormCaptain = "主窗口" + ConfigureFilePath.projectItemPath.Substring(lastIndex + 1);
                        MainFormCaptain = "主窗口" + ConfigureFilePath.projectItemPath;
                        AAAA.ShowSuccessDialog(MainFormCaptain + "项目切换成功！");

                        ConfigureFilePath.ImageTaskPath = ConfigureFilePath.projectItemPath + "\\" + "ImageTask";
                        //此处要加载所有视觉文件
                        //SignData.imageDirPath.DirPathValue(ConfigureFilePath.ImageTaskPath);
                        Globals.LogRecord("【" + MainFormCaptain + "】", true);

                        if (!Directory.Exists(ConfigureFilePath.ImageTaskPath))
                        {
                            Directory.CreateDirectory(ConfigureFilePath.ImageTaskPath);
                            Globals.LogRecord("【ImageTask文件夹创建！】", true);
                        }
                        ConfigureFilePath.MotionTaskPath = ConfigureFilePath.projectItemPath + "\\" + "MotionTask";
                        if (!Directory.Exists(ConfigureFilePath.MotionTaskPath))
                        {
                            Directory.CreateDirectory(ConfigureFilePath.MotionTaskPath);
                            Globals.LogRecord("【MotionTask文件夹创建！】", true);

                        }

                        ConfigureFilePath.MesPath = ConfigureFilePath.projectItemPath + "\\" + "MesDir";
                        if (!Directory.Exists(ConfigureFilePath.MesPath))
                        {
                            Directory.CreateDirectory(ConfigureFilePath.MesPath);
                            Globals.LogRecord("【MesDir文件夹创建！】", true);
                        }

                        return;
                    }

                    AAAA.ShowErrorDialog("工程目录及项目文件夹不存在,打开失败，请检查");
                    Globals.LogRecord("【工程目录及项目文件夹不存在,打开失败，请检查！】", true);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("工程目录及文件创建失败,请检查");
                return;
            }


        }

        //高精度计时器（如果存在这样的计时器）
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceCounter(ref long x);
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceFrequency(ref long x);

        public static void Delay_ms(long delay_Time)
        {
            long stop_Value = 0;
            long start_Value = 0;
            long freq = 0;
            long stamp_Value = 0;

            QueryPerformanceFrequency(ref freq);  //获取CPU频率
            long delay_count = delay_Time * freq / 1000;   //这里写成1000000就是微秒，写成1000就是毫秒
            QueryPerformanceCounter(ref start_Value); //获取初始前值

            do
            {
                QueryPerformanceCounter(ref stop_Value);//获取终止变量值
                stamp_Value = stop_Value - start_Value;

            }
            while (stamp_Value < delay_count);
        }

        /// <summary>
        /// 深度复制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T Copy<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制   
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }

        public static void WriteProLog(string str)
        {
            if (!Directory.Exists(ConfigureFilePath.Dir_Record_Alarm))
                Directory.CreateDirectory(ConfigureFilePath.Dir_Record_Alarm);

            var tempPath = ConfigureFilePath.Dir_Record_Alarm + DateTime.Now.ToString("yyyyMMdd") + ".csv";

            if (!File.Exists(tempPath))
            {
                CsvServer.Instance.WriteLine(tempPath, "AlarmTimes,AlarmName,AlarmType,AlarmLevel,MaintenanceInstructions");
            }

            string line = str;// alarmLog.AlarmTimes + "," + alarmLog.AlarmName + "," + alarmLog.AlarmType + "," + alarmLog.AlarmLevel + "," + alarmLog.MaintenanceInstructions;
            CsvServer.Instance.WriteLine(tempPath, line);
        }
        public static void LogRecord(string data, bool isWrite = true)
        {
            string LogPath= ConfigureFilePath.Dir_Record_TaskLog + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\";
            if (!System.IO.Directory.Exists(LogPath))
            {
                System.IO.Directory.CreateDirectory(LogPath);
            }
            if (Globals.inforShowEvent != null) Globals.inforShowEvent(null, data);
            if (isWrite)
            {

                CsvServer.Instance.WriteLine(LogPath + "TaskLog" + DateTime.Now.ToString("yyyyMMdd_HH") + ".txt",
                    DateTime.Now + " " + data);
            }
        }
        public static void LogState(Color data, bool isWrite = true)
        {
            if (Globals.inforShowState != null) Globals.inforShowState(null, data);

        }
        public static void LogPlcState(string PLC, Color data)
        {
            if (Globals.inforShowActionState != null) Globals.inforShowActionState(PLC, data);

        }

        #region DispMyRegion
        public static void DispRecordNum(ICogImage MyOutPutImage, CogToolBlock tbSelected, int dipNum = 1)
        {
            tbSelected.Inputs["Input1"].Value = MyOutPutImage;
            //this.cogRecordDisplay1.Image = MyOutPutImage;
            tbSelected.Run();
            if (tbSelected.CreateLastRunRecord().SubRecords.Count > 0)
            {
                ICogRecord DispalyRecord1 = tbSelected.CreateLastRunRecord().SubRecords[0];
                ICogRecord DispalyRecord = tbSelected.CreateLastRunRecord()
                    .SubRecords[tbSelected.CreateLastRunRecord().SubRecords.Count - 1];

                Globals.DispalyRecordNum(dipNum, DispalyRecord);
            }
        }
        #endregion




        public static void DispalyRecordNum(int DispNum, ICogRecord data)
        {
            if (Globals.inforShowActionDisp != null) Globals.inforShowActionDisp(DispNum, data);
        }

        public static void DispalyRecord(ICogRecord data)
        {
            if (Globals.inforShowRecod != null) Globals.inforShowRecod(null, data);
        }
        public static void FormErrorLogger(string formPosition, string message)
        {
            CsvServer.Instance.WriteLine(ConfigureFilePath.Dir_Record_FormLog + "ErrorLog" + DateTime.Now.ToString("yyyyMMdd") + ".txt",
                   DateTime.Now + "——（" + formPosition + "）" + message);
        }

        public static string HttpPost(string url, string data)
        {
            string responseContent = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                byte[] bs = Encoding.UTF8.GetBytes(data);
                request.ContentType = "application/x-www-form-urlencoded";//application/json
                request.ContentLength = bs.Length;
                request.Method = "POST";
                request.Timeout = 5000;

                request.GetRequestStream().Write(bs, 0, bs.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                responseContent = streamReader.ReadToEnd();
                streamReader.Close();

                response.Close();
                request.Abort();
            }
            catch
            {
                responseContent = "";
            }
            return responseContent;
        }
        public static void InitMainFrm()
        {
            var frm = MainForm.Instance;
            frm.ShowDialog();
        }
        /// <summary>
        /// ping主机
        /// </summary>
        /// <returns></returns>
        public static void PingHost()
        {
            //string Error_Msg = "";
            //string[] _ServerIP = Globals.SettingOption.URL.Replace("http://", "").Split(':');
            //Ping m_ping = new Ping();
            //try
            //{
            //    PingReply reply = m_ping.Send(_ServerIP[0]);
            //    if (reply.Status == IPStatus.Success)
            //    {
            //        Error_Msg = "连接MES：" + Globals.SettingOption.URL + " 成功";
            //        LogRecord(Error_Msg);
            //    }
            //    else
            //    {
            //        Error_Msg = "连接MES：" + Globals.SettingOption.URL + " 失败,请检查！";
            //        LogRecord(Error_Msg);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Error_Msg = "连接MES：" + Globals.SettingOption.URL + " 失败,请检查!" + "\r\n" + ex.Message;

            //}

        }
        //（上相机实际-上相机模板) -（下相机实际（旋转后）-下相机模板）
        //角度偏移R=(上相机实际R-上相机模板R）- (下相机实际R-下相机模板R)
        //下相机实际（旋转后）=下相机实际   旋转->   角度偏移R
        #region MyRegion

        /// <summary>
        /// 组合URL,添加UrlDefaultAddress
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        //public static string BuildUrlForFip(string url)
        //{
        //    //return string.Format(url, ConfigureUrl.UrlDefaultAddress);
        //}

        /// <summary>
        /// 组合URL,添加UrlDefaultAddress与serialNumber
        /// </summary>
        /// / <param name="url"></param>
        /// <returns></returns>
        //public static string BuildUrlForFipWithSerialNumber(string url)
        //{
        //    return string.Format(url, ConfigureUrl.UrlDefaultAddress, Globals.serialNumber);
        //}
        //public static Point GetPointValue(_Point pointName)
        //{

        //    var mPoint = Globals._pointList.Find(t => t.Name == pointName.ToString());
        //    if (mPoint != null)
        //    {
        //        return mPoint;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="X_Mark"></param>
        /// <param name="Y_Mark"></param>
        /// <param name="X_Cen"></param>
        /// <param name="Y_Cen"></param>
        /// <param name="Angle_Rota_Deg"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public static void Rota_Cen_XY_VB(double X_Mark, double Y_Mark, double X_Cen, double Y_Cen, double Angle_Rota_Deg, ref double X, ref double Y)
        {
            //Angle_Rota_Deg = DegToRad(Angle_Rota_Deg);
            X = Math.Cos(Angle_Rota_Deg) * (X_Mark - X_Cen) - Math.Sin(Angle_Rota_Deg) * (Y_Mark - Y_Cen) + X_Cen;
            Y = Math.Cos(Angle_Rota_Deg) * (Y_Mark - Y_Cen) + Math.Sin(Angle_Rota_Deg) * (X_Mark - X_Cen) + Y_Cen;
        }
        /// <summary>
        /// 定位计算
        /// </summary>
        /// <param name="DownX">下相机拍照识别X</param>
        /// <param name="DownY">下相机拍照识别Y</param>
        /// <param name="upR">上相机识别R</param>
        /// <param name="upModleR">上相机模板R</param>
        /// <param name="downR">下相机识别R</param>
        /// <param name="downModleR">下相机模板R</param>
        /// <param name="CircleCentreX">下相机旋转中心X</param>
        /// <param name="CircleCentreY">下相机旋转中心Y</param>
        /// <param name="UPX">上相机识别X</param>
        /// <param name="UPModleX">上相机模板X</param>
        /// <param name="UPY">上相机识别Y</param>
        /// <param name="UPModleY">上相机模板Y</param>
        /// <param name="DownModleX">下相机模板X</param>
        /// <param name="DownModleY">下相机模板Y</param>
        /// <param name="ActAsemX">输出组装位X</param>
        /// <param name="ActAsemY">输出组装位Y</param>
        /// <param name="ActAsemR">输出组装位R</param>
        /// <param name="AsemModleX">模板组装位X</param>
        /// <param name="AsemModleY">模板组装位Y</param>
        /// <param name="AsemModleR">模板组装位R</param>
        /// <param name="offset_X">偏移补偿X</param>
        /// <param name="offset_Y">偏移补偿Y</param>
        /// <param name="offset_R">偏移补偿R</param>
        /// <param name="isConst">是否输出固定定位数据</param>
        public static void Calcl(double DownX, double DownY, double upR, double upModleR, double downR, double downModleR, double CircleCentreX, double CircleCentreY, double UPX, double UPModleX, double UPY, double UPModleY, double DownModleX, double DownModleY, out double ActAsemX, out double ActAsemY, out double ActAsemR, double AsemModleX = 0, double AsemModleY = 0, double AsemModleR = 0, double offset_X = 0, double offset_Y = 0, double offset_R = 0, bool isConst = false)
        {
            double x = 0;
            double y = 0;
            double r = 0;

            double offset_rotation_r = (upR - upModleR) - (downR - downModleR);
            Rota_Cen_XY_VB(DownX, DownY, CircleCentreX, CircleCentreY, offset_rotation_r, ref x, ref y);
            if (isConst)
            {
                ActAsemX = AsemModleX;
                ActAsemY = AsemModleY;
                ActAsemR = AsemModleR;
            }
            else
            {
                ActAsemX = AsemModleX + (UPX - UPModleX) - (x - DownModleX) + offset_X;
                ActAsemY = AsemModleY + (UPY - UPModleY) - (y - DownModleY) + offset_Y;
                ActAsemR = AsemModleR + (upR - upModleR) - (downR - downModleR) + offset_R;
            }
        }
        /// <summary>
        /// 定位计算Pro
        /// </summary>
        /// <param name="UpAuto">上相机自动数据</param>
        /// <param name="UpModle">上相机模板数据</param>
        /// <param name="DownAuto">下相机自动数据</param>
        /// <param name="DownModle">下相机模板数据</param>
        /// <param name="CircleCentre">旋转中心数据</param>
        /// <param name="Offset">偏移数据</param>
        /// <param name="ActAsem">实际组装结果输出数据</param>
        public static void CalclPro(double[] UpAuto, double[] UpModle, double[] DownAuto, double[] DownModle, double[] CircleCentre, double[] Offset, ref double[] ActAsem)
        {
            double x = 999;
            double y = 999;
            double r = 999;

            double offset_rotation_r = UpAuto[2] - UpModle[2];//(UpAuto[2] - UpModle[2]) - (DownAuto[2] - DownModle[2]);
            //Rota_Cen_XY_VB(DownAuto[0], DownAuto[1], CircleCentre[0], CircleCentre[1], offset_rotation_r, ref x, ref y);

            Rota_Cen_XY_VB(DownModle[0], DownModle[1], CircleCentre[0], CircleCentre[1], offset_rotation_r, ref x, ref y);
            double tempR = RadToDeg(offset_rotation_r);
            ActAsem[0] = (UpAuto[0] - UpModle[0]) * (-1) + Globals.SettingOption.OffsetX;//- (x - DownModle[0]) + Offset[0]);
            ActAsem[1] = (UpAuto[1] - UpModle[1]) + Globals.SettingOption.OffsetY;//- (y - DownModle[1]) + Offset[1]);
            ActAsem[2] = 0;// tempR + Offset[2];//(UpAuto[2] - UpModle[2]) - (DownAuto[2] - DownModle[2])


            Globals.LogRecord("【" + "自动识别UpAutoX:    " + UpAuto[0].ToString() + "       模板UpModleX:       " + UpModle[0].ToString() + "！】");//++++++++++
            Globals.LogRecord("【" + "自动识别UpAutoY:    " + UpAuto[1].ToString() + "       模板UpModleY:       " + UpModle[1].ToString() + "！】");//++++++++++

            Globals.LogRecord("【" + "自动识别DownAutoX:  " + DownAuto[0].ToString() + "       模板DownAutoY:       " + DownModle[0].ToString() + "！】");//++++++++++
            Globals.LogRecord("【" + "自动识别DownAutoY:  " + DownAuto[1].ToString() + "       模板DownAutoY:       " + DownModle[1].ToString() + "！】");//++++++++++

            Globals.LogRecord("【" + "下相机旋转后X:      " + x.ToString() + "       下相机旋转后Y：       " + y.ToString() + "！】");//++++++++++
            Globals.LogRecord("【" + "偏移旋转角度R:      " + tempR.ToString() + "！】");//++++++++++

        }
        public static double RadToDeg(double Rad)
        {
            return (Rad / Math.PI * 180);
        }
        public static double DegToRad(double Deg)
        {
            return (Deg / 180 * Math.PI);
        }
    }
    /// <summary>
    /// 全局事件，启动停止，暂停继续，初始化，复位
    /// </summary>
    public class GlobalMainFlow : PubSubEvent<string> { }
    /// <summary>
    /// 全局事件，启动停止，暂停继续，初始化，复位
    /// </summary>
    public class GlobalControlEvent : PubSubEvent<Controller> { }
    /// <summary>
    /// 全局控制    
    /// </summary>  
    public enum Controller
    {
        none,
        /// <summary>   
        /// 初始化
        /// </summary>
        Initialize,
        /// <summary>
        /// 复位
        /// </summary>
        Home,
        /// <summary>
        /// 启动
        /// </summary>
        Start,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 继续
        /// </summary>
        Continue,
        /// <summary>
        /// 停止
        /// </summary>
        Stop,
        /// <summary>
        /// 运行一行
        /// </summary>
        RunRow,
        /// <summary>
        /// 运行选中流程
        /// </summary>
        RunFlow
    }

    //产品管理相关事件,创建，打开，保存，另存，添加变量，删除变量，编辑变量
    public class ProductEvent : PubSubEvent<string> { }
    public class CommunicationEventRev : PubSubEvent<string> { }
    public class CommunicationEventSerialRev : PubSubEvent<string> { }

    public class Yeild : PubSubEvent<int> { }

    public class YeildNG : PubSubEvent<int> { }
    public class YeildResault : PubSubEvent<bool> { }
}
