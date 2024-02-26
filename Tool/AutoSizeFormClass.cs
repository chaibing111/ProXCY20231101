using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
namespace sunyvpp
{

    #region 窗体自适应DR4
    class AutoSizeFormClass
    {
        //(1).声明结构,只记录窗体和其控件的初始位置和大小。  
        public struct controlRect
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
        }
        //(2).声明 1个对象  
        //注意这里不能使用控件列表记录 List<Control> nCtrl;，因为控件的关联性，记录的始终是当前的大小。  
        public List<controlRect> oldCtrl;
        //public List<controlRect> MenuStripoldCtrl;
        //int ctrl_first = 0;  
        //(3). 创建两个函数  
        //(3.1)记录窗体和其控件的初始位置和大小,  
        public void controllInitializeSize(Form mForm)
        {
            // if (ctrl_first == 0)  
            {
                //  ctrl_first = 1;  
                oldCtrl = new List<controlRect>();
                controlRect cR;
                cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
                oldCtrl.Add(cR);
                foreach (Control c in mForm.Controls)
                {
                    controlRect objCtrl;
                    objCtrl.Left = c.Left; objCtrl.Top = c.Top; objCtrl.Width = c.Width; objCtrl.Height = c.Height;
                    oldCtrl.Add(objCtrl);
                    if ((c as GroupBox) != null)
                    {
                        foreach (Control cb in c.Controls)
                        {
                            objCtrl.Left = cb.Left; objCtrl.Top = cb.Top; objCtrl.Width = cb.Width; objCtrl.Height = cb.Height;
                            oldCtrl.Add(objCtrl);
                        }
                    }

                }
            }
        }
        //(3.2)控件自适应大小,  
        public void controlAutoSize(Form mForm)
        {
            if (oldCtrl == null)
            {
                return;
            }
            float wScale = (float)mForm.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体  
            float hScale = (float)mForm.Height / (float)oldCtrl[0].Height;//.Height;  
            int ctrLeft0, ctrTop0, ctrWidth0, ctrHeight0;
            int ctrlNo = 1;//第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始  
            foreach (Control c in mForm.Controls)
            {
                ctrLeft0 = oldCtrl[ctrlNo].Left;
                ctrTop0 = oldCtrl[ctrlNo].Top;
                ctrWidth0 = oldCtrl[ctrlNo].Width;
                ctrHeight0 = oldCtrl[ctrlNo].Height;
                //c.Left = (int)((ctrLeft0 - wLeft0) * wScale) + wLeft1;//新旧控件之间的线性比例  
                //c.Top = (int)((ctrTop0 - wTop0) * h) + wTop1;  
                c.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1  
                c.Top = (int)((ctrTop0) * hScale);//  
                c.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);  
                c.Height = (int)(ctrHeight0 * hScale);//  
                ctrlNo += 1;
                if ((c as GroupBox) != null)
                {
                    foreach (Control cb in c.Controls)
                    {
                        ctrLeft0 = oldCtrl[ctrlNo].Left;
                        ctrTop0 = oldCtrl[ctrlNo].Top;
                        ctrWidth0 = oldCtrl[ctrlNo].Width;
                        ctrHeight0 = oldCtrl[ctrlNo].Height;

                        cb.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1  
                        cb.Top = (int)((ctrTop0) * hScale);//  
                        cb.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);  
                        cb.Height = (int)(ctrHeight0 * hScale);//  
                        ctrlNo += 1;
                    }
                }
            }
        }

    }

    #endregion
}
