﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HslCommunication.Profinet;
using System.Threading;
using HslCommunication;
using HslCommunication.Profinet.Omron;
using Sunny.UI;

namespace sunyvpp
{
    public partial class FormOmron : UIForm
    {

        public static FormOmron frmOmron = null;
        public static FormOmron GetInstance()
        {
            if (frmOmron == null || (frmOmron != null && frmOmron.IsDisposed))
            {
                frmOmron = new FormOmron();
            }
            return frmOmron;
        }
        public FormOmron( )
        {
            InitializeComponent( );
            //Globals.omronFinsUdp = new Globals.omronFinsUdp( );
            //Globals.omronFinsUdp.ConnectTimeOut = 2000;
            // Globals.omronFinsUdp.LogNet = new HslCommunication.LogNet.LogNetSingle( "omron.log.txt" );
        }
        

        //private OmronFinsUdp Globals.omronFinsUdp = null;

        private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            try
            {
                System.Diagnostics.Process.Start( linkLabel1.Text );
            }
            catch (Exception ex) 
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void FormSiemens_Load( object sender, EventArgs e )
        {
            //panel2.Enabled = true;
            //userCurve1.SetLeftCurve( "A", new float[0], Color.Tomato );

            //Language( Program.Language );

            //if (!Program.ShowAuthorInfomation)
            //{
            //    label2.Visible = true;
            //    linkLabel1.Visible = true;
            //    label20.Visible = true;
            //}
        }


        private void Language( int language )
        {
            if (language == 2)
            {
                Text = "Omron Read PLC Demo";
                label2.Text = "Blogs:";
                label4.Text = "Protocols:";
                label20.Text = "Author:Richard Hu";
                label5.Text = "Fins Tcp";
                label24.Text = "Unit Num";
                label25.Text = "Net Num";
                label23.Text = "PC Net Num";

                label1.Text = "Ip:";
                label3.Text = "Port:";
                button1.Text = "Connect";
                button2.Text = "Disconnect";
                label21.Text = "Address:";
                label6.Text = "address:";
                label7.Text = "result:";

                button_read_bool.Text = "Read Bit";
                button_read_short.Text = "r-short";
                button_read_ushort.Text = "r-ushort";
                button_read_int.Text = "r-int";
                button_read_uint.Text = "r-uint";
                button_read_long.Text = "r-long";
                button_read_ulong.Text = "r-ulong";
                button_read_float.Text = "r-float";
                button_read_double.Text = "r-double";
                button_read_string.Text = "r-string";
                label8.Text = "length:";
                label11.Text = "Address:";
                label12.Text = "length:";
                button25.Text = "Bulk Read";
                label13.Text = "Results:";
                label16.Text = "Message:";
                label14.Text = "Results:";
                button26.Text = "Read";

                label10.Text = "Address:";
                label9.Text = "Value:";
                label19.Text = "Note: The value of the string needs to be converted";
                button24.Text = "Write Bit";
                button22.Text = "w-short";
                button21.Text = "w-ushort";
                button20.Text = "w-int";
                button19.Text = "w-uint";
                button18.Text = "w-long";
                button17.Text = "w-ulong";
                button16.Text = "w-float";
                button15.Text = "w-double";
                button14.Text = "w-string";

                groupBox1.Text = "Single Data Read test";
                groupBox2.Text = "Single Data Write test";
                groupBox3.Text = "Bulk Read test";
                groupBox4.Text = "Message reading test, hex string needs to be filled in";
                groupBox5.Text = "Timed reading, curve display";
                
                label15.Text = "Address:";
                label18.Text = "Interval";
                button27.Text = "Start";
                label17.Text = "This assumes that the type of data is determined for short:";
            }
        }

        private void FormSiemens_FormClosing( object sender, FormClosingEventArgs e )
        {
            isThreadRun = true;
        }

        /// <summary>
        /// 统一的读取结果的数据解析，显示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="address"></param>
        /// <param name="textBox"></param>
        private void readResultRender<T>( OperateResult<T> result, string address, TextBox textBox )
        {
            if (result.IsSuccess)
            {
                textBox.AppendText( DateTime.Now.ToString( "[HH:mm:ss] " ) + $"[{address}] {result.Content}{Environment.NewLine}" );
            }
            else
            {
                MessageBox.Show( DateTime.Now.ToString( "[HH:mm:ss] " ) + $"[{address}] 读取失败{Environment.NewLine}原因：{result.ToMessageShowString( )}" );
            }
        }

