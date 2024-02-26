namespace sunyvpp
{
    partial class FrmTcpServer
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
            this.txtPort = new Sunny.UI.UITextBox();
            this.btnLisen = new Sunny.UI.UISymbolButton();
            this.cmbIP = new Sunny.UI.UIComboBox();
            this.btnSend = new Sunny.UI.UISymbolButton();
            this.btnLis = new Sunny.UI.UISymbolButton();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.chbReceive = new Sunny.UI.UICheckBox();
            this.chbSend = new Sunny.UI.UICheckBox();
            this.btn_DataReceivedClear = new Sunny.UI.UISymbolButton();
            this.btn_DataSendClear = new Sunny.UI.UISymbolButton();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.uiGroupBox4 = new Sunny.UI.UIGroupBox();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.uiGroupBox3 = new Sunny.UI.UIGroupBox();
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.uiPanel4 = new Sunny.UI.UIPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiPanel1.SuspendLayout();
            this.uiGroupBox1.SuspendLayout();
            this.uiGroupBox4.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.uiGroupBox2.SuspendLayout();
            this.uiGroupBox3.SuspendLayout();
            this.uiPanel3.SuspendLayout();
            this.uiPanel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPort
            // 
            this.txtPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPort.DoubleValue = 60000D;
            this.txtPort.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPort.IntValue = 60000;
            this.txtPort.Location = new System.Drawing.Point(140, 35);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPort.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPort.Name = "txtPort";
            this.txtPort.ShowText = true;
            this.txtPort.Size = new System.Drawing.Size(100, 28);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "60000";
            this.txtPort.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPort.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnLisen
            // 
            this.btnLisen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLisen.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLisen.Location = new System.Drawing.Point(664, 35);
            this.btnLisen.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLisen.Name = "btnLisen";
            this.btnLisen.Size = new System.Drawing.Size(116, 28);
            this.btnLisen.Symbol = 61447;
            this.btnLisen.TabIndex = 2;
            this.btnLisen.Text = "开始监听";
            this.btnLisen.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLisen.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnLisen.Click += new System.EventHandler(this.btnLisen_Click);
            // 
            // cmbIP
            // 
            this.cmbIP.DataSource = null;
            this.cmbIP.FillColor = System.Drawing.Color.White;
            this.cmbIP.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbIP.Location = new System.Drawing.Point(269, 35);
            this.cmbIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbIP.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbIP.Name = "cmbIP";
            this.cmbIP.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbIP.Size = new System.Drawing.Size(371, 28);
            this.cmbIP.TabIndex = 3;
            this.cmbIP.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbIP.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSend
            // 
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(664, 63);
            this.btnSend.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(116, 28);
            this.btnSend.Symbol = 61447;
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "发送";
            this.btnSend.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnLis
            // 
            this.btnLis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLis.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLis.Location = new System.Drawing.Point(140, 383);
            this.btnLis.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLis.Name = "btnLis";
            this.btnLis.Size = new System.Drawing.Size(103, 42);
            this.btnLis.Symbol = 61447;
            this.btnLis.TabIndex = 2;
            this.btnLis.Text = "获取IP";
            this.btnLis.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLis.Visible = true;
            this.btnLis.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnLis.Click += new System.EventHandler(this.btnLis_Click);
            // 
            // txtReceive
            // 
            this.txtReceive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceive.Location = new System.Drawing.Point(0, 32);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceive.Size = new System.Drawing.Size(894, 385);
            this.txtReceive.TabIndex = 6;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(3, 27);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(637, 148);
            this.txtSend.TabIndex = 6;
            this.txtSend.Text = "A";
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.Location = new System.Drawing.Point(14, 35);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Size = new System.Drawing.Size(116, 28);
            this.uiSymbolButton1.Symbol = 61447;
            this.uiSymbolButton1.TabIndex = 2;
            this.uiSymbolButton1.Text = "端口号：";
            this.uiSymbolButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // chbReceive
            // 
            this.chbReceive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chbReceive.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbReceive.Location = new System.Drawing.Point(200, 32);
            this.chbReceive.MinimumSize = new System.Drawing.Size(1, 1);
            this.chbReceive.Name = "chbReceive";
            this.chbReceive.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.chbReceive.Size = new System.Drawing.Size(172, 25);
            this.chbReceive.TabIndex = 7;
            this.chbReceive.Text = "接收16进制";
            this.chbReceive.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // chbSend
            // 
            this.chbSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chbSend.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbSend.Location = new System.Drawing.Point(14, 32);
            this.chbSend.MinimumSize = new System.Drawing.Size(1, 1);
            this.chbSend.Name = "chbSend";
            this.chbSend.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.chbSend.Size = new System.Drawing.Size(141, 25);
            this.chbSend.TabIndex = 7;
            this.chbSend.Text = "发送16进制";
            this.chbSend.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btn_DataReceivedClear
            // 
            this.btn_DataReceivedClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DataReceivedClear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DataReceivedClear.Location = new System.Drawing.Point(524, 32);
            this.btn_DataReceivedClear.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_DataReceivedClear.Name = "btn_DataReceivedClear";
            this.btn_DataReceivedClear.Size = new System.Drawing.Size(116, 28);
            this.btn_DataReceivedClear.Symbol = 61447;
            this.btn_DataReceivedClear.TabIndex = 30;
            this.btn_DataReceivedClear.Text = "清空接收区";
            this.btn_DataReceivedClear.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DataReceivedClear.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_DataReceivedClear.Click += new System.EventHandler(this.btn_DataReceivedClear_Click);
            // 
            // btn_DataSendClear
            // 
            this.btn_DataSendClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DataSendClear.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DataSendClear.Location = new System.Drawing.Point(664, 29);
            this.btn_DataSendClear.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_DataSendClear.Name = "btn_DataSendClear";
            this.btn_DataSendClear.Size = new System.Drawing.Size(116, 28);
            this.btn_DataSendClear.Symbol = 61447;
            this.btn_DataSendClear.TabIndex = 31;
            this.btn_DataSendClear.Text = "清空发送区";
            this.btn_DataSendClear.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DataSendClear.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btn_DataSendClear.Click += new System.EventHandler(this.btn_DataSendClear_Click);
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
            this.uiPanel1.Size = new System.Drawing.Size(894, 90);
            this.uiPanel1.TabIndex = 0;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiSymbolButton1);
            this.uiGroupBox1.Controls.Add(this.txtPort);
            this.uiGroupBox1.Controls.Add(this.cmbIP);
            this.uiGroupBox1.Controls.Add(this.btnLisen);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(894, 90);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "网络设置区";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.btnSend);
            this.uiGroupBox4.Controls.Add(this.txtSend);
            this.uiGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox4.Size = new System.Drawing.Size(894, 187);
            this.uiGroupBox4.TabIndex = 0;
            this.uiGroupBox4.Text = "数据发送区";
            this.uiGroupBox4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.uiGroupBox2);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(4, 105);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(894, 417);
            this.uiPanel2.TabIndex = 1;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.uiPanel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.txtReceive);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox2.Size = new System.Drawing.Size(894, 417);
            this.uiGroupBox2.TabIndex = 0;
            this.uiGroupBox2.Text = "数据接收区";
            this.uiGroupBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.chbSend);
            this.uiGroupBox3.Controls.Add(this.btn_DataSendClear);
            this.uiGroupBox3.Controls.Add(this.chbReceive);
            this.uiGroupBox3.Controls.Add(this.btn_DataReceivedClear);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox3.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox3.Size = new System.Drawing.Size(894, 75);
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
            this.uiPanel3.Location = new System.Drawing.Point(4, 532);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Size = new System.Drawing.Size(894, 75);
            this.uiPanel3.TabIndex = 2;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiPanel4
            // 
            this.uiPanel4.Controls.Add(this.uiGroupBox4);
            this.uiPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel4.Location = new System.Drawing.Point(4, 617);
            this.uiPanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel4.Name = "uiPanel4";
            this.uiPanel4.Size = new System.Drawing.Size(894, 187);
            this.uiPanel4.TabIndex = 3;
            this.uiPanel4.Text = null;
            this.uiPanel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.10112F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.89888F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(902, 809);
            this.tableLayoutPanel1.TabIndex = 33;
            // 
            // FrmTcpServer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(902, 844);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnLis);
            this.MaximizeBox = true;
            this.Name = "FrmTcpServer";
            this.Text = "FrmTcpServer";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTcpServer_FormClosed);
            this.Load += new System.EventHandler(this.FrmTcpServer_Load);
            this.uiPanel1.ResumeLayout(true);
            this.uiGroupBox1.ResumeLayout(true);
            this.uiGroupBox4.ResumeLayout(true);
            this.uiGroupBox4.PerformLayout();
            this.uiPanel2.ResumeLayout(true);
            this.uiGroupBox2.ResumeLayout(true);
            this.uiGroupBox2.PerformLayout();
            this.uiGroupBox3.ResumeLayout(true);
            this.uiPanel3.ResumeLayout(true);
            this.uiPanel4.ResumeLayout(true);
            this.tableLayoutPanel1.ResumeLayout(true);
            this.ResumeLayout(true);

        }

        #endregion
        private Sunny.UI.UITextBox txtPort;
        private Sunny.UI.UISymbolButton btnLisen;
        private Sunny.UI.UIComboBox cmbIP;
        private Sunny.UI.UISymbolButton btnSend;
        private Sunny.UI.UISymbolButton btnLis;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.TextBox txtSend;
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UICheckBox chbReceive;
        private Sunny.UI.UICheckBox chbSend;
        private Sunny.UI.UISymbolButton btn_DataReceivedClear;
        private Sunny.UI.UISymbolButton btn_DataSendClear;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIGroupBox uiGroupBox4;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private Sunny.UI.UIGroupBox uiGroupBox3;
        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UIPanel uiPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}