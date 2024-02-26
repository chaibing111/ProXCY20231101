using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;
using System.Diagnostics;

namespace sunyvpp
{
    public class CsvServer
    {
        private Thread _thread;
        private ConcurrentQueue<CsvInfo> queue = new ConcurrentQueue<CsvInfo>();
        private readonly static CsvServer instance = new CsvServer();
        public object obj = new object();
        CsvServer() { }
        public static CsvServer Instance
        {
            get { return instance; }
        }

        public void Start()
        {
            Stop();
            _thread = new Thread(new ThreadStart(ProcessEventQueue));
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Stop()
        {
            if (this._thread != null)
            {
                this._thread.Abort();
            }
        }
        private void Kill()//20170327 XSF
        {
            Process[] process = Process.GetProcesses();
            foreach (Process p in process)
            {
                if (p.ProcessName.ToUpper() == "EXCEL")
                {
                    p.CloseMainWindow();
                    p.WaitForExit();
                }
            }
        }
        private void ProcessEventQueue()
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    CsvInfo csvInfo;
                    queue.TryDequeue(out csvInfo);
                    try
                    {
                        lock (obj)
                        {
                            Kill();//20170327 XSF
                            StreamWriter sw = File.AppendText(csvInfo.Path);
                            if (csvInfo.IsAddReturnKey)
                            {
                                sw.WriteLine(csvInfo.Line);
                            }
                            else
                            {
                                sw.Write(csvInfo.Line);
                            }
                            sw.Dispose();
                        }
                    }
                    catch
                    {

                    }
                }
                Thread.Sleep(20);
            }
        }

        public void WriteLine(string path, string line, bool isaddreturnkey = true)
        {

            CsvInfo csvInfo = new CsvInfo();
            csvInfo.Path = path;
            csvInfo.Line = line;
            csvInfo.IsAddReturnKey = isaddreturnkey;
            queue.Enqueue(csvInfo);

        }

        public void WriteHeader(string path, string line)
        {
            try
            {
                lock (obj)
                {
                    StreamWriter swFromFile = new StreamWriter(path);
                    swFromFile.WriteLine(line);
                    swFromFile.Flush();
                    swFromFile.Close();
                }

            }
            catch (Exception)
            {


            }

        }
    }

    class CsvInfo
    {
        public string Path { get; set; }
        public string Line { get; set; }
        public bool IsAddReturnKey { get; set; }
    }
}
