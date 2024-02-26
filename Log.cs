using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    class Log
    {

        //2022-09-18:增加十字显示功能。增加ini取料位置
        //2022/12/08：完善自动标定流程代码，核对自动标定地址，信号具体细节信息。编写PLC读写窗体代码，修改主窗体为UIForm风格，优化UI界面。
        //在信誉瑞通项目上测试标定算法。
        //2023-03-31:增加激光参数读取功能，增加串口通讯部分代码。安装工控机显卡。

        #region MyRegion客户端
        //public static void ReceivedHandler1<ITcpClient>(ITcpClient client, ByteBlock byteBlock,
        //    IRequestInfo requestInfo)
        //{
        //    ReceiverRobotStr = "";
        //    string LastReciveMessage = "";
        //    ReceiverRobotStr = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
        //    LastReciveMessage = Globals.MyTcp.tcpClient.RemoteIPHost + ": " + ReceiverRobotStr;
        //    Globals.LogRecord("【网口接收数据：" + LastReciveMessage + "】");
        //    string[] str = ReceiverRobotStr.Split('_');
        //    string[] data = str[2].Split(',');

        //    string value;
        //    ICogImage MyOutPutImage = null;
        //    Bitmap bt = null;
        //    int ExPose = 2000;

        //    Globals.CameraDown.Capture_New(ExPose, out bt);
        //    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
        //    double x, y, r, pix_X, pix_Y;
        //    bool re;
        //    if (str[0] == "CaliBLDownS")
        //    {
        //        if (str[3] == "OK")
        //        {
        //            try
        //            {
        //                switch (str[1])
        //                {
        //                    case "1":
        //                        InitNpointCalib();
        //                        CalibrateCalc(ReceiverRobotStr, MyOutPutImage, CalibToolblock, out pix_X, out pix_Y,
        //                            out r, out re);
        //                        if (!re)
        //                        {
        //                            Globals.LogRecord("【标定失败,请重新检查视觉算法！" + "】");
        //                            inforForm.ShowErrorDialog("【标定失败,请重新检查视觉算法！】");
        //                            return;
        //                        }
        //                        break;
        //                    case "2":
        //                    case "3":
        //                    case "4":
        //                    case "5":
        //                    case "6":
        //                    case "7":
        //                    case "8":
        //                    case "9":
        //                        CalibrateCalc(ReceiverRobotStr, MyOutPutImage, CalibToolblock, out pix_X, out pix_Y,
        //                            out r, out re);
        //                        if (!re)
        //                        {
        //                            Globals.LogRecord("【标定失败,请重新检查视觉算法！" + "】");
        //                            inforForm.ShowErrorDialog("【标定失败,请重新检查视觉算法！】");
        //                            return;
        //                        }
        //                        break;
        //                    case "10":
        //                    case "11":
        //                    case "12":
        //                        CalibrateCalcRotation(ReceiverRobotStr, MyOutPutImage, AutoToolblock, out pix_X,
        //                            out pix_Y,
        //                            out r, out re);
        //                        break;
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Globals.LogRecord("【标定失败,请重新标定！" + "】");
        //                inforForm.ShowErrorDialog("标定失败");
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            Globals.LogRecord("【机器人运动失败！" + "】");
        //            return;
        //        }
        //    }

        //    if (str[0] == "RunLUpS")
        //    {
        //        if (str[3] == "OK")
        //        {
        //            switch (str[1])
        //            {
        //                case "1":
        //                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose1, out bt);
        //                    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
        //                    UpCameraInspectPhoto(MyOutPutImage, UpCameraInspectToolblock, out pix_X, out pix_Y, out r,
        //                        out re);
        //                    UpCameraInspectPhoto(ReceiverRobotStr, MyOutPutImage, UpCameraInspectToolblock, out pix_X,
        //                        out pix_Y, out r,
        //                        out re);
        //                    break;
        //                case "2":
        //                    Globals.CameraDown.Capture_New(Globals.SettingOption.DownCamExpose1, out bt);
        //                    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
        //                    DownCameraPhoto(ReceiverRobotStr, MyOutPutImage, AutoToolblock, out pix_X, out pix_Y, out r,
        //                        out re);
        //                    break;
        //                case "3":
        //                case "4":
        //                case "5":
        //                case "6":
        //                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose2, out bt);
        //                    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
        //                    UpCameraAssemPhoto(ReceiverRobotStr, MyOutPutImage, UpCameraAssemToolblock, out pix_X,
        //                        out pix_Y, out r,
        //                        out re);
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            Globals.LogRecord("【机器人运动失败！" + "】");
        //        }
        //    }

        //    if (str[0] == "CaliBLS")
        //    {
        //        if (str[3] == "OK")
        //        {
        //            switch (str[1])
        //            {
        //                case "1":
        //                    Globals.Camera.Capture_New(Globals.SettingOption.UpCamExpose2, out bt);
        //                    MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
        //                    UpCameraCalibratePhoto(ReceiverRobotStr, MyOutPutImage, UpCameraAssemToolblock, out pix_X,
        //                        out pix_Y, out r,
        //                        out re);
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            Globals.LogRecord("【机器人运动失败！" + "】");
        //        }
        //    }
        //}
        #endregion












    }
}
