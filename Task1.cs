using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ToolBlock;
using EventMgrLib;
using HslCommunication;
using MyMelsecHelper;
using TouchSocket.Core.ByteManager;
using TouchSocket.Sockets;
using VisionProHelper;
using Sunny.UI;
using System.Windows.Forms;
using csv;
using MyCalibrationHelper;
using ApeFree.DataStore.Local;
using ApeFree.DataStore.Adapters;
using ApeFree.DataStore.Core;
using ApeFree.DataStore;
using System.Diagnostics;
using HslCommunication.Profinet.Melsec;

namespace sunyvpp
{
    public class Task1
    {
        public static readonly Lazy<Task1> instance = new Lazy<Task1>(() => new Task1());
        private Task1() { }
        public static Task1 Instance { get; } = instance.Value;
        public static int PickAssembNum = 1;
        public static string ReceiverRobotStr = "";
        public static string ReceiverRobotStrValue = "";

        public static void ReceiveCommunicationSerialEventRev(string LastReciveMessage)
        {

        }
        public static void ReceiveCommunicationEventRev(string LastReciveMessage)
        {
            Globals.ReceiveDone = false;
            Globals.MyReciveStr = "";
            ReceiverRobotStrValue = "";
            if (LastReciveMessage.Contains(";"))
            {
                string[] a = LastReciveMessage.Split(";");
                if (a.Length >= 3)
                {
                    ReceiverRobotStrValue = a[2].Replace("]", "").Trim();
                    Globals.MyReciveStr = a[2].Replace("]", "").Trim();
                    Globals.ReceiveDone = true;
                    //Globals.LogRecord("【服务器接收数据：" + Globals.stripClient + "：" + "true" + "】",false);
                }
            }
            //Globals.LogRecord("【服务器接收数据：" + Globals.stripClient + "：" + LastReciveMessage + "】");
        }
        public static double[] UpCamData = new double[3];
        public static double[] DownCamData = new double[3];
        public static double[] offsetTemp = new double[3];
        public static double[] AssembleResault = new double[3];
        private static void MyRunMethod(ICogImage MyOutPutImage, CogToolBlock tbSelected, out double valuex, out double valuey, out double valuer, out bool valuere, int dipNum = 1)
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
                    //Globals.DispalyRecordNum(4, DispalyRecord);
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

        public static void DownCameraPhoto(string data, ICogImage MyOutPutImage, CogToolBlock tempToolblock, out double pix_X, out double pix_Y, out double r, out bool re)
        {
            string[] strData = data.Split('_');
            string dataName = strData[0];
            string dataNum = strData[1];
            string sendContHead = dataName + "_" + dataNum + "_";
            string[] dataPos = strData[2].Split(',');
            NewRunMethod(MyOutPutImage, sendContHead, tempToolblock, out pix_X, out pix_Y, out r, out re, true, 2);
            DownCamData[0] = pix_X;
            DownCamData[1] = pix_Y;
            DownCamData[2] = r;
        }
        public static void DownCameraPhoto(ICogImage MyOutPutImage, CogToolBlock tempToolblock, out double pix_X, out double pix_Y, out double r, out bool re)
        {
            MyRunMethod(MyOutPutImage, tempToolblock, out pix_X, out pix_Y, out r, out re, 2);
            DownCamData[0] = pix_X;
            DownCamData[1] = pix_Y;
            DownCamData[2] = r;
        }




        public static void UpCameraInspectPhoto(string data, ICogImage MyOutPutImage, CogToolBlock tempToolblock, out double pix_X, out double pix_Y, out double r, out bool re)
        {
            string[] strData = data.Split('_');
            string dataName = strData[0];
            string dataNum = strData[1];
            string sendContHead = dataName + "_" + dataNum + "_";
            string[] dataPos = strData[2].Split(',');
            NewRunMethod(MyOutPutImage, sendContHead, tempToolblock, out pix_X, out pix_Y, out r, out re, true, 1);
            if (re)
            {
                Globals.LogRecord("【上相机检测成功！" + pix_X.ToString() + pix_Y.ToString() + r.ToString() + "】");
            }
            else
            {
                Globals.LogRecord("【上相机检测失败！" + "】");
            }

        }
        public static void UpCameraInspectPhoto(ICogImage MyOutPutImage, CogToolBlock tempToolblock, out double pix_X, out double pix_Y, out double r, out bool re)
        {
            MyRunMethod(MyOutPutImage, tempToolblock, out pix_X, out pix_Y, out r, out re, 1);
            if (re)
            {
                Globals.LogRecord("【上相机检测成功！" + pix_X.ToString() + pix_Y.ToString() + r.ToString() + "】");
            }
            else
            {
                Globals.LogRecord("【上相机检测失败！" + pix_X.ToString() + pix_Y.ToString() + r.ToString() + "】");
            }

        }
        public static void UpCameraAssemPhoto(string data, ICogImage MyOutPutImage, CogToolBlock tempToolblock, out double pix_X, out double pix_Y, out double r, out bool re)
        {
            string[] strData = data.Split('_');
            string dataName = strData[0];
            string dataNum = strData[1];
            string sendContHead = dataName + "_" + dataNum + "_";
            string[] dataPos = strData[2].Split(',');
            string resaultSend = "NG";
            NewRunMethod(MyOutPutImage, sendContHead, tempToolblock, out pix_X, out pix_Y, out r, out re, false, 3);
            UpCamData[0] = pix_X;
            UpCamData[1] = pix_Y;
            UpCamData[2] = r;
            bool valuere = (bool)tempToolblock.Outputs["Re"].Value;
            Globals.LogRecord("【视觉工程" + "b" + "CaliBLS_1_1,2,3_OK！】");
            if (valuere)
            {
                resaultSend = "OK";
            }
            else
            {
                resaultSend = "NG";
            }
            offsetTemp[0] = Globals.SettingOption.OffsetX;
            offsetTemp[1] = Globals.SettingOption.OffsetY;
            offsetTemp[2] = Globals.SettingOption.OffsetR;
            AssembleResault[0] = 999999;
            AssembleResault[1] = 999999;
            AssembleResault[2] = 999999;
            switch (dataNum)
            {
                case "3":
                    CalcPro1(Globals.store.Value.UpCamFhotoPos1, AssembleResault);
                    break;
                case "4":
                    CalcPro1(Globals.store.Value.UpCamFhotoPos2, AssembleResault);
                    break;
                case "5":
                    CalcPro1(Globals.store.Value.UpCamFhotoPos3, AssembleResault);
                    break;
                case "6":
                    CalcPro1(Globals.store.Value.UpCamFhotoPos4, AssembleResault);
                    break;

            }
            string joinstrSend = sendContHead + "," + AssembleResault[0].ToString() + "," + AssembleResault[0].ToString() + "," + AssembleResault[0].ToString() + "_" + resaultSend;
            Globals.LogRecord("【" + joinstrSend + "！】");
            Globals.MyTcp.sendStr(joinstrSend);
        }

