
namespace sunyvpp
{
    partial class ChangePSW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePSW));
            this.txb_psw = new System.Windows.Forms.TextBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.cmb_UserNameCP = new System.Windows.Forms.ComboBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.txb_confirm_psw = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txb_psw
            // 
            this.txb_psw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(45)))), ((int)(((byte)(100)))));
            this.txb_psw.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.txb_psw.ForeColor = System.Drawing.Color.IndianRed;
            this.txb_psw.Location = new System.Drawing.Point(222, 175);
            this.txb_psw.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txb_psw.Name = "txb_psw";
            this.txb_psw.Size = new System.Drawing.Size(241, 33);
            this.txb_psw.TabIndex = 13;
            this.txb_psw.UseSystemPasswordChar = true;
            // 
            // btn_Close
            // 
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.Location = new System.Drawing.Point(139, 538);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(37, 35);
            this.btn_Close.TabIndex = 12;
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // cmb_UserNameCP
            // 
            this.cmb_UserNameCP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(45)))), ((int)(((byte)(100)))));
            this.cmb_UserNameCP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_UserNameCP.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.cmb_UserNameCP.ForeColor = System.Drawing.Color.IndianRed;
            this.cmb_UserNameCP.FormattingEnabled = true;
            this.cmb_UserNameCP.Items.AddRange(new object[] {
            "Admin",
            "Engineer",
            "Operator"});
            this.cmb_UserNameCP.Location = new System.Drawing.Point(222, 91);
            this.cmb_UserNameCP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_UserNameCP.Name = "cmb_UserNameCP";
            this.cmb_UserNameCP.Size = new System.Drawing.Size(241, 33);
            this.cmb_UserNameCP.TabIndex = 10;
            // 
            // btn_Login
            // 
            this.btn_Login.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateBlue;
            this.btn_Login.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.btn_Login.Location = new System.Drawing.Point(222, 359);
            this.btn_Login.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(153, 61);
            this.btn_Login.TabIndex = 11;
            this.btn_Login.Text = "确认修改";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // txb_confirm_psw
            // 
            this.txb_confirm_psw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(45)))), ((int)(((byte)(100)))));
            this.txb_confirm_psw.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.txb_confirm_psw.ForeColor = System.Drawing.Color.IndianRed;
            this.txb_confirm_psw.Location = new System.Drawing.Point(222, 256);
            this.txb_confirm_psw.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txb_confirm_psw.Name = "txb_confirm_psw";
            this.txb_confirm_psw.Size = new System.Drawing.Size(241, 33);
            this.txb_confirm_psw.TabIndex = 13;
            this.txb_confirm_psw.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(61, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 25);
            this.label2.TabIndex = 14;
            this.label2.Text = "密      码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(61, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "用   户  名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(61, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "确认密码";
            // 
            // ChangePSW
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(549, 505);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_confirm_psw);
            this.Controls.Add(this.txb_psw);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.cmb_UserNameCP);
            this.Controls.Add(this.btn_Login);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChangePSW";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "密码修改";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 551, 563);
            this.Load += new System.EventHandler(this.ChangePSW_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_psw;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.ComboBox cmb_UserNameCP;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.TextBox txb_confirm_psw;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}