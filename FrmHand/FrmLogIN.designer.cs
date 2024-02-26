
namespace sunyvpp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 true。</param>
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmb_UserName = new System.Windows.Forms.ComboBox();
            this.btn_Logout = new System.Windows.Forms.Button();
            this.btn_Login = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.txbPsw = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChangPSW = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // cmb_UserName
            // 
            this.cmb_UserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(45)))), ((int)(((byte)(100)))));
            this.cmb_UserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_UserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_UserName.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.cmb_UserName.ForeColor = System.Drawing.Color.IndianRed;
            this.cmb_UserName.FormattingEnabled = true;
            this.cmb_UserName.Items.AddRange(new object[] {
            "Admin",
            "Engineer",
            "Operator"});
            this.cmb_UserName.Location = new System.Drawing.Point(291, 88);
            this.cmb_UserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_UserName.Name = "cmb_UserName";
            this.cmb_UserName.Size = new System.Drawing.Size(241, 33);
            this.cmb_UserName.TabIndex = 5;
            // 
            // btn_Logout
            // 
            this.btn_Logout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateBlue;
            this.btn_Logout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btn_Logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Logout.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.btn_Logout.Location = new System.Drawing.Point(433, 263);
            this.btn_Logout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Logout.Name = "btn_Logout";
            this.btn_Logout.Size = new System.Drawing.Size(127, 61);
            this.btn_Logout.TabIndex = 7;
            this.btn_Logout.Text = "注销";
            this.btn_Logout.UseVisualStyleBackColor = true;
            this.btn_Logout.Click += new System.EventHandler(this.btn_Logout_Click_1);
            // 
            // btn_Login
            // 
            this.btn_Login.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateBlue;
            this.btn_Login.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.btn_Login.Location = new System.Drawing.Point(120, 263);
            this.btn_Login.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(127, 61);
            this.btn_Login.TabIndex = 6;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.Location = new System.Drawing.Point(498, 463);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(37, 35);
            this.btn_Close.TabIndex = 8;
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Visible = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txbPsw
            // 
            this.txbPsw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(45)))), ((int)(((byte)(100)))));
            this.txbPsw.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.txbPsw.ForeColor = System.Drawing.Color.IndianRed;
            this.txbPsw.Location = new System.Drawing.Point(291, 172);
            this.txbPsw.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txbPsw.Name = "txbPsw";
            this.txbPsw.Size = new System.Drawing.Size(241, 33);
            this.txbPsw.TabIndex = 9;
            this.txbPsw.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(136, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(136, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "密码";
            // 
            // btnChangPSW
            // 
            this.btnChangPSW.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSlateBlue;
            this.btnChangPSW.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnChangPSW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangPSW.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.btnChangPSW.Location = new System.Drawing.Point(485, 363);
            this.btnChangPSW.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChangPSW.Name = "btnChangPSW";
            this.btnChangPSW.Size = new System.Drawing.Size(123, 36);
            this.btnChangPSW.TabIndex = 7;
            this.btnChangPSW.Text = "密码修改";
            this.btnChangPSW.UseVisualStyleBackColor = true;
            this.btnChangPSW.Visible = false;
            this.btnChangPSW.Click += new System.EventHandler(this.btn_Logout_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(84, 404);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(99, 21);
            this.lblInfo.TabIndex = 12;
            this.lblInfo.Text = "已完成：0％";
            this.lblInfo.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(32, 413);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(652, 70);
            this.progressBar1.TabIndex = 11;
            this.progressBar1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(620, 404);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbPsw);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.cmb_UserName);
            this.Controls.Add(this.btnChangPSW);
            this.Controls.Add(this.btn_Logout);
            this.Controls.Add(this.btn_Login);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "登录";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 748, 539);
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_UserName;
        private System.Windows.Forms.Button btn_Logout;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txbPsw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChangPSW;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

