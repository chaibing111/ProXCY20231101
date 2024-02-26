using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sunyvpp
{
    public partial class XTipDlg : Form
    {
        public string output;
        public string m_Tip;
        public XTipDlg(string str,bool isCancell=true)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.m_Tip = str;
            this.Text = m_Tip;
            textBox1.Text = m_Tip;
            textBox1.SelectAll();
            textBox1.Enabled = true;
            //textBox1.BackColor
            this.AcceptButton = button1;
            this.output = "";
            if (isCancell==true)
            {
                this.button2.Visible = true;
            }
            else
            {
                this.button2.Visible = true;

            }
            
        }

        private void XTipDlg_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.output = textBox1.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            if (output == m_Tip)
            {
                this.Close();
                return;
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
