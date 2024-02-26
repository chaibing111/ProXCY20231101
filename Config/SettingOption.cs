using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
namespace sunyvpp
{
    [Serializable]
    public class OptionSetting
    {
        [Category("PLC1参数"), Description("PLC1 IP地址")]
        public string PLCIPAddress1 { get; set; }
        [Category("PLC1参数"), Description("PLC1端口号")]
        public int PLCPort1 { get; set; }
        //[Category("PLC2参数"), Description("PLC2 IP地址")]
        //public int PLCIPAddress2 { get; set; }
        //[Category("PLC2参数"), Description("PLC2端口号")]
        //public bool PLCPort2 { get; set; }
        [Category("视觉参数"), Description("OK图片保存")]
        public bool isSaveImageOK { get; set; }
        [Category("视觉参数"), Description("NG图片保存")]
        public bool isSaveImageNG { get; set; }

        [Category("视觉参数"), Description("是否启用视觉")]
        public bool isVision { get; set; } = true;
        [Category("视觉参数"), Description("是否启用视觉定位")]
        public bool isCorid { get; set; } = true;
        [Category("视觉参数"), Description("是否启用无料跳过")]
        public bool isNoProduct { get; set; } = true;
        [Category("视觉参数"), Description("上相机曝光检测位")]
        public int UpCamExpose { get; set; }
        [Category("视觉参数"), Description("上相机曝光1")]
        public int UpCamExpose1 { get; set; }
        [Category("视觉参数"), Description("上相机曝光2")]
        public int UpCamExpose2 { get; set; }
        [Category("视觉参数"), Description("上相机曝光3")]
        public int UpCamExpose3 { get; set; }
        [Category("视觉参数"), Description("上相机曝光4")]
        public int UpCamExpose4 { get; set; }

        [Category("视觉参数"), Description("下相机曝光1")]
        public int DownCamExpose1 { get; set; }
        [Category("视觉参数"), Description("下相机曝光2")]
        public int DownCamExpose2 { get; set; }
        [Category("视觉补偿参数"), Description("视觉定位偏移补偿X")]
        public double OffsetX { get; set; }
        [Category("视觉补偿参数"), Description("视觉定位偏移补偿Y")]
        public double OffsetY { get; set; }
        [Category("视觉补偿参数"), Description("视觉定位偏移补偿R")]
        public double OffsetR { get; set; }
        [Category("通讯超时参数"), Description("通讯超时报警")]
        public double DelayAlarm { get; set; }
        [Category("数据保存参数"), Description("日志保存天数")]
        public int LogSaveDay { get; set; }

        [Category("数据保存参数"), Description("图片保存天数")]
        public int ImageSaveDay { get; set; }

        [Category("光源串口参数"), Description("串口号")] 
        public string  LaserPortName { get; set; }

        [Category("光源串口参数"), Description("波特率")]
        public int LaserBaudRate { get; set; } = 9600;

        [Category("光源串口参数"), Description("奇偶校验位")]
        public Parity LaserParity { get; set; } = Parity.None;

        [Category("光源串口参数"), Description("数据位")]
        public int LaserDataBits { get; set; } = 8;

        [Category("光源串口参数"), Description("停止位")]
        public StopBits LaserStopBits { get; set; } = StopBits.One;

        [Category("机器人参数"), Description("机器人TCP通讯IP地址")]
        public string RobotIPAddress { get; set; }
        [Category("机器人参数"), Description("机器人TCP通讯Port端口号")]
        public int RobotTCPPort { get; set; }
        [Category("机器人参数"), Description("屏蔽夹紧气缸报警")]
        public bool IgnoreClose { get; set; }

    }
}
