
using Newtonsoft.Json;
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
using Cognex.VisionPro;
using sunyvpp;

namespace sunyvpp
{
    public partial class Form_Weld : Form
    {
        public Form_Weld()
        {
            InitializeComponent();
        }
        public static Form_Weld frmWeld = null;
        public static Form_Weld GetInstance()
        {
            if (frmWeld == null || (frmWeld != null && frmWeld.IsDisposed))
            {
                frmWeld = new Form_Weld();
            }
            return frmWeld;
        }
        private void Form_Main_Load(object sender, EventArgs e)
        {
            StationInfo.StationInfoInit();
            //if (!TCPHelper.ins.myTcpServerParmDic.ContainsKey("MESSever"))
            //{
            //    TCPHelper.ins.myTcpServerParmDic.Add("MESSever", new List<TcpMsg>() {
            //    new TcpMsg() {IpServer="127.0.0.1", PortServer="9999", isStr=true} });
            //}
            //TCPHelper.ins.TcpServerInit();  //测试用代码



            DataInit();
        }

        private void DataInit()
        {
            textBox_title.Text = StationInfo.ins.title;
            label_title.Text = StationInfo.ins.title;
            if (!string.IsNullOrEmpty(StationInfo.ins.title))
            {
                textBox_title.Visible = true;
            }
            textBox_Software.Text = StationInfo.ins.Software;
            textBox_EquipmentCode.Text = StationInfo.ins.EquipmentCode;
            textBox_SubEquipmentCode.Text = StationInfo.ins.SubEquipmentCode;
            textBox_WorkStationCode.Text = StationInfo.ins.WorkStationCode;
            textBox_parmCode1.Text = StationInfo.ins.parmCode1;
            textBox_batBatch.Text = StationInfo.ins.batBatch;

            foreach(string fileName in StationInfo.ins.weldDocs)
            {
                if (listBox_weldFiles.Items.IndexOf(fileName) < 0)
                {
                    listBox_weldFiles.Items.Add(fileName);
                }
            }
            

        }


        private void label_title_DoubleClick(object sender, EventArgs e)
        {
            textBox_title.Show();
        }

        private void textBox_title_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar ==(char)Keys.Enter)
            {
                StationInfo.ins.title = textBox_title.Text.Trim();
                label_title.Text = StationInfo.ins.title;
                textBox_title.Hide();
                FileHelper.InfoSave(StationInfo.ins, PublicFunction.stationInfo_path);
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            StationInfo.ins.Software = textBox_Software.Text.Trim();
            StationInfo.ins.EquipmentCode = textBox_EquipmentCode.Text.Trim();
            StationInfo.ins.SubEquipmentCode = textBox_SubEquipmentCode.Text.Trim();
            StationInfo.ins.WorkStationCode = textBox_WorkStationCode.Text.Trim();
            StationInfo.ins.parmCode1 = textBox_parmCode1.Text.Trim();
            StationInfo.ins.batBatch = textBox_batBatch.Text.Trim();
            StationInfo.ins.title = label_title.Text.Trim();
            StationInfo.ins.weldDocs.Clear();
            foreach (string fileName in listBox_weldFiles.Items)
            {
                if (StationInfo.ins.weldDocs.IndexOf(fileName) < 0)
                {
                    StationInfo.ins.weldDocs.Add(fileName);
                }
            }
            FileHelper.InfoSave(StationInfo.ins, PublicFunction.stationInfo_path);
        }

