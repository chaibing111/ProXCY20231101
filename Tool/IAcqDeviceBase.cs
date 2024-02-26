using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    public interface IAcqDeviceBase
    {
        bool GetCameraSN(out List<string> cameraSNs);//获取SN
        bool OpenDevice(string CamSN,bool IsCallBack = true);//使用SN打开相应相机
        bool Capture(out HObject ho_Image);//使用主动方式取一帧图片
        bool Capture();//触发一次软触发,从回调中取图
        bool SetExposureTime(float value);//设置曝光时间
        bool SetGain(float value);//设置增益
        bool SetExposureAuto(bool value);//设置开启自动曝光
        bool SetGainAuto(bool value);//设置开启自动增益
        bool SetTimeOut(uint value);//设置心跳时间
        bool SetTrigerMode(bool vlaue);//打开触发,true,关闭触发   
        bool SetTriggerSource(int index); //设置触发模式,1:line1触发,7:软触发
        bool SetCamName(string str);//设置相机用户名称
        bool StartGrab();//开始采集
        bool StopGrab();//停止采集
        bool CloseDevice();//关闭相机设备
    }


}
