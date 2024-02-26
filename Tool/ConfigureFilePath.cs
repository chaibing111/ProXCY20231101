using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    class ConfigureFilePath
    {
        // D1504切换文档1：A短2：A长，3:B短，4：B长。5：(Busbar_A两侧),6：(Busbar_A),7：(Busbar_B两侧),8：(Busbar_B)
        public static string LaserFileName = "";
        public static string PathSideAshort = "";
        public static string PathSideALong = "";
        public static string PathSideBshort = "";
        public static string PathSideBLong = "";
        public static string PathBusbarATwoSide = "";
        public static string PathBusbarA = "";
        public static string PathBusbarBTwoSide = "";
        public static string PathBusbarB = "";

        #region 日志路径
        public const string Dir_Record = "E:\\AutoStation_Record\\";
        public const string Dir_Record_FormLog = Dir_Record + "FormLog\\";
        public const string Dir_Record_Error = Dir_Record + "Error\\";
        public const string Dir_Record_Alarm = Dir_Record + "AlarmLog\\";
        public const string Dir_Record_CleanStatisticalLog = Dir_Record + "CleanStatisticalLog\\";
        public const string Dir_Record_ProcessTimeLog = Dir_Record + "ProcessTimeLog\\";
        public const string Dir_Record_MTAverageTimeLog = Dir_Record + "MTAverageTimeLog\\";
        public const string Dir_Record_Report = Dir_Record + "Report\\";
        public const string Dir_Record_FIPReport = Dir_Record_Report + "EXFOReport\\";
        public const string Dir_Record_TQReport = Dir_Record_Report + "TQReport\\";
        public const string Dir_Record_FIPImage = Dir_Record_Report + "EXFOImages\\";
        public const string Dir_Record_TQImage = Dir_Record_Report + "TQImages\\";
        public const string Dir_Record_ClearPenService = Dir_Record + "Device\\";
        public const string Dir_Record_TaskLog = Dir_Record + "TaskLog\\" ;
        public const string Dir_Record_LaserLog = Dir_Record + "LaserLog\\";
        public const string Dir_Record_SettingLog = Dir_Record + "SettingLog\\";
        public const string Dir_Record_PLCLog = Dir_Record + "PLCLog\\";
        #endregion
        #region 图片保存路径
        public static string Station1SavePathOK = Dir_Record + "ResultImage\\" + "Station1ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "OK" + "\\";
        public static string Station1SavePathNG = Dir_Record + "ResultImage\\" + "Station1ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "NG" + "\\";
                                                                               
        public static string Station2SavePathOK = Dir_Record + "ResultImage\\" + "Station2ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "OK" + "\\";
        public static string Station2SavePathNG = Dir_Record + "ResultImage\\" + "Station2ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "NG" + "\\";
                                                                               
        public static string Station3SavePathOK = Dir_Record + "ResultImage\\" + "Station3ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "OK" + "\\";
        public static string Station3SavePathNG = Dir_Record + "ResultImage\\" + "Station3ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "NG" + "\\";
                                                             
        public static string Station4SavePathOK = Dir_Record + "ResultImage\\" + "Station4ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "OK" + "\\";
        public static string Station4SavePathNG = Dir_Record + "ResultImage\\" + "Station4ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "NG" + "\\";

        public static string PictruePath = Dir_Record + "ResultImage\\";
        public static string DebugStationSavePath = Dir_Record + "DebugStationResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" ;

        #endregion
        #region 配置文件路径
        public static readonly string Dir_Config = AppDomain.CurrentDomain.BaseDirectory + "Config\\";

        public static readonly string Dir_Config_Device = Dir_Config + "Device\\";
        public static readonly string Dir_Config_Setting = Dir_Config + "Setting\\";
        public static readonly string Dir_Config_Position = Dir_Config + "Position\\";
        public static readonly string Path_Config_Setting_Optical = Dir_Config_Setting + "OpticalSetting.xml";
        public static readonly string Path_Config_Setting_Option = Dir_Config_Setting + "OptionSetting.xml";

        public static readonly string Path_Image = "E:\\Image";
        public static string ImageSavePathOK = "E:" + "\\ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "OK" + "\\";
        public static string ImageSavePathNG = "E:" + "\\ResultImage\\" + DateTime.Now.ToString("yyyy-MM-dd_HH") + "\\" + "NG" + "\\";
        public static string ImageDelet = "E:" + "\\ResultImage\\";
      


        /// <summary>
        /// 工程文件基础目录"E:\\ProJectPath"
        /// </summary>
        public static readonly string Path_Config_Pro = "E:\\ProJectPath";
        /// <summary>
        /// 工程文件ini文件保存
        /// </summary>
        public static readonly string Path_Config_ProFile = "E:\\ProJectPath\\projectItem.ini";

        /// <summary>
        /// 工程文件生成目录"E:\\ProJectPath\\CB02"
        /// </summary>
        public static  string projectItemPath = "";
        public static string ImageTaskPath = "";
        public static string MotionTaskPath = "";
        public static string MesPath = "";
        public static string PLC_Dic_path = "E:\\PlcsDic.json";
        public static readonly string Path_EXFODataTemplate = AppDomain.CurrentDomain.BaseDirectory + "\\ExcelTemplate\\EXFO Data Template.xls";
        public static readonly string Path_PLCInput = Dir_Config_Device + "PLC_Input.xml";
        public static readonly string Path_PLCOutput = Dir_Config_Device + "PLC_Output.xml";

        public static readonly string Path_PLCAlarmInfor = Dir_Config_Device + "AlarmInfor.xml";
        public static readonly string Path_PositionPath = Dir_Config_Position + "PositionInfor.xml";//Left
        public static readonly string Path_Position_Left = Dir_Config_Position + "PositionInfor.xml";//Left
        public static readonly string Path_Position_Right = Dir_Config_Position + "PositionInfor_Right.xml";
        public static readonly string Path_Position_Trans = Dir_Config_Position + "PositionInfor_Trans.xml";

        public static readonly string Path_AxisInforAddresses_Left = Dir_Config_Device + "AxisInforAddresses_Left.xml";
        public static readonly string Path_AxisInforAddresses_Right = Dir_Config_Device + "AxisInforAddresses_Right.xml";
        public static readonly string Path_AxisInforAddresses_Trans = Dir_Config_Device + "AxisInforAddresses_Trans.xml";

        public static readonly string Path_MTPassByInfor = Dir_Config_Device + "MTPassByInfors.xml";
        public static readonly string Path_PLC_OtherInfor = Dir_Config_Device + "PLC_OtherInfor.xml";
        public static readonly string Path_PLC_TaskInfor = Dir_Config_Device + "PLC_TaskInfor.xml";
        #endregion
    }
}