        private void button_weldDoc_Click(object sender, EventArgs e)
        {
            //*.fcd  多文档内容格式
            //[BAS]
            //ITEMNUM = 4
            //[ITEM0]
            //FILENAME = C:\Users\Administrator\Desktop\桌面文件\焊接激光\最新侧板2023.2.14\侧板焊接A短边8.orzx
            //  DEVNAME = 设备[12620821377161105]
            //DEVID = 12620821377161105
            //PORTS = 8,0
            //MARKEDCOUNT = 0
            //[ITEM1]
            //FILENAME = C:\Users\Administrator\Desktop\桌面文件\焊接激光\最新侧板2023.2.14\侧板焊接A长边9.orzx
            //  DEVNAME = 设备[12620821377161105]
            //DEVID = 12620821377161105
            //PORTS = 9,0
            //MARKEDCOUNT = 0

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.orzx)|*.orzx;*.fcd";
            //fileDialog.InitialDirectory = Application.StartupPath;
            if(fileDialog.ShowDialog()==  DialogResult.OK)
            {
                string fileName = fileDialog.FileName;
                string exten = Path.GetExtension(fileName);
                if (exten.ToLower() == ".orzx")
                {
                    if (listBox_weldFiles.Items.IndexOf(fileName) < 0)
                    {
                        listBox_weldFiles.Items.Add(fileName);
                    }
                    if (StationInfo.ins.weldDocs.IndexOf(fileName) < 0)
                    {
                        StationInfo.ins.weldDocs.Add(fileName);
                    }
                }
                else if (exten.ToLower() == ".fcd")
                {
                    int fileQty = 0;
                    string fileQtyStr = FileHelper.GetINI("BAS", "ITEMNUM", "0", fileName);
                    int.TryParse(fileQtyStr, out fileQty);
                    if (fileQty <= 0) return;

                    for (int i = 0; i < fileQty; i++)
                    {
                        string weldFileName = FileHelper.GetINI("ITEM" + i, "FILENAME", "", fileName);
                        if (string.IsNullOrEmpty(weldFileName) || !File.Exists(weldFileName)) continue;
                        if (listBox_weldFiles.Items.IndexOf(fileName) < 0)
                        {
                            listBox_weldFiles.Items.Add(weldFileName);
                        }
                        if (StationInfo.ins.weldDocs.IndexOf(weldFileName) < 0)
                        {
                            StationInfo.ins.weldDocs.Add(weldFileName);
                        }
                    }
                }
                FileHelper.InfoSave(StationInfo.ins, PublicFunction.stationInfo_path);
            }

        }

        private void listBox_weldFiles_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_weldFiles.SelectedIndex >= 0)
            {
                StationInfo.ins.weldDocs.Remove(listBox_weldFiles.Items[listBox_weldFiles.SelectedIndex].ToString());
                listBox_weldFiles.Items.RemoveAt(listBox_weldFiles.SelectedIndex);
                FileHelper.InfoSave(StationInfo.ins, PublicFunction.stationInfo_path);
            }
        }

        private void button_docClear_Click(object sender, EventArgs e)
        {
            listBox_weldFiles.Items.Clear();
            StationInfo.ins.weldDocs.Clear();
            FileHelper.InfoSave(StationInfo.ins, PublicFunction.stationInfo_path);
        }

        private void listBox_weldFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_weldFiles.SelectedIndex < 0) return;
            string fileName = listBox_weldFiles.SelectedItem.ToString();
            weldParmGet(fileName);
            //Bitmap bitmap = MarkAPI.ins.DrawFileInImg(pictureBox_weld.Width, pictureBox_weld.Height);
            Bitmap bitmap = MarkAPI.ins.DrawFileInImg(400, 300);
            if (bitmap != null)
            {
                pictureBox_weld.Image = bitmap;
                ICogImage MyOutPutImage = (ICogImage)new CogImage24PlanarColor(bitmap);
                cogDisplay1.Image = MyOutPutImage;
                cogDisplay1.Fit();
                //MainForm.Instance.cogDisplay1.Image = MyOutPutImage;
                //MainForm.Instance.cogDisplay1.Fit();
            }
        }

        private void weldParmGet(string parmFile)
        {
            BslErrCode ret = MarkAPI.ins.LoadDataFile(parmFile);
            if (ret == BslErrCode.BSL_ERR_SUCCESS)
            {
                ret = MarkAPI.ins.GetPenParam(0);
                if (ret == BslErrCode.BSL_ERR_SUCCESS)
                {
                    textBox_cfg1.Clear();
                    textBox_cfg1.Text = "\r\n" + ("加工次数：" + MarkAPI.ins.nMarkLoop);
                    textBox_cfg1.Text += "\r\n" + ("标刻速度mm/s：" + MarkAPI.ins.dMarkSpeed);
                    textBox_cfg1.Text += "\r\n" + ("功率百分比(0-100%)：" + MarkAPI.ins.dPowerRatio);
                    textBox_cfg1.Text += "\r\n" + ("电流A：" + MarkAPI.ins.dCurrent);
                    textBox_cfg1.Text += "\r\n" + ("频率HZ：" + MarkAPI.ins.nFreq);
                    textBox_cfg1.Text += "\r\n" + ("Q脉冲宽度us：" + MarkAPI.ins.nQPulseWidth);
                    textBox_cfg1.Text += "\r\n" + ("开始延时us：" + MarkAPI.ins.nStartTC);
                    textBox_cfg1.Text += "\r\n" + ("激光关闭延时us：" + MarkAPI.ins.nLaserOffTC);
                    textBox_cfg1.Text += "\r\n" + ("结束延时us：" + MarkAPI.ins.nEndTC);
                    textBox_cfg1.Text += "\r\n" + ("拐角延时us：" + MarkAPI.ins.nPolyTC);
                    textBox_cfg1.Text += "\r\n" + ("跳转速度mm/s：" + MarkAPI.ins.dJumpSpeed);
                    textBox_cfg1.Text += "\r\n" + ("跳转位置延时us：" + MarkAPI.ins.nJumpPosTC);
                    textBox_cfg1.Text += "\r\n" + ("跳转距离延时us：" + MarkAPI.ins.nJumpDistTC);
                    textBox_cfg1.Text += "\r\n" + ("末点补偿mm：" + MarkAPI.ins.dEndComp);
                    textBox_cfg1.Text += "\r\n" + ("脉冲点模式：" + MarkAPI.ins.bPulsePointMode);
                    textBox_cfg1.Text += "\r\n" + ("脉冲点数目：" + MarkAPI.ins.nPulseNum);
                    textBox_cfg1.Text += "\r\n" + ("打点时间：" + MarkAPI.ins.POINTTIME);
                }
            }
        }

        private void button_test_Click(object sender, EventArgs e)
        {
            DataUploadHelper.MyMes.form_MesConfig.Show();

            Dictionary<string, string> dicInfo = new Dictionary<string, string>();
            dicInfo.Add("托盘码", "1234567890");
            dicInfo.Add("功率百分比", "50");
            dicInfo.Add("Software", StationInfo.ins.Software);
            dicInfo.Add("EquipmentCode", StationInfo.ins.EquipmentCode);

            string ret = DataUploadHelper.MyMes.MesSend(0, "modBusbarWeld", JsonConvert.SerializeObject(dicInfo));  
        }
    }
}
