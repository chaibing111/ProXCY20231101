namespace sunyvpp
{
    partial class FrmProjSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProjSelect));
            this.cmbChangeProj = new Sunny.UI.UIComboBox();
            this.btnChangeProj = new Sunny.UI.UIButton();
            this.cmbCam = new Sunny.UI.UIComboBox();
            this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbExposeSelect = new Sunny.UI.UIComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbChangeProj
            // 
            this.cmbChangeProj.DataSource = null;
            this.cmbChangeProj.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbChangeProj.FillColor = System.Drawing.Color.White;
            this.cmbChangeProj.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbChangeProj.Location = new System.Drawing.Point(627, 93);
            this.cmbChangeProj.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbChangeProj.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbChangeProj.Name = "cmbChangeProj";
            this.cmbChangeProj.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbChangeProj.Size = new System.Drawing.Size(170, 42);
            this.cmbChangeProj.TabIndex = 0;
            this.cmbChangeProj.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbChangeProj.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnChangeProj
            // 
            this.btnChangeProj.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangeProj.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChangeProj.Location = new System.Drawing.Point(627, 143);
            this.btnChangeProj.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnChangeProj.Name = "btnChangeProj";
            this.btnChangeProj.Size = new System.Drawing.Size(170, 45);
            this.btnChangeProj.TabIndex = 1;
            this.btnChangeProj.Text = "切换运行工程";
            this.btnChangeProj.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChangeProj.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnChangeProj.Click += new System.EventHandler(this.btnChangeProj_Click);
            // 
            // cmbCam
            // 
            this.cmbCam.DataSource = null;
            this.cmbCam.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbCam.FillColor = System.Drawing.Color.White;
            this.cmbCam.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCam.Items.AddRange(new object[] {
            "上相机",
            "下相机"});
            this.cmbCam.Location = new System.Drawing.Point(4, 5);
            this.cmbCam.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbCam.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbCam.Name = "cmbCam";
            this.cmbCam.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbCam.Size = new System.Drawing.Size(610, 42);
            this.cmbCam.TabIndex = 1;
            this.cmbCam.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbCam.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
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
            this.cogRecordDisplay1.Location = new System.Drawing.Point(3, 55);
            this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay1.Name = "cogRecordDisplay1";
            this.cogRecordDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay1.OcxState")));
            this.cogRecordDisplay1.Size = new System.Drawing.Size(612, 448);
            this.cogRecordDisplay1.TabIndex = 36;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.cogRecordDisplay1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbCam, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.47431F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.52569F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(618, 506);
            this.tableLayoutPanel1.TabIndex = 37;
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
            this.cmbExposeSelect.Location = new System.Drawing.Point(628, 43);
            this.cmbExposeSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbExposeSelect.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbExposeSelect.Name = "cmbExposeSelect";
            this.cmbExposeSelect.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbExposeSelect.Size = new System.Drawing.Size(170, 42);
            this.cmbExposeSelect.TabIndex = 1;
            this.cmbExposeSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbExposeSelect.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // FrmProjSelect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(852, 547);
            this.Controls.Add(this.cmbExposeSelect);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnChangeProj);
            this.Controls.Add(this.cmbChangeProj);
            this.MaximizeBox = true;
            this.Name = "FrmProjSelect";
            this.Text = "视觉工程调试";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 800, 450);
            this.Load += new System.EventHandler(this.FrmProjSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(true);
            this.ResumeLayout(true);

        }

        #endregion

        private Sunny.UI.UIComboBox cmbChangeProj;
        private Sunny.UI.UIButton btnChangeProj;
        private Sunny.UI.UIComboBox cmbCam;
        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sunny.UI.UIComboBox cmbExposeSelect;
    }
}