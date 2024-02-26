namespace sunyvpp
{
    partial class FrmTCPClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTCPClient));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.txt_ServerIp = new Sunny.UI.UIIPTextBox();
            this.txt_ServerPort = new Sunny.UI.UITextBox();
            this.btn_Connect = new Sunny.UI.UISymbolButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.txt_DataReceived = new System.Windows.Forms.TextBox();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.uiGroupBox3 = new Sunny.UI.UIGroupBox();
            this.chb_0x_DataSend = new Sunny.UI.UICheckBox();
            this.chb_0x_DataReceived = new Sunny.UI.UICheckBox();
            this.btn_DataReceivedClear = new Sunny.UI.UISymbolButton();
            this.btn_DataSendClear = new Sunny.UI.UISymbolButton();
            this.uiPanel4 = new Sunny.UI.UIPanel();
            this.uiGroupBox4 = new Sunny.UI.UIGroupBox();
            this.txt_Send = new System.Windows.Forms.TextBox();
            this.btn_Send = new Sunny.UI.UISymbolButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            this.uiGroupBox1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiGroupBox2.SuspendLayout();
            this.uiPanel3.SuspendLayout();
            this.uiGroupBox3.SuspendLayout();
            this.uiPanel4.SuspendLayout();
            this.uiGroupBox4.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.3913F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.6087F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(943, 742);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this.uiPanel1.Size = new System.Drawing.Size(935, 82);
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txt_ServerIp);
            this.uiGroupBox1.Controls.Add(this.txt_ServerPort);
            this.uiGroupBox1.Controls.Add(this.btn_Connect);
            this.uiGroupBox1.Controls.Add(this.uiLabel2);
            this.uiGroupBox1.Controls.Add(this.uiLabel1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(935, 82);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "网络设置区";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txt_ServerIp
            // 
            this.txt_ServerIp.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ServerIp.Location = new System.Drawing.Point(87, 43);
            this.txt_ServerIp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_ServerIp.MinimumSize = new System.Drawing.Size(1, 1);
            this.txt_ServerIp.Name = "txt_ServerIp";
            this.txt_ServerIp.Padding = new System.Windows.Forms.Padding(1);
            this.txt_ServerIp.ShowText = true;
            this.txt_ServerIp.Size = new System.Drawing.Size(223, 28);
            this.txt_ServerIp.TabIndex = 0;
            this.txt_ServerIp.Text = "127.0.0.1";
            this.txt_ServerIp.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_ServerIp.Value = ((System.Net.IPAddress)(resources.GetObject("txt_ServerIp.Value")));
            this.txt_ServerIp.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txt_ServerPort
            // 
            this.txt_ServerPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_ServerPort.DoubleValue = 60000D;
            this.txt_ServerPort.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ServerPort.IntValue = 60000;
            this.txt_ServerPort.Location = new System.Drawing.Point(516, 43);
            this.txt_ServerPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_ServerPort.MinimumSize = new System.Drawing.Size(1, 16);
            this.txt_ServerPort.Name = "txt_ServerPort";
            this.txt_ServerPort.ShowText = true;
            this.txt_ServerPort.Size = new System.Drawing.Size(130, 28);
            this.txt_ServerPort.TabIndex = 0;
            this.txt_ServerPort.Text = "60000";
            this.txt_ServerPort.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txt_ServerPort.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Connect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Connect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Connect.Location = new System.Drawing.Point(664, 43);
            this.btn_Connect.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(116, 28);
            this.btn_Connect.Symbol = 61447;
            this.btn_Connect.TabIndex = 29;
            this.btn_Connect.Text = "连接";
            this.btn_Connect.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Connect.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(434, 43);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(92, 28);
            this.uiLabel2.TabIndex = 0;
            this.uiLabel2.Text = "Port:";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(16, 43);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(92, 28);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "IP:";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiGroupBox2);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(4, 97);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(935, 427);
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
            this.uiGroupBox2.Size = new System.Drawing.Size(935, 427);
            this.uiGroupBox2.TabIndex = 0;
            this.uiGroupBox2.Text = "数据接收区";
            this.uiGroupBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txt_DataReceived
            // 
            this.txt_DataReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_DataReceived.Location = new System.Drawing.Point(0, 32);
            this.txt_DataReceived.Multiline = true;
            this.txt_DataReceived.Name = "txt_DataReceived";
            this.txt_DataReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_DataReceived.Size = new System.Drawing.Size(935, 395);
            this.txt_DataReceived.TabIndex = 1;
            // 
            // uiPanel3
            // 
            this.uiPanel3.Controls.Add(this.uiGroupBox3);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel3.Location = new System.Drawing.Point(4, 534);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Size = new System.Drawing.Size(935, 65);
            this.uiPanel3.TabIndex = 2;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
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
            this.uiGroupBox3.Size = new System.Drawing.Size(935, 65);
            this.uiGroupBox3.TabIndex = 0;
            this.uiGroupBox3.Text = "数据设置区";
            this.uiGroupBox3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
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
            // uiPanel4
            // 
            this.uiPanel4.Controls.Add(this.uiGroupBox4);
            this.uiPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel4.Location = new System.Drawing.Point(4, 609);
            this.uiPanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel4.Name = "uiPanel4";
            this.uiPanel4.Size = new System.Drawing.Size(935, 128);
            this.uiPanel4.TabIndex = 3;
            this.uiPanel4.Text = null;
            this.uiPanel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
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
            this.uiGroupBox4.Size = new System.Drawing.Size(935, 128);
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
            this.txt_Send.Text = "T1";
            // 
            // btn_Send
            // 
            this.btn_Send.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Send.Enabled = true;
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
            // FrmTCPClient
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(943, 777);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = true;
            this.Name = "FrmTCPClient";
            this.Text = "FrmTCPClient";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTCPClient_FormClosed);
            this.Load += new System.EventHandler(this.FrmTCPClient_Load);
            this.tableLayoutPanel1.ResumeLayout(true);
            this.uiPanel1.ResumeLayout(true);
            this.uiGroupBox1.ResumeLayout(true);
            this.uiPanel2.ResumeLayout(true);
            this.uiGroupBox2.ResumeLayout(true);
            this.uiGroupBox2.PerformLayout();
            this.uiPanel3.ResumeLayout(true);
            this.uiGroupBox3.ResumeLayout(true);
            this.uiPanel4.ResumeLayout(true);
            this.uiGroupBox4.ResumeLayout(true);
            this.uiGroupBox4.PerformLayout();
            this.ResumeLayout(true);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UIGroupBox uiGroupBox3;
        private Sunny.UI.UIPanel uiPanel4;
        private Sunny.UI.UIGroupBox uiGroupBox4;
        private Sunny.UI.UIIPTextBox txt_ServerIp;
        private Sunny.UI.UITextBox txt_ServerPort;
        private Sunny.UI.UISymbolButton btn_Connect;
        private Sunny.UI.UICheckBox chb_0x_DataSend;
        private Sunny.UI.UICheckBox chb_0x_DataReceived;
        private Sunny.UI.UISymbolButton btn_DataReceivedClear;
        private Sunny.UI.UISymbolButton btn_DataSendClear;
        private Sunny.UI.UISymbolButton btn_Send;
        private System.Windows.Forms.TextBox txt_DataReceived;
        private System.Windows.Forms.TextBox txt_Send;
    }
}