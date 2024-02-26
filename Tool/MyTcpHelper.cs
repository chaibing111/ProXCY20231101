using System;
using System.Collections.Generic;
using System.Linq;
using TouchSocket.Sockets;
using System.Text;
using System.Threading.Tasks;
using TouchSocket.Core.Config;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TouchSocket.Core.ByteManager;
using TouchSocket.Core.Plugins;
using EventMgrLib;

namespace sunyvpp
{

    public class MyTcpHelper
    {
        #region 单例懒加载

        public void MyConnect(string strIP, int nPort, int nTimeMs)
        {

            m_strIP = strIP;
            m_nPort = nPort;
            m_nTime = nTimeMs;
            tcpClient = new TcpClient();
        }
        public MyTcpHelper(string strIP, int nPort, int nTimeMs=10)
        {
            m_strIP = strIP;
            m_nPort = nPort;
            m_nTime = nTimeMs;
            tcpClient = new TcpClient();
        }
        //public static MyTcpHelper Ins { get; } = instance.Value;

        #endregion
        /// <summary>
        ///对方IP地址 
        /// </summary>
        public static string m_strIP;
        /// <summary>
        ///端口号 
        /// </summary>
        public static int m_nPort;
        /// <summary>
        ///超时时间,单位毫秒
        /// </summary>
        public static int m_nTime;
        /// <summary>
        ///接收数据
        /// </summary>
        public string LastReciveMessage { get; set; }
        /// <summary>
        ///异步A01
        /// </summary>
        public string s_StrA01 = string.Empty;
        public TcpClient tcpClient;
        public bool ReConnection { get; set; }
        //声明配置
        public TouchSocketConfig config;

        public bool ConnectServer()
        {
            try
            {
                config = new TouchSocketConfig();
            string iphost = m_strIP + ":" + m_nPort;
            tcpClient.Connected = (client, e) => { };//成功连接到服务器
            tcpClient.Disconnected = (client, e) => { };//从服务器断开连接，当连接不成功时不会触发。
            config.SetRemoteIPHost(new IPHost(iphost))
                .UsePlugin()
                .SetBufferLength(1024 * 10);

            //从服务器断开连接，当连接不成功时不会触发。
            tcpClient.Received += ReceivedHandler;


            if (ReConnection)
            {
                config.ConfigurePlugins(a =>
                {
                    a.UseReconnection(-1, true, 1000);
                });
            };

            //载入配置
            tcpClient.Setup(config);
            //断线重连
            config.UsePlugin()
                .ConfigurePlugins(a =>
                {
                    a.UseReconnection(5, true, 1000);
                });
     

                tcpClient?.Connect();
                return true;
            }
            catch
            {
                return true;
            }
            //tcpClient.Received = (client, byteBlock, requestInfo) =>
            //{
            //    //从服务器收到信息
            //    string mes = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
            //    //this.BeginInvoke(new Action(() =>
            //    //{
            //    //    mes = mes + "\r\n";
            //    //}));
            //    s_StrA01 = mes;

            //    Console.WriteLine($"接收到信息：{mes}");
            //};
            return true;
        }
        public void sendStr(string str)
        {
            try
            {

                tcpClient?.Send(str);
            }
            catch (Exception e)
            {
                MessageBox.Show("连接失败");
            }
        }
        public void SendByte(byte[] byteMsg)
        {
            tcpClient?.Send(byteMsg);
        }
        public void Close()
        {
            tcpClient.Close();
        }
        public void ReceivedHandler<ITcpClient>(ITcpClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
        {
            LastReciveMessage = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);


            EventMgr.Ins.GetEvent<CommunicationEventRev>().Publish(LastReciveMessage);



        }
    }
}
