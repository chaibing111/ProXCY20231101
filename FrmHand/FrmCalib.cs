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
using csv;
using Sunny.UI;
using MyCalibrationHelper;
using ApeFree.DataStore;
using ApeFree.DataStore.Adapters;
using ApeFree.DataStore.Core;
using ApeFree.DataStore.Local;
using Cognex.VisionPro;

namespace sunyvpp
{
    public partial class FrmCalib : UIForm
    {

        public static FrmCalib genmove = null;
        //1.构造器私有化
        public static FrmCalib GetInstance()
        {
            if (genmove == null || (genmove != null && genmove.IsDisposed))
            {
                genmove = new FrmCalib();
            }
            return genmove;
        }
        public FrmCalib()
        {
            InitializeComponent();
        }

        private void FrmCalib_Load(object sender, EventArgs e)
        {

            if (!Directory.Exists(ConfigureFilePath.projectItemPath))
            {
                Directory.CreateDirectory(ConfigureFilePath.projectItemPath);
                return;
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(ConfigureFilePath.projectItemPath);
            FileSystemInfo[] fileArr = directoryInfo.GetFiles("*.csv");
            foreach (FileSystemInfo file in fileArr)
            {
                if (!Globals.ProCalibItemDic.ContainsKey(file.Name))
                {
                    Globals.ProCalibItemDic.Add(file.Name, new List<string>());
                }
            }

            cmbProduct.Items.Clear();

            List<string> cogNames = Globals.ProCalibItemDic.Keys.ToList();
            for (int i = 0; i < cogNames.Count; i++)
            {

                string cogName = cogNames[i];
                if (!cmbProduct.Items.Contains(cogName)
                   )
                {
                    cmbProduct.Items.Add(ConfigureFilePath.projectItemPath + "\\" + cogName);
                    Globals.ProjCalibPointNamePathList.Add(ConfigureFilePath.projectItemPath + "\\" + cogName);
                }

            }

            if (Globals.ProjCalibPointNamePathList.Count > 0)
            {
                cmbProduct.SelectedIndex = 0;

                Globals.ProjCalibPointNamePath = cmbProduct.SelectedItem.ToString();
                ReadCSVContent(Globals.ProjCalibPointNamePath);
            }

            //关闭自动增加列
            dataGridView2.AutoGenerateColumns = true;
            //允许拖放
            dataGridView2.AllowDrop = true;
            //不显示增加行功能
            dataGridView2.AllowUserToAddRows = true;
            //每次选中一行
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //不允许多选
            dataGridView2.MultiSelect = true;
            //内容只读
            dataGridView2.ReadOnly = true;

            //列宽度自适应 设置的整个控件的列宽枚举属性
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //设置标题栏标题居中
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //设置内容栏的居中
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //单独给最后一列设置内容靠左
            dataGridView2.Columns[dataGridView2.ColumnCount - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //设置第一列标题列的宽度和内容
            dataGridView2.RowHeadersWidth = 80;
            dataGridView2.TopLeftHeaderCell.Value = "序号";

            //for (int i = 0; i < 3; i++)
            //{
            DGV1.Rows.Add();
            DGV1.Rows[0].Cells[0].Value = 0;
            DGV1.Rows[0].Cells[1].Value = 25;
            DGV1.Rows.Add();
            DGV1.Rows[1].Cells[0].Value = 25;
            DGV1.Rows[1].Cells[1].Value = 0;
            DGV1.Rows.Add();
            DGV1.Rows[2].Cells[0].Value = 50;
            DGV1.Rows[2].Cells[1].Value = 25;
            //}

            Globals.LoadVisionParam();
            pgd.SelectedObject = Globals.store.Value;

        }


        public void WriteCalibBenchmarkData()
        {
            Globals.store.Save();
            ShowSuccessDialog("视觉参数数据保存成功！");
        }
        //读取csv
        public void ReadCSVContent(string ProjCalibPointNamePath)
        {
            this.dataGridView2.Rows.Clear();
            if (ProjCalibPointNamePath.Length > 0)
            {
                int j = 0;
                List<string[]> rowList = CsvUtil.ReadCSV(ProjCalibPointNamePath);
                int c = this.dataGridView2.ColumnCount;
                for (int i = 0; i < rowList.Count; i++)
                {
                    //读取一行数据....
                    string[] _readDate = rowList[i];

                    //for (int item = 1; item < _readDate.Length; item++)
                    //{
                    //    _list.Add(_readDate[item]);
                    //}

                }
                for (int i = 0; i < rowList.Count; i++)
                    this.dataGridView2.Rows.Add(rowList[i]);

                //cboUsers.Items.Clear();
                //for (int i = 0; i < rowList.Count; i++)
                //{
                //    if (rowList.Count > cboUsers.Items.Count)
                //    {
                //        cboUsers.Items.Add(this.dataGridView2[0, i].FormattedValue.ToString());
                //    }

                //}
            }
            List<string[]> rowList2 = CsvUtil.ReadCSV(ProjCalibPointNamePath);
            Globals.CalibRowList = rowList2;
            ShowSuccessDialog("导入成功!");
        }
        //写入csv
        private void WriteCSVContent(string ProjCalibPointNamePath)
        {
            int r = this.dataGridView2.RowCount;
            int c = this.dataGridView2.ColumnCount;
            if (r < 1 || c < 1)
            {
                MessageBox.Show("没有数据!");
            }
            List<string[]> rowList = new List<string[]>();
            //读取每行
            for (int i = 0; i < r; i++)
            {
                string[] rowCells = new string[c];
                //读取每列。。。
                for (int j = 0; j < c; j++)
                {
                    rowCells[j] = this.dataGridView2[j, i].FormattedValue.ToString();
                }
                rowList.Add(rowCells);
            }
            CsvUtil.WriteCSV(ProjCalibPointNamePath, rowList, false);
            MessageBox.Show("导出成功!");

        }
        private void dataGridView2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //增加行号的显示格式
            e.Row.HeaderCell.Value = e.Row.Index.ToString().PadLeft(2, '0');
        }

        private void btnChgPro_Click(object sender, EventArgs e)
        {
            if (Globals.ProjCalibPointNamePathList.Count > 0)
            {
                Globals.ProjCalibPointNamePath = cmbProduct.SelectedItem.ToString();
                ReadCSVContent(Globals.ProjCalibPointNamePath);
            }
        }
        RotateCalib rotateCalib = new RotateCalib();
        private void btnCaclRotCen_Click(object sender, EventArgs e)
        {
            rotateCalib.CamMotionPoints.Clear();
            try
            {
                double x1 = Convert.ToDouble(DGV1.Rows[0].Cells[0].Value);
                double y1 = Convert.ToDouble(DGV1.Rows[0].Cells[1].Value);

                double x2 = Convert.ToDouble(DGV1.Rows[1].Cells[0].Value);
                double y2 = Convert.ToDouble(DGV1.Rows[1].Cells[1].Value);

                double x3 = Convert.ToDouble(DGV1.Rows[2].Cells[0].Value);
                double y3 = Convert.ToDouble(DGV1.Rows[2].Cells[1].Value);
                rotateCalib.CamMotionPoints.Add(new PointD(x1, y1));
                rotateCalib.CamMotionPoints.Add(new PointD(x2, y2));
                rotateCalib.CamMotionPoints.Add(new PointD(x3, y3));

                rotateCalib.CamCenterPoint = MyRotateCalib.GetRCenter(rotateCalib.CamMotionPoints.ToArray(), out rotateCalib.CamRadio);

                txbRotationCenX.Text = rotateCalib.CamCenterPoint.X.ToString();
                txbRotationCenY.Text = rotateCalib.CamCenterPoint.Y.ToString();
                txbRotationCenR.Text = rotateCalib.CamRadio.ToString();
            }
            catch (Exception exception)
            {
                ;
            }
        }

        private void btnVisionSave_Click(object sender, EventArgs e)
        {
            WriteCalibBenchmarkData();
        }

        private void btnDownCamCalib_Click(object sender, EventArgs e)
        {
            Task.Run(() => {TaskDownCamClib.Instance.CalibRun();}  );
        }
        Bitmap bt;
        public ICogImage MyOutPutImage;
        public static double Pix_X = 999999;
        public static double Pix_Y = 999999;
        public static double Pix_R = 999999;
        public static bool VisionReSault = false;
        private void btnDownMoudle_Click(object sender, EventArgs e)
        {
            Globals.CameraDown.Capture_New(Globals.SettingOption.DownCamExpose1, out bt);
            MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bt);
            Globals.RunTool(MyOutPutImage, Globals.AutoToolblock, out Pix_X, out Pix_Y, out Pix_R, out VisionReSault);
            if (Globals.AutoToolblock.RunStatus.Result == CogToolResultConstants.Accept)
            {
                if (VisionReSault)
                {
                    Globals.store.Value.DownCamModel[0] = Pix_X;
                    Globals.store.Value.DownCamModel[1] = Pix_Y;
                    Globals.store.Value.DownCamModel[2] = Pix_R;
                    Globals.LogRecord("【下相机模板运行结果：" + "X：" + Pix_X.ToString() + "Y：" + Pix_Y.ToString() + "R：" + Pix_R.ToString() + "OK！】");
                    Globals.store.Save();
                    ShowSuccessDialog("下相机模板运行成功！");
                }
                else
                {
                    Globals.LogRecord("【视觉执行失败！】");
                    ShowSuccessDialog("视觉执行失败！");
                }
            }
        }
    }
}
