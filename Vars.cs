using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
//using DDR4Check.Communicate;

namespace sunyvpp
{
    /// <summary>
    /// 参数种类
    /// </summary>
    public enum ParamType
    {
        PLCParam = 0,
        LightParam = 1,
        VisionParam = 2,
        RobotParam = 3,
        SystemParam = 4
    }

    /// <summary>
    /// 端口类型
    /// </summary>
    public enum PortType
    {
        LightPort = 0,
        PLCPort = 1,
        RoBotPort = 2,
    }
    /// <summary>
    /// 物料的颜色类型
    /// </summary>
    public enum ProductColors
    { 
        Gold = 0,
        White = 1,
        Grey = 2
    }
    /// <summary>
    /// 图像结果保存类型
    /// </summary>
    public enum SaveImageType
    { 
        None = 0,
        Image = 1,
        ImageWithRecord = 2
    }

    public  class Vars
    {
        #region files saving path 
        #region help文件夹
        public static string LogPath = Application.StartupPath + "\\help\\Log\\"; //Log file saving path
        public static string helpfilePath = Application.StartupPath + "\\help\\SOP.docx";
        #endregion
        #region Param文件夹
        public static string DrawROIParamPath = Application.StartupPath + "\\Param\\";
        public static string ROIParamPath = Application.StartupPath + "\\Param\\ROI.xlsx";
        public static string ROITempPath = Application.StartupPath + "\\Param\\ROITemp.xlsx";
        public static string IOParamPath = Application.StartupPath + "\\Param\\IO.txt";
        public static string ROIPath = Application.StartupPath + "\\Param\\ROI.xlsx";
        #region DataGridView
        public static string dGV_CameraTempParam = Application.StartupPath + "\\Param\\DDR4.xlsx";
        public static string dGV_CameraParamOffter = Application.StartupPath + "\\Param\\DDR4ParamChangle.xlsx";
        #endregion
        #endregion
        #region Data文件夹
        public static string ExcelTemplatePath = Application.StartupPath + "\\Data\\DDR4.xlsx";
        public static string ExcelPath = Application.StartupPath + "\\Data\\";
        public static string ExcelPathStart = Application.StartupPath + "\\Data\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
        #endregion
        #region VisionFile文件夹
        public static string VisionTemplatePath0 = Application.StartupPath + "\\VisionFile\\Job0.vpp";
        public static string VisionTemplatePath1 = Application.StartupPath + "\\VisionFile\\Job1.vpp";
        public static string ROITemplatePath = Application.StartupPath + "\\VisionFile\\Temp.vpp";
        
        #endregion
        #region 备份文件夹
        public static string VisionBackupsPath = Application.StartupPath + "\\备份\\Job.vpp";
        #endregion
        

        
        
        
        


        #region Camera
        #endregion
        /// <summary>
        /// 产品类型
        /// </summary>
        public static int SelectProductionFlag = 0;
        #endregion

        #region UI pictrue path
        public static string Okbmp = Application.StartupPath + "\\..\\..\\..\\Images\\TurnOn.bmp";
        public static string Ngbmp = Application.StartupPath + "\\..\\..\\..\\Images\\TurnOff.bmp";
        #endregion

        //public static User CurrentUser = new User("操作员","123", 0);//default user

        #region Light Param
        public  static ProductColors SelectedColor = ProductColors.White;

        public static string PortName = "COM1";
        public static string BaudRate = "115200";
        public static string StopBits = "1";
        public static string DataBits = "8";
        public static string Parity = "none";

        public static string[] LightStep = new string[] { "Step0", "Step1", "Step2", "Step3", "StepHsg" }; //变光步骤标示
        //public static Dictionary<string, ChannelValue> LightStepValue = new Dictionary<string, ChannelValue>(); //变光值存储数组
        #endregion

        #region VisionParam
        public static string[] VisionParamKeys = new string[] { "FetchStandard", "FetchOffset", "HsgStandard", "FixStandard", "MtoHOffset", "CheckStandard" };//視覺變量名
        //public static Dictionary<string, SMSVision.SMSPostion> VisionParamValue = new Dictionary<string, SMSVision.SMSPostion>(); //視覺參數變量儲存
        #endregion

        #region save image used vars
        public static bool OnlyNgbmp = true; //only save ng bmp?
        public static SaveImageType SavedImageType = SaveImageType.None;
        public static string ImageSavePath = Application.StartupPath + "\\ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd")+"\\";
        #endregion          
    }
}
