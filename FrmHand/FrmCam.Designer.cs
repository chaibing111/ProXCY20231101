namespace sunyvpp
{
    partial class FrmCam
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCam));
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.cogDisplay1 = new Cognex.VisionPro.Display.CogDisplay();
            this.btnTriggerExec = new Sunny.UI.UISymbolButton();
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.chbCross = new Sunny.UI.UICheckBox();
            this.btnContinueAcq = new Sunny.UI.UISymbolButton();
            this.btnOpenImage = new Sunny.UI.UISymbolButton();
            this.btnSaveImage = new Sunny.UI.UISymbolButton();
            this.btnSetExpose = new Sunny.UI.UISymbolButton();
            this.btnSetExpose2 = new Sunny.UI.UISymbolButton();
            this.intupdown1 = new Sunny.UI.UIIntegerUpDown();
            this.intupdown2 = new Sunny.UI.UIIntegerUpDown();
            this.uiTableLayoutPanel1 = new Sunny.UI.UITableLayoutPanel();
            this.cbDeviceList = new Sunny.UI.UIComboBox();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.cmbExposeSelect = new Sunny.UI.UIComboBox();
            this.uiSymbolButton5 = new Sunny.UI.UISymbolButton();
            this.uiGroupBox3 = new Sunny.UI.UIGroupBox();
            this.cmbChangeProj = new Sunny.UI.UIComboBox();
            this.uiSymbolButton4 = new Sunny.UI.UISymbolButton();
            this.btnChangeRunProj = new Sunny.UI.UISymbolButton();
            this.btnDebug = new Sunny.UI.UISymbolButton();
            this.gpbCamRes = new Sunny.UI.UIGroupBox();
            this.rdbFile = new System.Windows.Forms.RadioButton();
            this.rdbCam = new System.Windows.Forms.RadioButton();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.chbUpCamPhotoPos = new Sunny.UI.UIComboBox();
            this.uiSymbolLabel3 = new Sunny.UI.UISymbolLabel();
            this.uiSymbolLabel2 = new Sunny.UI.UISymbolLabel();
            this.uiSymbolLabel1 = new Sunny.UI.UISymbolLabel();
            this.txbModleR = new Sunny.UI.UITextBox();
            this.txbModleY = new Sunny.UI.UITextBox();
            this.txbModleX = new Sunny.UI.UITextBox();
            this.btnUpCamModle = new Sunny.UI.UISymbolButton();
            this.btnDownCamModle = new Sunny.UI.UISymbolButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.cogDisplay1)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.uiTableLayoutPanel1.SuspendLayout();
            this.uiPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.uiGroupBox1.SuspendLayout();
            this.uiGroupBox3.SuspendLayout();
            this.gpbCamRes.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.Location = new System.Drawing.Point(4, 1145);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Size = new System.Drawing.Size(97, 50);
            this.uiSymbolButton1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButton1.TabIndex = 1;
            this.uiSymbolButton1.Text = "保存";
            this.uiSymbolButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiSymbolButton1.Click += new System.EventHandler(this.uiSymbolButton1_Click);
            // 
            // cogDisplay1
            // 
            this.cogDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogDisplay1.DoubleTapZoomCycleLength = 2;
            this.cogDisplay1.DoubleTapZoomSensitivity = 2.5D;
            this.cogDisplay1.Location = new System.Drawing.Point(0, 0);
            this.cogDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogDisplay1.MouseWheelSensitivity = 1D;
            this.cogDisplay1.Name = "cogDisplay1";
            this.cogDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogDisplay1.OcxState")));
            this.cogDisplay1.Size = new System.Drawing.Size(787, 769);
            this.cogDisplay1.TabIndex = 3;
            // 
            // btnTriggerExec
            // 
            this.btnTriggerExec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTriggerExec.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTriggerExec.Location = new System.Drawing.Point(46, 24);
            this.btnTriggerExec.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnTriggerExec.Name = "btnTriggerExec";
            this.btnTriggerExec.Size = new System.Drawing.Size(145, 47);
            this.btnTriggerExec.Style = Sunny.UI.UIStyle.Custom;
            this.btnTriggerExec.Symbol = 61447;
            this.btnTriggerExec.TabIndex = 32;
            this.btnTriggerExec.Text = "采集图片";
            this.btnTriggerExec.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTriggerExec.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnTriggerExec.Click += new System.EventHandler(this.btnTriggerExec_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.chbCross);
            this.uiGroupBox2.Controls.Add(this.btnContinueAcq);
            this.uiGroupBox2.Controls.Add(this.btnOpenImage);
            this.uiGroupBox2.Controls.Add(this.btnSaveImage);
            this.uiGroupBox2.Controls.Add(this.btnTriggerExec);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox2.Location = new System.Drawing.Point(4, 5);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox2.Size = new System.Drawing.Size(468, 168);
            this.uiGroupBox2.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox2.TabIndex = 33;
            this.uiGroupBox2.Text = "采集图像";
            this.uiGroupBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // chbCross
            // 
            this.chbCross.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chbCross.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbCross.Location = new System.Drawing.Point(45, 130);
            this.chbCross.MinimumSize = new System.Drawing.Size(1, 1);
            this.chbCross.Name = "chbCross";
            this.chbCross.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.chbCross.Size = new System.Drawing.Size(145, 47);
            this.chbCross.Style = Sunny.UI.UIStyle.Custom;
            this.chbCross.TabIndex = 34;
            this.chbCross.Text = "十字线";
            this.chbCross.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnContinueAcq
            // 
            this.btnContinueAcq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinueAcq.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnContinueAcq.Location = new System.Drawing.Point(275, 24);
            this.btnContinueAcq.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnContinueAcq.Name = "btnContinueAcq";
            this.btnContinueAcq.Size = new System.Drawing.Size(145, 47);
            this.btnContinueAcq.Style = Sunny.UI.UIStyle.Custom;
            this.btnContinueAcq.Symbol = 61447;
            this.btnContinueAcq.TabIndex = 32;
            this.btnContinueAcq.Text = "连续采集";
            this.btnContinueAcq.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnContinueAcq.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnContinueAcq.Click += new System.EventHandler(this.btnContinueAcq_Click);
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenImage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenImage.Location = new System.Drawing.Point(46, 77);
            this.btnOpenImage.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(145, 47);
            this.btnOpenImage.Style = Sunny.UI.UIStyle.Custom;
            this.btnOpenImage.Symbol = 61447;
            this.btnOpenImage.TabIndex = 32;
            this.btnOpenImage.Text = "打开图片";
            this.btnOpenImage.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenImage.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveImage.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveImage.Location = new System.Drawing.Point(275, 77);
            this.btnSaveImage.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(145, 47);
            this.btnSaveImage.Style = Sunny.UI.UIStyle.Custom;
            this.btnSaveImage.Symbol = 61447;
            this.btnSaveImage.TabIndex = 32;
            this.btnSaveImage.Text = "保存图片";
            this.btnSaveImage.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveImage.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnSetExpose
            // 
            this.btnSetExpose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetExpose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetExpose.Location = new System.Drawing.Point(45, 79);
            this.btnSetExpose.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSetExpose.Name = "btnSetExpose";
            this.btnSetExpose.Size = new System.Drawing.Size(145, 47);
            this.btnSetExpose.Style = Sunny.UI.UIStyle.Custom;
            this.btnSetExpose.Symbol = 61447;
            this.btnSetExpose.TabIndex = 32;
            this.btnSetExpose.Text = "曝光1设置";
            this.btnSetExpose.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetExpose.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSetExpose.Click += new System.EventHandler(this.btnSetExpose_Click);
            // 
            // btnSetExpose2
            // 
            this.btnSetExpose2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetExpose2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetExpose2.Location = new System.Drawing.Point(46, 130);
            this.btnSetExpose2.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSetExpose2.Name = "btnSetExpose2";
            this.btnSetExpose2.Size = new System.Drawing.Size(145, 47);
            this.btnSetExpose2.Style = Sunny.UI.UIStyle.Custom;
            this.btnSetExpose2.Symbol = 61447;
            this.btnSetExpose2.TabIndex = 32;
            this.btnSetExpose2.Text = "曝光2设置";
            this.btnSetExpose2.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetExpose2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSetExpose2.Click += new System.EventHandler(this.btnSetExpose2_Click);
            // 
            // intupdown1
            // 
            this.intupdown1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.intupdown1.Location = new System.Drawing.Point(244, 78);
            this.intupdown1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.intupdown1.MinimumSize = new System.Drawing.Size(100, 0);
            this.intupdown1.Name = "intupdown1";
            this.intupdown1.ShowText = false;
            this.intupdown1.Size = new System.Drawing.Size(177, 47);
            this.intupdown1.Style = Sunny.UI.UIStyle.Custom;
            this.intupdown1.TabIndex = 37;
            this.intupdown1.Text = "uiIntegerUpDown1";
            this.intupdown1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.intupdown1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // intupdown2
            // 
            this.intupdown2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.intupdown2.Location = new System.Drawing.Point(244, 130);
            this.intupdown2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.intupdown2.MinimumSize = new System.Drawing.Size(100, 0);
            this.intupdown2.Name = "intupdown2";
            this.intupdown2.ShowText = false;
            this.intupdown2.Size = new System.Drawing.Size(177, 47);
            this.intupdown2.Style = Sunny.UI.UIStyle.Custom;
            this.intupdown2.TabIndex = 37;
            this.intupdown2.Text = "uiIntegerUpDown1";
            this.intupdown2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.intupdown2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiTableLayoutPanel1
            // 
            this.uiTableLayoutPanel1.ColumnCount = 1;
            this.uiTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.uiTableLayoutPanel1.Controls.Add(this.cbDeviceList, 0, 0);
            this.uiTableLayoutPanel1.Controls.Add(this.uiPanel1, 0, 1);
            this.uiTableLayoutPanel1.Location = new System.Drawing.Point(3, 38);
            this.uiTableLayoutPanel1.Name = "uiTableLayoutPanel1";
            this.uiTableLayoutPanel1.RowCount = 2;
            this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.742548F));
            this.uiTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.25745F));
            this.uiTableLayoutPanel1.Size = new System.Drawing.Size(795, 817);
            this.uiTableLayoutPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTableLayoutPanel1.TabIndex = 38;
            this.uiTableLayoutPanel1.TagString = null;
            // 
            // cbDeviceList
            // 
            this.cbDeviceList.DataSource = null;
            this.cbDeviceList.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cbDeviceList.FillColor = System.Drawing.Color.White;
            this.cbDeviceList.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbDeviceList.Items.AddRange(new object[] {
            "上相机",
            "下相机"});
            this.cbDeviceList.Location = new System.Drawing.Point(4, 5);
            this.cbDeviceList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDeviceList.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbDeviceList.Name = "cbDeviceList";
            this.cbDeviceList.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbDeviceList.Size = new System.Drawing.Size(787, 24);
            this.cbDeviceList.Style = Sunny.UI.UIStyle.Custom;
            this.cbDeviceList.TabIndex = 38;
            this.cbDeviceList.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbDeviceList.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.cbDeviceList.SelectedIndexChanged += new System.EventHandler(this.cbDeviceList_SelectedIndexChanged);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.cogRecordDisplay1);
            this.uiPanel1.Controls.Add(this.cogDisplay1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel1.Location = new System.Drawing.Point(4, 43);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(787, 769);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel1.TabIndex = 11;
            this.uiPanel1.Text = "uiPanel1";
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cogRecordDisplay1
            // 
            this.cogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogRecordDisplay1.DoubleTapZoomCycleLength = 2;
            this.cogRecordDisplay1.DoubleTapZoomSensitivity = 2.5D;
            this.cogRecordDisplay1.Location = new System.Drawing.Point(0, 0);
            this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay1.Name = "cogRecordDisplay1";
            this.cogRecordDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay1.OcxState")));
            this.cogRecordDisplay1.Size = new System.Drawing.Size(787, 769);
            this.cogRecordDisplay1.TabIndex = 36;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.uiPanel2, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(801, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.55556F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.44444F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 197F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(476, 817);
            this.tableLayoutPanel1.TabIndex = 39;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmbExposeSelect);
            this.uiGroupBox1.Controls.Add(this.intupdown1);
            this.uiGroupBox1.Controls.Add(this.btnSetExpose);
            this.uiGroupBox1.Controls.Add(this.uiSymbolButton5);
            this.uiGroupBox1.Controls.Add(this.btnSetExpose2);
            this.uiGroupBox1.Controls.Add(this.intupdown2);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(4, 455);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(468, 187);
            this.uiGroupBox1.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox1.TabIndex = 34;
            this.uiGroupBox1.Text = "曝光设置";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cmbExposeSelect
            // 
            this.cmbExposeSelect.DataSource = null;
            this.cmbExposeSelect.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbExposeSelect.FillColor = System.Drawing.Color.White;
            this.cmbExposeSelect.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbExposeSelect.Items.AddRange(new object[] {
            "曝光1",
            "曝光2"});
            this.cmbExposeSelect.Location = new System.Drawing.Point(244, 25);
            this.cmbExposeSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbExposeSelect.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbExposeSelect.Name = "cmbExposeSelect";
            this.cmbExposeSelect.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbExposeSelect.Size = new System.Drawing.Size(177, 43);
            this.cmbExposeSelect.Style = Sunny.UI.UIStyle.Custom;
            this.cmbExposeSelect.TabIndex = 38;
            this.cmbExposeSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbExposeSelect.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolButton5
            // 
            this.uiSymbolButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton5.Location = new System.Drawing.Point(46, 25);
            this.uiSymbolButton5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton5.Name = "uiSymbolButton5";
            this.uiSymbolButton5.Size = new System.Drawing.Size(145, 47);
            this.uiSymbolButton5.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButton5.Symbol = 61447;
            this.uiSymbolButton5.TabIndex = 32;
            this.uiSymbolButton5.Text = "曝光选择";
            this.uiSymbolButton5.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiGroupBox3
            // 
            this.uiGroupBox3.Controls.Add(this.cmbChangeProj);
            this.uiGroupBox3.Controls.Add(this.uiSymbolButton4);
            this.uiGroupBox3.Controls.Add(this.btnChangeRunProj);
            this.uiGroupBox3.Controls.Add(this.btnDebug);
            this.uiGroupBox3.Controls.Add(this.gpbCamRes);
            this.uiGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox3.Location = new System.Drawing.Point(4, 183);
            this.uiGroupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox3.Size = new System.Drawing.Size(468, 262);
            this.uiGroupBox3.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox3.TabIndex = 35;
            this.uiGroupBox3.Text = "视觉工具";
            this.uiGroupBox3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cmbChangeProj
            // 
            this.cmbChangeProj.DataSource = null;
            this.cmbChangeProj.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbChangeProj.FillColor = System.Drawing.Color.White;
            this.cmbChangeProj.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbChangeProj.Location = new System.Drawing.Point(44, 83);
            this.cmbChangeProj.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbChangeProj.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbChangeProj.Name = "cmbChangeProj";
            this.cmbChangeProj.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbChangeProj.Size = new System.Drawing.Size(375, 35);
            this.cmbChangeProj.Style = Sunny.UI.UIStyle.Custom;
            this.cmbChangeProj.TabIndex = 38;
            this.cmbChangeProj.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbChangeProj.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolButton4
            // 
            this.uiSymbolButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton4.Location = new System.Drawing.Point(45, 24);
            this.uiSymbolButton4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton4.Name = "uiSymbolButton4";
            this.uiSymbolButton4.Size = new System.Drawing.Size(375, 47);
            this.uiSymbolButton4.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolButton4.Symbol = 61447;
            this.uiSymbolButton4.TabIndex = 32;
            this.uiSymbolButton4.Text = "工程文件vpp显示";
            this.uiSymbolButton4.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnChangeRunProj
            // 
            this.btnChangeRunProj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangeRunProj.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChangeRunProj.Location = new System.Drawing.Point(46, 209);
            this.btnChangeRunProj.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnChangeRunProj.Name = "btnChangeRunProj";
            this.btnChangeRunProj.Size = new System.Drawing.Size(145, 47);
            this.btnChangeRunProj.Style = Sunny.UI.UIStyle.Custom;
            this.btnChangeRunProj.Symbol = 61447;
            this.btnChangeRunProj.TabIndex = 32;
            this.btnChangeRunProj.Text = "运行工程";
            this.btnChangeRunProj.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChangeRunProj.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnChangeRunProj.Click += new System.EventHandler(this.btnChangeRunProj_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDebug.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDebug.Location = new System.Drawing.Point(276, 209);
            this.btnDebug.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(145, 47);
            this.btnDebug.Style = Sunny.UI.UIStyle.Custom;
            this.btnDebug.Symbol = 61447;
            this.btnDebug.TabIndex = 32;
            this.btnDebug.Text = "视觉调试";
            this.btnDebug.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDebug.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // gpbCamRes
            // 
            this.gpbCamRes.Controls.Add(this.rdbFile);
            this.gpbCamRes.Controls.Add(this.rdbCam);
            this.gpbCamRes.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpbCamRes.Location = new System.Drawing.Point(45, 121);
            this.gpbCamRes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gpbCamRes.MinimumSize = new System.Drawing.Size(1, 1);
            this.gpbCamRes.Name = "gpbCamRes";
            this.gpbCamRes.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.gpbCamRes.Size = new System.Drawing.Size(374, 79);
            this.gpbCamRes.Style = Sunny.UI.UIStyle.Custom;
            this.gpbCamRes.TabIndex = 39;
            this.gpbCamRes.Text = "图像源";
            this.gpbCamRes.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.gpbCamRes.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rdbFile
            // 
            this.rdbFile.AutoSize = true;
            this.rdbFile.Location = new System.Drawing.Point(230, 44);
            this.rdbFile.Name = "rdbFile";
            this.rdbFile.Size = new System.Drawing.Size(92, 25);
            this.rdbFile.TabIndex = 0;
            this.rdbFile.TabStop = true;
            this.rdbFile.Text = "文件图像";
            this.rdbFile.UseVisualStyleBackColor = true;
            // 
            // rdbCam
            // 
            this.rdbCam.AutoSize = true;
            this.rdbCam.Location = new System.Drawing.Point(3, 44);
            this.rdbCam.Name = "rdbCam";
            this.rdbCam.Size = new System.Drawing.Size(92, 25);
            this.rdbCam.TabIndex = 0;
            this.rdbCam.TabStop = true;
            this.rdbCam.Text = "相机图像";
            this.rdbCam.UseVisualStyleBackColor = true;
            // 
            // uiPanel2
            // 
            this.uiPanel2.Controls.Add(this.chbUpCamPhotoPos);
            this.uiPanel2.Controls.Add(this.uiSymbolLabel3);
            this.uiPanel2.Controls.Add(this.uiSymbolLabel2);
            this.uiPanel2.Controls.Add(this.uiSymbolLabel1);
            this.uiPanel2.Controls.Add(this.txbModleR);
            this.uiPanel2.Controls.Add(this.txbModleY);
            this.uiPanel2.Controls.Add(this.txbModleX);
            this.uiPanel2.Controls.Add(this.btnUpCamModle);
            this.uiPanel2.Controls.Add(this.btnDownCamModle);
            this.uiPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel2.Location = new System.Drawing.Point(4, 652);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(468, 160);
            this.uiPanel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel2.TabIndex = 36;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // chbUpCamPhotoPos
            // 
            this.chbUpCamPhotoPos.DataSource = null;
            this.chbUpCamPhotoPos.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.chbUpCamPhotoPos.FillColor = System.Drawing.Color.White;
            this.chbUpCamPhotoPos.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chbUpCamPhotoPos.Items.AddRange(new object[] {
            "上相机拍照位置1",
            "上相机拍照位置2",
            "上相机拍照位置3",
            "上相机拍照位置4"});
            this.chbUpCamPhotoPos.Location = new System.Drawing.Point(175, 115);
            this.chbUpCamPhotoPos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chbUpCamPhotoPos.MinimumSize = new System.Drawing.Size(63, 0);
            this.chbUpCamPhotoPos.Name = "chbUpCamPhotoPos";
            this.chbUpCamPhotoPos.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.chbUpCamPhotoPos.Size = new System.Drawing.Size(126, 40);
            this.chbUpCamPhotoPos.Style = Sunny.UI.UIStyle.Custom;
            this.chbUpCamPhotoPos.TabIndex = 38;
            this.chbUpCamPhotoPos.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.chbUpCamPhotoPos.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolLabel3
            // 
            this.uiSymbolLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolLabel3.Location = new System.Drawing.Point(338, 10);
            this.uiSymbolLabel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel3.Name = "uiSymbolLabel3";
            this.uiSymbolLabel3.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolLabel3.Size = new System.Drawing.Size(126, 26);
            this.uiSymbolLabel3.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolLabel3.Symbol = 94;
            this.uiSymbolLabel3.TabIndex = 34;
            this.uiSymbolLabel3.Text = "R坐标：";
            this.uiSymbolLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolLabel2
            // 
            this.uiSymbolLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolLabel2.Location = new System.Drawing.Point(175, 10);
            this.uiSymbolLabel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel2.Name = "uiSymbolLabel2";
            this.uiSymbolLabel2.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolLabel2.Size = new System.Drawing.Size(126, 26);
            this.uiSymbolLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolLabel2.Symbol = 94;
            this.uiSymbolLabel2.TabIndex = 34;
            this.uiSymbolLabel2.Text = "Y坐标：";
            this.uiSymbolLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolLabel1
            // 
            this.uiSymbolLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolLabel1.Location = new System.Drawing.Point(4, 10);
            this.uiSymbolLabel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolLabel1.Name = "uiSymbolLabel1";
            this.uiSymbolLabel1.Padding = new System.Windows.Forms.Padding(28, 0, 0, 0);
            this.uiSymbolLabel1.Size = new System.Drawing.Size(126, 26);
            this.uiSymbolLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSymbolLabel1.Symbol = 94;
            this.uiSymbolLabel1.TabIndex = 34;
            this.uiSymbolLabel1.Text = "X坐标：";
            this.uiSymbolLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txbModleR
            // 
            this.txbModleR.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbModleR.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbModleR.Location = new System.Drawing.Point(338, 59);
            this.txbModleR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbModleR.MinimumSize = new System.Drawing.Size(1, 16);
            this.txbModleR.Name = "txbModleR";
            this.txbModleR.ShowText = false;
            this.txbModleR.Size = new System.Drawing.Size(126, 37);
            this.txbModleR.Style = Sunny.UI.UIStyle.Custom;
            this.txbModleR.TabIndex = 33;
            this.txbModleR.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txbModleR.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txbModleY
            // 
            this.txbModleY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbModleY.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbModleY.Location = new System.Drawing.Point(175, 59);
            this.txbModleY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbModleY.MinimumSize = new System.Drawing.Size(1, 16);
            this.txbModleY.Name = "txbModleY";
            this.txbModleY.ShowText = false;
            this.txbModleY.Size = new System.Drawing.Size(126, 37);
            this.txbModleY.Style = Sunny.UI.UIStyle.Custom;
            this.txbModleY.TabIndex = 33;
            this.txbModleY.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txbModleY.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txbModleX
            // 
            this.txbModleX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbModleX.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbModleX.Location = new System.Drawing.Point(4, 59);
            this.txbModleX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbModleX.MinimumSize = new System.Drawing.Size(1, 16);
            this.txbModleX.Name = "txbModleX";
            this.txbModleX.ShowText = false;
            this.txbModleX.Size = new System.Drawing.Size(126, 37);
            this.txbModleX.Style = Sunny.UI.UIStyle.Custom;
            this.txbModleX.TabIndex = 33;
            this.txbModleX.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txbModleX.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnUpCamModle
            // 
            this.btnUpCamModle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpCamModle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpCamModle.Location = new System.Drawing.Point(4, 115);
            this.btnUpCamModle.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnUpCamModle.Name = "btnUpCamModle";
            this.btnUpCamModle.Size = new System.Drawing.Size(145, 47);
            this.btnUpCamModle.Style = Sunny.UI.UIStyle.Custom;
            this.btnUpCamModle.Symbol = 61447;
            this.btnUpCamModle.TabIndex = 32;
            this.btnUpCamModle.Text = "上相机模板保存";
            this.btnUpCamModle.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpCamModle.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnUpCamModle.Click += new System.EventHandler(this.btnUpCamModle_Click);
            // 
            // btnDownCamModle
            // 
            this.btnDownCamModle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownCamModle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownCamModle.Location = new System.Drawing.Point(320, 115);
            this.btnDownCamModle.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDownCamModle.Name = "btnDownCamModle";
            this.btnDownCamModle.Size = new System.Drawing.Size(145, 47);
            this.btnDownCamModle.Style = Sunny.UI.UIStyle.Custom;
            this.btnDownCamModle.Symbol = 61447;
            this.btnDownCamModle.TabIndex = 32;
            this.btnDownCamModle.Text = "下相机模板保存";
            this.btnDownCamModle.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownCamModle.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnDownCamModle.Click += new System.EventHandler(this.btnDownCamModle_Click);
            // 
            // FrmCam
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1280, 858);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.uiTableLayoutPanel1);
            this.Controls.Add(this.uiSymbolButton1);
            this.Name = "FrmCam";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "相机调试";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 1033, 728);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCam_FormClosed);
            this.Load += new System.EventHandler(this.FrmCam_Load);
            this.SizeChanged += new System.EventHandler(this.FrmVisionParam_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.cogDisplay1)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiTableLayoutPanel1.ResumeLayout(false);
            this.uiPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox3.ResumeLayout(false);
            this.gpbCamRes.ResumeLayout(false);
            this.gpbCamRes.PerformLayout();
            this.uiPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Cognex.VisionPro.Display.CogDisplay cogDisplay1;
        private Sunny.UI.UISymbolButton btnTriggerExec;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private Sunny.UI.UISymbolButton btnContinueAcq;
        private Sunny.UI.UICheckBox chbCross;
        private Sunny.UI.UISymbolButton btnSetExpose;
        private Sunny.UI.UISymbolButton btnSetExpose2;
        private Sunny.UI.UIIntegerUpDown intupdown1;
        private Sunny.UI.UIIntegerUpDown intupdown2;
        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private Sunny.UI.UIComboBox cmbExposeSelect;
        private Sunny.UI.UISymbolButton btnOpenImage;
        private Sunny.UI.UISymbolButton btnSaveImage;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIComboBox cbDeviceList;
        private Sunny.UI.UIGroupBox uiGroupBox3;
        private Sunny.UI.UIComboBox cmbChangeProj;
        private Sunny.UI.UISymbolButton uiSymbolButton4;
        private Sunny.UI.UISymbolButton btnChangeRunProj;
        private Sunny.UI.UISymbolButton btnDebug;
        private Sunny.UI.UISymbolButton uiSymbolButton5;
        private Sunny.UI.UIGroupBox gpbCamRes;
        private System.Windows.Forms.RadioButton rdbFile;
        private System.Windows.Forms.RadioButton rdbCam;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;
        private Sunny.UI.UIPanel uiPanel2;
        private Sunny.UI.UISymbolLabel uiSymbolLabel3;
        private Sunny.UI.UISymbolLabel uiSymbolLabel2;
        private Sunny.UI.UISymbolLabel uiSymbolLabel1;
        private Sunny.UI.UITextBox txbModleR;
        private Sunny.UI.UITextBox txbModleY;
        private Sunny.UI.UITextBox txbModleX;
        private Sunny.UI.UISymbolButton btnUpCamModle;
        private Sunny.UI.UISymbolButton btnDownCamModle;
        private Sunny.UI.UIComboBox chbUpCamPhotoPos;
    }
}