        public static void UpCameraAssemPhoto(int dataNum, ICogImage MyOutPutImage, CogToolBlock tempToolblock, out double pix_X, out double pix_Y, out double r, out bool re)
        {
            string resaultSend;
            MyRunMethod(MyOutPutImage, tempToolblock, out pix_X, out pix_Y, out r, out re, 3);
            UpCamData[0] = pix_X;
            UpCamData[1] = pix_Y;
            UpCamData[2] = r;
            bool valuere = (bool)tempToolblock.Outputs["Re"].Value;

            if (valuere)
            {
                resaultSend = "OK";
                Globals.LogRecord("【视觉工程运行OK！】");
            }
            else
            {
                resaultSend = "NG";
                Globals.LogRecord("【视觉工程运行NG！】");
            }
            offsetTemp[0] = Globals.SettingOption.OffsetX;
            offsetTemp[1] = Globals.SettingOption.OffsetY;
            offsetTemp[2] = Globals.SettingOption.OffsetR;
            AssembleResault[0] = 999999;
            AssembleResault[1] = 999999;
            AssembleResault[2] = 999999;
            switch (dataNum)
            {
                case 1:
                    CalcPro1(Globals.store.Value.UpCamFhotoPos1, AssembleResault);
                    break;
                case 2:
                    CalcPro1(Globals.store.Value.UpCamFhotoPos2, AssembleResault);
                    break;
                case 3:
                    CalcPro1(Globals.store.Value.UpCamFhotoPos3, AssembleResault);
                    break;
                case 4:
                    CalcPro1(Globals.store.Value.UpCamFhotoPos4, AssembleResault);
                    break;

            }
            string joinstrSend = "上相机拍照位置" + dataNum.ToString() + "," + UpCamData[0].ToString() + "," + UpCamData[1].ToString() + "," + UpCamData[2].ToString() + "_" + resaultSend;
            Globals.LogRecord("【" + joinstrSend + "！】");
        }


        /// <summary>
        /// 定位加强版1
        /// </summary>
        /// <param name="UpCamFhotoPos1">上相机模板位置</param>
        /// <param name="ModelAssemblyPos1">组装模板数据</param>
        /// <param name="AssembleResaultTemp">组装结果输出数据</param>
        public static void CalcPro1(double[] UpCamFhotoPos1, double[] AssembleResaultTemp)
        {
            offsetTemp[0] = Globals.SettingOption.OffsetX;
            offsetTemp[1] = Globals.SettingOption.OffsetY;
            offsetTemp[2] = Globals.SettingOption.OffsetR;
            AssembleResaultTemp[0] = 999999;
            AssembleResaultTemp[1] = 999999;
            AssembleResaultTemp[2] = 999999;
            Globals.CalclPro(UpCamData, UpCamFhotoPos1, DownCamData, Globals.store.Value.DownCamModel,
                Globals.store.Value.DownCamCen, offsetTemp, ref AssembleResaultTemp);
            Globals.LogRecord("【视觉补偿数据X:" + AssembleResaultTemp[0].ToString() +
                              "Y:      " + AssembleResaultTemp[1].ToString() +
                              "R:      " + AssembleResaultTemp[2].ToString() + "】");
        }
        public static RotateCalib rotateCalib = new RotateCalib();
        public static void CalibrateCalcRotation(string data, ICogImage MyOutPutImage, CogToolBlock tempToolblock, out double pix_X, out double pix_Y, out double r, out bool re)
        {
            string[] strData = data.Split('_');
            string dataName = strData[0];
            string dataNum = strData[1];
            string sendContHead = dataName + "_" + dataNum + "_";
            string[] dataPos = strData[2].Split(',');
            NewRunMethod(MyOutPutImage, sendContHead, tempToolblock, out pix_X, out pix_Y, out r, out re);
            if (dataNum == "10")
            {
                rotateCalib.CamMotionPoints.Clear();
            }
            rotateCalib.CamMotionPoints.Add(new PointD(pix_X, pix_Y));
            if (dataNum == "12")
            {
                rotateCalib.CamCenterPoint = MyRotateCalib.GetRCenter(rotateCalib.CamMotionPoints.ToArray(), out rotateCalib.CamRadio);
                LoadRotationVisionParam();
                Globals.store.Value.DownCamCen[0] = rotateCalib.CamCenterPoint.X;
                Globals.store.Value.DownCamCen[1] = rotateCalib.CamCenterPoint.Y;
                Globals.store.Value.DownCamCen[2] = rotateCalib.CamRadio;

                Globals.store.Save();
                Globals.LogRecord("【视觉参数数据保存成功！" + "】");
            }
        }

        public static void LoadRotationVisionParam()
        {
            // 本地存储配置（默认使用Json格式）
            var settings = new LocalStoreAccessSettings(ConfigureFilePath.projectItemPath + "/VisionParam.conf")
            {
                SerializationAdapter = new XmlSerializationAdapter()
            };
            // 本地存储器
            IStore<VisionDataParam> store = StoreFactory.Factory.CreateLocalStore<VisionDataParam>(settings);
            Globals.store = store;
            Globals.store.Load();

        }


        private static void NewRunMethod(ICogImage MyOutPutImage, string sendStr, CogToolBlock tbSelected, out double valuex, out double valuey, out double valuer, out bool valuere, bool IsSendData = false, int dipNum = 1)
        {
            tbSelected.Inputs["InputImage"].Value = MyOutPutImage;
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
                valuex = Convert.ToInt32(Math.Round(valuex, 3));
                valuey = Convert.ToInt32(Math.Round(valuey, 3));
                valuer = Convert.ToInt32(Math.Round(valuey, 3));


                if (valuere)
                {
                    resaultSend = "OK";
                }
                else
                {
                    resaultSend = "NG";
                }

                if (false)
                {
                    string joinstrSend = sendStr + valuex.ToString() + "," + valuey.ToString() + "," + valuer.ToString() + "_" + resaultSend;
                    if (IsSendData)
                    {
                        Globals.MyTcp.sendStr(joinstrSend);
                    }
                    Globals.LogRecord("【视觉工程" + joinstrSend + "CaliBLS_1_1,2,3_OK！】");
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
                string joinstrSend = sendStr + "," + valuex.ToString() + "," + valuey.ToString() + "," + valuer.ToString() + "_" + resaultSend;
                Globals.LogRecord("【" + joinstrSend + "！】");
                if (IsSendData)
                {
                    Globals.MyTcp.sendStr(joinstrSend);
                }
            }
        }