        /// <summary>
        /// 统一的数据写入的结果显示
        /// </summary>
        /// <param name="result"></param>
        /// <param name="address"></param>
        private void writeResultRender( OperateResult result, string address )
        {
            if (result.IsSuccess)
            {
                MessageBox.Show( DateTime.Now.ToString( "[HH:mm:ss] " ) + $"[{address}] 写入成功" );
            }
            else
            {
                MessageBox.Show( DateTime.Now.ToString( "[HH:mm:ss] " ) + $"[{address}] 写入失败{Environment.NewLine}原因：{result.ToMessageShowString( )}" );
            }
        }


        #region Connect And Close



        private void button1_Click( object sender, EventArgs e )
        {
            // 连接
            System.Net.IPAddress address;
            if (!System.Net.IPAddress.TryParse( textBox1.Text, out  address ))
            {
                MessageBox.Show( "Ip地址输入不正确！" );
                return;
            }
            int port;
            if (!int.TryParse( textBox2.Text, out  port ))
            {
                MessageBox.Show( "端口号输入不正确！" );
                return;
            }

            byte SA1;
            if(!byte.TryParse(textBox15.Text,out  SA1))
            {
                MessageBox.Show( "本机网络号输入不正确！" );
                return;
            }

            byte DA2;
            if(!byte.TryParse(textBox16.Text,out  DA2 ))
            {
                MessageBox.Show( "PLC的单元号输入不正确！" );
                return;
            }
            byte DA1;
            if(!byte.TryParse(textBox17.Text,out  DA1))
            {
                MessageBox.Show( "PLC的节点号输入不正确！" );
                return;
            }

            //Globals.omronFinsUdp.IpAddress = textBox1.Text;
            //Globals.omronFinsUdp.Port = port;
            //Globals.omronFinsUdp.SA1 = SA1;
            //Globals.omronFinsUdp.DA1 = DA1;
            //Globals.omronFinsUdp.DA2 = DA2;

            Globals.omronFinsUdp = new OmronFinsUdp("127.0.0.1",9600);//(Globals.SettingOption.PLCIPAddress, Globals.SettingOption.Port);
            Globals.omronFinsUdp.SA1 = 201;// (byte)Globals.SettingOption.SA1;
            Globals.omronFinsUdp.DA2 = 0;
            //omronFinsUdp.ByteTransform.DataFormat = (HslCommunication.Core.DataFormat)(DataFormat);
            //  ABCD  BADC   CDAB  DCBA
            //omronFinsUdp.ByteTransform.IsStringReverseByteWord = true;

            //OperateResult read = Globals.omronFinsUdp.ReadInt32("D1000");
            //int InPlace = Globals.omronFinsUdp.ReadInt32("D1000").Content;
            OperateResult connect = Globals.omronFinsUdp.ReadInt32("D1200");
            try
            {
               
                // OperateResult connect = OperateResult.CreateSuccessResult( ); 
                if (connect.IsSuccess)
                {
                    MessageBox.Show( "连接成功！" );
                    button2.Enabled = true;
                    button1.Enabled = true;
                    panel2.Enabled = true;
                }
                else
                {
                    MessageBox.Show( "连接失败！" );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void button2_Click( object sender, EventArgs e )
        {
            // 断开连接
            //Globals.omronFinsUdp.ConnectClose( );
            button2.Enabled = true;
            button1.Enabled = true;
            panel2.Enabled = true;
        }







        #endregion

        #region 单数据读取测试


        private void button_read_bool_Click( object sender, EventArgs e )
        {
            // 读取bool变量
            readResultRender( Globals.omronFinsUdp.ReadBool( textBox3.Text ), textBox3.Text, textBox4 );
        }
        private void button_read_short_Click( object sender, EventArgs e )
        {
            // 读取short变量
            readResultRender( Globals.omronFinsUdp.ReadInt16( textBox3.Text ), textBox3.Text, textBox4 );
        }

        private void button_read_ushort_Click( object sender, EventArgs e )
        {
            // 读取ushort变量
            readResultRender( Globals.omronFinsUdp.ReadUInt16( textBox3.Text ), textBox3.Text, textBox4 );
        }

        private void button_read_int_Click( object sender, EventArgs e )
        {
            // 读取int变量
            readResultRender( Globals.omronFinsUdp.ReadInt32( textBox3.Text ), textBox3.Text, textBox4 );
        }
        private void button_read_uint_Click( object sender, EventArgs e )
        {
            // 读取uint变量
            readResultRender( Globals.omronFinsUdp.ReadUInt32( textBox3.Text ), textBox3.Text, textBox4 );
        }
        private void button_read_long_Click( object sender, EventArgs e )
        {
            // 读取long变量
            readResultRender( Globals.omronFinsUdp.ReadInt64( textBox3.Text ), textBox3.Text, textBox4 );
        }

        private void button_read_ulong_Click( object sender, EventArgs e )
        {
            // 读取ulong变量
            readResultRender( Globals.omronFinsUdp.ReadUInt64( textBox3.Text ), textBox3.Text, textBox4 );
        }

        private void button_read_float_Click( object sender, EventArgs e )
        {
            // 读取float变量
            readResultRender( Globals.omronFinsUdp.ReadFloat( textBox3.Text ), textBox3.Text, textBox4 );
        }

        private void button_read_double_Click( object sender, EventArgs e )
        {
            // 读取double变量
            readResultRender( Globals.omronFinsUdp.ReadDouble( textBox3.Text ), textBox3.Text, textBox4 );
        }

        private void button_read_string_Click( object sender, EventArgs e )
        {
            // 读取字符串
            readResultRender( Globals.omronFinsUdp.ReadString( textBox3.Text, ushort.Parse( textBox5.Text ) ), textBox3.Text, textBox4 );
        }


        #endregion

        #region 单数据写入测试


        private void button24_Click( object sender, EventArgs e )
        {
            // bool写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, bool.Parse( textBox7.Text ) ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void button22_Click( object sender, EventArgs e )
        {
            // short写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, short.Parse( textBox7.Text ) ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void button21_Click( object sender, EventArgs e )
        {
            // ushort写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, ushort.Parse( textBox7.Text ) ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }


        private void button20_Click( object sender, EventArgs e )
        {
            // int写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, int.Parse( textBox7.Text ) ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void button19_Click( object sender, EventArgs e )
        {
            // uint写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, uint.Parse( textBox7.Text ) ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void button18_Click( object sender, EventArgs e )
        {
            // long写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, long.Parse( textBox7.Text ) ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void button17_Click( object sender, EventArgs e )
        {
            // ulong写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, ulong.Parse( textBox7.Text ) ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void button16_Click( object sender, EventArgs e )
        {
            // float写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, float.Parse( textBox7.Text ) ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void button15_Click( object sender, EventArgs e )
        {
            // double写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, double.Parse( textBox7.Text ) ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }


        private void button14_Click( object sender, EventArgs e )
        {
            // string写入
            try
            {
                writeResultRender( Globals.omronFinsUdp.Write( textBox8.Text, textBox7.Text ), textBox8.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }
        }




        #endregion

        #region 批量读取测试

        private void button25_Click( object sender, EventArgs e )
        {
            try
            {
                OperateResult<byte[]> read = Globals.omronFinsUdp.Read( textBox6.Text, ushort.Parse( textBox9.Text ) );
                if (read.IsSuccess)
                {
                    textBox10.Text = "结果：" + HslCommunication.BasicFramework.SoftBasic.ByteToHexString( read.Content );
                }
                else
                {
                    MessageBox.Show( "读取失败：" + read.ToMessageShowString( ) );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( "读取失败：" + ex.Message );
            }
        }



        #endregion

        #region 报文读取测试


        private void button26_Click( object sender, EventArgs e )
        {
            try
            {
                OperateResult<byte[]> read = Globals.omronFinsUdp.ReadFromCoreServer( HslCommunication.BasicFramework.SoftBasic.HexStringToBytes( textBox13.Text ) );
                if (read.IsSuccess)
                {
                    textBox11.Text = "结果：" + HslCommunication.BasicFramework.SoftBasic.ByteToHexString( read.Content );
                }
                else
                {
                    MessageBox.Show( "读取失败：" + read.ToMessageShowString( ) );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( "读取失败：" + ex.Message );
            }
        }


        #endregion

        #region 定时器读取测试

        // 外加曲线显示

        private Thread thread = null;              // 后台读取的线程
        private int timeSleep = 300;               // 读取的间隔
        private bool isThreadRun = true;          // 用来标记线程的运行状态

        private void button27_Click( object sender, EventArgs e )
        {
            // 启动后台线程，定时读取PLC中的数据，然后在曲线控件中显示

            if (!isThreadRun)
            {
                if (!int.TryParse( textBox14.Text, out timeSleep ))
                {
                    MessageBox.Show( "间隔时间格式输入错误！" );
                    return;
                }
                button27.Text = "停止";
                isThreadRun = true;
                thread = new Thread( ThreadReadServer );
                thread.IsBackground = true;
                thread.Start( );
            }
            else
            {
                button27.Text = "启动";
                isThreadRun = true;
            }
        }

        private void ThreadReadServer()
        {
            while (isThreadRun)
            {
                Thread.Sleep( timeSleep );

                try
                {
                    OperateResult<short> read = Globals.omronFinsUdp.ReadInt16( textBox12.Text );
                    if (read.IsSuccess)
                    {
                        // 显示曲线
                        if (isThreadRun) Invoke( new Action<short>( AddDataCurve ), read.Content );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show( "读取失败：" + ex.Message );
                }

            }
        }


        private void AddDataCurve( short data )
        {
            userCurve1.AddCurveData( "A", data );
        }

        #endregion

        private void test()
        {
            // 读取操作，这里的D100可以替换成C100,A100,W100,H100效果时一样的
            bool D100_7 = Globals.omronFinsUdp.ReadBool( "D100.7" ).Content;  // 读取D100.7是否通断，注意D100.0等同于D100
            short short_D100 = Globals.omronFinsUdp.ReadInt16( "D100" ).Content; // 读取D100组成的字
            ushort ushort_D100 = Globals.omronFinsUdp.ReadUInt16( "D100" ).Content; // 读取D100组成的无符号的值
            int int_D100 = Globals.omronFinsUdp.ReadInt32( "D100" ).Content;         // 读取D100-D101组成的有符号的数据
            uint uint_D100 = Globals.omronFinsUdp.ReadUInt32( "D100" ).Content;      // 读取D100-D101组成的无符号的值
            float float_D100 = Globals.omronFinsUdp.ReadFloat( "D100" ).Content;   // 读取D100-D101组成的单精度值
            long long_D100 = Globals.omronFinsUdp.ReadInt64( "D100" ).Content;      // 读取D100-D103组成的大数据值
            ulong ulong_D100 = Globals.omronFinsUdp.ReadUInt64( "D100" ).Content;   // 读取D100-D103组成的无符号大数据
            double double_D100 = Globals.omronFinsUdp.ReadDouble( "D100" ).Content; // 读取D100-D103组成的双精度值
            string str_D100 = Globals.omronFinsUdp.ReadString( "D100", 5 ).Content;// 读取D100-D104组成的ASCII字符串数据

            // 写入操作，这里的D100可以替换成C100,A100,W100,H100效果时一样的
            Globals.omronFinsUdp.Write( "D100", (byte)0x33 );            // 写单个字节
            Globals.omronFinsUdp.Write( "D100", (short)12345 );          // 写双字节有符号
            Globals.omronFinsUdp.Write( "D100", (ushort)45678 );         // 写双字节无符号
            Globals.omronFinsUdp.Write( "D100", (uint)3456789123 );      // 写双字无符号
            Globals.omronFinsUdp.Write( "D100", 123.456f );              // 写单精度
            Globals.omronFinsUdp.Write( "D100", 1234556434534545L );     // 写大整数有符号
            Globals.omronFinsUdp.Write( "D100", 523434234234343UL );     // 写大整数无符号
            Globals.omronFinsUdp.Write( "D100", 123.456d );              // 写双精度
            Globals.omronFinsUdp.Write( "D100", "K123456789" );// 写ASCII字符串

            OperateResult<byte[]> read = Globals.omronFinsUdp.Read( "D100", 5 );
            {
                if (read.IsSuccess)
                {
                    // 此处需要根据实际的情况来自定义来处理复杂的数据
                    short D100 = Globals.omronFinsUdp.ByteTransform.TransInt16( read.Content, 0 );
                    short D101 = Globals.omronFinsUdp.ByteTransform.TransInt16( read.Content, 2 );
                    short D102 = Globals.omronFinsUdp.ByteTransform.TransInt16( read.Content, 4 );
                    short D103 = Globals.omronFinsUdp.ByteTransform.TransInt16( read.Content, 6 );
                    short D104 = Globals.omronFinsUdp.ByteTransform.TransInt16( read.Content, 7 );
                }
                else
                {
                    // 发生了异常
                }
            }
        }        
    }
}
