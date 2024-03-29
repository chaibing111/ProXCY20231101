using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro;
using System.Drawing;

namespace sunyvpp
{
    public  class CommonClass
    {
        public delegate void ParamChanged(ParamType type);
        /// <summary>
        /// 參數變化時觸發的事件
        /// </summary>
        //public event ParamChanged OnParamChanged;
        //private LogFileOperater logfile;
        private MainForm Main = null;

        public CommonClass(MainForm mainfrm)
        {
            Main = mainfrm;
        }

        #region Delay

        public static void Delay(int delay_time)
        {
            int StartTick = Environment.TickCount;

            while (Environment.TickCount - StartTick < delay_time)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }

        #endregion

        #region ReadParam and SaveParam 
        //save param 
        //public  bool SaveParam(ParamType type)
        //{
        //    try
        //    {
        //        switch (type)
        //        {
        //            case ParamType.LightParam:
        //                {
        //                    XmlOperater xml = new XmlOperater();
        //                    xml.OpenXml(Vars.LightParamPath, Xml_FileAccess.ReadWrite);

        //                    xml.AddNode("/Root","PortParam","",null); 
        //                    xml.AddNode("/Root/PortParam", "PortName", Vars.PortName, null);
        //                    xml.AddNode("/Root/PortParam", "Baudrate", Vars.BaudRate, null);
        //                    xml.AddNode("/Root/PortParam", "StopBits", Vars.StopBits, null);
        //                    xml.AddNode("/Root/PortParam", "DataBits", Vars.DataBits, null);
        //                    xml.AddNode("/Root/PortParam", "Parity", Vars.Parity, null);

        //                    foreach (string Step in Vars.LightStep)
        //                    { 
        //                        if (!Vars.LightStepValue.ContainsKey(Step))
        //                        {
        //                            Vars.LightStepValue.Add(Step, new DDR4Check.Communicate.ChannelValue(0, 0, 0, 0));
        //                        }
        //                        if (Step == "StepHsg")
        //                        {
        //                            xml.AddNode("/Root", Step, "", new Xml_Attribute[] {new Xml_Attribute("Color",Vars.SelectedColor.ToString()) });
                                   
        //                            xml.AddNode("/Root/" + Step +"[@Color= '"+ Vars.SelectedColor.ToString() + "']", "A", Vars.LightStepValue[Step].A.ToString(), null);
        //                            xml.AddNode("/Root/" + Step +"[@Color= '"+ Vars.SelectedColor.ToString() + "']", "B", Vars.LightStepValue[Step].B.ToString(), null);
        //                            xml.AddNode("/Root/" + Step +"[@Color= '"+ Vars.SelectedColor.ToString() + "']", "C", Vars.LightStepValue[Step].C.ToString(), null);
        //                            xml.AddNode("/Root/" + Step +"[@Color= '"+ Vars.SelectedColor.ToString() + "']", "D", Vars.LightStepValue[Step].D.ToString(), null);
        //                        }
        //                        else
        //                        {
        //                            xml.AddNode("/Root", Step, "", null);
        //                            xml.AddNode("/Root/" + Step, "A", Vars.LightStepValue[Step].A.ToString(), null);
        //                            xml.AddNode("/Root/" + Step, "B", Vars.LightStepValue[Step].B.ToString(), null);
        //                            xml.AddNode("/Root/" + Step, "C", Vars.LightStepValue[Step].C.ToString(), null);
        //                            xml.AddNode("/Root/" + Step, "D", Vars.LightStepValue[Step].D.ToString(), null);
        //                        }
        //                    }