        public static string ReceiveComRS232 = "";


        public static void ShowReceiveMessage(object sender, string e)
        {
            ReceiveComRS232 = "";
            ReceiveComRS232 = e;
            Globals.LogRecord("【串口接收数据：" + ReceiveComRS232 + "】");
        }
        Bitmap bt;
        public ICogImage MyOutPutImage;
        public static double Pix_X = 0;
        public static double Pix_Y = 0;
        public static double Pix_R = 0;
        public static bool VisionReSault = false;
        public static bool VisionReSaultISNull = false;
        public void RunReadRobot()
        {
            
            Globals.varProDuctNum = 0;
            Globals.varProDuctNumOK = 0;
            Globals.varProDuctNumNG = 0;
            while (true)
            {
                //System.Threading.Thread.Sleep(10000);

                if (Globals.State == Controller.Start)
                {
                    switch (Globals.TaskStep)
                    {
                        case 10:
                            //打开上相机拍照光源
                            Globals.myLaserSerialPort.SendMessage("SA0255#");
                            Globals.MySetRobotGlobal("10");

                            WaitRobot("11");
                            bool pickDoneInspect = WaitRobot("11");
                            if (!WaitRobot("11"))
                            {
                                Globals.TaskStep = 150;
                                Globals.LogRecord("【Task1步序号10：等待机器人运动到拍照检测位置超时！" + "】", true);
                                Globals.State = Controller.Pause;
                                //写入PLC报警信息    
                                //OperateResult<int> a =
                                //    Globals.melsec_net.ReadInt32("D400");
                                Globals.melsec_net.Write("M1205", true);
                                Globals.melsec_net.Write("D402", 5);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1205", true, out string errMsg2);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 5, out string errMsg1);

                            }
                            else
                            {
                                Globals.TaskStep = 20;
                                Globals.LogRecord("【Task1步序号10：等待机器人运动到拍照检测位置成功！" + "】");
                            }
                            break;
                        //拍照检测
                        case 20:

                            Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose, out bt);
                            MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                            if (Globals.SettingOption.isVision)
                            {
                                UpCameraInspectPhoto(MyOutPutImage, Globals.UpCameraInspectToolblock, out Pix_X, out Pix_Y, out Pix_R,
                                    out VisionReSault);
                            }

                            Globals.DispRecordNum(MyOutPutImage, Globals.UpCameraInspectToolblock,2);
                            //WaitRobot("11");
                            //bool pickDoneInspect = WaitRobot("11");
                            //if (pickDoneInspect)
                            //{
                            //    Globals.TaskStep = 8000;
                            //    Globals.MySetRobotGlobal("8000");
                            //}
                            //Globals.TaskStep = 21;
                            //Globals.MySetRobotGlobal("20");

                            //if (Pix_R < -2)//机械手反转取料
                            //{
                            //    Globals.TaskStep = 8000;
                            //    Globals.MySetRobotGlobal("8000");
                            //}
                            //else
                            //{
                            //    Globals.TaskStep = 21;
                            //    Globals.MySetRobotGlobal("20");
                            //}

                            if (VisionReSault)
                            {

                                //Globals.TaskStep = 21;
                                Globals.MySetRobotGlobal("20");
                                WaitRobot("21");
                                if (Pix_R < -2)//机械手抛料
                                {
                                    Globals.TaskStep = 8000;
                                    Globals.MySetRobotGlobal("8000");

                                    //Globals.TaskStep = 21;
                                    //Globals.MySetRobotGlobal("20");
                                    //WaitRobot("21");
                                    //bool pickDone = WaitRobot("21");
                                    //if (pickDone)
                                    //{
                                    //    Globals.TaskStep = 8000;
                                    //    Globals.MySetRobotGlobal("8000");
                                    //}
                                }
                                else
                                {
                                    Globals.TaskStep = 21;
                                    //Globals.MySetRobotGlobal("20");
                                }
                            }
                            else
                            {
                                Globals.TaskStep = 21;
                                Globals.MySetRobotGlobal("20");
                                WaitRobot("21");
                                bool pickDone = WaitRobot("21");
                                if (!WaitRobot("21"))
                                {
                                    Globals.TaskStep = 8000;
                                    Globals.MySetRobotGlobal("8000");
                                }
                            }
                            Globals.myLaserSerialPort.SendMessage("SA0000#");
                            break;
                        //机器人到换向取料位置取料-机器人步序号【2000】
                        case 25:
                            if (!WaitRobot("2001"))
                            {
                                Globals.TaskStep = 260;
                                Globals.melsec_net.Write("M1216", true);
                                Globals.melsec_net.Write("D402", 16);

                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1216", true, out string errMsg2);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 16, out string errMsg1);
                                Globals.LogRecord("【Task1步序号25：换向取料手抓夹紧超时报警！" + "】", true);
                                Globals.State = Controller.Pause;
                            }
                            else
                            {
                                Globals.TaskStep = 30;
                                Globals.MySetRobotGlobal("30");
                                Globals.LogRecord("【Task1步序号25：换向取料手抓夹紧完成！" + "】", true);
                            }
                            break;
                        //机器人到取料位置取料-机器人步序号【20】
                        case 21:
                            if (!WaitRobot("21"))
                            {
                                Globals.TaskStep = 140;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1204", true, out string errMsg2);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 4, out string errMsg1);

                                Globals.LogRecord("【Task1步序号20：取料手抓夹紧超时报警！" + "】", true);
                                Globals.State = Controller.Pause;
                            }
                            else
                            {
                                Globals.TaskStep = 30;
                                Globals.MySetRobotGlobal("30");
                                Globals.LogRecord("【Task1步序号20：取料手抓夹紧完成！" + "】", true);
                            }
                            break;
                        //运动到下相机拍照位置
                        case 30:
                            if (WaitRobot("31"))
                            {
                                //打开下相机拍照光源
                                Globals.myLaserSerialPort.SendMessage("SB0255#");

                                //Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose1, out bt);
                                //MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                                //if (Globals.SettingOption.isVision)
                                //{
                                Globals.CameraDown.Capture_New(Globals.SettingOption.DownCamExpose1, out bt);
                                MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                                DownCameraPhoto(MyOutPutImage, Globals.AutoToolblock, out Pix_X, out Pix_Y, out Pix_R,
                                    out VisionReSault);

                                //    if (VisionReSault)
                                //    {
                                //        switch (PickAssembNum)
                                //        {
                                //            case 1:
                                //                Globals.TaskStep = 40;
                                //                Globals.MySetRobotGlobal("40");
                                //                break;
                                //            case 2:
                                //                Globals.TaskStep = 50;
                                //                Globals.MySetRobotGlobal("50");
                                //                break;
                                //            case 3:
                                //                Globals.TaskStep = 60;
                                //                Globals.MySetRobotGlobal("60");
                                //                break;
                                //            case 4:
                                //                Globals.TaskStep = 70;
                                //                Globals.MySetRobotGlobal("70");
                                //                break;
                                //        }
                                //    }
                                //    else
                                //    {
                                //        Globals.TaskStep = 8000;
                                //        Globals.MySetRobotGlobal("8000");
                                //    }
                                //}
                                if (true)//else
                                {
                                    switch (PickAssembNum)
                                    {
                                        case 1:
                                            Globals.TaskStep = 40;
                                            Globals.MySetRobotGlobal("40");
                                            break;
                                        case 2:
                                            Globals.TaskStep = 50;
                                            Globals.MySetRobotGlobal("50");
                                            break;
                                        case 3:
                                            Globals.TaskStep = 60;
                                            Globals.MySetRobotGlobal("60");
                                            break;
                                        case 4:
                                            Globals.TaskStep = 70;
                                            Globals.MySetRobotGlobal("70");
                                            break;
                                    }
                                }
                                Globals.myLaserSerialPort.SendMessage("SB0000#");
                                break;
                            }
                            else
                            {
                                Globals.TaskStep = 160;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1206", true, out string errMsg2);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 6, out string errMsg1);

                                Globals.State = Controller.Pause;
                                Globals.LogRecord("【Task1步序号30：等待机器人运动到下相机拍照位置超时！" + "】", true);
                            }
                            break;

                        case 40://运动到上相机拍照位置1【40】
                                //读取倍速链就绪
                                //读取寄存器
                            Globals.LogRecord("【Task1步序号40：运动到上相机拍照位置1，等待倍速链就绪！" + "】", true);
                            while (true)
                            {
                                OperateResult<bool> ContentData =
                                    Globals.melsec_net.ReadBool("M6");
                                bool M6 = ContentData.Content;
                                if (M6)
                                {
                                    break;
                                }
                                //if (MyMelsecHelper.HslUtils.ReadResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M6", out bool ContentData, out string errMsg40))
                                //{
                                //    bool M6 = ContentData;
                                //    if (M6)
                                //    {
                                //        break;
                                //    }
                                //}
                            }
                            Globals.myLaserSerialPort.SendMessage("SA0255#");
                            if (WaitRobot("41"))
                            {
                                if (Globals.SettingOption.isVision)
                                {
                                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose1, out bt);
                                    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                                    UpCameraAssemPhoto(1, MyOutPutImage, Globals.UpCameraAssemToolblock, out Pix_X, out Pix_Y, out Pix_R,
                                        out VisionReSault);
                                    Task.Run(() =>
                                    {
                                        if (Globals.SettingOption.isSaveImageNG)
                                        {
                                            if (!VisionReSault)
                                            {
                                                Globals.SaveScreenImageInputImage(bt, ConfigureFilePath.Station1SavePathNG);
                                            }
                                        }
                                    });
                                    double tempx = 0;
                                    double tempy = 0;
                                    double tempr = 0;
                                    Globals.RunInsPect(MyOutPutImage, Globals.UpCameraInsFlowToolblock, out tempx, out tempy, out tempr,
                                        out VisionReSaultISNull);
                                    Globals.myLaserSerialPort.SendMessage("SA0000#");
                                    if (Globals.SettingOption.isNoProduct)
                                    {
                                        VisionReSault = VisionReSaultISNull && VisionReSault;
                                    }

                                    if (VisionReSault)
                                    {
                                        if (Globals.SettingOption.isCorid)
                                        {
                                            Globals.MySetRobotOffsetX(AssembleResault[0].ToString());
                                            Globals.MySetRobotOffsetY(AssembleResault[1].ToString());
                                            //Globals.MySetRobotOffsetX(AssembleResault[2].ToString());
                                        }
                                        else
                                        {
                                            Globals.MySetRobotOffsetX("0");
                                            Globals.MySetRobotOffsetY("0");
                                            Globals.MySetRobotOffsetR("0");
                                        }
                                        Globals.TaskStep = 80;
                                        Globals.MySetRobotGlobal("80");
                                        EventMgr.Ins.GetEvent<YeildResault>().Publish(true);//false
                                        Globals.LogRecord("【Task1步序号40：上相机拍照位置1视觉拍照运行成功！" + "】", true);
                                    }
                                    else
                                    {
                                        //Globals.varProDuctNum += 1;
                                        Globals.varProDuctNumNG += 1;
                                        Globals.TaskStep = 50;
                                        Globals.MySetRobotGlobal("50");
                                        EventMgr.Ins.GetEvent<YeildResault>().Publish(false);
                                        //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1217", true, out string errMsg4);
                                        //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 17, out string errMsg1);
                                        //Globals.State = Controller.Pause;
                                        Globals.LogRecord("【Task1步序号40：上相机拍照位置1视觉拍照失败！" + "】", true);
                                    }
                                }
                                else
                                {
                                    Globals.TaskStep = 80;
                                    Globals.MySetRobotGlobal("80");
                                    Globals.LogRecord("【Task1步序号40：上相机拍照位置1视觉拍照运行成功！" + "】", true);
                                }
                            }
                            else
                            {
                                Globals.TaskStep = 180;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1208", true, out string errMsg3);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 8, out string errMsg1);
                                Globals.State = Controller.Pause;
                                Globals.LogRecord("【Task1步序号40：等待机器人运动到上相机拍照位置1超时！" + "】", true);
                            }

                            break;
                        //运动到上相机拍照位置2【50】
                        case 50:
                            //读取倍速链就绪
                            //读取寄存器
                            Globals.myLaserSerialPort.SendMessage("SA0255#");
                            Globals.LogRecord("【Task1步序号50：运动到上相机拍照位置2，等待倍速链就绪！" + "】", true);
                            while (true)
                            {
                                OperateResult<bool> ContentData =
                                    Globals.melsec_net.ReadBool("M6");
                                bool M6 = ContentData.Content;
                                if (M6)
                                {
                                    break;
                                }

                                //if (MyMelsecHelper.HslUtils.ReadResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M6", out bool ContentData, out string errMsg40))
                                //{
                                //    bool M6 = ContentData.c;
                                //    if (M6)
                                //    {
                                //        break;
                                //    }
                                //}
                            }
                            if (WaitRobot("51"))
                            {
                                if (Globals.SettingOption.isVision)
                                {
                                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose1, out bt);
                                    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                                    UpCameraAssemPhoto(2, MyOutPutImage, Globals.UpCameraAssemToolblock, out Pix_X, out Pix_Y, out Pix_R,
                                        out VisionReSault);

                                    Task.Run(() =>
                                    {
                                        if (Globals.SettingOption.isSaveImageNG)
                                        {
                                            if (!VisionReSault)
                                            {
                                                Globals.SaveScreenImageInputImage(bt, ConfigureFilePath.Station2SavePathNG);
                                                      }
                                        }
                                    });
                                    double tempx = 0;
                                    double tempy = 0;
                                    double tempr = 0;
                                    Globals.RunInsPect(MyOutPutImage, Globals.UpCameraInsFlowToolblock, out tempx, out tempy, out tempr,
                                        out VisionReSaultISNull);
                                    if (Globals.SettingOption.isNoProduct)
                                    {
                                        VisionReSault = VisionReSaultISNull && VisionReSault;
                                    }

                                    Globals.myLaserSerialPort.SendMessage("SA0000#");
                                    if (VisionReSault)
                                    {
                                        //Globals.MySetRobotOffsetX(AssembleResault[0].ToString());
                                        //Globals.MySetRobotOffsetX(AssembleResault[1].ToString());
                                        //Globals.MySetRobotOffsetX(AssembleResault[2].ToString());
                                        if (Globals.SettingOption.isCorid)
                                        {
                                            Globals.MySetRobotOffsetX(AssembleResault[0].ToString());
                                            Globals.MySetRobotOffsetY(AssembleResault[1].ToString());
                                            //Globals.MySetRobotOffsetX(AssembleResault[2].ToString());
                                        }
                                        else
                                        {
                                            Globals.MySetRobotOffsetX("0");
                                            Globals.MySetRobotOffsetY("0");
                                            Globals.MySetRobotOffsetR("0");
                                        }


                                        Globals.TaskStep = 90;
                                        Globals.MySetRobotGlobal("90");
                                        Globals.LogRecord("【Task1步序号50：上相机拍照位置2视觉拍照运行成功！" + "】", true);
                                        EventMgr.Ins.GetEvent<YeildResault>().Publish(true);//false
                                    }
                                    else
                                    {
                                        //Globals.varProDuctNum += 1;
                                        Globals.varProDuctNumNG += 1;
                                        Globals.TaskStep = 60;
                                        Globals.MySetRobotGlobal("60");
                                        //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1218", true, out string errMsg);
                                        //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 18, out string errMsg1);
                                        Globals.LogRecord("【Task1步序号50：上相机拍照位置2视觉拍照失败！" + "】", true);
                                        EventMgr.Ins.GetEvent<YeildResault>().Publish(false);//false
                                    }
                                }
                                else
                                {
                                    Globals.TaskStep = 90;
                                    Globals.MySetRobotGlobal("90");
                                    Globals.LogRecord("【Task1步序号50：上相机拍照位置2视觉拍照运行成功！" + "】", true);

                                }
                            }
                            else
                            {
                                Globals.TaskStep = 190;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1209", true, out string errMsg);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 9, out string errMsg1);
                                Globals.State = Controller.Pause;
                                Globals.LogRecord("【Task1步序号50：等待机器人运动到上相机拍照位置2超时！" + "】", true);
                            }
                            break;

                        //运动到上相机拍照位置3【60】
                        case 60:
                            //读取倍速链就绪
                            //读取寄存器
                            Globals.LogRecord("【Task1步序号60：运动到上相机拍照位置3，等待倍速链就绪！" + "】", true);
                            while (true)
                            {
                                OperateResult<bool> ContentData =
                                    Globals.melsec_net.ReadBool("M6");
                                bool M6 = ContentData.Content;
                                if (M6)
                                {
                                    break;
                                }



                                //if (MyMelsecHelper.HslUtils.ReadResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M6", out bool ContentData, out string errMsg40))
                                //{
                                //    bool M6 = ContentData;
                                //    if (M6)
                                //    {
                                //        break;
                                //    }
                                //}
                            }
                            //打开上相机拍照光源
                            Globals.myLaserSerialPort.SendMessage("SA0255#");
                            if (WaitRobot("61"))
                            {
                                if (Globals.SettingOption.isVision)
                                {
                                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose3, out bt);
                                    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                                    UpCameraAssemPhoto(3, MyOutPutImage, Globals.UpCameraAssemToolblockNew, out Pix_X, out Pix_Y, out Pix_R,
                                        out VisionReSault);
                                    Task.Run(() =>
                                    {
                                        if (Globals.SettingOption.isSaveImageNG)
                                        {
                                            if (!VisionReSault)
                                            {
                                                Globals.SaveScreenImageInputImage(bt, ConfigureFilePath.Station3SavePathNG);
                                            }
                                        }
                                    });
                                    double tempx = 0;
                                    double tempy = 0;
                                    double tempr = 0;
                                    Globals.RunInsPect(MyOutPutImage, Globals.UpCameraInsMonitorToolblock, out tempx, out tempy, out tempr,
                                        out VisionReSaultISNull);
                                    if (Globals.SettingOption.isNoProduct)
                                    {
                                        VisionReSault = VisionReSaultISNull && VisionReSault;
                                    }


                                    Globals.myLaserSerialPort.SendMessage("SA0000#");
                                    if (VisionReSault)
                                    {
                                        //Globals.MySetRobotOffsetX(AssembleResault[0].ToString());
                                        //Globals.MySetRobotOffsetX(AssembleResault[1].ToString());
                                        //Globals.MySetRobotOffsetX(AssembleResault[2].ToString());
                                        if (Globals.SettingOption.isCorid)
                                        {
                                            Globals.MySetRobotOffsetX(AssembleResault[0].ToString());
                                            Globals.MySetRobotOffsetY(AssembleResault[1].ToString());
                                            //Globals.MySetRobotOffsetX(AssembleResault[2].ToString());
                                        }
                                        else
                                        {
                                            Globals.MySetRobotOffsetX("0");
                                            Globals.MySetRobotOffsetY("0");
                                            Globals.MySetRobotOffsetR("0");
                                        }


                                        Globals.TaskStep = 100;
                                        Globals.MySetRobotGlobal("100");
                                        Globals.LogRecord("【Task1步序号60：上相机拍照位置3视觉拍照运行成功！" + "】", true);
                                        EventMgr.Ins.GetEvent<YeildResault>().Publish(true);//false
                                    }
                                    else
                                    {
                                        Globals.TaskStep = 70;
                                        Globals.MySetRobotGlobal("70");
                                        //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1219", true, out string errMsg);
                                        //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 19, out string errMsg1);
                                        Globals.LogRecord("【Task1步序号60：上相机拍照位置3视觉拍照失败！" + "】", true);
                                        //Globals.varProDuctNum += 1;
                                        Globals.varProDuctNumNG += 1;
                                        EventMgr.Ins.GetEvent<YeildResault>().Publish(false);//false
                                    }
                                }
                                else
                                {
                                    Globals.TaskStep = 100;
                                    Globals.MySetRobotGlobal("100");
                                    Globals.LogRecord("【Task1步序号60：上相机拍照位置3视觉拍照运行成功！" + "】", true);

                                }
                            }
                            else
                            {
                                Globals.TaskStep = 200;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1210", true, out string errMsg);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 10, out string errMsg1);
                                Globals.State = Controller.Pause;
                                Globals.LogRecord("【Task1步序号60：等待机器人运动到上相机拍照位置3超时！" + "】", true);
                            }
                            break;
                        //运动到上相机拍照位置4【70】
                        case 70:
                            //读取倍速链就绪
                            //读取寄存器
                            Globals.myLaserSerialPort.SendMessage("SA0255#");
                            Globals.LogRecord("【Task1步序号40：运动到上相机拍照位置4，等待倍速链就绪！" + "】", true);
                            while (true)
                            {
                                OperateResult<bool> ContentData =
                                    Globals.melsec_net.ReadBool("M6");
                                bool M6 = ContentData.Content;
                                if (M6)
                                {
                                    break;
                                }
                            }
                            if (WaitRobot("71"))
                            {
                                if (Globals.SettingOption.isVision)
                                {
                                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose4, out bt);
                                    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                                    UpCameraAssemPhoto(4, MyOutPutImage, Globals.UpCameraAssemToolblockNew, out Pix_X, out Pix_Y, out Pix_R,
                                        out VisionReSault);
                                    Task.Run(() =>
                                    {
                                        if (Globals.SettingOption.isSaveImageNG)
                                        {
                                            if (!VisionReSault)
                                            {
                                                Globals.SaveScreenImageInputImage(bt, ConfigureFilePath.Station4SavePathNG);
                                            }
                                        }
                                    });

                              
                                    double tempx = 0;
                                    double tempy = 0;
                                    double tempr = 0;
                                    Globals.RunInsPect(MyOutPutImage, Globals.UpCameraInsMonitorToolblock, out tempx, out tempy, out tempr,
                                        out VisionReSaultISNull);
                                    if (Globals.SettingOption.isNoProduct)
                                    {
                                        VisionReSault = VisionReSaultISNull && VisionReSault;
                                    }


                                    Globals.myLaserSerialPort.SendMessage("SA0000#");
                                    if (VisionReSault)
                                    {
                                        if (Globals.SettingOption.isCorid)
                                        {
                                            Globals.MySetRobotOffsetX(AssembleResault[0].ToString());
                                            Globals.MySetRobotOffsetY(AssembleResault[1].ToString());
                                            //Globals.MySetRobotOffsetX(AssembleResault[2].ToString());
                                        }
                                        else
                                        {
                                            Globals.MySetRobotOffsetX("0");
                                            Globals.MySetRobotOffsetY("0");
                                            Globals.MySetRobotOffsetR("0");
                                        }
                                        Globals.TaskStep = 110;
                                        Globals.MySetRobotGlobal("110");
                                        Globals.LogRecord("【Task1步序号70：上相机拍照位置4视觉拍照运行成功！" + "】", true);
                                        EventMgr.Ins.GetEvent<YeildResault>().Publish(true);//false
                                    }
                                    else
                                    {
                                        //Globals.varProDuctNum += 1;
                                        Globals.varProDuctNumNG += 1;
                                        EventMgr.Ins.GetEvent<YeildResault>().Publish(false);//false
                                        //全部组装完成M208
                                        Globals.melsec_net.Write("M208", true);
                                        Globals.LogRecord("【组装完成，M208=true！" + "】", true);
                                        while (true)
                                        {
                                            OperateResult<bool> a =
                                                Globals.melsec_net.ReadBool("M6");
                                            bool M6 = a.Content;
                                            if (!M6)
                                            {
                                                Globals.LogRecord("【倍速链准备就绪信号置0，M6=false！" + "】", true);
                                                Globals.melsec_net.Write("M208", false);
                                                Globals.LogRecord("【组装完成信号置0，M208=false！" + "】", true);
                                                break;
                                            }
                                            
                                        }
                                        PickAssembNum = 1;
                                        Globals.TaskStep = 40;
                                        Globals.MySetRobotGlobal("40");
                                        Globals.LogRecord("【Task1步序号70：上相机拍照位置4视觉拍照失败，到拍照位置1重新开始！" + "】", true);
                                    }
                                }
                                else
                                {
                                    Globals.TaskStep = 110;
                                    Globals.MySetRobotGlobal("110");
                                    Globals.LogRecord("【Task1步序号70：上相机拍照位置4视觉拍照运行成功！" + "】", true);
                                }
                            }
                            else
                            {
                                Globals.TaskStep = 210;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1211", true, out string errMsg);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 11, out string errMsg1);
                                Globals.State = Controller.Pause;
                                Globals.LogRecord("【Task1步序号70：等待机器人运动到上相机拍照位置4超时！" + "】", true);
                            }

