using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.Profinet.Melsec;
using HslCommunication;
using System.Windows.Forms;
using HslCommunication.Profinet;
using System.Threading;
using HslCommunication.Profinet.Omron;
namespace MyMelsecHelper
{
    public class PlcParm
    {
        public string PlcName = "";
        public string IpAdr = "192.168.0.100";
        public int PortAdr = 1000;
        public byte LocIpHead = 100;
        public byte DA2 = 0;
        public HslCommunication.Core.DataFormat ABCDtype = HslCommunication.Core.DataFormat.CDAB;
        public bool IsConnected = false;
        public string PlcType = "UDP";
        public byte station = 0;
        public byte slot = 0;
        public bool AddressStartWithZero = true;
        public bool IsStringReverse = false;
        public string LoginAccountID = "";
        public string LoginAccountPS = "";

        [NonSerialized]
        public HslCommunication.Profinet.Omron.OmronFinsNet omronFinsTcp = null;
        [NonSerialized]
        public HslCommunication.ModBus.ModbusTcpNet busTcpClient = null;
        [NonSerialized]
        public HslCommunication.Profinet.Melsec.MelsecMcNet melsecNet = new MelsecMcNet();


    }

    public enum HslDataType
    {
        hslBool,
        hslInt16,
        hslUInt16,
        hslInt32,
        hslUInt32,
        hslInt64,
        hslUInt64,
        hslFloat,
        hslDouble,
        hslString
    }

    public class HslUtils
    {
        public static Dictionary<string, PlcParm> PlcsDic = new Dictionary<string, PlcParm>();

        public static FormMelsecBinary formMelsecBinary = new FormMelsecBinary();

        public static bool PlcClose(string plcName)
        {

            PlcsDic[plcName].melsecNet.ConnectClose();
            PlcsDic[plcName].IsConnected = false;
            return true;
        }

