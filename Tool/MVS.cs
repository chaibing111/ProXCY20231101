using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Threading;

using System.Windows.Forms;
namespace sunyvpp
{
    //图像委托
    //public delegate void ReadImageDelegate(HObject ho_image);
    public delegate void ReadImageDelegate(Bitmap ho_image);

    [Serializable]
    public class MVS : IAcqDeviceBase
    {
        #region 属性
        MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
        IntPtr pData;

        private object BufForDriverLock = new object();
        public bool IsGrabbing = true;                                                         //开始采集
        public bool IsContinuesMode = true;                                                    //连续采集 
        public event ReadImageDelegate ReadImageEvent;                                          //图像事件

        [NonSerialized]
        private MyCamera CameraDevice;                                                          //相机对象
        private int nRet = MyCamera.MV_OK;
        private static MyCamera.cbOutputExdelegate ImageCallback;                               //采集信息更新委托
        private static MyCamera.cbExceptiondelegate CameraLostDel;                              //相机异常委托
        [NonSerialized]
        private MyCamera.MV_CC_DEVICE_INFO stDevInfo;


        [NonSerialized]
        private Mutex m_mutex;                                                                  //锁，保证多线程安全 
        private UInt32 AyloadSize = 0;                                                          //网络包大小
        private IntPtr Temp = IntPtr.Zero;                                                      //从驱动获得数据指针或转换格式后图像数据指针
        private IntPtr ImageBuffer = IntPtr.Zero;                                               //转换格式时图像数据缓存指针
        private byte[] DataForRed;                                                              //从驱动获得数据时,红色通道存储空间
        private byte[] DataForGreen;                                                            //从驱动获得数据时,绿色通道存储空间
        private byte[] DataForBlue;                                                             //从驱动获得数据时,蓝色通道存储空间
        [NonSerialized]
        private IntPtr RedPtr = IntPtr.Zero;                                                    //转换成Halcon的HObject对象时,红色通道指针
        [NonSerialized]
        private IntPtr GreenPtr = IntPtr.Zero;                                                  //转换成Halcon的HObject对象时,绿色通道指针
        [NonSerialized]
        private IntPtr BluePtr = IntPtr.Zero;                                                   //转换成Halcon的HObject对象时,蓝色通道指针
        IntPtr m_BufForDriver;
        #endregion

        public MVS()
        {
            CameraDevice = new MyCamera();
            m_mutex = new Mutex();
            DataForRed = new byte[20971520];
            DataForGreen = new byte[20971520];
            DataForBlue = new byte[20971520];
        }

        //回调函数
        private void OnImageGrabbed(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            m_mutex.WaitOne();

            //HObject ho_image = new HObject();
            Bitmap ho_image = null;
            #region 海康相机SDK内部像素格式转换          

            if (ImageBuffer == IntPtr.Zero)
                ImageBuffer = Marshal.AllocHGlobal((int)AyloadSize * 3);

            if (ImageBuffer == IntPtr.Zero)
            {
                return;
            }

            System.Drawing.Bitmap bmpNow = null;
            #region 彩色图像
            if (IsColorPixelFormat(pFrameInfo.enPixelType))
            {
                if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                {
                    Temp = pData;
                }
                else
                {
                    int nRet = ConvertToRGB(CameraDevice, pData, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType, ImageBuffer);
                    if (MyCamera.MV_OK != nRet)
                    {
                        return;
                    }
                    Temp = ImageBuffer;
                }

                unsafe
                {
                    byte* pBufForSaveImage = (byte*)Temp;

                    UInt32 nSupWidth = (pFrameInfo.nWidth + (UInt32)3) & 0xfffffffc;

                    for (int nRow = 0; nRow < pFrameInfo.nHeight; nRow++)
                    {
                        for (int col = 0; col < pFrameInfo.nWidth; col++)
                        {
                            DataForRed[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col)];
                            DataForGreen[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 1)];
                            DataForBlue[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 2)];
                        }
                    }
                }

                RedPtr = Marshal.UnsafeAddrOfPinnedArrayElement(DataForRed, 0);
                GreenPtr = Marshal.UnsafeAddrOfPinnedArrayElement(DataForGreen, 0);
                BluePtr = Marshal.UnsafeAddrOfPinnedArrayElement(DataForBlue, 0);

                //try
                //{
                //    HOperatorSet.GenImage3Extern(out ho_image, new HTuple("byte"), pFrameInfo.nWidth, pFrameInfo.nHeight,
                //                         (new HTuple(RedPtr)), (new HTuple(GreenPtr)), (new HTuple(BluePtr)), IntPtr.Zero);
                //}
                //catch
                //{