                            break;

                        //运动到组装位置1【80】
                        case 80:
                            if (WaitRobot("81"))
                            {
                                Globals.TaskStep = 10;
                                Globals.MySetRobotGlobal("10");
                                PickAssembNum = 2;
                                Globals.LogRecord("【Task1步序号80：运动到组装位置1成功！" + "】", true);

                                Globals.varProDuctNumOK += 1;
                                //Globals.varProDuctNum += 1;
                                Globals.LogRecord("【Task1步序号80：运动到组装位置1产量：" + Globals.varProDuctNum.ToString()+ "】", true);
                                EventMgr.Ins.GetEvent<Yeild>().Publish(Globals.varProDuctNumOK);
                            }
                            else
                            {
                                Globals.TaskStep = 220;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1212", true, out string errMsg);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 12, out string errMsg1);
                                Globals.State = Controller.Pause;
                                Globals.LogRecord("【Task1步序号80：等待机器人运动到组装位置1超时！" + "】", true);
                            }
                            break;

                        //运动到组装位置2【90】
                        case 90:
                            if (WaitRobot("91"))
                            {
                                Globals.TaskStep = 10;
                                Globals.MySetRobotGlobal("10");
                                PickAssembNum = 3;
                                Globals.LogRecord("【Task1步序号90：运动到组装位置2成功！" + "】", true);

                                Globals.varProDuctNumOK += 1;
                                //Globals.varProDuctNum += 1;
                                Globals.LogRecord("【Task1步序号90：运动到组装位置2产量：" + Globals.varProDuctNum.ToString() + "】", true);
                                EventMgr.Ins.GetEvent<Yeild>().Publish(Globals.varProDuctNumOK);
                            }
                            else
                            {
                                Globals.TaskStep = 230;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1213", true, out string errMsg);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 13, out string errMsg1);
                                Globals.State = Controller.Pause;
                                Globals.LogRecord("【Task1步序号90：等待机器人运动到组装位置2超时！" + "】", true);
                            }
                            break;

