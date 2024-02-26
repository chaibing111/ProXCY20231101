namespace sunyvpp
{
    partial class PLCTest
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
            this.txbReadValue = new Sunny.UI.UITextBox();
            this.btnWriteValue = new Sunny.UI.UISymbolButton();
            this.btnReadValue = new Sunny.UI.UISymbolButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txbWriteValue = new Sunny.UI.UITextBox();
            this.txbReadAddress = new Sunny.UI.UITextBox();
            this.txbWriteAddress = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // txbReadValue
            // 
            this.txbReadValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbReadValue.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbReadValue.Location = new System.Drawing.Point(842, 140);
            this.txbReadValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbReadValue.MinimumSize = new System.Drawing.Size(1, 16);
            this.txbReadValue.Name = "txbReadValue";
            this.txbReadValue.ShowText = true;
            this.txbReadValue.Size = new System.Drawing.Size(195, 50);
            this.txbReadValue.Style = Sunny.UI.UIStyle.Custom;
            this.txbReadValue.TabIndex = 1;
            this.txbReadValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txbReadValue.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnWriteValue
            // 
            this.btnWriteValue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWriteValue.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWriteValue.Location = new System.Drawing.Point(1120, 506);
            this.btnWriteValue.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnWriteValue.Name = "btnWriteValue";
            this.btnWriteValue.Size = new System.Drawing.Size(145, 47);
            this.btnWriteValue.Style = Sunny.UI.UIStyle.Custom;
            this.btnWriteValue.Symbol = 61447;
            this.btnWriteValue.TabIndex = 33;
            this.btnWriteValue.Text = "写入";
            this.btnWriteValue.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnWriteValue.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnWriteValue.Click += new System.EventHandler(this.btnWriteValue_Click);
            // 
            // btnReadValue
            // 
            this.btnReadValue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReadValue.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReadValue.Location = new System.Drawing.Point(1120, 140);
            this.btnReadValue.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnReadValue.Name = "btnReadValue";
            this.btnReadValue.Size = new System.Drawing.Size(145, 47);
            this.btnReadValue.Style = Sunny.UI.UIStyle.Custom;
            this.btnReadValue.Symbol = 61447;
            this.btnReadValue.TabIndex = 34;
            this.btnReadValue.Text = "读取";
            this.btnReadValue.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReadValue.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnReadValue.Click += new System.EventHandler(this.btnReadValue_Click);
            // 
            // button1
            // 
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(627, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 50);
            this.button1.TabIndex = 35;
            this.button1.Text = "读取值";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(115, 140);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 50);
            this.button2.TabIndex = 35;
            this.button2.Text = "读地址";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(627, 503);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 50);
            this.button3.TabIndex = 35;
            this.button3.Text = "写入值";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(115, 503);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 50);
            this.button4.TabIndex = 35;
            this.button4.Text = "写地址";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // txbWriteValue
            // 
            this.txbWriteValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbWriteValue.DoubleValue = 1D;
            this.txbWriteValue.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbWriteValue.IntValue = 1;
            this.txbWriteValue.Location = new System.Drawing.Point(842, 503);
            this.txbWriteValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbWriteValue.MinimumSize = new System.Drawing.Size(1, 16);
            this.txbWriteValue.Name = "txbWriteValue";
            this.txbWriteValue.ShowText = true;
            this.txbWriteValue.Size = new System.Drawing.Size(195, 50);
            this.txbWriteValue.Style = Sunny.UI.UIStyle.Custom;
            this.txbWriteValue.TabIndex = 1;
            this.txbWriteValue.Text = "1";
            this.txbWriteValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txbWriteValue.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txbReadAddress
            // 
            this.txbReadAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbReadAddress.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbReadAddress.Location = new System.Drawing.Point(330, 140);
            this.txbReadAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbReadAddress.MinimumSize = new System.Drawing.Size(1, 16);
            this.txbReadAddress.Name = "txbReadAddress";
            this.txbReadAddress.ShowText = true;
            this.txbReadAddress.Size = new System.Drawing.Size(195, 50);
            this.txbReadAddress.Style = Sunny.UI.UIStyle.Custom;
            this.txbReadAddress.TabIndex = 1;
            this.txbReadAddress.Text = "D2500";
            this.txbReadAddress.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txbReadAddress.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txbWriteAddress
            // 
            this.txbWriteAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txbWriteAddress.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txbWriteAddress.Location = new System.Drawing.Point(330, 503);
            this.txbWriteAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbWriteAddress.MinimumSize = new System.Drawing.Size(1, 16);
            this.txbWriteAddress.Name = "txbWriteAddress";
            this.txbWriteAddress.ShowText = true;
            this.txbWriteAddress.Size = new System.Drawing.Size(195, 50);
            this.txbWriteAddress.Style = Sunny.UI.UIStyle.Custom;
            this.txbWriteAddress.TabIndex = 1;
            this.txbWriteAddress.Text = "D2500";
            this.txbWriteAddress.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txbWriteAddress.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // PLCTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1280, 742);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnWriteValue);
            this.Controls.Add(this.btnReadValue);
            this.Controls.Add(this.txbWriteAddress);
            this.Controls.Add(this.txbWriteValue);
            this.Controls.Add(this.txbReadAddress);
            this.Controls.Add(this.txbReadValue);
            this.Name = "PLCTest";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "PLCTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 800, 450);
            this.Load += new System.EventHandler(this.PLCTest_Load);
            this.SizeChanged += new System.EventHandler(this.PLCTest_SizeChanged);
            this.ResumeLayout(true);

        }

        #endregion

        private Sunny.UI.UITextBox txbReadValue;
        private Sunny.UI.UISymbolButton btnWriteValue;
        private Sunny.UI.UISymbolButton btnReadValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private Sunny.UI.UITextBox txbWriteValue;
        private Sunny.UI.UITextBox txbReadAddress;
        private Sunny.UI.UITextBox txbWriteAddress;
    }
}