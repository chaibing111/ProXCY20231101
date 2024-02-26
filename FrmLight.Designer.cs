namespace sunyvpp
{
    partial class FrmLight
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.uiPanel3 = new Sunny.UI.UIPanel();
            this.btnOpenLight = new Sunny.UI.UISymbolButton();
            this.btnCloseLight = new Sunny.UI.UISymbolButton();
            this.uiSymbolButton4 = new Sunny.UI.UISymbolButton();
            this.cmbChannelChange = new Sunny.UI.UIComboBox();
            this.uiPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiPanel3
            // 
            this.uiPanel3.Controls.Add(this.btnOpenLight);
            this.uiPanel3.Controls.Add(this.btnCloseLight);
            this.uiPanel3.Controls.Add(this.uiSymbolButton4);
            this.uiPanel3.Controls.Add(this.cmbChannelChange);
            this.uiPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiPanel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPanel3.Location = new System.Drawing.Point(0, 35);
            this.uiPanel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel3.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel3.Name = "uiPanel3";
            this.uiPanel3.Size = new System.Drawing.Size(522, 233);
            this.uiPanel3.TabIndex = 16;
            this.uiPanel3.Text = null;
            this.uiPanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiPanel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnOpenLight
            // 
            this.btnOpenLight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenLight.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenLight.Location = new System.Drawing.Point(66, 148);
            this.btnOpenLight.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOpenLight.Name = "btnOpenLight";
            this.btnOpenLight.Size = new System.Drawing.Size(145, 47);
            this.btnOpenLight.Symbol = 61447;
            this.btnOpenLight.TabIndex = 44;
            this.btnOpenLight.Text = "打开光源";
            this.btnOpenLight.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenLight.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnOpenLight.Click += new System.EventHandler(this.btnOpenLight_Click);
            // 
            // btnCloseLight
            // 
            this.btnCloseLight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseLight.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCloseLight.Location = new System.Drawing.Point(296, 148);
            this.btnCloseLight.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCloseLight.Name = "btnCloseLight";
            this.btnCloseLight.Size = new System.Drawing.Size(145, 47);
            this.btnCloseLight.Symbol = 61447;
            this.btnCloseLight.TabIndex = 45;
            this.btnCloseLight.Text = "关闭光源";
            this.btnCloseLight.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCloseLight.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnCloseLight.Click += new System.EventHandler(this.btnCloseLight_Click);
            // 
            // uiSymbolButton4
            // 
            this.uiSymbolButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSymbolButton4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton4.Location = new System.Drawing.Point(66, 18);
            this.uiSymbolButton4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSymbolButton4.Name = "uiSymbolButton4";
            this.uiSymbolButton4.Size = new System.Drawing.Size(375, 47);
            this.uiSymbolButton4.Symbol = 61447;
            this.uiSymbolButton4.TabIndex = 43;
            this.uiSymbolButton4.Text = "光源通道选择";
            this.uiSymbolButton4.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiSymbolButton4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cmbChannelChange
            // 
            this.cmbChannelChange.DataSource = null;
            this.cmbChannelChange.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cmbChannelChange.FillColor = System.Drawing.Color.White;
            this.cmbChannelChange.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbChannelChange.Items.AddRange(new object[] {
            "通道1",
            "通道2",
            "通道3",
            "通道4"});
            this.cmbChannelChange.Location = new System.Drawing.Point(66, 92);
            this.cmbChannelChange.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbChannelChange.MinimumSize = new System.Drawing.Size(63, 0);
            this.cmbChannelChange.Name = "cmbChannelChange";
            this.cmbChannelChange.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cmbChannelChange.Size = new System.Drawing.Size(375, 35);
            this.cmbChannelChange.TabIndex = 42;
            this.cmbChannelChange.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmbChannelChange.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // FrmLight
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(522, 268);
            this.Controls.Add(this.uiPanel3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLight";
            this.Text = "光源";
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 450);
            this.Load += new System.EventHandler(this.FrmLight_Load);
            this.uiPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel uiPanel3;
        private Sunny.UI.UIComboBox cmbChannelChange;
        private Sunny.UI.UISymbolButton uiSymbolButton4;
        private Sunny.UI.UISymbolButton btnOpenLight;
        private Sunny.UI.UISymbolButton btnCloseLight;
    }
}