        public static bool PlcConnect(string plcName)
        {
            PlcsDic[plcName].melsecNet.IpAddress = PlcsDic[plcName].IpAdr;
            PlcsDic[plcName].melsecNet.Port = PlcsDic[plcName].PortAdr;
            try
            {
                OperateResult connect = PlcsDic[plcName].melsecNet.ConnectServer();
                if (connect.IsSuccess)
                {
                    PlcsDic[plcName].IsConnected = true;
                    return true;
                }
                else
                {
                    PlcsDic[plcName].IsConnected = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 统一的读取结果的数据解析，显示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="address"></param>
        /// <param name="textBox"></param>
        public static void ReadResultRender<T>(OperateResult<T> result, string address, TextBox textBox)
        {
            if (result.IsSuccess)
            {
                textBox.AppendText(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] {result.Content}{Environment.NewLine}");
            }
            else
            {
                MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 读取失败{Environment.NewLine}原因：{result.ToMessageShowString()}");
            }
        }
        /// <summary>
        /// 外部调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="address"></param>
        /// <param name="textBox"></param>
        public static bool ReadResultRender<T>(string plcName, OperateResult<T> result, string address)
        {
            if (false == PlcsDic[plcName].IsConnected)
            {
                PlcConnect(plcName);
                if (result.IsSuccess)
                {
                    MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] {result.Content}{Environment.NewLine}");
                    return true;
                }
                else
                {
                    MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 读取失败{Environment.NewLine}原因：{result.ToMessageShowString()}");
                    return false;
                }
            }
            else
            {
                
                MessageBox.Show(plcName+"连接失败！");
                return false;
            }
        }

        /// <summary>
        /// 读取结果的数据解析，返回读取是否成功，OUT结果输出，OUT异常消息
        /// </summary>
        /// <typeparam name="types">类型对象</typeparam>
        /// <param name="result">读取的结果值</param>
        /// <param name="address">地址信息</param>
        /// <param name="contentData">结果输出</param>
        /// <returns></returns>
        public static bool ReadResultRender<T>(string plcName, HslDataType readType, string address, out T contentData, out string errMsg, ushort lens = 1)
        {
            if (false == PlcsDic[plcName].IsConnected)
            {
                PlcConnect(plcName);
            }

            OperateResult result;
                if (readType == HslDataType.hslBool)
                    result = PlcsDic[plcName].melsecNet.ReadBool(address);
                else if (readType == HslDataType.hslInt16)
                    result = PlcsDic[plcName].melsecNet.ReadInt16(address);
                else if (readType == HslDataType.hslInt32)
                    result = PlcsDic[plcName].melsecNet.ReadInt32(address);
                else if (readType == HslDataType.hslInt64)
                    result = PlcsDic[plcName].melsecNet.ReadInt64(address);
                else if (readType == HslDataType.hslFloat)
                    result = PlcsDic[plcName].melsecNet.ReadFloat(address);
                else if (readType == HslDataType.hslDouble)
                    result = PlcsDic[plcName].melsecNet.ReadDouble(address);
                else
                    result = PlcsDic[plcName].melsecNet.ReadString(address, lens);
            
            contentData = ((OperateResult<T>)result).Content;
            if (result.IsSuccess)
            {
                errMsg = "";
                return true;
            }
            else
            {
                errMsg = result.ToMessageShowString();
                return false;
            }
        }

        /// <summary>
        /// 写入数据，返回写入是否成功，OUT异常消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="plcName"></param>
        /// <param name="writeType"></param>
        /// <param name="address"></param>
        /// <param name="writeData"></param>
        /// <param name="errMsg"></param>
        /// <param name="lens"></param>
        /// <returns></returns>
        public static bool WriteResultRender<T>(string plcName, HslDataType writeType, string address, T writeData, out string errMsg, ushort lens = 1)
        {
            if (false == PlcsDic[plcName].IsConnected)
            {
                PlcConnect(plcName);
            }

            OperateResult result;

                if (writeType == HslDataType.hslBool)
                    result = PlcsDic[plcName].melsecNet.Write(address, (bool)(object)writeData);
                else if (writeType == HslDataType.hslInt16)
                    result = PlcsDic[plcName].melsecNet.Write(address, (Int16)(object)writeData);
                else if (writeType == HslDataType.hslUInt16)
                    result = PlcsDic[plcName].melsecNet.Write(address, (UInt16)(object)writeData);
                else if (writeType == HslDataType.hslInt32)
                    result = PlcsDic[plcName].melsecNet.Write(address, (Int32)(object)writeData);
                else if (writeType == HslDataType.hslUInt32)
                    result = PlcsDic[plcName].melsecNet.Write(address, (UInt32)(object)writeData);
                else if (writeType == HslDataType.hslInt64)
                    result = PlcsDic[plcName].melsecNet.Write(address, (Int64)(object)writeData);
                else if (writeType == HslDataType.hslUInt64)
                    result = PlcsDic[plcName].melsecNet.Write(address, (UInt64)(object)writeData);
                else if (writeType == HslDataType.hslFloat)
                    result = PlcsDic[plcName].melsecNet.Write(address, (float)(object)writeData);
                else if (writeType == HslDataType.hslDouble)
                    result = PlcsDic[plcName].melsecNet.Write(address, (double)(object)writeData);
                else
                    result = PlcsDic[plcName].melsecNet.Write(address, (string)(object)writeData);
            
            if (result.IsSuccess)
            {
                errMsg = "";
                return true;
            }
            else
            {
                errMsg = result.ToMessageShowString();
                return false;
            }

        }


        /// <summary>
        /// 统一的数据写入的结果显示MC
        /// </summary>
        /// <param name="result"></param>
        /// <param name="address"></param>
        public static void WriteResultRender(OperateResult result, string address)
        {
            if (result.IsSuccess)
            {
                MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 写入成功");
            }
            else
            {
                MessageBox.Show(DateTime.Now.ToString("[HH:mm:ss] ") + $"[{address}] 写入失败{Environment.NewLine}原因：{result.ToMessageShowString()}");
            }
        }


        public static OperateResult<byte[]> BulkReadRenderResult(string plcName, string address,string lengthTextBox)
        {
            //try
            //{
                return PlcsDic[plcName].melsecNet.Read(address, ushort.Parse(lengthTextBox));
                //if (read.IsSuccess)
                //{
                //    string ReadContentResault = "结果：" + HslCommunication.BasicFramework.SoftBasic.ByteToHexString(read.Content);
                //    readresultConten = read.Content;
                //    MessageBox.Show("读取结果：" + ReadContentResault);
                //}
                //else
                //{
                //    MessageBox.Show("读取失败：" + read.ToMessageShowString());
                //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("读取失败：" + ex.Message);
            //}

        }
        public static OperateResult<bool[]>  BulkReadBoolRenderResult(string plcName, string address, string lengthTextBox)
        {
            //try
            //{
            return PlcsDic[plcName].melsecNet.ReadBool(address, ushort.Parse(lengthTextBox));
            //if (read.IsSuccess)
            //{
            //    string ReadContentResault = "结果：" + HslCommunication.BasicFramework.SoftBasic.ByteToHexString(read.Content);
            //    readresultConten = read.Content;
            //    MessageBox.Show("读取结果：" + ReadContentResault);
            //}
            //else
            //{
            //    MessageBox.Show("读取失败：" + read.ToMessageShowString());
            //}
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("读取失败：" + ex.Message);
            //}
        }
        public static readonly string IpAddressInputWrong = "IpAddress input wrong";
        public static readonly string PortInputWrong = "Port input wrong";
        public static readonly string SlotInputWrong = "Slot input wrong";
        public static readonly string BaudRateInputWrong = "Baud rate input wrong";
        public static readonly string DataBitsInputWrong = "Data bit input wrong";
        public static readonly string StopBitInputWrong = "Stop bit input wrong";
    }
}