        //                    xml.Save(Vars.LightParamPath);
        //                    break;
        //                }
        //            case ParamType.SystemParam:
        //                {
        //                    IniFileOperater Ini = new IniFileOperater(Vars.SystemParamPath);
        //                    Ini.WriteEntry("Product","Color",((int)Vars.SelectedColor));
        //                    int temp = Vars.OnlyNgbmp == true ? 1 : 0;
        //                    Ini.WriteEntry("ImageSave","OnlyNgBmp" ,temp);
        //                    Ini.WriteEntry("ImageSave", "SaveImageType", (int)Vars.SavedImageType);
        //                    break;
        //                }
        //            case ParamType.PLCParam:
        //                {
        //                    break;
        //                }
        //            case ParamType.RobotParam:
        //                {
        //                    break;
        //                }
        //            case ParamType.VisionParam:
        //                {
        //                    IniFileOperater Ini = new IniFileOperater(Vars.VisionParamPath);
        //                    foreach(string name in Vars.VisionParamKeys)
        //                    {
        //                        if (!Vars.VisionParamValue.ContainsKey(name))
        //                            Vars.VisionParamValue.Add(name,new SMSVision.SMSPostion(0, 0, 0));
        //                        Ini.WriteEntry(name, "X", Vars.VisionParamValue[name].X);
        //                        Ini.WriteEntry(name, "Y", Vars.VisionParamValue[name].Y);
        //                        Ini.WriteEntry(name, "Rotation", Vars.VisionParamValue[name].Rotation);
        //                    }
        //                    break;
        //                }
        //        }
        //        if (OnParamChanged != null)
        //            OnParamChanged(type);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    { 
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}
        //read param
        //public  bool ReadParam(ParamType type)
        //{
        //    try
        //    {
        //        switch (type)
        //        {
        //            case ParamType.LightParam:
        //                {
        //                    Vars.LightStepValue.Clear();
        //                    if (!File.Exists(Vars.LightParamPath))
        //                        SaveParam(type);
        //                    XmlOperater xml = new XmlOperater();
        //                    xml.OpenXml(Vars.LightParamPath, Xml_FileAccess.ReadWrite);


        //                    xml.ReadValue("/Root/PortParam/PortName", out Vars.PortName);
        //                    xml.ReadValue("/Root/PortParam/Baudrate", out Vars.BaudRate);
        //                    xml.ReadValue("/Root/PortParam/StopBits", out Vars.StopBits);
        //                    xml.ReadValue("/Root/PortParam/DataBits", out Vars.DataBits);
        //                    xml.ReadValue("/Root/PortParam/Parity", out Vars.Parity);

        //                    foreach (string Step in Vars.LightStep)
        //                    {
        //                        Vars.LightStepValue.Add(Step, new DDR4Check.Communicate.ChannelValue(0, 0, 0, 0));
        //                        byte Temp = 0;
        //                        if (Step == "StepHsg")
        //                        {
        //                            xml.ReadValue("/Root/StepHsg[@Color ='" + Vars.SelectedColor.ToString() + "']/A", out Temp);
        //                            Vars.LightStepValue[Step].A = Temp;
        //                            xml.ReadValue("/Root/StepHsg[@Color ='" + Vars.SelectedColor.ToString() + "']/B", out Temp);
        //                            Vars.LightStepValue[Step].B = Temp;
        //                            xml.ReadValue("/Root/StepHsg[@Color ='" + Vars.SelectedColor.ToString() + "']/C", out Temp);
        //                            Vars.LightStepValue[Step].C = Temp;
        //                            xml.ReadValue("/Root/StepHsg[@Color ='" + Vars.SelectedColor.ToString() + "']/D", out Temp);
        //                            Vars.LightStepValue[Step].D = Temp;
        //                        }
        //                        else
        //                        {
        //                            xml.ReadValue("/Root/" + Step + "/A", out Temp);
        //                            Vars.LightStepValue[Step].A = Temp;
        //                            xml.ReadValue("/Root/" + Step + "/B", out Temp);
        //                            Vars.LightStepValue[Step].B = Temp;
        //                            xml.ReadValue("/Root/" + Step + "/C", out Temp);
        //                            Vars.LightStepValue[Step].C = Temp;
        //                            xml.ReadValue("/Root/" + Step + "/D", out Temp);
        //                            Vars.LightStepValue[Step].D = Temp;
        //                        }
        //                    }
        //                    xml.Save(Vars.LightParamPath);
        //                    break;
        //                }
        //            case ParamType.SystemParam://儲存其他項目
        //                {
        //                    IniFileOperater Ini = new IniFileOperater(Vars.SystemParamPath);
        //                    int t = 0;
        //                    Ini.ReadEntry("Product","Color",out t);
        //                    Vars.SelectedColor = (ProductColors)t;
        //                    Ini.ReadEntry("ImageSave","OnlyNgBmp",out t);
        //                    Vars.OnlyNgbmp = t == 1 ? true : false;
        //                    Ini.ReadEntry("ImageSave","SaveImageType",out t);
        //                    Vars.SavedImageType = (SaveImageType)t;
        //                    break;
        //                }
        //            case ParamType.PLCParam:
        //                {
        //                    break;
        //                }
        //            case ParamType.RobotParam:
        //                {
        //                    break;
        //                }
        //            case ParamType.VisionParam:
        //                {
        //                    if (!File.Exists(Vars.VisionParamPath))
        //                        SaveParam(ParamType.VisionParam);
        //                    IniFileOperater Ini = new IniFileOperater(Vars.VisionParamPath);
        //                    foreach (string name in Vars.VisionParamKeys)
        //                    {
        //                        if (!Vars.VisionParamValue.ContainsKey(name))
        //                        {
        //                            Vars.VisionParamValue.Add(name, new SMSVision.SMSPostion(0, 0, 0));
        //                        }
        //                        double temp = 0;
        //                        Ini.ReadEntry(name, "X", out  temp);
        //                        Vars.VisionParamValue[name].X = temp;
        //                        Ini.ReadEntry(name, "Y", out  temp);
        //                        Vars.VisionParamValue[name].Y = temp;
        //                        Ini.ReadEntry(name, "Rotation", out  temp);
        //                        Vars.VisionParamValue[name].Rotation = temp;
        //                    }
        //                    break;
        //                }
        //        }
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        SaveParam(type);
        //        return false;
        //    }
        //}
        #endregion

