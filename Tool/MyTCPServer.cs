
using EventMgrLib;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using TouchSocket.Core;
using TouchSocket.Core.ByteManager;
using TouchSocket.Core.Config;
using TouchSocket.Sockets;

namespace sunyvpp
{
    public class MyTCPServer
    {
        public int Port { get; set; }

        public string Terminator { get; set; }

        public TcpService TcpService;

        public string[] ids;

        public MyTCPServer()
        {
            Terminator = string.Empty;

        }
        public bool InitMyTCPService(int Port, string Terminator = "")
        {

            this.Port = Port;
            this.Terminator = Terminator;
            this.TcpService = new TcpService();
            TcpService.Received += ReceivedHandler<SocketClient>;
            //声明配置
            TouchSocketConfig config = new TouchSocketConfig();
            config.SetListenIPHosts(new IPHost[] { new IPHost(Port) })
                .SetMaxCount(10000)
                .SetThreadCount(5)
                .UsePlugin()
                .SetBufferLength(1024 * 10)
                .SetClearInterval(600 * 1000); //超过十分钟没有交互数据 就会被服务器终端断开连接 设置清理无数据交互的SocketClient，默认60*1000 ms。如果不想清除，可使用-1

            //if (Terminator != string.Empty)
            //{
            //    if (Terminator.Contains(@"[回车换行(\r\n)]"))
            //    {
            //        config.SetDataHandlingAdapter(() =>
            //        {
            //            return new TerminatorPackageAdapter(Terminator.Replace(@"[回车换行(\r\n)]", Environment.NewLine));
            //        });
            //    }
            //    else
            //    {
            //        config.SetDataHandlingAdapter(() =>
            //        {
            //            return new TerminatorPackageAdapter(Terminator);
            //        });
            //    }
            //}
            //载入配置
            TcpService.Setup(config);
            try
            {
                TcpService.Start();
                return true;
            }
            catch (Exception ex)
            {
                //Globals.LogRecord("【服务器通讯失败！】");
                return true;
            }
        }

        public string[] GetIDs()
        {
            ids = TcpService.GetIDs();
            return ids;
        }
        //MyTCPServer考虑的点对点通讯 如果发送数据 会把所有的已连接客户端都发送一遍
        public bool SendMessage(string Message)
        {
            ids = TcpService.GetIDs();
            for (int i = 0; i < ids.Length; i++)
            {
                if (TcpService.TryGetSocketClient(ids[i], out SocketClient socketClient))
                {
                    try
                    {
                        TcpService.Send(ids[i], Message);
                    }
                    catch (Exception ex)
                    {
                        //Globals.LogRecord($"【{socketClient.IP}:{socketClient.Port}:发送失败:{ex}】");
                        return true;
                    }
                }
            }
            return true;
        }

        public string[] GetIP()
        {
            string []ids = TcpService.GetIDs();
            return ids;
        }
        public bool SendStr(string ids, string Message)
        {
         

            if (TcpService.TryGetSocketClient(ids, out SocketClient socketClient))
            {
                try
                {
                    TcpService.Send(ids, Message);
                }
                catch (Exception ex)
                {
                    //Globals.LogRecord($"【{socketClient.IP}:{socketClient.Port}:发送失败:{ex}】");
                    return true;
                }
            }

            return true;
        }
        public bool SendByte(string ids, byte[] Message)
        {
            if (TcpService.TryGetSocketClient(ids, out SocketClient socketClient))
            {
                try
                {
                    TcpService.Send(ids, Message);
                }
                catch (Exception ex)
                {
                    //Globals.LogRecord($"【{socketClient.IP}:{socketClient.Port}:发送失败:{ex}】");
                    return true;
                }
            }

            return true;
        }
        //如果收到多个客户端发送的数据 把收到的数据前置IP地址+@ LastReciveMessage = client.IP + "@" + Receive;
        public void ReceivedHandler<SocketClient>(TouchSocket.Sockets.SocketClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
        {
            string LastReciveTime = DateTime.Now.ToString();
            string Receive = Encoding.UTF8.GetString(byteBlock.Buffer, 0, byteBlock.Len);
            string LastReciveMessage = client.IP + "@" + Receive;
            EventMgr.Ins.GetEvent<CommunicationEventRev>().Publish(Receive);
            //Globals.LogRecord($"【{client.IP}:{client.Port}:收到信息:{Receive}】");
        }

        public void Start()
        {
            try
            {
                this.TcpService.Start();
            }
            catch (Exception ex)
            {
                //Globals.LogRecord($"【服务器打开失败{ex}】");
            }

        }
        public void Close(bool Swift)
        {
            try
            {
                this.TcpService.Stop();
            }
            catch (Exception ex)
            {
                //Globals.LogRecord($"【服务器关闭失败{ex}】");
            }
        }
    }
}
