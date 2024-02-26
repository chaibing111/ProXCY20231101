namespace sunyvpp
{
    partial class FrmProOpen
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
            this.cmbPro = new Sunny.UI.UIComboBox();
            this.uiSymbolButton1 = new Sunny.UI.UISymbolButton();
            this.btnOpenPro = new Sunny.UI.UISymbolButton();
            this.SuspendLayout();
            // 
            // cmbPro
            // 
            this.cmbPro.DataSource = null;
            this.cmbPro.FillColor = System.Drawing.Color.White;
            this.cmbPro.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPro.Location = new System.Drawing.Point(245, 103);
            this.cmbPro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbPro.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbPro.Name = "cmbPro";
            this.cmbPro.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbPro.Size = new System.Drawing.Size(355, 32);
            this.cmbPro.TabIndex = 0;
            this.cmbPro.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbPro.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSymbolButton1
            // 
            this.uiSymbolButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.Location = new System.Drawing.Point(73, 103);
            this.uiSymbolButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton1.Name = "uiSymbolButton1";
            this.uiSymbolButton1.Size = new System.Drawing.Size(109, 32);
            this.uiSymbolButton1.Symbol = 61447;
            this.uiSymbolButton1.TabIndex = 7;
            this.uiSymbolButton1.Text = "项目名称：";
            this.uiSymbolButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnOpenPro
            // 
            this.btnOpenPro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenPro.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenPro.Location = new System.Drawing.Point(557, 240);
            this.btnOpenPro.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOpenPro.Name = "btnOpenPro";
            this.btnOpenPro.Size = new System.Drawing.Size(109, 32);
            this.btnOpenPro.Symbol = 61447;
            this.btnOpenPro.TabIndex = 8;
            this.btnOpenPro.Text = "确定";
            this.btnOpenPro.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenPro.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnOpenPro.Click += new System.EventHandler(this.btnOpenPro_Click);
            // 
            // FrmProOpen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenPro);
            this.Controls.Add(this.uiSymbolButton1);
            this.Controls.Add(this.cmbPro);
            this.Name = "FrmProOpen";
            this.Text = "FrmProOpen";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.Load += new System.EventHandler(this.FrmProOpen_Load);
            this.ResumeLayout(true);

        }

        #endregion

        private Sunny.UI.UIComboBox cmbPro;
        private Sunny.UI.UISymbolButton uiSymbolButton1;
        private Sunny.UI.UISymbolButton btnOpenPro;
    }
}