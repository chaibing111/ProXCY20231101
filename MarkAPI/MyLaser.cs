using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cognex.VisionPro.OCRMax.Implementation.Internal;

namespace sunyvpp
{
    public class MyLaser
    {
        /// <summary>
        /// D1504切换文档1：A短2：A长，3:B短，4：B长。5：(Busbar_A两侧),6：(Busbar_A),7：(Busbar_B两侧),8：(Busbar_B)
        /// </summary>
        public string ChangeLaserDocumentAddress = "D1504";
        /// <summary>
        /// 切换焊接开始 D1506
        /// </summary>
        public string LaserStartAddress = "D1506";
        /// <summary>
        /// 切换文档完成 D1214
        /// </summary>
        public string ChangeLaserOKAddress = "D1214";
        private readonly static object lockCard = new object();
        public static readonly Lazy<MyLaser> instance = new Lazy<MyLaser>(() => new MyLaser());
        public static MyLaser Instance { get; } = instance.Value;

        public MyLaser()
        {
            
        }
        /// <summary>
        /// 加载并切换文档
        /// </summary>
        /// <param name="a"></param>
        public bool AlterMethod(int a)
        {
            // D1504切换文档1：A短2：A长，3:B短，4：B长。5：(Busbar_A两侧),6：(Busbar_A),7：(Busbar_B两侧),8：(Busbar_B)
            //PathSideAshort = "";
            //PathSideALong = "";
            //PathSideBshort = "";
            //PathSideBLong = "";

            //PathBusbarATwoSide = "";
            //PathBusbarA = "";
            //PathBusbarBTwoSide = "";
            //PathBusbarB = "";
            switch (a)
            {
                case 1:
                    ConfigureFilePath.LaserFileName = ConfigureFilePath. PathSideAshort;
                    break;
                case 2:
                    ConfigureFilePath.LaserFileName = ConfigureFilePath.PathSideALong;
                    break;
                case 3:
                    ConfigureFilePath.LaserFileName = ConfigureFilePath.PathSideBshort;
                    break;
                case 4:
                    ConfigureFilePath.LaserFileName = ConfigureFilePath.PathSideBLong;
                    break;
                case 5:
                    ConfigureFilePath.LaserFileName = ConfigureFilePath.PathBusbarATwoSide;
                    break;
                case 6:
                    ConfigureFilePath.LaserFileName = ConfigureFilePath.PathBusbarA;
                    break;
                case 7:
                    ConfigureFilePath.LaserFileName = ConfigureFilePath.PathBusbarBTwoSide;
                    break;
                case 8:
                    ConfigureFilePath.LaserFileName = ConfigureFilePath.PathBusbarB;
                    break;
            }

            BslErrCode ret = MarkAPI.ins.LoadDataFile(ConfigureFilePath.LaserFileName);
            if (ret == BslErrCode.BSL_ERR_SUCCESS)
            {
                Globals.LogRecord("【激光文档加载成功！" + ConfigureFilePath.LaserFileName + "】");
                ret = MarkAPI.ins.AppendFileToDevice();
                if (ret == BslErrCode.BSL_ERR_SUCCESS)
                {
                    Globals.LogRecord("【激光文档关联成功！" + ConfigureFilePath.LaserFileName + "】");
                    return true;
                }
                else
                {
                    Globals.LogRecord("【激光文档关联成功！" + MarkAPI.ins.MsgGet() + "】");
                    return true;
                }

                
            }
            else
            {
                Globals.LogRecord("【激光文档加载失败！" + ret.ToString() + "】");
                return true;
            }

        }

        public void GetLaserParam()
        {
            BslErrCode ret = MarkAPI.ins.GetPenParam(0);
            if (ret == BslErrCode.BSL_ERR_SUCCESS)
            {
                
                ret = MarkAPI.ins.GetWobble(0);
                if (ret == BslErrCode.BSL_ERR_SUCCESS)
                {
                    NewMethodParam();
                }
                else
                {
                    Globals.LogRecord("【激光参数获取失败！" + MarkAPI.ins.MsgGet() + "】");
                }
            }
            else
            {
                Globals.LogRecord("【激光参数获取失败！" + MarkAPI.ins.MsgGet() + "】");
            }
        }

