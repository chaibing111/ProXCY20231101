namespace sunyvpp
{
    partial class FrmBar
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
            this.label_process = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.loadingtext = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_process
            // 
            this.label_process.AutoSize = true;
            this.label_process.BackColor = System.Drawing.Color.Transparent;
            this.label_process.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_process.ForeColor = System.Drawing.Color.Blue;
            this.label_process.Location = new System.Drawing.Point(278, 636);
            this.label_process.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_process.Name = "label_process";
            this.label_process.Size = new System.Drawing.Size(162, 44);
            this.label_process.TabIndex = 11;
            this.label_process.Text = "Loading";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 917);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1280, 67);
            this.progressBar1.TabIndex = 10;
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.Red;
            this.uiLabel1.Location = new System.Drawing.Point(519, 115);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(467, 173);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 13;
            this.uiLabel1.Text = "HERO-LASER";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // loadingtext
            // 
            this.loadingtext.AutoSize = true;
            this.loadingtext.BackColor = System.Drawing.Color.Transparent;
            this.loadingtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingtext.ForeColor = System.Drawing.Color.Blue;
            this.loadingtext.Location = new System.Drawing.Point(756, 636);
            this.loadingtext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.loadingtext.Name = "loadingtext";
            this.loadingtext.Size = new System.Drawing.Size(0, 44);
            this.loadingtext.TabIndex = 14;
            // 
            // FrmBar
            // 
            this.AllowShowTitle = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1280, 984);
            this.Controls.Add(this.loadingtext);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.label_process);
            this.Controls.Add(this.progressBar1);
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "FrmBar";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowTitle = true;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "FrmBar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 800, 450);
            this.Load += new System.EventHandler(this.FrmBar_Load);
            this.ResumeLayout(true);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_process;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.Label loadingtext;
    }
}