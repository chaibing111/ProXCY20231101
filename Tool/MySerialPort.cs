
using EventMgrLib;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace sunyvpp
{
    public class MySerialPort
    {
        public event EventHandler<string> inforShowEvent;
        public string SerialPortName { get; set; }

        public int BaudRate { get; set; }

        public Parity MyParity { get; set; }

        public StopBits MyStopBits { get; set; }

        public int DataBits { get; set; }

        public string Terminator { get; set; }

        public SerialPort Serial;

        public string Receive="";
        public MySerialPort()
        {

        }

  
        public bool InitMySerialPort()
        {
            return InitMySerialPort(this.SerialPortName, this.BaudRate, this.MyParity, this.DataBits, this.MyStopBits, this.Terminator);
        }

        public bool InitMySerialPort(string SerialPortName, int BaudRate, Parity Parity, int DataBits, StopBits StopBits, string Terminator = "")
        {
            this.SerialPortName = SerialPortName;
            this.BaudRate = BaudRate;
            this.MyParity = Parity;
            this.MyStopBits = StopBits;
            this.DataBits = DataBits;
            this.Terminator = Terminator;

            this.Serial = new SerialPort(SerialPortName, BaudRate, Parity, DataBits, StopBits);
            Serial.DataReceived += Serial_DataReceived;

            try
            {
                Serial.Open();
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Receive = "";
            try
            {
                int read = this.Serial.BytesToRead;
                byte[] buffer = new byte[read];
                int count = this.Serial.Read(buffer, 0, read);
                Receive = Encoding.UTF8.GetString(buffer);
                if (this.inforShowEvent != null) this.inforShowEvent(null, Receive);

                EventMgr.Ins.GetEvent<CommunicationEventSerialRev>().Publish(Receive);
            }
            catch (Exception exception)
            {
                ;
            }
        }

        public bool SendMessage(string Message)
        {
            try
            {
                byte[] sendbytes = Encoding.UTF8.GetBytes(Message);
                Serial.Write(sendbytes, 0, sendbytes.Length);
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public void Open()
        {
            try
            {
                this.Serial.Open();
            }
            catch (Exception ex)
            {
                ;
            }
        }
        public void Close()
        {
            try
            {
                this.Serial.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