        private static void NewMethodParam()
        {
            Globals.dicInfo.Clear();
            Globals.dicInfo.Add("托盘码", "1234567890");
            Globals.dicInfo.Add("功率百分比", "50");
            Globals.dicInfo.Add("Software", StationInfo.ins.Software);
            Globals.dicInfo.Add("EquipmentCode", StationInfo.ins.EquipmentCode);

            Globals.dicInfo.Add("是否摆动：", MarkAPI.ins.nWOBBLE.ToString());
            Globals.dicInfo.Add("显示摆动路径：", MarkAPI.ins.nWOBBLESHOWROUTE.ToString());
            Globals.dicInfo.Add("摆动直径：", MarkAPI.ins.nWOBBLEDIAMETER.ToString());
            Globals.dicInfo.Add("摆动间距：", MarkAPI.ins.nWOBBLEDISTANCE.ToString());
            Globals.dicInfo.Add("抖动类型：", MarkAPI.ins.nWOBBLE_TYPE.ToString());
            Globals.dicInfo.Add("椭圆长轴长度：", MarkAPI.ins.nWOBBLE_E11ipse_A.ToString());
            Globals.dicInfo.Add("椭圆短轴长度：", MarkAPI.ins.nWOBBLE_Ellipse_B.ToString());
            Globals.dicInfo.Add("正弦振幅长度：", MarkAPI.ins.nWOBBLE_Sin_Amplitude.ToString());
            Globals.dicInfo.Add("正弦周期长度：", MarkAPI.ins.nWOBBLE_Sin_Cycle.ToString());
            Globals.dicInfo.Add("平滑系数：", MarkAPI.ins.nWOBBLE_SmoothPar.ToString());
            Globals.dicInfo.Add("8字间距：", MarkAPI.ins.nWOBBLE_Space.ToString());
            Globals.dicInfo.Add("8字高：", MarkAPI.ins.nWOBBLE_Height.ToString());
            Globals.dicInfo.Add("8字宽：", MarkAPI.ins.nWOBBLE_Width.ToString());



            Globals.dicInfo.Add("加工次数：", MarkAPI.ins.nMarkLoop.ToString());
            Globals.dicInfo.Add("标刻速度mm/s：", MarkAPI.ins.dMarkSpeed.ToString());
            Globals.dicInfo.Add("功率百分比(0-100%)：", MarkAPI.ins.dPowerRatio.ToString());
            Globals.dicInfo.Add("电流A：", MarkAPI.ins.dCurrent.ToString());
            Globals.dicInfo.Add("频率HZ：", MarkAPI.ins.nFreq.ToString());
            Globals.dicInfo.Add("Q脉冲宽度us：", MarkAPI.ins.nQPulseWidth.ToString());
            Globals.dicInfo.Add("开始延时us：", MarkAPI.ins.nStartTC.ToString());
            Globals.dicInfo.Add("激光关闭延时us：", MarkAPI.ins.nLaserOffTC.ToString());
            Globals.dicInfo.Add("结束延时us：", MarkAPI.ins.nEndTC.ToString());
            Globals.dicInfo.Add("拐角延时us：", MarkAPI.ins.nPolyTC.ToString());
            Globals.dicInfo.Add("跳转速度mm/s：", MarkAPI.ins.dJumpSpeed.ToString());
            Globals.dicInfo.Add("跳转位置延时us：", MarkAPI.ins.nJumpPosTC.ToString());
            Globals.dicInfo.Add("跳转距离延时us：", MarkAPI.ins.nJumpDistTC.ToString());
            Globals.dicInfo.Add("末点补偿mm：", MarkAPI.ins.dEndComp.ToString());
            Globals.dicInfo.Add("脉冲点模式：", MarkAPI.ins.bPulsePointMode.ToString());
            Globals.dicInfo.Add("脉冲点数目：", MarkAPI.ins.nPulseNum.ToString());
            Globals.dicInfo.Add("打点时间：", MarkAPI.ins.POINTTIME.ToString());
        }

    }

}
