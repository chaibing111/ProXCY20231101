namespace sunyvpp
{
    partial class FrmCOM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, true.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Send = new Sunny.UI.UISymbolButton();
            this.uiGroupBox4 = new Sunny.UI.UIGroupBox();
            this.txt_Send = new System.Windows.Forms.TextBox();
            this.uiPanel4 = new Sunny.UI.UIPanel();
            this.chb_0x_DataSend = new Sunny.UI.UICheckBox();
            this.chb_0x_DataReceived = new Sunny.UI.UICheckBox();
            this.btn_DataReceivedClear = new Sunny.UI.UISymbolButton();
            this.btn_DataSendClear = new Sunny.UI.UISymbolButton();
            this.uiGroupBox3 = new Sunny.UI.UIGroupBox();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.txt_DataReceived = new System.Windows.Forms.TextBox();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.btn_OpenCom = new Sunny.UI.UISymbolButton();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.cmbStopBits = new Sunny.UI.UIComboBox();
            this.cmbDataBits = new Sunny.UI.UIComboBox();
            this.cmbParity = new Sunny.UI.UIComboBox();
            this.cmbBaudRate = new Sunny.UI.UIComboBox();
            this.cmbComName = new Sunny.UI.UIComboBox();
            this.uiSymbolButton5 = new Sunny.UI.UISymbolButton();
            this.uiSymbolButton4 = new Sunny.UI.UISymbolButton();
            this.uiSymbolButton3 = new Sunny.UI.UISymbolButton();
            this.uiSymbolButton2 = new Sunny.UI.UISymbolButton();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.btnClose = new Sunny.UI.UISymbolButton();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiGroupBox4.SuspendLayout();
            this.uiPanel4.SuspendLayout();
            this.uiGroupBox3.SuspendLayout();
            this.uiPanel3.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiGroupBox2.SuspendLayout();
            this.uiGroupBox1.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Send
            // 
            this.btn_Send.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Send.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Send.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Send.Location = new System.Drawing.Point(664, 74);
            this.btn_Send.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(116, 28);
            this.btn_Send.Symbol = 61447;
            this.btn_Send.TabIndex = 29;
            this.btn_Send.Text = "发送";
            this.btn_Send.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Send.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.txt_Send);
            this.uiGroupBox4.Controls.Add(this.btn_Send);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox4.Size = new System.Drawing.Size(790, 128);
            this.uiGroupBox4.TabIndex = 0;
            this.uiGroupBox4.Text = "数据发送区";
            this.uiGroupBox4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txt_Send
            // 
            this.txt_Send.Location = new System.Drawing.Point(3, 28);
            this.txt_Send.Multiline = true;
            this.txt_Send.Name = "txt_Send";
            this.txt_Send.Size = new System.Drawing.Size(643, 97);
            this.txt_Send.TabIndex = 1;
            this.txt_Send.Text = "SA0255#";
            // 
            // uiPanel4
            // 
            this.uiPanel4.Controls.Add(this.uiGroupBox4);
            this.uiPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel4.Location = new System.Drawing.Point(4, 623);
            this.uiPanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel4.Name = "uiPanel4";
            this.uiPanel4.Size = new System.Drawing.Size(790, 128);
            this.uiPanel4.TabIndex = 3;
            this.uiPanel4.Text = null;
            this.uiPanel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // chb_0x_DataSend
            // 
            this.chb_0x_DataSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chb_0x_DataSend.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chb_0x_DataSend.Location = new System.Drawing.Point(232, 35);
            this.chb_0x_DataSend.MinimumSize = new System.Drawing.Size(1, 1);
            this.chb_0x_DataSend.Name = "chb_0x_DataSend";
            this.chb_0x_DataSend.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.chb_0x_DataSend.Size = new System.Drawing.Size(128, 25);
            this.chb_0x_DataSend.TabIndex = 30;
            this.chb_0x_DataSend.Text = "十六进制发送";
            this.chb_0x_DataSend.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // chb_0x_DataReceived
            // 
            this.chb_0x_DataReceived.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chb_0x_DataReceived.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chb_0x_DataReceived.Location = new System.Drawing.Point(36, 35);
            this.chb_0x_DataReceived.MinimumSize = new System.Drawing.Size(1, 1);
            this.chb_0x_DataReceived.Name = "chb_0x_DataReceived";
            this.chb_0x_DataReceived.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.chb_0x_DataReceived.Size = new System.Drawing.Size(128, 25);
            this.chb_0x_DataReceived.TabIndex = 30;
            this.chb_0x_DataReceived.Text = "十六进制接收";
            this.chb_0x_DataReceived.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btn_DataReceivedClear
            // 
            this.btn_DataReceivedClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DataReceivedClear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DataReceivedClear.Location = new System.Drawing.Point(530, 32);
            this.btn_DataReceivedClear.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_DataReceivedClear.Name = "btn_DataReceivedClear";
            this.btn_DataReceivedClear.Size = new System.Drawing.Size(116, 28);
            this.btn_DataReceivedClear.Symbol = 61447;
            this.btn_DataReceivedClear.TabIndex = 29;
            this.btn_DataReceivedClear.Text = "清空接收区";
            this.btn_DataReceivedClear.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DataReceivedClear.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_DataReceivedClear.Click += new System.EventHandler(this.btn_DataReceivedClear_Click);
            // 
            // btn_DataSendClear
            // 
            this.btn_DataSendClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DataSendClear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DataSendClear.Location = new System.Drawing.Point(664, 32);
            this.btn_DataSendClear.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_DataSendClear.Name = "btn_DataSendClear";
            this.btn_DataSendClear.Size = new System.Drawing.Size(116, 28);
            this.btn_DataSendClear.Symbol = 61447;
            this.btn_DataSendClear.TabIndex = 29;
            this.btn_DataSendClear.Text = "清空发送区";
            this.btn_DataSendClear.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DataSendClear.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_DataSendClear.Click += new System.EventHandler(this.btn_DataSendClear_Click);
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.chb_0x_DataSend);
            this.uiGroupBox3.Controls.Add(this.chb_0x_DataReceived);
            this.uiGroupBox3.Controls.Add(this.btn_DataReceivedClear);
            this.uiGroupBox3.Controls.Add(this.btn_DataSendClear);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox3.Size = new System.Drawing.Size(790, 84);
            this.uiGroupBox3.TabIndex = 0;
            this.uiGroupBox3.Text = "数据设置区";
            this.uiGroupBox3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiPanel3
            // 
            this.uiPanel3.Controls.Add(this.uiGroupBox3);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel3.Location = new System.Drawing.Point(4, 529);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Size = new System.Drawing.Size(790, 84);
            this.uiPanel3.TabIndex = 2;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txt_DataReceived
            // 
            this.txt_DataReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_DataReceived.Location = new System.Drawing.Point(0, 32);
            this.txt_DataReceived.Multiline = true;
            this.txt_DataReceived.Name = "txt_DataReceived";
            this.txt_DataReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_DataReceived.Size = new System.Drawing.Size(790, 328);
            this.txt_DataReceived.TabIndex = 1;
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiGroupBox2);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(4, 159);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(790, 360);
            this.uiPanel2.TabIndex = 1;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.uiPanel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.txt_DataReceived);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox2.Size = new System.Drawing.Size(790, 360);
            this.uiGroupBox2.TabIndex = 0;
            this.uiGroupBox2.Text = "数据接收区";
            this.uiGroupBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btn_OpenCom
            // 
            this.btn_OpenCom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_OpenCom.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OpenCom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_OpenCom.Location = new System.Drawing.Point(664, 35);
            this.btn_OpenCom.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_OpenCom.Name = "btn_OpenCom";
            this.btn_OpenCom.Size = new System.Drawing.Size(116, 28);
            this.btn_OpenCom.Symbol = 61447;
            this.btn_OpenCom.TabIndex = 29;
            this.btn_OpenCom.Text = "打开串口";
            this.btn_OpenCom.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OpenCom.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_OpenCom.Click += new System.EventHandler(this.btn_OpenCom_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmbStopBits);
            this.uiGroupBox1.Controls.Add(this.cmbDataBits);
            this.uiGroupBox1.Controls.Add(this.cmbParity);
            this.uiGroupBox1.Controls.Add(this.cmbBaudRate);
            this.uiGroupBox1.Controls.Add(this.cmbComName);
            this.uiGroupBox1.Controls.Add(this.uiSymbolButton5);
            this.uiGroupBox1.Controls.Add(this.uiSymbolButton4);
            this.uiGroupBox1.Controls.Add(this.uiSymbolButton3);
            this.uiGroupBox1.Controls.Add(this.uiSymbolButton2);
            this.uiGroupBox1.Controls.Add(this.uiSymbolButton1);
            this.uiGroupBox1.Controls.Add(this.btnClose);
            this.uiGroupBox1.Controls.Add(this.btn_OpenCom);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(790, 144);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "网络设置区";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DataSource = null;
            this.cmbStopBits.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbStopBits.FillColor = System.Drawing.Color.White;
            this.cmbStopBits.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbStopBits.Location = new System.Drawing.Point(138, 105);
            this.cmbStopBits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbStopBits.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbStopBits.Size = new System.Drawing.Size(141, 25);
            this.cmbStopBits.TabIndex = 30;
            this.cmbStopBits.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbStopBits.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.DataSource = null;
            this.cmbDataBits.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbDataBits.FillColor = System.Drawing.Color.White;
            this.cmbDataBits.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDataBits.Location = new System.Drawing.Point(475, 68);
            this.cmbDataBits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbDataBits.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbDataBits.Size = new System.Drawing.Size(141, 25);
            this.cmbDataBits.TabIndex = 30;
            this.cmbDataBits.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbDataBits.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cmbParity
            // 
            this.cmbParity.DataSource = null;
            this.cmbParity.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbParity.FillColor = System.Drawing.Color.White;
            this.cmbParity.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbParity.Location = new System.Drawing.Point(138, 71);
            this.cmbParity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbParity.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbParity.Size = new System.Drawing.Size(141, 25);
            this.cmbParity.TabIndex = 30;
            this.cmbParity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbParity.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DataSource = null;
            this.cmbBaudRate.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbBaudRate.FillColor = System.Drawing.Color.White;
            this.cmbBaudRate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbBaudRate.Location = new System.Drawing.Point(475, 37);
            this.cmbBaudRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbBaudRate.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbBaudRate.Size = new System.Drawing.Size(141, 25);
            this.cmbBaudRate.TabIndex = 30;
            this.cmbBaudRate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbBaudRate.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cmbComName
            // 
            this.cmbComName.DataSource = null;
            this.cmbComName.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbComName.FillColor = System.Drawing.Color.White;
            this.cmbComName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbComName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11"});
            this.cmbComName.Location = new System.Drawing.Point(138, 37);
            this.cmbComName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbComName.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbComName.Name = "cmbComName";
            this.cmbComName.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbComName.Size = new System.Drawing.Size(141, 25);
            this.cmbComName.TabIndex = 30;
            this.cmbComName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbComName.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolButton5
            // 
            this.uiSymbolButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiSymbolButton5.Location = new System.Drawing.Point(3, 103);
            this.uiSymbolButton5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton5.Name = "uiSymbolButton5";
            this.uiSymbolButton5.Size = new System.Drawing.Size(116, 28);
            this.uiSymbolButton5.Symbol = 61447;
            this.uiSymbolButton5.TabIndex = 29;
            this.uiSymbolButton5.Text = "停止位";
            this.uiSymbolButton5.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolButton4
            // 
            this.uiSymbolButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiSymbolButton4.Location = new System.Drawing.Point(340, 66);
            this.uiSymbolButton4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton4.Name = "uiSymbolButton4";
            this.uiSymbolButton4.Size = new System.Drawing.Size(116, 28);
            this.uiSymbolButton4.Symbol = 61447;
            this.uiSymbolButton4.TabIndex = 29;
            this.uiSymbolButton4.Text = "数据位";
            this.uiSymbolButton4.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolButton3
            // 
            this.uiSymbolButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiSymbolButton3.Location = new System.Drawing.Point(3, 69);
            this.uiSymbolButton3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton3.Name = "uiSymbolButton3";
            this.uiSymbolButton3.Size = new System.Drawing.Size(116, 28);
            this.uiSymbolButton3.Symbol = 61447;
            this.uiSymbolButton3.TabIndex = 29;
            this.uiSymbolButton3.Text = "效验位";
            this.uiSymbolButton3.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolButton2
            // 
            this.uiSymbolButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiSymbolButton2.Location = new System.Drawing.Point(340, 35);
            this.uiSymbolButton2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton2.Name = "uiSymbolButton2";
            this.uiSymbolButton2.Size = new System.Drawing.Size(116, 28);
            this.uiSymbolButton2.Symbol = 61447;
            this.uiSymbolButton2.TabIndex = 29;
            this.uiSymbolButton2.Text = "波特率";
            this.uiSymbolButton2.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiSymbolButton1.Location = new System.Drawing.Point(3, 35);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Size = new System.Drawing.Size(116, 28);
            this.uiSymbolButton1.Symbol = 61447;
            this.uiSymbolButton1.TabIndex = 29;
            this.uiSymbolButton1.Text = "串口号";
            this.uiSymbolButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(664, 68);
            this.btnClose.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 28);
            this.btnClose.Symbol = 61447;
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "关闭串口";
            this.btnClose.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiGroupBox1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(4, 5);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(790, 144);
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.uiPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uiPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.uiPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.uiPanel4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.38931F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.61069F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(798, 756);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // FrmCOM
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(798, 791);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmCOM";
            this.Text = "FrmCOM";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.Load += new System.EventHandler(this.FrmCOM_Load);
            this.uiGroupBox4.ResumeLayout(false);
            this.uiGroupBox4.PerformLayout();
            this.uiPanel4.ResumeLayout(false);
            this.uiGroupBox3.ResumeLayout(false);
            this.uiPanel3.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UISymbolButton btn_Send;
        private Sunny.UI.UIGroupBox uiGroupBox4;
        private System.Windows.Forms.TextBox txt_Send;
        private Sunny.UI.UIPanel uiPanel4;
        private Sunny.UI.UICheckBox chb_0x_DataSend;
        private Sunny.UI.UICheckBox chb_0x_DataReceived;
        private Sunny.UI.UISymbolButton btn_DataReceivedClear;
        private Sunny.UI.UISymbolButton btn_DataSendClear;
        private Sunny.UI.UIGroupBox uiGroupBox3;
        private Sunny.UI.UIPanel uiPanel3;
        private System.Windows.Forms.TextBox txt_DataReceived;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private Sunny.UI.UISymbolButton btn_OpenCom;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIPanel uiPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sunny.UI.UIComboBox cmbComName;
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UIComboBox cmbStopBits;
        private Sunny.UI.UIComboBox cmbDataBits;
        private Sunny.UI.UIComboBox cmbParity;
        private Sunny.UI.UIComboBox cmbBaudRate;
        private Sunny.UI.UISymbolButton uiSymbolButton5;
        private Sunny.UI.UISymbolButton uiSymbolButton4;
        private Sunny.UI.UISymbolButton uiSymbolButton3;
        private Sunny.UI.UISymbolButton uiSymbolButton2;
        private Sunny.UI.UISymbolButton btnClose;
    }
}