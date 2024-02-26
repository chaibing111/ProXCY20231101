using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace sunyvpp
{
    [Serializable]
    public class mSettingOption
    {

        //[Category("Parameter"), Description("设置IP地址")]
        [Category("通信")]
        [Description("设置IP地址")]
        public string IP { get; set; }
        [Category("通信"), Description("设置端口号")]
        public string Port { get; set; }
        [Category("贴合感应"), Description("是否启用贴合感应")]
        public bool TieSensor { get; set; }

        [Category("贴合偏移抖动"), Description("贴合后摩擦距离")]
        public double DIstX_OFF { get; set; }
        [Category("贴合偏移抖动"), Description("贴合后摩擦距离")]
        public double DIstY_OFF { get; set; }
        [Category("轴到位误差限制"), Description("Axis_X_到位误差限制")]
        public double Axis_X_Error { get; set; }

        [Category("轴到位误差限制"), Description("Axis_Y到位误差限制")]
        public double Axis_Y_Error { get; set; }

        [Category("轴到位误差限制"), Description("Axis_Z到位误差限制")]
        public double Axis_Z_Error { get; set; }

        [Category("轴到位误差限制"), Description("Axis_U到位误差限制")]
        public double Axis_U_Error { get; set; }

        [Category("轴到位误差限制"), Description("上相机拍照轴到位误差限制")]
        public double Axis_X1_Error { get; set; }
        [Category("轴到位误差限制"), Description("载具轴到位误差限制")]
        public double Axis_Y1_Error { get; set; }

        [Category("Parameter"), Description("机器ID")]
        public string MachineId { get; set; }
        [Category("AutoMatrix"), Description("行扫描是否开启")]
        public bool IsRowScan { get; set; }
        [Category("AutoMatrix"), Description("拍照点位行/列")]
        public int Row_Colum { get; set; }
        [Category("AutoMatrix"), Description("拍照点位行偏移")]
        public double X_OFF { get; set; }
        [Category("AutoMatrix"), Description("拍照点位列偏移")]
        public double Y_OFF { get; set; }
        [Category("Parameter"), Description("组装补偿:Xoffset!")]
        public double[] 补偿Offset { get; set; }

        [Category("Parameter"), Description("每班生产工单号，绑码需要")]
        public string 工单 { get; set; }
        [Category("AxisParameter"), Description("【轴参数】速度")]
        public double Vel { get; set; }
        [Category("AxisParameter"), Description("【轴参数】Z_速度")]
        public double Vel_Z { get; set; }
        [Category("AxisParameter"), Description("【轴参数】R_速度")]
        public double Vel_R { get; set; }
        [Category("AxisParameter"), Description("【轴参数】加速度")]
        public double ACC { get; set; }
        [Category("AxisParameter"), Description("【轴参数】减速度")]
        public double DEC { get; set; }
        [Category("AxisParameter"), Description("【轴参数】减速度")]
        public short SmoothTime { get; set; }
        
        [Category("LineAxisParameter"), Description("最小匀速时间默认1ms")]
        public double evenTime { get; set; }
        [Category("LineAxisParameter"), Description("插补速度 pulse/S")]
        public double synVelMax { get; set; }
        [Category("LineAxisParameter"), Description("插补加速度 pulse/S/S")]
        public double synAccMax { get; set; }
        [Category("LineAxisParameter"), Description("插补平滑减速度 pulse/S/S")]
        public double synDecSmooth { get; set; }
        [Category("LineAxisParameter"), Description("插补急停减速度 pulse/S/S")]
        public double synDecAbrupt { get; set; }
        [Category("PLC_Parameter"), Description("起始X")]
        public double Calib_X { get; set; }

        [Category("PLC_Parameter"), Description("起始Y")]
        public double Calib_Y { get; set; }
        [Category("PLC_Parameter"), Description("间距")]
        public double Calib_Distant { get; set; }
        [Category("PLC_Parameter"), Description("起始X")]
        public double Calib_ConvB_X { get; set; }

        [Category("PLC_Parameter"), Description("起始Y")]
        public double Calib_ConvB_Y { get; set; }
        [Category("PLC_Parameter"), Description("间距")]
        public double Calib_ConvB_Distant { get; set; }
        [Category("Parameter"), Description("相机曝光")]
        public int Expose { get; set; }

        [Category("Parameter"), Description("相机增益")]
        public int Gain { get; set; }

        //[Category("Parameter"), Description("选择自动绑码，所有二位码都需要扫码到")]
        //public string b网卡物理地址 { get; set; }


        //private int _TrayIndex = 1;
        //[Category("Parameter"), Description("右满Tray取料的Index!"), ReadOnly(true), Browsable(true)]
        //private int _TrayIndex222 = 1;

    }
}
