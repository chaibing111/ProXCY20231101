using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace sunyvpp
{
    public partial class FrmVisionParam : UIForm
    {
        private mSettingOption mSettingOptionFrmVisionParam;
        public static FrmVisionParam frmVisionParam = null;
        AutoSizeFormClass ascFrmVisionParam = new AutoSizeFormClass();//实例化自动适应窗体类
        //1.构造器私有化
        private FrmVisionParam()
        {
            InitializeComponent();

            if (File.Exists("1.json"))
            {
                Globals.mSettingOptionParm = (mSettingOption)JsonHelper.Instance.JsonFileToObject("1.json", Globals.mSettingOptionParm);
            }

            txbStartX.Text = Globals.mSettingOptionParm.Calib_X.ToString();
            txbStartY.Text = Globals.mSettingOptionParm.Calib_Y.ToString();
            txbDist.Text = Globals.mSettingOptionParm.Calib_Distant.ToString();

            txbStartConvX.Text = Globals.mSettingOptionParm.Calib_ConvB_X.ToString();
            txbStartConvY.Text = Globals.mSettingOptionParm.Calib_ConvB_Y.ToString();
            txbStartConvDist.Text = Globals.mSettingOptionParm.Calib_ConvB_Distant.ToString();
            
        }


        public static FrmVisionParam GetInstance()
        {
            if (frmVisionParam == null || (frmVisionParam != null && frmVisionParam.IsDisposed))
            {
                frmVisionParam = new FrmVisionParam();
            }
            return frmVisionParam;
        }

        private void uiSymbolButton1_Click(object sender, EventArgs e)

        {
            Globals.mSettingOptionParm.Calib_X  =Convert.ToDouble(txbStartX.Text) ;
            Globals.mSettingOptionParm.Calib_Y = Convert.ToDouble(txbStartY.Text);
            Globals.mSettingOptionParm.Calib_Distant = Convert.ToDouble(txbDist.Text);

            Globals.mSettingOptionParm.Calib_ConvB_X = Convert.ToDouble(txbStartConvX.Text);
            Globals.mSettingOptionParm.Calib_ConvB_Y = Convert.ToDouble(txbStartConvY.Text);
            Globals.mSettingOptionParm.Calib_ConvB_Distant = Convert.ToDouble(txbStartConvDist.Text);

            JsonHelper.Instance.ObjectToJsonFile("1.json", Globals.mSettingOptionParm);
            if (File.Exists("1.json"))
            {
                Globals.mSettingOptionParm = (mSettingOption)JsonHelper.Instance.JsonFileToObject("1.json", Globals.mSettingOptionParm);
            }

            txbStartX.Text = Globals.mSettingOptionParm.Calib_X.ToString();
            txbStartY.Text = Globals.mSettingOptionParm.Calib_Y.ToString();
            txbDist.Text = Globals.mSettingOptionParm.Calib_Distant.ToString();

            txbStartConvX.Text = Globals.mSettingOptionParm.Calib_ConvB_X.ToString();
            txbStartConvY.Text = Globals.mSettingOptionParm.Calib_ConvB_Y.ToString();
            txbStartConvDist.Text = Globals.mSettingOptionParm.Calib_ConvB_Distant.ToString();

            ShowInfoDialog("参数保存", "参数保存成功！", UIStyle.Green);
            //ShowSuccessDialog("参数保存成功！");
        }

        private void FrmVisionParam_Load(object sender, EventArgs e)
        {
            ascFrmVisionParam.controllInitializeSize(this);

        }

        private void FrmVisionParam_SizeChanged(object sender, EventArgs e)
        {

            // 3.为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应
            ascFrmVisionParam.controlAutoSize(this);
        }
    }
}