                        //运动到组装位置3【100】
                        case 100:
                            if (WaitRobot("101"))
                            {
                                Globals.TaskStep = 10;
                                Globals.MySetRobotGlobal("10");
                                PickAssembNum = 4;

                                Globals.varProDuctNumOK += 1;
                                //Globals.varProDuctNum += 1;
                                Globals.LogRecord("【Task1步序号100：运动到组装位置3产量：" + Globals.varProDuctNum.ToString() + "】", true);
                                EventMgr.Ins.GetEvent<Yeild>().Publish(Globals.varProDuctNumOK);
                                Globals.LogRecord("【Task1步序号100：运动到组装位置3成功！" + "】", true);
                            }
                            else
                            {
                                Globals.TaskStep = 240;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1214", true, out string errMsg);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 14, out string errMsg1);
                                Globals.State = Controller.Pause;
                                Globals.LogRecord("【Task1步序号100：等待机器人运动到组装位置3超时！" + "】", true);
                            }
                            break;

                        //运动到组装位置4【110】
                        case 110:
                            if (WaitRobot("111"))
                            {
                                //全部组装完成M208
                                Globals.melsec_net.Write("M208", true);
                                Globals.LogRecord("【组装完成，M208=true！" + "】", true);
                                //Globals.melsec_net.Write("D402", 5);

                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M208", true, out string errMsg110);
                                ////读取倍速链就绪
                                while (true)
                                {
                                    OperateResult<bool> a =
                                        Globals.melsec_net.ReadBool("M6");
                                    bool M6 = a.Content;
                                    if (!M6)
                                    {
                                        Globals.LogRecord("【倍速链准备就绪信号置0，M6=false！" + "】", true);
                                        Globals.melsec_net.Write("M208", false);
                                        Globals.LogRecord("【组装完成信号置0，M208=false！" + "】", true);
                                        break;
                                    }
                                    //break;
                                    //if (MyMelsecHelper.HslUtils.ReadResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M6", out bool ContentData, out string errMsg40))
                                    //{
                                    //    bool M6 = ContentData;
                                    //    if (!M6)
                                    //    {
                                    //        MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M208", false, out string errMsg);
                                    //        break;
                                    //    }
                                    //}
                                }
                                Globals.TaskStep = 10;
                                Globals.MySetRobotGlobal("10");
                                PickAssembNum = 1;

                                Globals.varProDuctNumOK += 1;
                                //Globals.varProDuctNum += 1;
                                Globals.LogRecord("【Task1步序号110：运动到组装位置4产量：" + Globals.varProDuctNum.ToString() + "】", true);
                                EventMgr.Ins.GetEvent<Yeild>().Publish(Globals.varProDuctNumOK);
                                Globals.LogRecord("【Task1步序号110：运动到组装位置4成功！" + "】", true);
                            }
                            else
                            {
                                Globals.TaskStep = 250;
                                PickAssembNum = 1;
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1215", true, out string errMsg);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 15, out string errMsg1);
                                Globals.State = Controller.Pause;
                                Globals.LogRecord("【Task1步序号110：等待机器人运动到组装位置4超时！" + "】", true);
                            }
                            break;
                        //NG抛料位置
                        case 8000:
                            if (!WaitRobot("8001"))
                            {
                                Globals.TaskStep = 170;
                                //运动到NG抛料位置失败
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslBool, "M1207", true, out string errMsg);
                                //MyMelsecHelper.HslUtils.WriteResultRender(Globals.PLC_Key[0], HslDataType.hslInt16, "D402", 7, out string errMsg1);

