using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ToolGroup;
namespace sunyvpp
{
    public partial class ToolBlockEdit : Form
    {
        private string savePath = Application.StartupPath + "\\vpp\\" + "TB_Edit.vpp";
        private CogToolBlock tb;
        //public ToolBlockEdit()
        //{
        //    InitializeComponent();
        //}
        public ToolBlockEdit(string SavePath, CogToolBlock toolBlock)
        {
            InitializeComponent();
            savePath = SavePath;
            tb = toolBlock;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            CogSerializer.SaveObjectToFile(tb, savePath);
        }

        private void btn_SaveAndClose_Click(object sender, EventArgs e)
        {
            CogSerializer.SaveObjectToFile(tb, savePath);
            Close();
        }
    }
}
