namespace DDR4Check
{
    partial class VisionTemplateForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cogJobManagerEdit1 = new Cognex.VisionPro.QuickBuild.CogJobManagerEdit();
            this.btn_save = new System.Windows.Forms.Button();
            this.bt_SaveAgain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cogJobManagerEdit1
            // 
            this.cogJobManagerEdit1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cogJobManagerEdit1.Location = new System.Drawing.Point(0, 0);
            this.cogJobManagerEdit1.Name = "cogJobManagerEdit1";
            this.cogJobManagerEdit1.ShowLocalizationTab = false;
            this.cogJobManagerEdit1.Size = new System.Drawing.Size(671, 481);
            this.cogJobManagerEdit1.Subject = null;
            this.cogJobManagerEdit1.TabIndex = 0;
            this.cogJobManagerEdit1.Load += new System.EventHandler(this.cogJobManagerEdit1_Load);
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("DFKai-SB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_save.Location = new System.Drawing.Point(32, 365);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(153, 65);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "保  存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // bt_SaveAgain
            // 
            this.bt_SaveAgain.Font = new System.Drawing.Font("DFKai-SB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.bt_SaveAgain.Location = new System.Drawing.Point(214, 365);
            this.bt_SaveAgain.Name = "bt_SaveAgain";
            this.bt_SaveAgain.Size = new System.Drawing.Size(153, 65);
            this.bt_SaveAgain.TabIndex = 2;
            this.bt_SaveAgain.Text = "备 份";
            this.bt_SaveAgain.UseVisualStyleBackColor = true;
            this.bt_SaveAgain.Click += new System.EventHandler(this.bt_SaveAgain_Click);
            // 
            // VisionTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 481);
            this.Controls.Add(this.bt_SaveAgain);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.cogJobManagerEdit1);
            this.Name = "VisionTemplateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视觉模板";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisionTemplateFrm_FormClosing);
            this.Load += new System.EventHandler(this.VisionTemplateFrm_Load);
            this.SizeChanged += new System.EventHandler(this.VisionTemplateForm_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Cognex.VisionPro.QuickBuild.CogJobManagerEdit cogJobManagerEdit1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button bt_SaveAgain;
    }
}