                                Globals.LogRecord("【Task1步序号8000：运动到NG抛料位置超时报警！" + "】", true);
                                Globals.State = Controller.Pause;
                            }
                            else
                            {
                                Globals.TaskStep = 10;
                                Globals.LogRecord("【Task1步序号8000：运动到NG抛料位置流程完成！" + "】", true);
                            }
                            break;

                    }
                }

                if (Globals.State == Controller.Home)
                {
                    Globals.TaskStep = 10;
                    switch (Globals.TaskStep)
                    {
                        //取料手抓夹紧超时报警
                        case 140:
                            Globals.TaskStep = 21;
                            Globals.MySetRobotGlobal("20");
                            break;

                        case 150:
                        //下相机拍照位置到位超时报警
                        case 160:
                            Globals.TaskStep = 30;
                            Globals.MySetRobotGlobal("30");
                            break;
                        case 170:
                        //上相机1
                        case 180:
                            Globals.TaskStep = 40;
                            Globals.MySetRobotGlobal("40");
                            break;
                        //上相机2
                        case 190:
                            Globals.TaskStep = 50;
                            Globals.MySetRobotGlobal("50");
                            break;
                        //上相机3
                        case 200:
                            Globals.TaskStep = 60;
                            Globals.MySetRobotGlobal("60");
                            break;
                        //上相机4
                        case 210:
                            Globals.TaskStep = 70;
                            Globals.MySetRobotGlobal("70");
                            break;
                        //组装位未完成超时
                        case 220:
                        case 230:
                        case 240:
                        case 250:
                            Globals.TaskStep = 10;
                            Globals.MySetRobotGlobal("10");
                            break;
                        //换向取料手抓夹紧超时报警
                        case 260:
                            Globals.TaskStep = 25;
                            Globals.MySetRobotGlobal("2000");
                            break;
                        //上相机拍照位置1-4拍照失败  40-70
                        case 270:
                            Globals.TaskStep = 40;
                            break;
                        case 280:
                            Globals.TaskStep = 50;
                            break;
                        case 290:
                            Globals.TaskStep = 60;
                            break;
                        case 300:
                            Globals.TaskStep = 70;
                            break;
                    }
                }

            }
        }

        public bool WaitRobot(string value, int timeOut = 999000)
        {
            Stopwatch swSign = new Stopwatch();
            swSign.Reset();
            swSign.Start();
            while (true)
            {
                //拍照检测位置运动到位
                Globals.MyReadRobotGlobal();
                if (ReceiverRobotStrValue == value)
                {
                    break;
                }

                if (Globals.State == Controller.Stop)
                {
                    break;
                }
                //if (swSign.ElapsedMilliseconds >= timeOut)
                //{
                //    swSign.Stop();
                //    swSign.Reset();
                //    Thread.Sleep(10);
                //    return false;
                //    break;
                //}
                Thread.Sleep(10);
            }
            return true;
        }




        public void RunPLC1State()
        {
            int numState = -1;
            int D400 = -1;
            while (true)
            {
                if (Globals.State == Controller.Start)
                {
                    if (Globals.PLC_Key.Count > 0)
                    {
                        OperateResult<int> a =
                         Globals.melsec_net.ReadInt32("D400");

                        D400 = a.Content;

                        //读取寄存器
                        //if (MyMelsecHelper.HslUtils.ReadResultRender(Globals.PLC_Key[0], HslDataType.hslInt32, "D400", out int ContentData, out string errMsg))
                        //{
                        //    D400 = ContentData;
                        //}
                    }

                    switch (D400)
                    {
                        case 0://停止
                            if (numState != 0)
                                numState = 0;
                            Globals.LogRecord("【PLC1：停止状态" + "】", true);
                            Globals.LogPlcState("PLC1", Color.Yellow);
                            break;
                        case 1://运行
                            if (numState != 1)
                            {
                                numState = 1;
                                Globals.LogRecord("【PLC1：运行状态" + "】", true);
                                Globals.LogPlcState("PLC1", Color.Lime);
                            }
                            break;

                        case 2://调试
                            if (numState != 2)
                            {
                                numState = 2;
                                Globals.LogRecord("【PLC1：调试状态" + "】", true);
                                Globals.LogPlcState("PLC1", Color.Yellow);
                            }
                            break;
                        case 3://复位
                            if (numState != 3)
                            {
                                numState = 3;
                                Globals.LogRecord("【PLC1：复位状态" + "】", true);
                                Globals.LogPlcState("PLC1", Color.Yellow);
                            }
                            break;
                        case 4://暂停
                            if (numState != 4)
                            {
                                numState = 4;
                                Globals.LogRecord("【PLC1：暂停状态" + "】", true);
                                Globals.LogPlcState("PLC1", Color.Yellow);
                            }
                            break;
                        case 5://报警
                            if (numState != 5)
                            {
                                numState = 5;
                                Globals.LogRecord("【PLC1：报警状态" + "】", true);
                                Globals.LogPlcState("PLC1", Color.Red);
                                OperateResult<bool[]> alarm =
                                    Globals.melsec_net.ReadBool("M1201", 100);

                                //D400 = a.Content;
                                //OperateResult<bool[]> alarm = MyMelsecHelper.HslUtils.BulkReadBoolRenderResult(Globals.PLC_Key[0], "D1201", "100");
                                if (alarm.IsSuccess)
                                {
                                    Alarm.Main_PLC_Alarm(alarm.Content);
                                }
                                else
                                {
                                    Globals.LogRecord("【PLC1：报警信息读取失败" + "】", true);
                                }
                            }
                            break;
                        default:
                            Globals.LogRecord("【PLC1：程序错误,请联系PLC开发人员" + "】", true);
                            Globals.LogPlcState("PLC1", Color.Red);
                            break;
                    }
                    Thread.Sleep(500);
                }
            }
        }

        public static void RunPLC2State()
        {
            int numState = -1;
            string D400 = "-1";
            while (true)
            {
                if (Globals.State == Controller.Start)
                {
                    if (Globals.PLC_Key.Count > 0)
                    {
                        //读取寄存器
                        if (MyMelsecHelper.HslUtils.ReadResultRender(Globals.PLC_Key[0], HslDataType.hslString, "D400", out string ContentData, out string errMsg))
                        {
                            D400 = ContentData;
                        }
                    }
                    switch (D400)
                    {
                        case "0"://停止
                            if (numState != 0)
                                numState = 0;
                            Globals.LogRecord("【PLC2：停止状态" + "】", true);
                            Globals.LogPlcState("PLC2", Color.Yellow);
                            break;
                        case "1"://运行
                            if (numState != 1)
                            {
                                numState = 1;
                                Globals.LogRecord("【PLC2：运行状态" + "】", true);
                                Globals.LogPlcState("PLC2", Color.Lime);
                            }
                            break;

                        case "2"://调试
                            if (numState != 2)
                            {
                                numState = 2;
                                Globals.LogRecord("【PLC2：调试状态" + "】", true);
                                Globals.LogPlcState("PLC2", Color.Yellow);
                            }
                            break;
                        case "3"://复位
                            if (numState != 3)
                            {
                                numState = 3;
                                Globals.LogRecord("【PLC2：复位状态" + "】", true);
                                Globals.LogPlcState("PLC2", Color.Yellow);
                            }
                            break;
                        case "4"://暂停
                            if (numState != 4)
                            {
                                numState = 4;
                                Globals.LogRecord("【PLC2：暂停状态" + "】", true);
                                Globals.LogPlcState("PLC2", Color.Yellow);
                            }
                            break;
                        case "5"://报警
                            if (numState != 5)
                            {
                                numState = 5;
                                Globals.LogRecord("【PLC2：报警状态" + "】", true);
                                Globals.LogPlcState("PLC2", Color.Red);

                                OperateResult<bool[]> alarm = MyMelsecHelper.HslUtils.BulkReadBoolRenderResult(Globals.PLC_Key[0], "D1201", "100");
                                if (alarm.IsSuccess)
                                {
                                    Alarm.Main_PLC_Alarm(alarm.Content);
                                }
                                else
                                {
                                    Globals.LogRecord("【PLC1：报警信息读取失败" + "】", true);
                                }
                            }
                            break;
                        default:
                            Globals.LogRecord("【PLC2：程序错误,请联系PLC开发人员" + "】", true);
                            Globals.LogPlcState("PLC2", Color.Red);
                            break;
                    }
                    Thread.Sleep(500);
                }
            }
        }

    }

}



//140：	取料手抓夹紧超时报警                    M1204       D402=4
//150：	等待机器人运动到拍照检测位置超时        M1205       D402=5
//160：	下相机拍照位置到位超时报警              M1206       D402=6
//170：	NG位手抓打开超时报警                    M1207       D402=7
//180：	机器人运动到上相机拍照位置1超时         M1208       D402=8
//190：	机器人运动到上相机拍照位置2超时         M1209       D402=9
//200：	机器人运动到上相机拍照位置3超时         M1210       D402=10
//210：	机器人运动到上相机拍照位置4超时         M1211       D402=11
//220：	机器人运动到组装位置1超时               M1212       D402=12
//230：	机器人运动到组装位置2超时               M1213       D402=13
//240：	机器人运动到组装位置3超时               M1214       D402=14
//250：	机器人运动到组装位置4超时               M1215       D402=15
//260：	换向取料手抓夹紧超时报警                M1216       D402=16
//270：	上相机拍照位置1拍照失败                 M1217       D402=17
//280：	上相机拍照位置2拍照失败                 M1218       D402=18
//290：	上相机拍照位置3拍照失败                 M1219       D402=19
//300：	上相机拍照位置4拍照失败                 M1220       D402=20

