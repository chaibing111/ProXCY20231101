using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csv;
using Sunny.UI;
using System.Drawing;
using Newtonsoft.Json.Bson;
using MyCalibrationHelper;
using VisionProHelper;

namespace sunyvpp
{
    class TaskDownCamClib
    {
        public static readonly Lazy<TaskDownCamClib> instance = new Lazy<TaskDownCamClib>(() => new TaskDownCamClib());
        public TaskDownCamClib() { }


        public static TaskDownCamClib Instance { get; } = instance.Value;
        public static List<string[]> rowList = new List<string[]>();
        public static UIForm inforForm = new UIForm();
        public static CogCalibNPointToNPointTool Calib_tbCamCalib;
        public static CogCalibNPointToNPointTool Calib_tbRotCalib;
        public static double[] dataPosX = new double[9];
        public static double[] dataPosY = new double[9];
        public static void InitNpointCalib()
        {
            Calib_tbCamCalib = MyVisionProMgr.ins.CogToolBlockDic["TB_Down.vpp"].Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            Calib_tbRotCalib = MyVisionProMgr.ins.CogToolBlockDic["TB_CalibRot.vpp"].Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            Calib_tbCamCalib.Calibration.NumPoints = 9;
            Calib_tbRotCalib.Calibration.NumPoints = 9;
        }

