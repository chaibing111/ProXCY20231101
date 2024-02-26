
namespace sunyvpp
{
    partial class Form_Weld
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Weld));
            this.label_Software = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.textBox_title = new System.Windows.Forms.TextBox();
            this.textBox_Software = new System.Windows.Forms.TextBox();
            this.label_EquipmentCode = new System.Windows.Forms.Label();
            this.textBox_EquipmentCode = new System.Windows.Forms.TextBox();
            this.label_SubEquipmentCode = new System.Windows.Forms.Label();
            this.textBox_SubEquipmentCode = new System.Windows.Forms.TextBox();
            this.label_WorkStationCode = new System.Windows.Forms.Label();
            this.textBox_WorkStationCode = new System.Windows.Forms.TextBox();
            this.label_parmCode1 = new System.Windows.Forms.Label();
            this.textBox_parmCode1 = new System.Windows.Forms.TextBox();
            this.label_batBatch = new System.Windows.Forms.Label();
            this.textBox_batBatch = new System.Windows.Forms.TextBox();
            this.groupBox_parm = new System.Windows.Forms.GroupBox();
            this.textBox_batBatchQty = new System.Windows.Forms.TextBox();
            this.button_test = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.label_batBatchQty = new System.Windows.Forms.Label();
            this.groupBox_weldFile = new System.Windows.Forms.GroupBox();
            this.button_docClear = new System.Windows.Forms.Button();
            this.button_weldDoc = new System.Windows.Forms.Button();
            this.listBox_weldFiles = new System.Windows.Forms.ListBox();
            this.groupBox_weldParm = new System.Windows.Forms.GroupBox();
            this.textBox_cfg2 = new System.Windows.Forms.TextBox();
            this.textBox_cfg1 = new System.Windows.Forms.TextBox();
            this.groupBox_weldImage = new System.Windows.Forms.GroupBox();
            this.pictureBox_weld = new System.Windows.Forms.PictureBox();
            this.cogDisplay1 = new Cognex.VisionPro.Display.CogDisplay();
            this.groupBox_parm.SuspendLayout();
            this.groupBox_weldFile.SuspendLayout();
            this.groupBox_weldParm.SuspendLayout();
            this.groupBox_weldImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_weld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogDisplay1)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Software
            // 
            this.label_Software.AutoSize = true;
            this.label_Software.Location = new System.Drawing.Point(8, 37);
            this.label_Software.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_Software.Name = "label_Software";
            this.label_Software.Size = new System.Drawing.Size(158, 29);
            this.label_Software.TabIndex = 0;
            this.label_Software.Text = "软件名称：";
            // 
            // label_title
            // 
            this.label_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_title.BackColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(2, 2);
            this.label_title.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(1304, 29);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "BusBar焊接工站";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_title.DoubleClick += new System.EventHandler(this.label_title_DoubleClick);
            // 
            // textBox_title
            // 
            this.textBox_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_title.Location = new System.Drawing.Point(4, 2);
            this.textBox_title.Name = "textBox_title";
            this.textBox_title.Size = new System.Drawing.Size(1300, 40);
            this.textBox_title.TabIndex = 1;
            this.textBox_title.Text = "BusBar焊接工站";
            this.textBox_title.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_title.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_title_KeyPress);
            // 
            // textBox_Software
            // 
            this.textBox_Software.Location = new System.Drawing.Point(10, 59);
            this.textBox_Software.Name = "textBox_Software";
            this.textBox_Software.Size = new System.Drawing.Size(171, 40);
            this.textBox_Software.TabIndex = 2;
            // 
            // label_EquipmentCode
            // 
            this.label_EquipmentCode.AutoSize = true;
            this.label_EquipmentCode.Location = new System.Drawing.Point(189, 37);
            this.label_EquipmentCode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_EquipmentCode.Name = "label_EquipmentCode";
            this.label_EquipmentCode.Size = new System.Drawing.Size(158, 29);
            this.label_EquipmentCode.TabIndex = 0;
            this.label_EquipmentCode.Text = "设备编号：";
            // 
            // textBox_EquipmentCode
            // 
            this.textBox_EquipmentCode.Location = new System.Drawing.Point(187, 59);
            this.textBox_EquipmentCode.Name = "textBox_EquipmentCode";
            this.textBox_EquipmentCode.Size = new System.Drawing.Size(174, 40);
            this.textBox_EquipmentCode.TabIndex = 2;
            // 
            // label_SubEquipmentCode
            // 
            this.label_SubEquipmentCode.AutoSize = true;
            this.label_SubEquipmentCode.Location = new System.Drawing.Point(367, 37);
            this.label_SubEquipmentCode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_SubEquipmentCode.Name = "label_SubEquipmentCode";
            this.label_SubEquipmentCode.Size = new System.Drawing.Size(187, 29);
            this.label_SubEquipmentCode.TabIndex = 0;
            this.label_SubEquipmentCode.Text = "子设备编号：";
            // 
            // textBox_SubEquipmentCode
            // 
            this.textBox_SubEquipmentCode.Location = new System.Drawing.Point(367, 59);
            this.textBox_SubEquipmentCode.Name = "textBox_SubEquipmentCode";
            this.textBox_SubEquipmentCode.Size = new System.Drawing.Size(123, 40);
            this.textBox_SubEquipmentCode.TabIndex = 2;
            // 
            // label_WorkStationCode
            // 
            this.label_WorkStationCode.AutoSize = true;
            this.label_WorkStationCode.Location = new System.Drawing.Point(500, 37);
            this.label_WorkStationCode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_WorkStationCode.Name = "label_WorkStationCode";
            this.label_WorkStationCode.Size = new System.Drawing.Size(158, 29);
            this.label_WorkStationCode.TabIndex = 0;
            this.label_WorkStationCode.Text = "站点编号：";
            // 
            // textBox_WorkStationCode
            // 
            this.textBox_WorkStationCode.Location = new System.Drawing.Point(496, 59);
            this.textBox_WorkStationCode.Name = "textBox_WorkStationCode";
            this.textBox_WorkStationCode.Size = new System.Drawing.Size(108, 40);
            this.textBox_WorkStationCode.TabIndex = 2;
            // 
            // label_parmCode1
            // 
            this.label_parmCode1.AutoSize = true;
            this.label_parmCode1.Location = new System.Drawing.Point(612, 37);
            this.label_parmCode1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_parmCode1.Name = "label_parmCode1";
            this.label_parmCode1.Size = new System.Drawing.Size(158, 29);
            this.label_parmCode1.TabIndex = 0;
            this.label_parmCode1.Text = "参数编号：";
            // 
            // textBox_parmCode1
            // 
            this.textBox_parmCode1.Location = new System.Drawing.Point(612, 59);
            this.textBox_parmCode1.Name = "textBox_parmCode1";
            this.textBox_parmCode1.Size = new System.Drawing.Size(217, 40);
            this.textBox_parmCode1.TabIndex = 2;
            // 
            // label_batBatch
            // 
            this.label_batBatch.AutoSize = true;
            this.label_batBatch.Location = new System.Drawing.Point(8, 105);
            this.label_batBatch.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_batBatch.Name = "label_batBatch";
            this.label_batBatch.Size = new System.Drawing.Size(129, 29);
            this.label_batBatch.TabIndex = 0;
            this.label_batBatch.Text = "工单号：";
            // 
            // textBox_batBatch
            // 
            this.textBox_batBatch.Location = new System.Drawing.Point(101, 102);
            this.textBox_batBatch.Name = "textBox_batBatch";
            this.textBox_batBatch.Size = new System.Drawing.Size(260, 40);
            this.textBox_batBatch.TabIndex = 2;
            // 
            // groupBox_parm
            // 
            this.groupBox_parm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_parm.Controls.Add(this.textBox_batBatchQty);
            this.groupBox_parm.Controls.Add(this.button_test);
            this.groupBox_parm.Controls.Add(this.button_save);
            this.groupBox_parm.Controls.Add(this.label_EquipmentCode);
            this.groupBox_parm.Controls.Add(this.textBox_batBatch);
            this.groupBox_parm.Controls.Add(this.label_Software);
            this.groupBox_parm.Controls.Add(this.textBox_parmCode1);
            this.groupBox_parm.Controls.Add(this.label_SubEquipmentCode);
            this.groupBox_parm.Controls.Add(this.textBox_WorkStationCode);
            this.groupBox_parm.Controls.Add(this.label_batBatchQty);
            this.groupBox_parm.Controls.Add(this.textBox_Software);
            this.groupBox_parm.Controls.Add(this.label_batBatch);
            this.groupBox_parm.Controls.Add(this.label_WorkStationCode);
            this.groupBox_parm.Controls.Add(this.textBox_SubEquipmentCode);
            this.groupBox_parm.Controls.Add(this.textBox_EquipmentCode);
            this.groupBox_parm.Controls.Add(this.label_parmCode1);
            this.groupBox_parm.Location = new System.Drawing.Point(6, 37);
            this.groupBox_parm.Name = "groupBox_parm";
            this.groupBox_parm.Size = new System.Drawing.Size(1293, 141);
            this.groupBox_parm.TabIndex = 3;
            this.groupBox_parm.TabStop = true;
            this.groupBox_parm.Text = "软件参数";
            // 
            // textBox_batBatchQty
            // 
            this.textBox_batBatchQty.Location = new System.Drawing.Point(468, 102);
            this.textBox_batBatchQty.Name = "textBox_batBatchQty";
            this.textBox_batBatchQty.Size = new System.Drawing.Size(136, 40);
            this.textBox_batBatchQty.TabIndex = 2;
            // 
            // button_test
            // 
            this.button_test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_test.Location = new System.Drawing.Point(960, 52);
            this.button_test.Name = "button_test";
            this.button_test.Size = new System.Drawing.Size(144, 36);
            this.button_test.TabIndex = 3;
            this.button_test.Text = "Form_MES";
            this.button_test.UseVisualStyleBackColor = true;
            this.button_test.Click += new System.EventHandler(this.button_test_Click);
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save.Location = new System.Drawing.Point(1139, 52);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(144, 36);
            this.button_save.TabIndex = 3;
            this.button_save.Text = "保  存";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // label_batBatchQty
            // 
            this.label_batBatchQty.AutoSize = true;
            this.label_batBatchQty.Location = new System.Drawing.Point(372, 108);
            this.label_batBatchQty.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_batBatchQty.Name = "label_batBatchQty";
            this.label_batBatchQty.Size = new System.Drawing.Size(158, 29);
            this.label_batBatchQty.TabIndex = 0;
            this.label_batBatchQty.Text = "工单总数：";
            // 
            // groupBox_weldFile
            // 
            this.groupBox_weldFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_weldFile.Controls.Add(this.button_docClear);
            this.groupBox_weldFile.Controls.Add(this.button_weldDoc);
            this.groupBox_weldFile.Controls.Add(this.listBox_weldFiles);
            this.groupBox_weldFile.Location = new System.Drawing.Point(6, 184);
            this.groupBox_weldFile.Name = "groupBox_weldFile";
            this.groupBox_weldFile.Size = new System.Drawing.Size(1293, 196);
            this.groupBox_weldFile.TabIndex = 3;
            this.groupBox_weldFile.TabStop = true;
            this.groupBox_weldFile.Text = "焊接文档";
            // 
            // button_docClear
            // 
            this.button_docClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_docClear.Location = new System.Drawing.Point(979, 13);
            this.button_docClear.Name = "button_docClear";
            this.button_docClear.Size = new System.Drawing.Size(144, 36);
            this.button_docClear.TabIndex = 3;
            this.button_docClear.Text = "清空文档列表";
            this.button_docClear.UseVisualStyleBackColor = true;
            this.button_docClear.Click += new System.EventHandler(this.button_docClear_Click);
            // 
            // button_weldDoc
            // 
            this.button_weldDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_weldDoc.Location = new System.Drawing.Point(1139, 13);
            this.button_weldDoc.Name = "button_weldDoc";
            this.button_weldDoc.Size = new System.Drawing.Size(144, 36);
            this.button_weldDoc.TabIndex = 3;
            this.button_weldDoc.Text = "焊接文档";
            this.button_weldDoc.UseVisualStyleBackColor = true;
            this.button_weldDoc.Click += new System.EventHandler(this.button_weldDoc_Click);
            // 
            // listBox_weldFiles
            // 
            this.listBox_weldFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_weldFiles.FormattingEnabled = true;
            this.listBox_weldFiles.ItemHeight = 29;
            this.listBox_weldFiles.Location = new System.Drawing.Point(2, 50);
            this.listBox_weldFiles.Name = "listBox_weldFiles";
            this.listBox_weldFiles.Size = new System.Drawing.Size(1287, 120);
            this.listBox_weldFiles.TabIndex = 0;
            this.listBox_weldFiles.SelectedIndexChanged += new System.EventHandler(this.listBox_weldFiles_SelectedIndexChanged);
            this.listBox_weldFiles.DoubleClick += new System.EventHandler(this.listBox_weldFiles_DoubleClick);
            // 
            // groupBox_weldParm
            // 
            this.groupBox_weldParm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox_weldParm.Controls.Add(this.textBox_cfg2);
            this.groupBox_weldParm.Controls.Add(this.textBox_cfg1);
            this.groupBox_weldParm.Location = new System.Drawing.Point(6, 386);
            this.groupBox_weldParm.Name = "groupBox_weldParm";
            this.groupBox_weldParm.Size = new System.Drawing.Size(814, 416);
            this.groupBox_weldParm.TabIndex = 3;
            this.groupBox_weldParm.TabStop = true;
            this.groupBox_weldParm.Text = "关键参数";
            // 
            // textBox_cfg2
            // 
            this.textBox_cfg2.BackColor = System.Drawing.Color.White;
            this.textBox_cfg2.Location = new System.Drawing.Point(412, 28);
            this.textBox_cfg2.Multiline = true;
            this.textBox_cfg2.Name = "textBox_cfg2";
            this.textBox_cfg2.ReadOnly = true;
            this.textBox_cfg2.Size = new System.Drawing.Size(396, 382);
            this.textBox_cfg2.TabIndex = 2;
            // 
            // textBox_cfg1
            // 
            this.textBox_cfg1.BackColor = System.Drawing.Color.White;
            this.textBox_cfg1.Location = new System.Drawing.Point(10, 28);
            this.textBox_cfg1.Multiline = true;
            this.textBox_cfg1.Name = "textBox_cfg1";
            this.textBox_cfg1.ReadOnly = true;
            this.textBox_cfg1.Size = new System.Drawing.Size(396, 382);
            this.textBox_cfg1.TabIndex = 2;
            // 
            // groupBox_weldImage
            // 
            this.groupBox_weldImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_weldImage.Controls.Add(this.cogDisplay1);
            this.groupBox_weldImage.Controls.Add(this.pictureBox_weld);
            this.groupBox_weldImage.Location = new System.Drawing.Point(826, 386);
            this.groupBox_weldImage.Name = "groupBox_weldImage";
            this.groupBox_weldImage.Size = new System.Drawing.Size(469, 416);
            this.groupBox_weldImage.TabIndex = 4;
            this.groupBox_weldImage.TabStop = true;
            this.groupBox_weldImage.Text = "轨迹图像";
            // 
            // pictureBox_weld
            // 
            this.pictureBox_weld.Location = new System.Drawing.Point(6, 28);
            this.pictureBox_weld.Name = "pictureBox_weld";
            this.pictureBox_weld.Size = new System.Drawing.Size(457, 382);
            this.pictureBox_weld.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_weld.TabIndex = 0;
            this.pictureBox_weld.TabStop = true;
            // 
            // cogDisplay1
            // 
            this.cogDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogDisplay1.DoubleTapZoomCycleLength = 2;
            this.cogDisplay1.DoubleTapZoomSensitivity = 2.5D;
            this.cogDisplay1.Location = new System.Drawing.Point(6, 28);
            this.cogDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogDisplay1.MouseWheelSensitivity = 1D;
            this.cogDisplay1.Name = "cogDisplay1";
            this.cogDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogDisplay1.OcxState")));
            this.cogDisplay1.Size = new System.Drawing.Size(457, 382);
            this.cogDisplay1.TabIndex = 1;
            // 
            // Form_Weld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 814);
            this.Controls.Add(this.groupBox_weldImage);
            this.Controls.Add(this.groupBox_weldParm);
            this.Controls.Add(this.groupBox_weldFile);
            this.Controls.Add(this.groupBox_parm);
            this.Controls.Add(this.textBox_title);
            this.Controls.Add(this.label_title);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form_Weld";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "巴思量焊接数据采集";
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.groupBox_parm.ResumeLayout(true);
            this.groupBox_parm.PerformLayout();
            this.groupBox_weldFile.ResumeLayout(true);
            this.groupBox_weldParm.ResumeLayout(true);
            this.groupBox_weldParm.PerformLayout();
            this.groupBox_weldImage.ResumeLayout(true);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_weld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogDisplay1)).EndInit();
            this.ResumeLayout(true);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Software;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.TextBox textBox_title;
        private System.Windows.Forms.TextBox textBox_Software;
        private System.Windows.Forms.Label label_EquipmentCode;
        private System.Windows.Forms.TextBox textBox_EquipmentCode;
        private System.Windows.Forms.Label label_SubEquipmentCode;
        private System.Windows.Forms.TextBox textBox_SubEquipmentCode;
        private System.Windows.Forms.Label label_WorkStationCode;
        private System.Windows.Forms.TextBox textBox_WorkStationCode;
        private System.Windows.Forms.Label label_parmCode1;
        private System.Windows.Forms.TextBox textBox_parmCode1;
        private System.Windows.Forms.Label label_batBatch;
        private System.Windows.Forms.TextBox textBox_batBatch;
        private System.Windows.Forms.GroupBox groupBox_parm;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.GroupBox groupBox_weldFile;
        private System.Windows.Forms.Button button_weldDoc;
        private System.Windows.Forms.ListBox listBox_weldFiles;
        private System.Windows.Forms.GroupBox groupBox_weldParm;
        private System.Windows.Forms.TextBox textBox_cfg2;
        private System.Windows.Forms.TextBox textBox_cfg1;
        private System.Windows.Forms.GroupBox groupBox_weldImage;
        private System.Windows.Forms.PictureBox pictureBox_weld;
        private System.Windows.Forms.Button button_docClear;
        private System.Windows.Forms.Button button_test;
        private System.Windows.Forms.TextBox textBox_batBatchQty;
        private System.Windows.Forms.Label label_batBatchQty;
        private Cognex.VisionPro.Display.CogDisplay cogDisplay1;
    }
}