                //    return;
                //}
            }
            #endregion

            #region 黑白图像
            else if (IsMonoPixelFormat(pFrameInfo.enPixelType))
            {
                if (pFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {

                    Temp = pData;
                    //************************Mono8 转 Bitmap*******************************
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(pFrameInfo.nWidth, pFrameInfo.nHeight, pFrameInfo.nWidth * 1, System.Drawing.Imaging.PixelFormat.Format8bppIndexed, pData);

                    System.Drawing.Imaging.ColorPalette cp = bmp.Palette;
                    // init palette
                    for (int i = 0; i < 256; i++)
                    {
                        cp.Entries[i] = System.Drawing.Color.FromArgb(i, i, i);
                    }
                    // set palette back
                    bmp.Palette = cp;
                    bmpNow = (System.Drawing.Bitmap)bmp.Clone();
                    bmp.Dispose();
                    //bmp.Save("image.bmp", ImageFormat.Bmp);
                }
                else
                {
                    int nRet = ConvertToMono8(CameraDevice, pData, ImageBuffer, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType);
                    if (MyCamera.MV_OK != nRet)
                    {
                        return;
                    }
                    Temp = ImageBuffer;
                }

                //try
                //{
                //    HOperatorSet.GenImage1Extern(out ho_image, "byte", pFrameInfo.nWidth, pFrameInfo.nHeight, Temp, IntPtr.Zero);
                //}
                //catch
                //{
                //    return;
                //}
            }
            #endregion

            #endregion

            if (ReadImageEvent != null)
            {
                ReadImageEvent(bmpNow);
            }


            m_mutex.ReleaseMutex();
        }

        //相机异常
        private void OnCameraConnectionLost(uint nMsgType, IntPtr pUser)
        {
            try
            {
                if (nMsgType != 32769) return;    //非指定事件不处理
                if (this.CameraDevice == null) return;
                this.CameraDevice.MV_CC_CloseDevice_NET();
                this.CameraDevice.MV_CC_DestroyDevice_NET();
                IsGrabbing = true;

                while (true)
                {
                    if (this.IsGrabbing) break;
                    if (this.CameraDevice.MV_CC_CreateDevice_NET(ref this.stDevInfo) != MyCamera.MV_OK)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    if (this.CameraDevice.MV_CC_OpenDevice_NET() != MyCamera.MV_OK)
                    {
                        this.CameraDevice.MV_CC_DestroyDevice_NET();
                        Thread.Sleep(100);
                        continue;
                    }
                    StartGrab();
                    break;
                }
            }
            catch (Exception ex)
            {
                //Logs.Error("相机出现异常!", ex);
            }
        }
        uint m_nRowStep;

        /// <summary>
        /// 获取当前可用设备SN
        /// </summary>
        /// <param name="cameraSNs"></param>
        public bool GetCameraSN(out List<string> cameraSNs)
        {
            try
            {
                int nRet;
                cameraSNs = new List<string>();
                MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST(); ;
                System.GC.Collect();
                nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
                if (0 != nRet)
                {
                    return true;
                }
                for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
                {
                    MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                        MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        cameraSNs.Add(gigeInfo.chSerialNumber);
                    }
                    else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                        MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        cameraSNs.Add(usbInfo.chSerialNumber);
                    }
                }
            }
            catch (Exception)
            {
                cameraSNs = null;
                return true;
            }
            return true;
        }

        /// <summary>
        /// 使用SN打开设备
        /// </summary>
        /// <param name="CamSN"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool OpenDevice(string CamSN, bool IsCallBack = false)
        {
            try
            {
                MyCamera.MV_CC_DEVICE_INFO_LIST stDevList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
                nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref stDevList);
                if (MyCamera.MV_OK != nRet)
                {
                    return true;
                }
                IntPtr CamName = IntPtr.Zero;
                for (Int32 i = 0; i < stDevList.nDeviceNum; i++)
                {
                    stDevInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(stDevList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (stDevInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        MyCamera.MV_GIGE_DEVICE_INFO stGigEDeviceInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(stDevInfo.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        if (stGigEDeviceInfo.chSerialNumber == CamSN)
                        {
                            CamName = stDevList.pDeviceInfo[i];
                            break;
                        }
                    }
                    else if (stDevInfo.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        MyCamera.MV_USB3_DEVICE_INFO stUsbDeviceInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(stDevInfo.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        if (stUsbDeviceInfo.chSerialNumber == CamSN)
                        {
                            CamName = stDevList.pDeviceInfo[i];
                            break;
                        }
                    }
                }
                stDevInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(CamName, typeof(MyCamera.MV_CC_DEVICE_INFO));

                #region 正常取图,方法
                //try
                //{
                //    //MyCamera.MV_CC_DEVICE_INFO Info = new MyCamera.MV_CC_DEVICE_INFO();
                //    CameraDevice.MV_CC_GetDeviceInfo_NET(ref stDevInfo);
                //}
                //catch (Exception ex)
                //{
                //    ;
                //}
                //// ch:获取包大小 || en: Get Payload Size
                //MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                //nRet = CameraDevice.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                //g_nPayloadSize = stParam.nCurValue;
                //pData = Marshal.AllocHGlobal((Int32)g_nPayloadSize);

                //// ch:获取高 || en: Get Height
                //nRet = CameraDevice.MV_CC_GetIntValue_NET("Height", ref stParam);
                //if (MyCamera.MV_OK != nRet)
                //{
                //    //MessageBox.Show("Get Height Fail");
                //    //return;
                //}
                //uint nHeight = stParam.nCurValue;

                //// ch:获取宽 || en: Get Width
                //nRet = CameraDevice.MV_CC_GetIntValue_NET("Width", ref stParam);
                //uint nWidth = stParam.nCurValue;

                //// ch:获取步长 || en: Get nRowStep
                //m_nRowStep = nWidth * nHeight;
                //pImageBuffer = Marshal.AllocHGlobal((int)m_nRowStep * 3);
                //// ch:设置触发模式为off || en:set trigger mode as off
                //if (g_nPayloadSize > 从驱动获取图像的缓存大小)
                //{
                //    if (相机图像数据接收指针 != IntPtr.Zero)
                //    {
                //        Marshal.Release(相机图像数据接收指针);
                //    }
                //    从驱动获取图像的缓存大小 = g_nPayloadSize;
                //    相机图像数据接收指针 = Marshal.AllocHGlobal((Int32)从驱动获取图像的缓存大小);
                //}

                ////nRet = CameraDevice.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);  //2-Continous    0 -singleFrame
                ////CameraDevice.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
                ////CameraDevice.MV_CC_SetEnumValue_NET("GainAuto", 0);
                //int nRet1 = CameraDevice.MV_CC_SetEnumValue_NET("TriggerMode", 1);
                //int nRet2 = CameraDevice.MV_CC_SetEnumValue_NET("TriggerSource", 7);
                //if (MyCamera.MV_OK != nRet || MyCamera.MV_OK != nRet1 || MyCamera.MV_OK != nRet2)
                //{
                //    MessageBox.Show("相机初始化失败");
                //}
                #endregion
                //海康正常初始化设备
                // ch:创建设备 | en:Create device
                nRet = CameraDevice.MV_CC_CreateDevice_NET(ref stDevInfo);
                if (MyCamera.MV_OK != nRet)
                {
                    return true;
                }
                // ch:打开设备 | en:Open device
                nRet = CameraDevice.MV_CC_OpenDevice_NET();
                #region MyRegion注释代码
                // ch:探测网络最佳包大小(只对GigE相机有效) //这次飞拍用的USB相机先屏蔽掉
                /* if (stDevInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                 {


                     int nPacketSize = CameraDevice.MV_CC_GetOptimalPacketSize_NET();

                     if (nPacketSize > 0)
                     {
                         nRet = CameraDevice.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                         if (nRet != MyCamera.MV_OK)
                         {
                             CameraDevice.MV_CC_CloseDevice_NET();
                             CameraDevice.MV_CC_DestroyDevice_NET();
                             CameraDevice = null;
                             return true;
                         }
                         AyloadSize = (uint)nPacketSize;
                     }
                     else
                     {
                         CameraDevice.MV_CC_CloseDevice_NET();
                         CameraDevice.MV_CC_DestroyDevice_NET();
                         CameraDevice = null;
                         return true;
                     }
                 }*/
                #endregion
                SetExposureAuto(false);
                SetGainAuto(false);
                //OpenCam();
                if (IsCallBack)
                {
                    Register();
                }

                CameraLostDel = new MyCamera.cbExceptiondelegate(OnCameraConnectionLost);//相机掉线重连


                return true;
            }
            catch (Exception ex)
            {
                //Logs.Error("相机打开时发生错误!", ex);
                return true;
            }

        }

        /// <summary>
        /// 注册回调函数
        /// </summary>
        public void Register()
        {
            ImageCallback = new MyCamera.cbOutputExdelegate(OnImageGrabbed);//码流回调函数
            nRet = CameraDevice.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero);
        }

        /// <summary>
        /// 取消回调函数
        /// </summary>
        public void Deregister()
        {
            ImageCallback -= new MyCamera.cbOutputExdelegate(OnImageGrabbed);//码流回调函数
            nRet = CameraDevice.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero);

        }

        /// <summary>
        /// 使用主动方式获取一帧图片数据
        /// </summary>
        /// <param name="ho_Image"></param>
        /// <returns></returns>
        public bool Capture(out HObject ho_Image)
        {
            //使用主动方式采图需关闭回调函数
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            try
            {

                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
              
                nRet = CameraDevice.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                if (MyCamera.MV_OK != nRet)//获取有效负载错误
                {
                    ho_Image = null;
                    return true;
                }
                MyCamera.MV_FRAME_OUT pFrameInfo = new MyCamera.MV_FRAME_OUT();
                nRet = CameraDevice.MV_CC_SetCommandValue_NET("TriggerSoftware");
                nRet = CameraDevice.MV_CC_GetImageBuffer_NET(ref pFrameInfo, 1000);
                //if (MyCamera.MV_OK == nRet)
                //{
                //    HOperatorSet.GenImage1(out ho_Image, "byte", pFrameInfo.stFrameInfo.nWidth, pFrameInfo.stFrameInfo.nHeight, pFrameInfo.pBufAddr);
                //}
                //else
                //{
                //    ho_Image = null;
                //}
                if (pFrameInfo.pBufAddr != IntPtr.Zero)
                {
                    nRet = CameraDevice.MV_CC_FreeImageBuffer_NET(ref pFrameInfo);//释放buff
                    if (MyCamera.MV_OK != nRet) { }
                }

                }
            catch (Exception)
            {
                ho_Image = null;
                return true;
            }
            return true;
        }

        /// <summary>
        ///  海康正常取图代码,转Bitmap
        /// </summary>
        /// <param name="CopyOutPutBitmap"></param>
        /// <returns></returns>
        public bool Capture_New(out Bitmap CopyOutPutBitmap)
        {
            SetExposureTime(Globals.SettingOption.UpCamExpose1);//"曝光"
            //CameraDevice.SetExposureTime(Globals.SettingOption.CamExpose);//"曝光"
            MyCamera.MV_FRAME_OUT stImageOut = new MyCamera.MV_FRAME_OUT();
            try
            {
                #region MyRegion
                CameraDevice.MV_CC_SetCommandValue_NET("TriggerSoftware");
                    nRet = CameraDevice.MV_CC_GetImageBuffer_NET(ref stImageOut, 1000);
                    if (nRet == MyCamera.MV_OK)
                    {
                        Console.WriteLine("Get Image Buffer:" + "Width[" + Convert.ToString(stImageOut.stFrameInfo.nWidth) + "] , Height[" + Convert.ToString(stImageOut.stFrameInfo.nHeight)
                                        + "] , FrameNum[" + Convert.ToString(stImageOut.stFrameInfo.nFrameNum) + "]");
                        CameraDevice.MV_CC_FreeImageBuffer_NET(ref stImageOut);
                    }
                    else
                    {
                        Console.WriteLine("Get Image failed:{0:x8}", nRet);
                    }
                //if (stImageOut.stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_HB_Mono8)
                //{
                //Mono8转Bitmap
                //如果是用Capture_New   Mono8转Bitmap     CaptureCalib   //BGR转Bitmap
                //Camera.CaptureCalib(out Bitmap bt);
                //Camera.Capture_New(out Bitmap bt);
                Bitmap bmp = new Bitmap(stImageOut.stFrameInfo.nWidth, stImageOut.stFrameInfo.nHeight, stImageOut.stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, stImageOut.pBufAddr);
                ColorPalette cp = bmp.Palette;
                for (int i = 0; i < 256; i++)
                {
                    cp.Entries[i] = Color.FromArgb(i, i, i);
                }
                bmp.Palette = cp;
                //bmp.Save("image.bmp", ImageFormat.Bmp);
                CopyOutPutBitmap = (Bitmap)bmp.Clone();
                //}
                //else
                //{
                //    //BGR转Bitmap
                //    Bitmap bmp = new Bitmap(stImageOut.stFrameInfo.nWidth, stImageOut.stFrameInfo.nHeight, stImageOut.stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, stImageOut.pBufAddr);
                //    //bmp.Save("image.bmp", ImageFormat.Bmp);
                //    CopyOutPutBitmap = (Bitmap)bmp.Clone();
                //}
                //if (stImageOut.stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_HB_Mono8)
                //{

                //Bitmap bmp = new Bitmap(stImageOut.stFrameInfo.nWidth, stImageOut.stFrameInfo.nHeight, stImageOut.stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, stImageOut.pBufAddr);
                //    //bmp.Save("image.bmp", ImageFormat.Bmp);
                //    CopyOutPutBitmap = (Bitmap)bmp.Clone();

                //}
                return true;
                #endregion


            }
            catch (Exception)
            {
                CopyOutPutBitmap = null;
                return true;
            }
        }
        public bool Capture_New(int CamExpose, out Bitmap CopyOutPutBitmap)
        {
            SetExposureTime(CamExpose);//"曝光"

            //SetGain(14);
            MyCamera.MV_FRAME_OUT stImageOut = new MyCamera.MV_FRAME_OUT();
            try
            {
                #region MyRegion
                CameraDevice.MV_CC_SetCommandValue_NET("TriggerSoftware");
                nRet = CameraDevice.MV_CC_GetImageBuffer_NET(ref stImageOut, 1000);
                if (nRet == MyCamera.MV_OK)
                {
                    Console.WriteLine("Get Image Buffer:" + "Width[" + Convert.ToString(stImageOut.stFrameInfo.nWidth) + "] , Height[" + Convert.ToString(stImageOut.stFrameInfo.nHeight)
                                    + "] , FrameNum[" + Convert.ToString(stImageOut.stFrameInfo.nFrameNum) + "]");
                    CameraDevice.MV_CC_FreeImageBuffer_NET(ref stImageOut);
                }
                else
                {
                    Console.WriteLine("Get Image failed:{0:x8}", nRet);
                }
                //if (stImageOut.stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_HB_Mono8)
                //{
                //Mono8转Bitmap
                //如果是用Capture_New   Mono8转Bitmap     CaptureCalib   //BGR转Bitmap
                //Camera.CaptureCalib(out Bitmap bt);
                //Camera.Capture_New(out Bitmap bt);
                Bitmap bmp = new Bitmap(stImageOut.stFrameInfo.nWidth, stImageOut.stFrameInfo.nHeight, stImageOut.stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, stImageOut.pBufAddr);
                ColorPalette cp = bmp.Palette;
                for (int i = 0; i < 256; i++)
                {
                    cp.Entries[i] = Color.FromArgb(i, i, i);
                }
                bmp.Palette = cp;
                //bmp.Save("image.bmp", ImageFormat.Bmp);
                CopyOutPutBitmap = (Bitmap)bmp.Clone();
                //}
                //else
                //{
                //    //BGR转Bitmap
                //    Bitmap bmp = new Bitmap(stImageOut.stFrameInfo.nWidth, stImageOut.stFrameInfo.nHeight, stImageOut.stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, stImageOut.pBufAddr);
                //    //bmp.Save("image.bmp", ImageFormat.Bmp);
                //    CopyOutPutBitmap = (Bitmap)bmp.Clone();
                //}
                //if (stImageOut.stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_HB_Mono8)
                //{

                //Bitmap bmp = new Bitmap(stImageOut.stFrameInfo.nWidth, stImageOut.stFrameInfo.nHeight, stImageOut.stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, stImageOut.pBufAddr);
                //    //bmp.Save("image.bmp", ImageFormat.Bmp);
                //    CopyOutPutBitmap = (Bitmap)bmp.Clone();

                //}
                return true;
                #endregion


            }
            catch (Exception)
            {
                CopyOutPutBitmap = null;
                return true;
            }
        }
        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(int Destination, int add, int Length);
        private Bitmap HObjectToBitmap(HObject ho_Image,out Bitmap bmpImage)
        {
            try
            {
                HOperatorSet.GetImagePointer1(ho_Image, out HTuple pointer, out HTuple type, out HTuple width, out HTuple height);
                 bmpImage = new Bitmap(width.I, height.I, PixelFormat.Format8bppIndexed);
                ColorPalette pal = bmpImage.Palette;
                for (int i = 0; i < 256; i++)
                {
                    pal.Entries[i] = Color.FromArgb(255, i, i, i);
                }
                bmpImage.Palette = pal;
                BitmapData bitmapData = bmpImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                int pixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
                int stride = bitmapData.Stride;
                int ptr = bitmapData.Scan0.ToInt32();
                if (width % 4 == 0)
                    CopyMemory(ptr, pointer, width * height * pixelSize);
                else
                {
                    for (int i = 0; i < height; i++)
                    {
                        CopyMemory(ptr, pointer, width * pixelSize);
                        pointer += width;
                        ptr += bitmapData.Stride;
                    }
                }
                bmpImage.UnlockBits(bitmapData);
                return bmpImage;
            }
            catch (Exception)
            {
                bmpImage = null;
                return null;
            }
        }

        public bool Capture( out Bitmap mimage)
        {
            HTuple type, width, height, pointer;
            HObject haImage, ho_Image;
            mimage = null;
            //使用主动方式采图需关闭回调函数
            HOperatorSet.GenEmptyObj(out ho_Image);
            ho_Image.Dispose();
            try
            {

                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                //nRet = CameraDevice.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                if (MyCamera.MV_OK != nRet)//获取有效负载错误
                {
                    ho_Image = null;
                    return true;
                }
                MyCamera.MV_FRAME_OUT pFrameInfo = new MyCamera.MV_FRAME_OUT();
                nRet = CameraDevice.MV_CC_SetCommandValue_NET("TriggerSoftware");
                nRet = CameraDevice.MV_CC_GetImageBuffer_NET(ref pFrameInfo, 1000);
                if (MyCamera.MV_OK == nRet)
                {
                    HOperatorSet.GenImage1(out ho_Image, "byte", pFrameInfo.stFrameInfo.nWidth, pFrameInfo.stFrameInfo.nHeight, pFrameInfo.pBufAddr);
                }
                else
                {
                    ho_Image = null;
                }
                if (pFrameInfo.pBufAddr != IntPtr.Zero)
                {
                    nRet = CameraDevice.MV_CC_FreeImageBuffer_NET(ref pFrameInfo);//释放buff
                    if (MyCamera.MV_OK != nRet) { }
                }
               
                Bitmap res = null;
                 HObjectToBitmap(ho_Image,out mimage);
                //HOperatorSet.GetImagePointer3(image, out hred, out hgreen, out hblue, out type, out width, out height);
                //HOperatorSet.GetImageSize(ho_Image, out width, out height);
                //HOperatorSet.InterleaveChannels(ho_Image, out haImage, "rgb", 4 * width, 0);
                //HOperatorSet.GetImagePointer1(haImage, out pointer, out type, out width, out height);

                //IntPtr ptr = pointer;
                // new Bitmap(width / 4, height, width, System.Drawing.Imaging.PixelFormat.Format24bppRgb, ptr);
                //Bitmap mimage = new Bitmap(res);
                //mimage =( Bitmap) res.Clone();
                return true;
            }
            catch (Exception)
            {
                ho_Image = null;
                return true;
            }
            return true;
        }

        private Boolean IsMonoData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;

                default:
                    return true;
            }
        }
        private Boolean IsColorData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YCBCR411_8_CBYYCRYY:
                    return true;

                default:
                    return true;
            }
        }

        public void Getbitmap(MyCamera In相机, MyCamera.MV_FRAME_OUT_INFO_EX In贞信息, IntPtr In相机数据指针, IntPtr In用于保存图像的指针, out Bitmap OutBitmap)
        {
            Stopwatch TIM = new Stopwatch();
            TIM.Restart();
            OutBitmap = null;
            if (RemoveCustomPixelFormats(In贞信息.enPixelType))
            {
                Console.WriteLine("Not Support!");
                return;
            }

            IntPtr CCpTemp = IntPtr.Zero;
            MyCamera.MvGvspPixelType CC像素格式 = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;
            if (In贞信息.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8 || In贞信息.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed)
            {
                CCpTemp = In相机数据指针;
                CC像素格式 = In贞信息.enPixelType;
            }
            else
            {
                UInt32 CC图像需要大小 = 0;
                //  lock (BufForDriverLock)   //屏蔽
                {
                    if (In贞信息.nFrameLen == 0)
                    {
                        Console.WriteLine("Save Bmp Fail!");
                        return;
                    }

                    if (IsMonoData(In贞信息.enPixelType))
                    {
                        CC像素格式 = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
                        CC图像需要大小 = (uint)In贞信息.nWidth * In贞信息.nHeight;
                    }
                    else if (IsColorData(In贞信息.enPixelType))
                    {
                        CC像素格式 = MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed;
                        CC图像需要大小 = (uint)In贞信息.nWidth * In贞信息.nHeight * 3;
                    }
                    else
                    {
                        Console.WriteLine("No such pixel type!");
                        return;
                    }
                    UInt32 CC用于保存图像的缓存大小 = 0;
                    if (CC用于保存图像的缓存大小 < CC图像需要大小)
                    {
                        if (In用于保存图像的指针 != IntPtr.Zero)
                        {
                            Marshal.Release(In用于保存图像的指针);
                        }
                        CC用于保存图像的缓存大小 = CC图像需要大小;
                        In用于保存图像的指针 = Marshal.AllocHGlobal((Int32)CC用于保存图像的缓存大小);
                    }
                    MyCamera.MV_PIXEL_CONVERT_PARAM CC图像转换结构体 = new MyCamera.MV_PIXEL_CONVERT_PARAM();
                    CC图像转换结构体.nWidth = In贞信息.nWidth;
                    CC图像转换结构体.nHeight = In贞信息.nHeight;
                    CC图像转换结构体.pSrcData = In相机数据指针;
                    CC图像转换结构体.nSrcDataLen = In贞信息.nFrameLen;
                    CC图像转换结构体.enSrcPixelType = In贞信息.enPixelType;
                    CC图像转换结构体.enDstPixelType = CC像素格式;
                    CC图像转换结构体.pDstBuffer = In用于保存图像的指针;
                    CC图像转换结构体.nDstBufferSize = CC用于保存图像的缓存大小;
                    int nRet = 0;
                    GC.Collect();
                    ////这句话增加内存
                    nRet = In相机.MV_CC_ConvertPixelType_NET(ref CC图像转换结构体);

                    ////这句话增加内存
                    GC.Collect();
                    if (MyCamera.MV_OK != nRet)
                    {
                        Console.WriteLine("Convert Pixel Type Fail!");
                        return;
                    }
                    CCpTemp = In用于保存图像的指针;
                }
            }


            //    lock (BufForDriverLock)    //屏蔽
            {
                if (CC像素格式 == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    //************************Mono8 转 Bitmap*******************************
                    Bitmap bmp = new Bitmap(In贞信息.nWidth, In贞信息.nHeight, In贞信息.nWidth * 1, PixelFormat.Format8bppIndexed, CCpTemp);

                    ColorPalette cp = bmp.Palette;
                    // init palette
                    for (int i = 0; i < 256; i++)
                    {
                        cp.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    // set palette back
                    bmp.Palette = cp;
                    //   bmp.Save("image.bmp", ImageFormat.Bmp);
                    //   pictureBox2.Image = bmp;
                    OutBitmap = bmp;
                }
                else
                {
                    //*********************BGR8 转 Bitmap**************************
                    try
                    {
                        Bitmap bmp = new Bitmap(In贞信息.nWidth, In贞信息.nHeight, In贞信息.nWidth * 3, PixelFormat.Format24bppRgb, CCpTemp);
                        //  bmp.Save("image.bmp", ImageFormat.Bmp);
                        //   pictureBox2.Image = bmp;
                        OutBitmap = bmp;
                    }
                    catch
                    {
                        Console.WriteLine("Write File Fail!");
                    }
                }

                TIM.Stop();

            }

            //   ShowErrorMsg("Save Succeed!", 0);

            GC.Collect();


        }
        public UInt32 从驱动获取图像的缓存大小 = 0;
        public IntPtr 相机图像数据接收指针;

        IntPtr pImageBuffer;
        // ch:用于保存图像的缓存 | en:Buffer for saving image
        public UInt32 用于保存图像的缓存大小 = 0;
        public IntPtr 用于保存图像的指针;
        public MyCamera.MV_FRAME_OUT_INFO_EX 输出贞信息;
        public Bitmap MyOutPutBitmap;
        public UInt32 g_nPayloadSize = 0;
        Bitmap CopyOutPutBitmap;
        public Bitmap CaptureCalib(out Bitmap CopyOutPutBitmap)        //System.Windows.Forms.PictureBox picure)
        {
            #region MyRegion
            int nRet;
            //MyCamera device = obj as MyCamera;
            nRet = CameraDevice.MV_CC_ClearImageBuffer_NET();
            //CameraDevice.SetExposureTime(Globals.SettingOption.CamExpose);//"曝光"
            //Camera.SetGain(Globals.SettingOption.CamGain);//"增益"
            nRet = CameraDevice.MV_CC_SetFloatValue_NET("ExposureTime", Globals.SettingOption.UpCamExpose1);//"曝光"
            CameraDevice.MV_CC_SetFloatValue_NET("Gain", 0);//"增益"
            //CameraDevice.MV_CC_SetFloatValue_NET("Gain", Globals.SettingOption.CamGain);//"增益"

            //nRet = m_MyCamera.MV_CC_SetIntValue_NET("Height", 照片高);
            //    nRet = m_MyCamera.MV_CC_SetIntValue_NET("Width", 照片宽);
            //    nRet = m_MyCamera.MV_CC_SetHeight_NET(照片高);
            //    nRet = m_MyCamera.MV_CC_SetWidth_NET(照片宽);
            nRet = CameraDevice.MV_CC_SetCommandValue_NET("TriggerSoftware");
            if (MyCamera.MV_OK != nRet)
            {
                MessageBox.Show("Trigger Fail");
            }

            nRet = CameraDevice.MV_CC_GetOneFrameTimeout_NET(pData, g_nPayloadSize, ref pFrameInfo, 500);
            Getbitmap(CameraDevice, pFrameInfo, pData, 用于保存图像的指针, out MyOutPutBitmap);
            CopyOutPutBitmap = (Bitmap)MyOutPutBitmap.Clone();

            return CopyOutPutBitmap;


            #endregion



            #region MyRegion
            //MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE(); 
            //nRet = CameraDevice.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            //if (MyCamera.MV_OK != nRet)
            //{

            //}

            //UInt32 nPayloadSize = stParam.nCurValue;
            //if (nPayloadSize > 0)
            //{
            //    if (m_BufForDriver != IntPtr.Zero)
            //    {
            //        Marshal.Release(m_BufForDriver);
            //    }
            //    //m_nBufSizeForDriver = nPayloadSize;
            //    m_BufForDriver = Marshal.AllocHGlobal((Int32)nPayloadSize);
            //}

            //if (m_BufForDriver == IntPtr.Zero)
            //{
            //    return true;
            //}

            //MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
            //MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo = new MyCamera.MV_DISPLAY_FRAME_INFO();

            //lock (BufForDriverLock)
            //{
            //    nRet = CameraDevice.MV_CC_GetOneFrameTimeout_NET(m_BufForDriver, nPayloadSize, ref stFrameInfo, 1000);
            //    //if (nRet == MyCamera.MV_OK)
            //    //{
            //    //    m_stFrameInfo = stFrameInfo;
            //    //}
            //}

            //if (nRet == MyCamera.MV_OK)
            //{
            //    if (RemoveCustomPixelFormats(stFrameInfo.enPixelType))
            //    {
            //        ;
            //    }

            //    stDisplayInfo.hWnd = picure.Handle;
            //    stDisplayInfo.pData = m_BufForDriver;
            //    stDisplayInfo.nDataLen = stFrameInfo.nFrameLen;
            //    stDisplayInfo.nWidth = stFrameInfo.nWidth;
            //    stDisplayInfo.nHeight = stFrameInfo.nHeight;
            //    stDisplayInfo.enPixelType = stFrameInfo.enPixelType;
            //    CameraDevice.MV_CC_DisplayOneFrame_NET(ref stDisplayInfo);
            //}
            //else
            //{

            //}
            #endregion


            //try
            //{
            //    HOperatorSet.GenImage1Extern(out ho_image, "byte", pFrameInfo.nWidth, pFrameInfo.nHeight, Temp, IntPtr.Zero);
            //}
            //catch
            //{
            //    return;
            //}


        }

        private bool RemoveCustomPixelFormats(MyCamera.MvGvspPixelType enPixelFormat)
        {
            Int32 nResult = ((int)enPixelFormat) & (unchecked((Int32)0x80000000));
            if (0x80000000 == nResult)
            {
                return true;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 软触发一次
        /// </summary>
        /// <returns></returns>
        public bool Capture()
        {
            try
            {
                nRet = CameraDevice.MV_CC_SetCommandValue_NET("TriggerSoftware");
                if (nRet != MyCamera.MV_OK)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 设置曝光
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetExposureTime(float value)
        {
            try
            {
                nRet = CameraDevice.MV_CC_SetFloatValue_NET("ExposureTime", value);
                if (nRet != MyCamera.MV_OK)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetGain(float value)
        {
            try
            {
                nRet = CameraDevice.MV_CC_SetFloatValue_NET("Gain", value);
                if (nRet != MyCamera.MV_OK)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 设置开启自动曝光
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetExposureAuto(bool value)
        {

            try
            {
                if (value)
                {
                    nRet = CameraDevice.MV_CC_SetEnumValue_NET("ExposureAuto", 1);//开启自动曝光
                }
                else
                {
                    nRet = CameraDevice.MV_CC_SetEnumValue_NET("ExposureAuto", 0);//关闭自动曝光
                }

                if (nRet != MyCamera.MV_OK)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 设置开启自动增益
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetGainAuto(bool value)
        {
            try
            {
                if (value)
                {
                    nRet = CameraDevice.MV_CC_SetEnumValue_NET("GainAuto", 1);//自动增益开启
                }
                else
                {
                    nRet = CameraDevice.MV_CC_SetEnumValue_NET("GainAuto", 0);//自动增益关闭
                }

                if (nRet != MyCamera.MV_OK)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 设置心跳超时时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetTimeOut(uint value)
        {
            try
            {

                nRet = CameraDevice.MV_CC_SetHeartBeatTimeout_NET(value);
                if (nRet != MyCamera.MV_OK)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 设置触发模式
        /// </summary>
        /// <returns></returns>
        public bool SetTrigerMode(bool value)
        {
            try
            {
                if (value)
                {
                    CameraDevice.MV_CC_SetEnumValue_NET("TriggerMode", 1);//开启触发模式              
                }
                else
                {
                    CameraDevice.MV_CC_SetEnumValue_NET("TriggerMode", 0);//关闭触发模式              
                }
            }
            catch (Exception)
            {
                return true;
            }

            return true;
        }

        /// <summary>
        /// 设置触发源
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool SetTriggerSource(int index)
        {
            try
            {
                if (index == 0)
                {
                    CameraDevice.MV_CC_SetEnumValue_NET("TriggerSource", 0);//选择线0为触发源
                }
                else if (index == 1)
                {
                    CameraDevice.MV_CC_SetEnumValue_NET("TriggerSource", 1);//选择线1为触发源
                }
                else if (index == 2)
                {
                    CameraDevice.MV_CC_SetEnumValue_NET("TriggerSource", 2);//选择线2为触发源
                }
                else if (index == 7)
                {
                    CameraDevice.MV_CC_SetEnumValue_NET("TriggerSource", 7);//软触发
                }
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 设置相机名称
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool SetCamName(string str)
        {
            try
            {
                nRet = CameraDevice.MV_CC_SetStringValue_NET("DeviceUserID", str); ;
                if (nRet != MyCamera.MV_OK)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 开启采集
        /// </summary>
        /// <returns></returns>
        public bool StartGrab()
        {
            try
            {
                if (CameraDevice.MV_CC_StartGrabbing_NET() != 0)
                {
                    return true;
                }
                IsGrabbing = true;
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 停止采集
        /// </summary>
        /// <returns></returns>
        public bool StopGrab()
        {
            try
            {
                CameraDevice.MV_CC_StopGrabbing_NET();
                IsGrabbing = true;
            }
            catch (Exception)
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool CloseDevice()
        {
            if (CameraDevice == null)
            {
                return true;
            }
            if (IsGrabbing)
            {
                CameraDevice.MV_CC_StopGrabbing_NET();
                IsGrabbing = true;
            }
            if (CameraDevice.MV_CC_CloseDevice_NET() != 0)
            {
                return true;
            }
            if (CameraDevice.MV_CC_DestroyDevice_NET() != 0)
            {
                return true;

            }
            GC.Collect();
            CameraDevice = null;
            return true;
        }

        #region 海康相机SDK官方函数
        /// <summary>
        /// 判断是否黑白图像
        /// </summary>
        /// <param name="enType"></param>
        /// <returns></returns>
        private bool IsMonoPixelFormat(MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;
                default:
                    return true;
            }
        }

        /// <summary>
        /// 判断是否彩色图像
        /// </summary>
        /// <param name="enType"></param>
        /// <returns></returns>
        private bool IsColorPixelFormat(MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                    return true;
                default:
                    return true;
            }
        }

        /// <summary>
        /// 转换为黑白图像
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pInData"></param>
        /// <param name="pOutData"></param>
        /// <param name="nHeight"></param>
        /// <param name="nWidth"></param>
        /// <param name="nPixelType"></param>
        /// <returns></returns>
        public Int32 ConvertToMono8(object obj, IntPtr pInData, IntPtr pOutData, ushort nHeight, ushort nWidth, MyCamera.MvGvspPixelType nPixelType)
        {
            if (IntPtr.Zero == pInData || IntPtr.Zero == pOutData)
            {
                return MyCamera.MV_E_PARAMETER;
            }

            int nRet = MyCamera.MV_OK;
            MyCamera device = obj as MyCamera;
            MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pInData;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }

            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pOutData;//转换后的数据
            stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * 3);

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return nRet;
        }

        /// <summary>
        /// 转换为彩色图像
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="pSrc"></param>
        /// <param name="nHeight"></param>
        /// <param name="nWidth"></param>
        /// <param name="nPixelType"></param>
        /// <param name="pDst"></param>
        /// <returns></returns>
        public Int32 ConvertToRGB(object obj, IntPtr pSrc, ushort nHeight, ushort nWidth, MyCamera.MvGvspPixelType nPixelType, IntPtr pDst)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return MyCamera.MV_E_PARAMETER;
            }

            int nRet = MyCamera.MV_OK;
            MyCamera device = obj as MyCamera;
            MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pSrc;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }

            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pDst;//转换后的数据
            stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            stPixelConvertParam.nDstBufferSize = (uint)nWidth * nHeight * 3;

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }

            return MyCamera.MV_OK;
        }

        #endregion

    }
}
