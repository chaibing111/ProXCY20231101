namespace sunyvpp
{
    partial class FrmProCreate
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
            this.btnCreatePro = new Sunny.UI.UISymbolButton();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.txtFileName = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // btnCreatePro
            // 
            this.btnCreatePro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreatePro.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreatePro.Location = new System.Drawing.Point(579, 213);
            this.btnCreatePro.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCreatePro.Name = "btnCreatePro";
            this.btnCreatePro.Size = new System.Drawing.Size(109, 32);
            this.btnCreatePro.Symbol = 61447;
            this.btnCreatePro.TabIndex = 7;
            this.btnCreatePro.Text = "确定";
            this.btnCreatePro.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreatePro.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnCreatePro.Click += new System.EventHandler(this.btnCreatePro_Click);
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.Location = new System.Drawing.Point(73, 122);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Size = new System.Drawing.Size(109, 32);
            this.uiSymbolButton1.Symbol = 61447;
            this.uiSymbolButton1.TabIndex = 6;
            this.uiSymbolButton1.Text = "项目名称：";
            this.uiSymbolButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtFileName
            // 
            this.txtFileName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFileName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFileName.Location = new System.Drawing.Point(318, 122);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFileName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ShowText = true;
            this.txtFileName.Size = new System.Drawing.Size(230, 32);
            this.txtFileName.TabIndex = 5;
            this.txtFileName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtFileName.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // FrmProCreate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCreatePro);
            this.Controls.Add(this.uiSymbolButton1);
            this.Controls.Add(this.txtFileName);
            this.Name = "FrmProCreate";
            this.Text = "新建项目";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.ResumeLayout(true);

        }

        #endregion

        private Sunny.UI.UISymbolButton btnCreatePro;
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UITextBox txtFileName;
    }
}