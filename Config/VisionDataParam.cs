using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    public class VisionDataParam
    {
        [Category("视觉参数"), Description("模板组装基准位置1X/Y/R")]
        public double[] ModelAssemblyPos1 { get; set; } 

        [Category("视觉参数"), Description("模板组装基准位置2X/Y/R")]
        public double[] ModuleAssemblyPos2 { get; set; } 

        [Category("视觉参数"), Description("模板组装基准位置3X/Y/R")]
        public double[] ModuleAssemblyPos3 { get; set; } 

        [Category("视觉参数"), Description("模板组装基准位置4X/Y/R")]
        public double[] ModuleAssemblyPos4 { get; set; } 

        [Category("视觉参数"), Description("上相机模板拍照1X/Y/R")]
        public double[] UpCamFhotoPos1 { get; set; }

        [Category("视觉参数"), Description("上相机模板拍照2X/Y/R")]
        public double[] UpCamFhotoPos2 { get; set; } 

        [Category("视觉参数"), Description("上相机模板拍照3X/Y/R")]
        public double[] UpCamFhotoPos3 { get; set; }

        [Category("视觉参数"), Description("上相机模板拍照4X/Y/R")]
        public double[] UpCamFhotoPos4 { get; set; }

        [Category("视觉参数"), Description("下相机取料旋转中心X/Y/R")]
        public double[] DownCamCen { get; set; }
        [Category("视觉参数"), Description("下相机模板X/Y/R")]
        public double[] DownCamModel { get; set; }
    }
}