        #region OPen port fuction (contains serailport and tcpip)
        //public void OpenPort(PortType type)
        //{
        //    try
        //    {
        //        switch (type)
        //        {
        //            case PortType.LightPort:
        //                {
        //                    if (Main.lightPort1.IsOpen)
        //                        Main.lightPort1.Close();
        //                    Main.lightPort1.PortName = Vars.PortName;
        //                    Main.lightPort1.BaudRate = int.Parse(Vars.BaudRate);
        //                    Main.lightPort1.DataBits = int.Parse(Vars.DataBits);
        //                    switch (Vars.StopBits)
        //                    {
        //                        case "none": Main.lightPort1.StopBits = System.IO.Ports.StopBits.None; break;
        //                        case "1": Main.lightPort1.StopBits = System.IO.Ports.StopBits.One; break;
        //                        case "1.5": Main.lightPort1.StopBits = System.IO.Ports.StopBits.OnePointFive; break;
        //                        case "2": Main.lightPort1.StopBits = System.IO.Ports.StopBits.Two; break;
        //                        default: break;
        //                    }
        //                    switch (Vars.Parity)
        //                    {
        //                        case "none": Main.lightPort1.Parity = System.IO.Ports.Parity.None; break;
        //                        case "odd": Main.lightPort1.Parity = System.IO.Ports.Parity.Odd; break;
        //                        case "even": Main.lightPort1.Parity = System.IO.Ports.Parity.Even; break;
        //                        default: break;
        //                    }
        //                    Main.lightPort1.Open(); 
        //                    break;
        //                }
        //            case PortType.PLCPort:
        //                {

        //                    break;
        //                }
        //            case PortType.RoBotPort:
        //                {

        //                    break;
        //                }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public void ClosePort(PortType type)
        //{
        //    try
        //    {
        //        switch (type)
        //        {
        //            case PortType.LightPort:
        //                {
        //                    if (Main.lightPort1.IsOpen)
        //                        Main.lightPort1.Close();
        //                    break;
        //                }
        //            case PortType.PLCPort:
        //                {

        //                    break;
        //                }
        //            case PortType.RoBotPort:
        //                {

        //                    break;
        //                }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        #endregion

        #region save image
        //public static void SaveImage(CogDisplay display ,SaveImageType type, string name)
        //{
        //    if (!Directory.Exists(name.Remove(name.LastIndexOf("\\"))))
        //        Directory.CreateDirectory(name.Remove(name.LastIndexOf("\\")));
        //    switch (type)
        //    {
        //        case SaveImageType.None: break;
        //        case SaveImageType.Image: display.CreateContentBitmap(CogDisplayContentBitmapConstants.Image, null, 0).Save(name); break;
        //        case SaveImageType.ImageWithRecord: display.Image.ToBitmap().Save(name); break;
        //        default: break;
        //    }
        //}
        #endregion

      
    }
}
