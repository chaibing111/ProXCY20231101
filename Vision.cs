using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Cognex.VisionPro;
using Cognex.VisionPro.QuickBuild;
using Cognex.VisionPro.ToolGroup;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.Implementation;

namespace sunyvpp
{
   public class Vision
    {

       private MainForm m;
       public Vision(MainForm main)
       {
           m = main;
       }
       #region 公共变量
       public CogJobManager _Jm = new CogJobManager();
        public CogJob LeftCCDJb = new CogJob();//UpCCD
        public CogToolGroup LeftCCDTg = new CogToolGroup();
        public CogAcqFifoTool CameraAcq_L = new CogAcqFifoTool();
        public CogToolBlock CameraStitch_L = new CogToolBlock();
        public CogToolBlock CameraBaseLine_L = new CogToolBlock();
        public CogToolBlock CameraRun_L = new CogToolBlock();

        public CogJob RightCCDJb = new CogJob();//DownCCD
        public CogToolGroup RightCCDTg = new CogToolGroup();
        public CogAcqFifoTool CameraAcq_R = new CogAcqFifoTool();
        public CogToolBlock CameraStitch_R = new CogToolBlock();
        public CogToolBlock CameraBaseLine_R = new CogToolBlock();
        public CogToolBlock CameraRun_R = new CogToolBlock();
        
        private Cognex.VisionPro.CogGraphicCollection Gh = new CogGraphicCollection();

        public CogJobManager Jm
        {
            get { return _Jm; }
            set { _Jm = value; }
        }
       #endregion
        /// <summary>
        /// 初始化視覺功能函數
        /// </summary>
        /// <param name="filename">視覺文檔路徑</param>
        public void VisionInitial(string filename)
        {
            try
            {
                //if (CogMisc.GetLicensedFeatures(false).Count == 0)
                //{
                //    MessageBox.Show("請確認加密狗已經被正確安裝！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    Environment.Exit(1);
                //}
                //是否有存檔，讀取視覺存檔

                //switch (Vars.SelectProductionFlag)
                //{
                //    case 0: //植球前
                //                 if (File.Exists(filename))
                //        {
                //            FileStream s = new FileStream(filename, FileMode.Open);
                //            _Jm = CogSerializer.LoadObjectFromStream(s) as CogJobManager;
                //            s.Close();
                //        }
                //        else
                //        {
                //            //m.PublicFuction.DisplayMessage("异常", "加载视觉文件失败！", Color.Red);
                //            MessageBox.Show("缺少程序運行必要的文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            Environment.Exit(1);
                //        }
                //        //左相机
                //        LeftCCDJb = _Jm.Job(0);
                //        LeftCCDTg = LeftCCDJb.VisionTool as CogToolGroup;
                //        CameraAcq_L = LeftCCDTg.Tools["LeftCamera"] as CogAcqFifoTool;

                //        //右相机
                //        RightCCDJb = _Jm.Job(1);
                //        RightCCDTg = RightCCDJb.VisionTool as CogToolGroup;
                //        CameraAcq_R = RightCCDTg.Tools["RightCamera"] as CogAcqFifoTool;
                //        break;
                //    case 1://植球后
                //        //左相机
                //        LeftCCDJb = _Jm.Job(0);
                //        LeftCCDTg = LeftCCDJb.VisionTool as CogToolGroup;
                //        CameraAcq_L = LeftCCDTg.Tools["LeftCamera"] as CogAcqFifoTool;
                //        //右相机
                //        RightCCDJb = _Jm.Job(1);
                //        RightCCDTg = RightCCDJb.VisionTool as CogToolGroup;
                //        CameraAcq_R = RightCCDTg.Tools["RightCamera"] as CogAcqFifoTool;
                //        break;
                //    default: break;
                        
                //}

                LeftCCDJb = _Jm.Job(0);
                LeftCCDTg = LeftCCDJb.VisionTool as CogToolGroup;
                CameraAcq_L = LeftCCDTg.Tools["LeftCamera"] as CogAcqFifoTool;

                //右相机
                RightCCDJb = _Jm.Job(1);
                RightCCDTg = RightCCDJb.VisionTool as CogToolGroup;
                CameraAcq_R = RightCCDTg.Tools["RightCamera"] as CogAcqFifoTool;


            }
            catch (Exception ex)
            {
                //m.PublicFuction.DisplayMessage("异常", ex.Message, Color.Red);
                MessageBox.Show(ex.Message);
                Environment.Exit(1);
            }
        }
        #region
        public void CameraAcq_Left()
        {
            CameraAcq_L.Run();
        }
        public void CameraBaseLine_Left()
        {
            CameraBaseLine_L.Inputs[0].Value = CameraAcq_L.OutputImage as CogImage8Grey;
            CameraBaseLine_L.Run();

        }
        public void CameraRun_Left()
        {
            CameraRun_L.Inputs[0].Value = CameraAcq_L.OutputImage as CogImage8Grey;
            CameraRun_L.Run();
            
        }

        public void CameraAcq_Right()
        {
            CameraAcq_R.Run();
        }
        public void CameraBaseLine_Right()
        {
            CameraBaseLine_R.Inputs[0].Value = CameraAcq_R.OutputImage as CogImage8Grey;
            CameraBaseLine_R.Run();
        }
        public void CameraRun_Right()
        {
            CameraRun_R.Inputs[0].Value = CameraAcq_R.OutputImage as CogImage8Grey;
            CameraRun_R.Run();
        }

        public void JobRun0()
        {
            LeftCCDJb.Run();
        }
        public void JobRun1()
        {
            RightCCDJb.Run();
        }
        #endregion

        /// <summary>
        /// 視覺文檔存儲
        /// </summary>
        /// <param name="filename">文檔存儲路徑</param>
        public void SaveVisonFile(string filename)
        {
            try
            {
                CogSerializer.SaveObjectToFile(_Jm, filename
                    , typeof(System.Runtime.Serialization.Formatters.Binary.BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                //m.PublicFuction.DisplayMessage("操作", "保存视觉文档成功", Color.Green);
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                //m.PublicFuction.DisplayMessage("异常", ex.Message, Color.Red);
                MessageBox.Show(ex.Message);
            }
        }
        #region 保存图片
        public   ICogImage[] Image = new ICogImage[2];
        public void PictureSave(string Name)
        {
            //if (!Directory.Exists(Vars.ImageSavePath))
            //{
            //    Directory.CreateDirectory(Vars.ImageSavePath);
            //    Image[0].ToBitmap().Save(Vars.ImageSavePath + Name + DateTime.Now.ToString("yyyy-MM-dd") + ".Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //}
            //else
            //{
            //    Image[0].ToBitmap().Save(Vars.ImageSavePath + Name + DateTime.Now.ToString("yyyy-MM-dd") + ".Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //}
         }
        #endregion
        
    }
}