        public static void CalibrateCalc(string dataNum, ICogImage MyOutPutImage, CogToolBlock tempToolblock, out double pix_X, out double pix_Y, out double r, out bool re)
        {
            Globals.RunTool(MyOutPutImage, tempToolblock, out pix_X, out pix_Y, out r, out re, 2);
            //像素坐标
            Calib_tbCamCalib.Calibration.SetUncalibratedPoint(Convert.ToInt16(dataNum) - 1, pix_X, pix_Y);

            Calib_tbRotCalib.Calibration.SetUncalibratedPoint(Convert.ToInt16(dataNum) - 1, pix_X, pix_Y);
            //机械手坐标
            Calib_tbCamCalib.Calibration.SetRawCalibratedPoint(Convert.ToInt16(dataNum) - 1, Convert.ToDouble(dataPosX[Convert.ToInt16(dataNum) - 1]), Convert.ToDouble(dataPosY[Convert.ToInt16(dataNum) - 1]));
            Calib_tbRotCalib.Calibration.SetRawCalibratedPoint(Convert.ToInt16(dataNum) - 1, Convert.ToDouble(dataPosX[Convert.ToInt16(dataNum) - 1]), Convert.ToDouble(dataPosY[Convert.ToInt16(dataNum) - 1]));

            string[] writecsv = new string[] { pix_X.ToString(), pix_Y.ToString(), dataPosX[Convert.ToInt16(dataNum) - 1].ToString(), dataPosY[Convert.ToInt16(dataNum) - 1].ToString() };// dataNum + "," + pix_X.ToString() + "," + pix_Y + "," + data[0] + "," + data[1];}
            if (dataNum == "1")
            {
                rowList.Clear();
            }
            rowList.Add(writecsv);
            if (dataNum == "9")
            {
                //标定
                Calib_tbCamCalib.Calibration.Calibrate();

                Calib_tbRotCalib.Calibration.Calibrate();
                Globals.AutoToolblock.Run();
                Globals.CalibRotToolblock.Run();
            }
            if (tempToolblock.CreateLastRunRecord().SubRecords.Count > 0)
            {
                ICogRecord DispalyRecord1 = tempToolblock.CreateLastRunRecord().SubRecords[0];
                ICogRecord DispalyRecord = tempToolblock.CreateLastRunRecord()
                    .SubRecords[tempToolblock.CreateLastRunRecord().SubRecords.Count - 1];
                Globals.DispalyRecordNum(1, DispalyRecord1);
            }
            CsvUtil.WriteCSV(ConfigureFilePath.projectItemPath + "\\" + "SysClibrateData.csv", rowList, false);
        }
        public static void NinePointCalib(int num, ICogImage MyOutPutImage, CogToolBlock CalibToolblock, out double pix_X, out double pix_Y, out double r, out bool re)
        {
            string CalibStep = num.ToString();
            pix_X = 999;
            pix_Y = 999;
            r = 999;
            re = false;
            switch (CalibStep)
            {
                case "1":
                    InitNpointCalib();
                    CalibrateCalc(CalibStep, MyOutPutImage, CalibToolblock, out pix_X, out pix_Y,
                        out r, out re);
                    Globals.LogRecord("【pix_X:    " + pix_X + "    pix_Y:  " + pix_Y + "】");
                    if (!re)
                    {
                        Globals.LogRecord("【标定失败,请重新检查视觉算法！" + "】");
                        inforForm.ShowErrorDialog("【标定失败,请重新检查视觉算法！】");
                        return;
                    }
                    break;
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    CalibrateCalc(CalibStep, MyOutPutImage, CalibToolblock, out pix_X, out pix_Y,
                        out r, out re);
                    Globals.LogRecord("【pix_X:    " + pix_X + "    pix_Y:  " + pix_Y + "】");
                    if (!re)
                    {
                        Globals.LogRecord("【标定失败,请重新检查视觉算法！" + "】");
                        inforForm.ShowErrorDialog("【标定失败,请重新检查视觉算法！】");
                        return;
                    }
                    break;
            }
        }
        Bitmap bt;
        public ICogImage MyOutPutImage;
        public static double Pix_X = 999999;
        public static double Pix_Y = 999999;
        public static double Pix_R = 999999;
        public static bool VisionReSault = false;
        public static int CalibStep = 10;
        public static void CalcRobotPos()
        {
            /*
          *       5   <-    4   <-    3
          *       6         1   ->    2
          *       7   ->    8   ->    9
             *
             *
          *       (1,1)      (0,1)      (-1,1)
          *       (1,0)      (0,0)      (-1,0)
          *       (1,-1)     (0,-1)     (-1,-1)
          */
            double valuex = 0;
            double valuey = 0;
            double calib_MoudleX = 0;
            double calib_MoudleY = 0;

            //中间列X
            dataPosX[7] = dataPosX[3] = dataPosX[0];
            //左边列X
            dataPosX[6] = dataPosX[5] = dataPosX[4] = dataPosX[0] - 5;
            //右边列X
            dataPosX[8] = dataPosX[2] = dataPosX[1] = dataPosX[0] + 5;
            //中间行Y
            dataPosY[5] = dataPosY[1] = dataPosY[0];
            //上边行Y
            dataPosY[4] = dataPosY[3] = dataPosY[2] = dataPosY[0] + 5;
            //下边行Y
            dataPosY[8] = dataPosY[7] = dataPosY[6] = dataPosY[0] - 5;

        }
        RotateCalib rotateCalib = new RotateCalib();
        public static double[] rotatePointX = new double[3];
        public static double[] rotatePointY = new double[3];
        public void CalibRun()
        {
            Globals.LoadVisionParam();
            CalibStep = 10;
            Globals.ReadGloablScope("POS_CalibX", "103");
            while (true)
            {
                if (Globals.ReceiveDone)
                {
                    double temp1 = Convert.ToDouble(Globals.MyReciveStr);
                    dataPosX[0] = Math.Round(temp1, 3);
                    Globals.ReceiveDone = false;
                    break;
                }
            }
            Globals.ReadGloablScope("POS_CalibY", "104");
            while (true)
            {
                if (Globals.ReceiveDone)
                {
                    double temp2 = Convert.ToDouble(Globals.MyReciveStr);
                    dataPosY[0] = Math.Round(temp2, 3);
                    Globals.ReceiveDone = false;
                    break;
                }
            }
            Globals.LogRecord(" 【机械手标定初始位置 :" + "【X:" + dataPosX[0].ToString() + "，Y:" + dataPosY[0].ToString() + "!】");
            CalcRobotPos();
            if (Globals.State == Controller.Stop)
            {
                for (int i = 1; i <= 9; i++)
                {
                    string num = (i + 1000).ToString();
                    string reNum = (i + 6000).ToString();
                    Globals.MySetRobotGlobal(num);
                    if (Globals.WaitRobotValue("GlobalStep", "100", reNum))
                    {
                        Globals.CameraDown.Capture_New(Globals.SettingOption.DownCamExpose2, out bt);
                        MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                        NinePointCalib(i, MyOutPutImage, Globals.CalibToolblock, out Pix_X, out Pix_Y, out Pix_R, out VisionReSault);




                        if (!VisionReSault)
                        {
                            inforForm.ShowErrorDialog("【标定失败,请重新检查视觉算法！】");
                            return;
                        }
                    }
                    else
                    {
                        inforForm.ShowErrorDialog("【机器人未运动超时，标定失败,请重新标定！】");
                        return;
                    }
                }

                inforForm.ShowErrorDialog("【下相机标定完成,请继续开始旋转中心计算！】");

                for (int i = 1; i <= 3; i++)
                {
                    string num = (i + 1009).ToString();
                    string reNum = (i + 6009).ToString();
                    Globals.MySetRobotGlobal(num);
                    if (Globals.WaitRobotValue("GlobalStep", "100", reNum))
                    {
                        Globals.CameraDown.Capture_New(Globals.SettingOption.DownCamExpose2, out bt);
                        MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
                        Globals.RunTool(MyOutPutImage, MyVisionProMgr.ins.CogToolBlockDic["TB_CalibRot.vpp"], out Pix_X, out Pix_Y, out Pix_R, out VisionReSault);
                        rotatePointX[i - 1] = Pix_X;
                        rotatePointY[i - 1] = Pix_Y;
                    }
                    else
                    {
                        inforForm.ShowErrorDialog("【机器人未运动超时，旋转中心计算失败,请重新计算旋转中心！】");
                        return;
                    }
                }
                rotateCalib.CamMotionPoints.Clear();
                try
                {
                    double x1 = Convert.ToDouble(rotatePointX[0]);
                    double y1 = Convert.ToDouble(rotatePointY[0]);

                    double x2 = Convert.ToDouble(rotatePointX[1]);
                    double y2 = Convert.ToDouble(rotatePointY[1]);

                    double x3 = Convert.ToDouble(rotatePointX[2]);
                    double y3 = Convert.ToDouble(rotatePointY[2]);

                    Globals.LogRecord("【X1:" + rotatePointX[0].ToString() + ";  Y1:" + rotatePointY[0].ToString() +

                                      ";  X2:" + rotatePointX[1].ToString() + ";  Y2:" + rotatePointY[1].ToString() +

                                      ";  X3:" + rotatePointX[2].ToString() + ";  Y3:" + rotatePointY[2].ToString() +
                                        "】");

                    rotateCalib.CamMotionPoints.Add(new PointD(x1, y1));
                    rotateCalib.CamMotionPoints.Add(new PointD(x2, y2));
                    rotateCalib.CamMotionPoints.Add(new PointD(x3, y3));

                    rotateCalib.CamCenterPoint = MyRotateCalib.GetRCenter(rotateCalib.CamMotionPoints.ToArray(), out rotateCalib.CamRadio);

                    Globals.store.Value.DownCamCen[0] =Math.Round(rotateCalib.CamCenterPoint.X, 3); 
                    Globals.store.Value.DownCamCen[1] =Math.Round(rotateCalib.CamCenterPoint.Y, 3); 
                    Globals.store.Value.DownCamCen[2] = Math.Round(rotateCalib.CamRadio, 3); 
                    Globals.store.Save();

                    string[] writecsv1 = new string[] { rotatePointX[0].ToString(), rotatePointY[0].ToString(), rotatePointX[1].ToString(), rotatePointY[1].ToString(),
                    rotatePointX[2].ToString() ,rotatePointY[2].ToString()};
                    List<string[]> rowListRot = new ListEx<string[]>();
                    rowListRot.Add(writecsv1);
                    CsvUtil.WriteCSV(ConfigureFilePath.projectItemPath + "\\" + "SysClibrateRotData.csv", rowListRot, false);
                    Globals.LogRecord("【旋转中心计算结果：" + "X：" + rotateCalib.CamCenterPoint.X.ToString() + "Y：" + rotateCalib.CamCenterPoint.Y.ToString() + "OK！】");
                    rowListRot.Clear();
                    inforForm.ShowSuccessDialog("视觉参数数据保存成功！");
                }
                catch (Exception exception)
                {
                    Globals.LogRecord("【旋转中心计算失败,请重新计算旋转中心！】");
                    inforForm.ShowErrorDialog("【旋转中心计算失败,请重新计算旋转中心！】");
                }

            }
        }
    }
}
