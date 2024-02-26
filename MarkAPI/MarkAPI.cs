using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    public class MarkAPI
    {
        #region Win API
        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, string funcName);

        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);
        #endregion

        /// <summary>
        /// DLL加载库句柄
        /// </summary>
        public IntPtr hLib;

        /// <summary>
        /// 交互窗体句柄
        /// </summary>
        public IntPtr hwnd;

        public bool dllInvokeFlag = true;
        public List<string> Messages = new List<string>();

        public string nowItemFilePath;

        /// <summary>
        /// 设备ID列表
        /// </summary>
        public ObservableCollection<string> Devlist { get; set; } = new ObservableCollection<string>();

        public static MarkAPI ins = new MarkAPI();

        public MarkAPI(string DLLPath = "MarkSDK.dll")
        {
            if (dllInvokeFlag) return;
            hLib = LoadLibrary(DLLPath);
            if (hLib == IntPtr.Zero)
            {
                dllInvokeFlag = true;
                MsgLog(DLLPath + "加载失败");
            }
            else
            {
                dllInvokeFlag = true;
                MsgLog(DLLPath + "加载成功");
            }
        }

        ~MarkAPI()
        {
            //不能显示释放dll，会导致dll中的线程无法正常结束,dll中的ExitInstance()函数会自动完成资源的释放
            // if (hLib != IntPtr.Zero)
            //  FreeLibrary(hLib);
        }

        private void MsgLog(string msg)
        {
            Messages.Insert(0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + msg);
            if (Messages.Count > 100) Messages.RemoveAt(99);
        }

        public string MsgGet()
        {
            if (Messages.Count > 0)
            {
                return MarkAPI.ins.Messages[0];
            }
            return "";
        }

        /// <summary>
        /// 将要执行的函数转换为委托
        /// </summary>
        /// <param name="APIName"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public Delegate GetFunctionAddress(string APIName, Type t)
        {
            IntPtr api = GetProcAddress(hLib, APIName);
            if (api == IntPtr.Zero)
            {
                //System.Windows.MessageBox.Show("加载api: " + APIName + "失败");
                MsgLog("加载api: " + APIName + "失败");
                return null;
            }

            return (Delegate)Marshal.GetDelegateForFunctionPointer(api, t);
        }


        /// <summary>
        /// 初始化板卡Devid
        /// </summary>
        public bool GetDeviceList()
        {
            if (hLib != IntPtr.Zero)
            {
                BSL_GetAllDevices func = (BSL_GetAllDevices)GetFunctionAddress("GetAllDevices2", typeof(BSL_GetAllDevices));  //GetAllDevices
                if (func != null)
                {
                    int iDevCount = 0;
                    STU_DEVID[] Devid = new STU_DEVID[10];
                    Devlist.Clear();
                    BslErrCode IRes = func(Devid, ref iDevCount);
                    if (IRes == BslErrCode.BSL_ERR_SUCCESS)
                    {
                        int Icount = iDevCount;
                        for (int i = 0; i < Icount; i++)
                        {
                            string str = Encoding.Default.GetString(Devid[i].wszDevName).TrimEnd('\0');
                            Devlist.Add(str);
                        }
                    }
                    else
                    {
                        MsgLog($"ErrorCode={IRes}");
                        //System.Windows.MessageBox.Show($"ErrorCode={IRes}");
                    }
                    Devid = null;
                    if (Devlist.Count > 0)
                    {
                        return true;
                    }
                    //GC.Collect();
                }
                else
                {
                    MsgLog("没有找到该函数");
                }
            }
            else
            {
                dllInvokeFlag = true;
                MsgLog("MarkSDK.dll加载失败");
            }
            return true;
        }

        /// <summary>
        /// 硬件复位
        /// </summary>
        /// <param name="_hwnd"></param>
        /// <returns></returns>
        public BslErrCode ResetDevices(IntPtr _hwnd)
        {
            BSL_ResetDevices func = (BSL_ResetDevices)GetFunctionAddress("ResetDevices", typeof(BSL_ResetDevices));
            if (func == null)
            {
                hwnd = IntPtr.Zero;
                MsgLog("ResetDevices:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }

            try
            {
                BslErrCode errCode = func(_hwnd);
                if (errCode == BslErrCode.BSL_ERR_SUCCESS)
                {
                    hwnd = _hwnd;
                }
                else
                {
                    hwnd = IntPtr.Zero;
                }
                MsgLog("ResetDevices:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                hwnd = IntPtr.Zero;
                MsgLog("ResetDevices:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }


        /// <summary>
        /// 加载ORZ工程文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public BslErrCode LoadDataFile(string filename)
        {
            BSL_LoadDataFile func = (BSL_LoadDataFile)GetFunctionAddress("LoadDataFile", typeof(BSL_LoadDataFile));
            if (func == null)
            {
                nowItemFilePath = "";
                MsgLog("LoadDataFile:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                BslErrCode errCode = func(filename);
                if (errCode == BslErrCode.BSL_ERR_SUCCESS)
                {
                    nowItemFilePath = filename;
                }
                else
                {
                    nowItemFilePath = "";
                }
                MsgLog("LoadDataFile:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                nowItemFilePath = "";
                MsgLog("LoadDataFile:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }

        /// <summary>
        /// 卸载ORZ工程文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public BslErrCode UnloadDataFile()
        {
            if (string.IsNullOrEmpty(nowItemFilePath))
            {
                MsgLog("UnloadDataFile:" + BslErrCode.BSL_ERR_SUCCESS.ToString());
                return BslErrCode.BSL_ERR_SUCCESS;
            }

            BSL_UnLoadDataFile func = (BSL_UnLoadDataFile)GetFunctionAddress("UnloadDataFile", typeof(BSL_UnLoadDataFile));
            if (func == null)
            {
                nowItemFilePath = "";
                MsgLog("UnloadDataFile:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                BslErrCode errCode = func(nowItemFilePath);
                nowItemFilePath = "";
                MsgLog("UnloadDataFile:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                nowItemFilePath = "";
                MsgLog("UnloadDataFile:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }



        /// <summary>
        /// 获取工程文件图像
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Bitmap DrawFileInImg(int width = 128, int height = 128, bool bDrawAxis = true)
        {
            if (string.IsNullOrEmpty(nowItemFilePath))
            {
                MsgLog("DrawFileInImg:File is nothing");
                return null;
            }
            BSL_DrawFileInImg func = (BSL_DrawFileInImg)GetFunctionAddress("DrawFileInImg", typeof(BSL_DrawFileInImg));
            if (func == null)
            {
                nowItemFilePath = "";
                MsgLog("DrawFileInImg:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return null;
            }
            try
            {
                IntPtr hbitmap = func(nowItemFilePath, width, height, bDrawAxis);
                if (hbitmap != IntPtr.Zero)
                {
                    Bitmap bitmap = new Bitmap(System.Drawing.Image.FromHbitmap(hbitmap));
                    MsgLog("DrawFileInImg:" + BslErrCode.BSL_ERR_SUCCESS.ToString());
                    return bitmap;
                    //MemoryStream stream = new MemoryStream();
                    //bitmap.Save(stream, ImageFormat.Bmp);
                    //BitmapImage img = new BitmapImage();
                    //img.BeginInit();
                    //img.StreamSource = stream;
                    //img.EndInit();
                    //ImageSource = img;
                }
                else
                {
                    MsgLog("DrawFileInImg:" + BslErrCode.BSL_ERR_WRONGPARAM.ToString());
                }
            }
            catch (Exception ex)
            {
                MsgLog("DrawFileInImg:" + ex.Message);
            }
            return null;
        }



        public int nMarkLoop;              //加工次数
        public double dMarkSpeed;          //标刻次数mm/s
        public double dPowerRatio;         //功率百分比(0-100%)	
        public double dCurrent;            //电流A
        public int nFreq;                  //频率HZ
        public int nQPulseWidth;        //Q脉冲宽度us	
        public int nStartTC;               //开始延时us
        public int nLaserOffTC;            //激光关闭延时us
        public int nEndTC;                 //结束延时us
        public int nPolyTC;                //拐角延时us
        public double dJumpSpeed;          //跳转速度mm/s
        public int nJumpPosTC;             //跳转位置延时us 
        public int nJumpDistTC;            //跳转距离延时us	
        public double dEndComp;            //末点补偿mm				
        public bool bPulsePointMode;       //脉冲点模式 
        public int nPulseNum;              //脉冲点数目
        public float POINTTIME;           // 打点时间
        /// <summary>
        /// 获取当前项目文件参数
        /// </summary>
        /// <returns></returns>
        public BslErrCode GetPenParam(uint nPenNo)
        {
            if (string.IsNullOrEmpty(nowItemFilePath))
            {
                MsgLog("GetPenParam:" + BslErrCode.BSL_ERR_OPENVEC_FAIL.ToString());
                return BslErrCode.BSL_ERR_OPENVEC_FAIL;
            }

            BSL_GetPenParam func = (BSL_GetPenParam)GetFunctionAddress("GetPenParam", typeof(BSL_GetPenParam));
            if (func == null)
            {
                nowItemFilePath = "";
                MsgLog("GetPenParam:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                BslErrCode errCode = func(nowItemFilePath, nPenNo,
                    ref nMarkLoop,
                    ref dMarkSpeed,
                    ref dPowerRatio,
                    ref dCurrent,
                    ref nFreq,
                    ref nQPulseWidth,
                    ref nStartTC,
                    ref nLaserOffTC,
                    ref nEndTC,
                    ref nPolyTC,
                    ref dJumpSpeed,
                    ref nJumpPosTC,
                    ref nJumpDistTC,
                    ref dEndComp,
                    ref bPulsePointMode,
                    ref nPulseNum,
                    ref POINTTIME
                    );
                MsgLog("LoadDataFile:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                nowItemFilePath = "";
                MsgLog("LoadDataFile:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }

        public bool nWOBBLE;                       //是否摆动
        public bool nWOBBLESHOWROUTE;              //显示摆动路径
        public float nWOBBLEDIAMETER;              //摆动直径
        public float nWOBBLEDISTANCE;              //摆动间距
        public int   nWOBBLE_TYPE;                  //抖动类型: 0-圆形抖动; 1-椭圆形抖动; 2-正弦形抖动; 3-立8螺旋; 4-横8螺旋
        public float nWOBBLE_E11ipse_A;            //椭圆长轴长度
        public float nWOBBLE_Ellipse_B;            //椭圆短轴长度
        public float nWOBBLE_Sin_Amplitude;        //正弦振幅长度
        public float nWOBBLE_Sin_Cycle;            //正弦周期长度			 	
        public float nWOBBLE_SmoothPar;            //椭圆及正弦抖动时的曲线平滑系数,取值范围
        public float nWOBBLE_Space;                //8字间距
        public float nWOBBLE_Height;               //8字高
        public float nWOBBLE_Width;                //8字宽

        /// <summary>
        /// 获取当前项目文件参数
        /// </summary>
        /// <returns></returns>
        public BslErrCode GetWobble(uint nPenNo)
        {
            if (string.IsNullOrEmpty(nowItemFilePath))
            {
                MsgLog("GetWobble:" + BslErrCode.BSL_ERR_OPENVEC_FAIL.ToString());
                return BslErrCode.BSL_ERR_OPENVEC_FAIL;
            }

            BSL_GetWobble func = (BSL_GetWobble)GetFunctionAddress("GetWobble", typeof(BSL_GetWobble));
            if (func == null)
            {
                nowItemFilePath = "";
                MsgLog("GetWobble:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                BslErrCode errCode = func(
                    ref nWOBBLE,
                    ref nWOBBLESHOWROUTE,
                    ref nWOBBLEDIAMETER,
                    ref nWOBBLEDISTANCE,
                    ref nWOBBLE_TYPE,
                    ref nWOBBLE_E11ipse_A,
                    ref nWOBBLE_Ellipse_B,
                    ref nWOBBLE_Sin_Amplitude,
                    ref nWOBBLE_Sin_Cycle,
                    ref nWOBBLE_SmoothPar,
                    ref nWOBBLE_Space,
                    ref nWOBBLE_Height,
                    ref nWOBBLE_Width,
                    nowItemFilePath, nPenNo);
                MsgLog("LoadDataFile:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                nowItemFilePath = "";
                MsgLog("LoadDataFile:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }



        public BslErrCode GetEntSizeByIndex(int iIndex,out RectangleF rectF)
        {
            rectF = new RectangleF(0, 0, 0, 0);
            if (string.IsNullOrEmpty(nowItemFilePath))
            {
                MsgLog("GetEntSizeByIndex:" + BslErrCode.BSL_ERR_OPENVEC_FAIL.ToString());
                return BslErrCode.BSL_ERR_OPENVEC_FAIL;
            }
            double dMinx = 0;
            double dMiny = 0;
            double dMaxx = 0;
            double dMaxy = 0;
            double dZ = 0;
            BSL_GetEntSizeByIndex func = (BSL_GetEntSizeByIndex)GetFunctionAddress("GetEntSizeByIndex", typeof(BSL_GetEntSizeByIndex));
            if (func == null)
            {
                nowItemFilePath = "";
                MsgLog("GetEntSizeByIndex:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                BslErrCode errCode = func(nowItemFilePath, iIndex,
                    ref dMinx,
                    ref dMiny,
                    ref dMaxx,
                    ref dMaxy,
                    ref dZ);
                rectF.X = (float)dMinx; rectF.Y = (float)dMiny;
                rectF.Width = (float)(dMaxx- dMinx); rectF.Height = (float)(dMaxy- dMiny);
                MsgLog("LoadDataFile:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                nowItemFilePath = "";
                MsgLog("LoadDataFile:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }


        /// <summary>
        /// 工程文件+设备ID关联
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public BslErrCode AppendFileToDevice(string strFileName = "")
        {
            if (string.IsNullOrEmpty(strFileName)) strFileName = nowItemFilePath;
            if (string.IsNullOrEmpty(nowItemFilePath))
            {
                MsgLog("AppendFileToDevice:" + BslErrCode.BSL_ERR_OPENVEC_FAIL.ToString());
                return BslErrCode.BSL_ERR_OPENVEC_FAIL;
            }

            BSL_AppendFileToDevice func = (BSL_AppendFileToDevice)GetFunctionAddress("AppendFileToDevice", typeof(BSL_AppendFileToDevice));
            if (func == null)
            {
                MsgLog("AppendFileToDevice:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                if (Devlist.Count <= 0)
                {
                    MsgLog("AppendFileToDevice:" + BslErrCode.BSL_ERR_NODEVICE.ToString());
                    return BslErrCode.BSL_ERR_NODEVICE;
                }
                string myDevId = Devlist[0];
                BslErrCode errCode = func(strFileName, myDevId);

                MsgLog("AppendFileToDevice:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                MsgLog("AppendFileToDevice:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }

        /// <summary>
        /// 工程文件+设备ID解除关联
        /// </summary>
        /// <returns></returns>
        public BslErrCode UnappendFileToDevice()
        {
            if (string.IsNullOrEmpty(nowItemFilePath))
            {
                MsgLog("UnappendFileToDevice:" + BslErrCode.BSL_ERR_OPENVEC_FAIL.ToString());
                return BslErrCode.BSL_ERR_OPENVEC_FAIL;
            }

            BSL_UnappendFileFromDevice func = (BSL_UnappendFileFromDevice)GetFunctionAddress("UnappendFileToDevice", typeof(BSL_UnappendFileFromDevice));  //UnappendFileFromDevice
            if (func == null)
            {
                MsgLog("UnappendFileToDevice:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                if (Devlist.Count <= 0)
                {
                    MsgLog("UnappendFileToDevice:" + BslErrCode.BSL_ERR_NODEVICE.ToString());
                    return BslErrCode.BSL_ERR_NODEVICE;
                }
                string myDevId = Devlist[0];
                BslErrCode errCode = func(nowItemFilePath, myDevId);

                MsgLog("UnappendFileToDevice:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                MsgLog("UnappendFileToDevice:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }


        /// <summary>
        /// 根据设备ID出激光
        /// </summary>
        /// <returns></returns>
        public BslErrCode MarkByDeviceId()
        {
            if (string.IsNullOrEmpty(nowItemFilePath))
            {
                MsgLog("MarkByDeviceId:" + BslErrCode.BSL_ERR_OPENVEC_FAIL.ToString());
                return BslErrCode.BSL_ERR_OPENVEC_FAIL;
            }

            BSL_MarkByDeviceId func = (BSL_MarkByDeviceId)GetFunctionAddress("MarkByDeviceId", typeof(BSL_MarkByDeviceId));
            if (func == null)
            {
                nowItemFilePath = "";
                MsgLog("MarkByDeviceId:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                if (Devlist.Count <= 0)
                {
                    MsgLog("MarkByDeviceId:" + BslErrCode.BSL_ERR_NODEVICE.ToString());
                    return BslErrCode.BSL_ERR_NODEVICE;
                }
                string myDevId = Devlist[0];
                BslErrCode errCode = func(myDevId);

                MsgLog("MarkByDeviceId:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                nowItemFilePath = "";
                MsgLog("MarkByDeviceId:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }


        /// <summary>
        /// 出点激光
        /// </summary>
        /// <returns></returns>
        public BslErrCode MarkPoint()
        {
            BSL_MarkPoint func = (BSL_MarkPoint)GetFunctionAddress("MarkPoint", typeof(BSL_MarkPoint));
            if (func == null)
            {
                MsgLog("MarkPoint:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                if (Devlist.Count <= 0)
                {
                    MsgLog("MarkPoint:" + BslErrCode.BSL_ERR_NODEVICE.ToString());
                    return BslErrCode.BSL_ERR_NODEVICE;
                }
                string myDevId = Devlist[0];
                BslErrCode errCode = func(myDevId, 0, 0, 0, 0);

                MsgLog("MarkPoint:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                MsgLog("MarkPoint:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }

        }

        /// <summary>
        /// 停止加工
        /// </summary>
        /// <returns></returns>
        public BslErrCode StopMark()
        {
            BSL_StopMark func = (BSL_StopMark)GetFunctionAddress("StopMark", typeof(BSL_StopMark));
            if (func == null)
            {
                MsgLog("StopMark:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                if (Devlist.Count <= 0)
                {
                    MsgLog("StopMark:" + BslErrCode.BSL_ERR_NODEVICE.ToString());
                    return BslErrCode.BSL_ERR_NODEVICE;
                }
                string myDevId = Devlist[0];
                BslErrCode errCode = func(myDevId);

                MsgLog("StopMark:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                MsgLog("StopMark:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }

        
        /// <summary>
        /// 出红光
        /// </summary>
        /// <param name="bCountinue">是否连续红光</param>
        /// <returns></returns>
        public BslErrCode RedLightMark(bool bCountinue)
        {
            BSL_RedLightMark func = (BSL_RedLightMark)GetFunctionAddress("RedLightMark", typeof(BSL_RedLightMark));
            if (func == null)
            {
                MsgLog("RedLightMark:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                if (Devlist.Count <= 0)
                {
                    MsgLog("RedLightMark:" + BslErrCode.BSL_ERR_NODEVICE.ToString());
                    return BslErrCode.BSL_ERR_NODEVICE;
                }
                
                string myDevId = Devlist[0];
                BslErrCode errCode = func(myDevId, bCountinue);

                MsgLog("RedLightMark:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                MsgLog("RedLightMark:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }

        }

        /// <summary>
        /// 读取输入端口
        /// </summary>
        /// <param name="inPorts"></param>
        /// <returns></returns>
        public BslErrCode ReadInPort(out string inPorts)
        {
            inPorts = null;
            BSL_ReadInPort func = (BSL_ReadInPort)GetFunctionAddress("ReadInPort", typeof(BSL_ReadInPort));
            if (func == null)
            {
                MsgLog("ReadInPort:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                if (Devlist.Count <= 0)
                {
                    MsgLog("ReadInPort:" + BslErrCode.BSL_ERR_NODEVICE.ToString());
                    return BslErrCode.BSL_ERR_NODEVICE;
                }

                Int16 inPort = 0;
                string myDevId = Devlist[0];
                BslErrCode errCode = func(myDevId, ref inPort);
                if (errCode == 0)
                {
                    inPorts = Convert.ToString(inPort, 2);
                }
                MsgLog("ReadInPort:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                MsgLog("ReadInPort:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }

        /// <summary>
        /// 读取输出端口
        /// </summary>
        /// <param name="outPorts"></param>
        /// <returns></returns>
        public BslErrCode GetOutPort(out string outPorts)
        {
            outPorts = null;
            BSL_GetOutPort func = (BSL_GetOutPort)GetFunctionAddress("GetOutPort", typeof(BSL_GetOutPort));
            if (func == null)
            {
                MsgLog("GetOutPort:" + BslErrCode.BSL_ERR_NOMETHOD.ToString());
                return BslErrCode.BSL_ERR_NOMETHOD;
            }
            try
            {
                if (Devlist.Count <= 0)
                {
                    MsgLog("GetOutPort:" + BslErrCode.BSL_ERR_NODEVICE.ToString());
                    return BslErrCode.BSL_ERR_NODEVICE;
                }

                Int16 outPort = 0;
                string myDevId = Devlist[0];
                BslErrCode errCode = func(myDevId, ref outPort);
                if (errCode == 0)
                {
                    outPorts = Convert.ToString(outPort, 2);
                }
                MsgLog("GetOutPort:" + errCode.ToString());
                return errCode;
            }
            catch (Exception ex)
            {
                MsgLog("GetOutPort:" + ex.Message);
                return BslErrCode.BSL_ERR_UNKNOW;
            }
        }


    }//    public class DllInvoke

}


