
namespace sunyvpp
{
    partial class ToolBlockEdit
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
            this.btn_SaveAndClose = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_SaveAndClose
            // 
            this.btn_SaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SaveAndClose.Location = new System.Drawing.Point(712, 4);
            this.btn_SaveAndClose.Name = "btn_SaveAndClose";
            this.btn_SaveAndClose.Size = new System.Drawing.Size(100, 23);
            this.btn_SaveAndClose.TabIndex = 5;
            this.btn_SaveAndClose.Text = "保存并关闭";
            this.btn_SaveAndClose.UseVisualStyleBackColor = true;
            this.btn_SaveAndClose.Click += new System.EventHandler(this.btn_SaveAndClose_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.Location = new System.Drawing.Point(609, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(100, 23);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            this.cogToolBlockEditV21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogToolBlockEditV21.Location = new System.Drawing.Point(0, 0);
            this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.Size = new System.Drawing.Size(800, 450);
            this.cogToolBlockEditV21.SuspendElectricRuns = true;
            this.cogToolBlockEditV21.TabIndex = 3;
            // 
            // ToolBlockEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_SaveAndClose);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.cogToolBlockEditV21);
            this.Name = "ToolBlockEdit";
            this.Text = "ToolBlockEdit";
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            this.ResumeLayout(true);

        }

        #endregion

        private System.Windows.Forms.Button btn_SaveAndClose;
        private System.Windows.Forms.Button btn_Save;
        public Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
    }
}