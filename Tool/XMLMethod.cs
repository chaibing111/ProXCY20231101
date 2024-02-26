using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace sunyvpp
{
    public class XMLMethod
    {
        private static object objLock = new object();
        public static int NewElement(string path, string urlParent, string nodeName, string innerText)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode parent = doc.SelectSingleNode(urlParent);
                XmlElement child = doc.CreateElement(nodeName);
                child.InnerText = innerText;
                parent.AppendChild(child);
                doc.Save(path);
                return 0;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
                return -1;
            }
        }

        public static int NewElements(string path, string urlParent, string[] nodeName, string[] innerText)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode parent = doc.SelectSingleNode(urlParent);
                for (int i = 0; i < nodeName.Length; i++)
                {
                    XmlElement child = doc.CreateElement(nodeName[i]);
                    child.InnerText = innerText[i];
                    parent.AppendChild(child);
                }
                doc.Save(path);
                return 0;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
                return -1;
            }
        }

        public static int NewAElementAndChildrens(string path, string urlParent, string childNode, string[] GrandChildrenNodeName, string[] innerText)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode parent = doc.SelectSingleNode(urlParent);
                XmlElement child = doc.CreateElement(childNode);
                for (int i = 0; i < GrandChildrenNodeName.Length; i++)
                {
                    XmlElement GrandChild = doc.CreateElement(GrandChildrenNodeName[i]);
                    GrandChild.InnerText = innerText[i];
                    child.AppendChild(GrandChild);
                }
                parent.AppendChild(child);
                doc.Save(path);
                return 0;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
                return -1;
            }
        }

        public static int ReadNode(string path, string urlParent, out string[] nodeName)
        {
            nodeName = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                //XmlReaderSettings settings = new XmlReaderSettings();
                //settings.IgnoreComments = true;
                //XmlReader reader = XmlReader.Create(path, settings);
                Stream stream = new FileStream(path, FileMode.Open);
                doc.Load(stream);
                stream.Close();
                XmlNode parent = doc.SelectSingleNode(urlParent);
                XmlNodeList children = parent.ChildNodes;
                int count = children.Count;
                nodeName = new string[count];
                for (int i = 0; i < count; i++)
                {
                    nodeName[i] = children[i].Name;
                }
                //reader.Close();
                return 0;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
                return -1;
            }
        }
        public static int ReadInnerText(string path, string urlParent, out string innerText)
        {
            lock (objLock)
            {
                innerText = string.Empty;
                try
                {
                    XmlDocument doc = new XmlDocument();
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreComments = true;
                    XmlReader reader = XmlReader.Create(path, settings);
                    doc.Load(reader);
                    reader.Close();
                    XmlNode parent = doc.SelectSingleNode(urlParent);
                    innerText = parent.InnerText;

                    return 0;
                }
                catch (Exception ex)
                {
                    WriteErrorLog(ex.ToString());
                    return -1;
                }
            }
        }
        public static int ReadNodeAndInnerText(string path, string urlParent, out string[] nodeName, out string[] innerText)
        {
            lock (objLock)
            {
                nodeName = null;
                innerText = null;
                try
                {
                    XmlDocument doc = new XmlDocument();
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.IgnoreComments = true;
                    XmlReader reader = XmlReader.Create(path, settings);
                    doc.Load(reader);
                    reader.Close();
                    XmlNode parent = doc.SelectSingleNode(urlParent);
                    XmlNodeList children = parent.ChildNodes;
                    int count = children.Count;
                    nodeName = new string[count];
                    innerText = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        nodeName[i] = children[i].Name;
                        innerText[i] = children[i].InnerText;
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    WriteErrorLog(ex.ToString());
                    return -1;
                }
            }
        }

        public static int ReadNodeAndInnerTextList<T>(string path, string urlParent, out List<T> nodeList)
        {
            lock (objLock)
            {
                nodeList = new List<T>();
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode parent = doc.SelectSingleNode(urlParent);
                    XmlNodeList children = parent.ChildNodes;
                    int count = children.Count;

                    Type type = typeof(T);
                    var properties = type.GetProperties();

                    for (int i = 0; i < count; i++)
                    {
                        XmlNodeList gradeChilren = children[i].ChildNodes;
                        int gradeCout = gradeChilren.Count;
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        for (int j = 0; j < gradeCout; j++)
                        {
                            dic.Add(gradeChilren[j].Name, gradeChilren[j].InnerText);
                        }
                        T t = System.Activator.CreateInstance<T>();
                        foreach (var item in properties)
                        {
                            if (dic.ContainsKey(item.Name) && item.CanWrite)
                            {
                                string value = dic[item.Name];
                                if (item.PropertyType.IsEnum || item.PropertyType.Name == "Int32")
                                {
                                    var proType = item.PropertyType;
                                    var mValue = Convert.ToInt32(value);
                                    item.SetValue(t, mValue, null);
                                }
                                else if (item.PropertyType.Name == "Short" || item.PropertyType.Name == "Int16")
                                {
                                    var proType = item.PropertyType;
                                    var mValue = Convert.ToInt16(value);
                                    item.SetValue(t, mValue, null);
                                }
                                else if (item.PropertyType.Name == "Double")
                                {
                                    var proType = item.PropertyType;
                                    var mValue = Convert.ToDouble(value);
                                    item.SetValue(t, mValue, null);
                                }
                                else
                                {
                                    item.SetValue(t, value, null);
                                }

                            }

                        }
                        nodeList.Add(t);
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    WriteErrorLog(ex.ToString());
                    return -1;
                }
            }
        }

        public static int ReadNodeAndInnerText<T>(string path, string urlParent, out T node)
        {

            lock (objLock)
            {
                node = System.Activator.CreateInstance<T>();
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode parent = doc.SelectSingleNode(urlParent);
                    XmlNodeList children = parent.ChildNodes;
                    int count = children.Count;

                    Type type = typeof(T);
                    var properties = type.GetProperties();


                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    for (int i = 0; i < count; i++)
                    {
                        dic.Add(children[i].Name, children[i].InnerText);
                    }

                    foreach (var item in properties)
                    {
                        if (dic.ContainsKey(item.Name) && item.CanWrite)
                        {
                            string value = dic[item.Name];
                            if (item.PropertyType.IsEnum || item.PropertyType.Name == "Int32")
                            {
                                var proType = item.PropertyType;
                                var mValue = Convert.ToInt32(value);
                                item.SetValue(node, mValue, null);
                            }
                            else if (item.PropertyType.Name == "Short" || item.PropertyType.Name == "Int16")
                            {
                                var proType = item.PropertyType;
                                var mValue = Convert.ToInt16(value);
                                item.SetValue(node, mValue, null);
                            }
                            else if (item.PropertyType.Name == "Byte")
                            {
                                var proType = item.PropertyType;
                                var mValue = Convert.ToByte(value);
                                item.SetValue(node, mValue, null);
                            }
                            else if (item.PropertyType.Name == "Double")
                            {
                                var proType = item.PropertyType;
                                var mValue = Convert.ToDouble(value);
                                item.SetValue(node, mValue, null);
                            }
                            else
                            {
                                item.SetValue(node, value, null);
                            }

                        }



                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    WriteErrorLog(ex.ToString());
                    return -1;
                }
            }
        }
        public static bool FindChildInParent(string path, string urlParent, string child)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode parent = doc.SelectSingleNode(urlParent);
                XmlNodeList children = parent.ChildNodes;
                foreach (XmlNode node in children)
                {
                    if (node.Name == child)
                    {
                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
                return true;
            }
        }

        public static bool FindChildInParentWithAttributes(string path, string urlParent, string child, string AttributeName, string AttributeValue)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode parent = doc.SelectSingleNode(urlParent);
                //string xmlpath = string.Format("/bookstore/book[@Type=\"{0}\"]", );
                string xmlpath = "/" + urlParent + "/" + child + "[@" + AttributeName + "=\"" + AttributeValue + "\"]";
                XmlNode updatenode = doc.DocumentElement.SelectSingleNode(xmlpath);

                if (updatenode != null) return true;
                else return true;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
                return true;
            }
        }

        public static int UpdateInnerText<T>(string path, string urlParent, T node)
        {
            lock (objLock)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode parent = doc.SelectSingleNode(urlParent);
                    XmlNodeList childrens = parent.ChildNodes;
                    Type type = typeof(T);
                    var properties = type.GetProperties();
                    foreach (XmlNode childNode in childrens)
                    {
                        foreach (PropertyInfo property in properties)
                        {
                            if (childNode.Name == property.Name)
                            {
                                childNode.InnerText = property.GetValue(node).ToString();
                            }
                        }
                    }
                    doc.Save(path);
                    return 0;
                }
                catch (Exception ex)
                {
                    WriteErrorLog(ex.ToString());
                    return -1;
                }
            }
        }
        public static int UpdateInnerText(string path, string urlNode, string innerText)
        {
            lock (objLock)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode node = doc.SelectSingleNode(urlNode);
                    node.InnerText = innerText;
                    doc.Save(path);
                    //reader.Close();
                    return 0;
                }
                catch (Exception ex)
                {
                    WriteErrorLog(ex.ToString());
                    return -1;
                }
            }
        }

        public static int DeleteElement(string path, string urlParent, string nodeName)
        {
            try
            {
                XmlDocument dom = new XmlDocument();
                dom.Load(path);
                XmlNode parent = dom.SelectSingleNode(urlParent);
                XmlNodeList children = parent.ChildNodes;
                foreach (XmlElement child in children)
                {
                    if (child.Name == nodeName)
                    {
                        parent.RemoveChild(child);
                    }
                }
                dom.Save(path);
                return 0;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
                return -1;
            }
        }

        public static int DeleteNodeAndInnerText(string path, string urlParent, string nodeName)
        {
            try
            {
                XmlDocument dom = new XmlDocument();
                dom.Load(path);
                XmlNode parent = dom.SelectSingleNode(urlParent);
                XmlNodeList children = parent.ChildNodes;
                foreach (XmlElement child in children)
                {
                    if (child.Name == nodeName)
                    {
                        child.RemoveAll();
                        parent.RemoveChild(child);
                    }
                }
                dom.Save(path);
                return 0;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
                return -1;
            }
        }

        public static int DeleteInnerText(string path, string urlParent, string nodeName)
        {
            try
            {
                XmlDocument dom = new XmlDocument();
                dom.Load(path);
                XmlNode parent = dom.SelectSingleNode(urlParent);
                XmlNodeList children = parent.ChildNodes;
                foreach (XmlElement child in children)
                {
                    if (child.Name == nodeName)
                    {
                        child.RemoveAll();
                    }
                }
                dom.Save(path);
                return 0;
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.ToString());
                return -1;
            }
        }

        private static void WriteErrorLog(string message)
        {
            CsvServer.Instance.WriteLine( DateTime.Now.ToString("yyyyMMdd") + ".txt",
                DateTime.Now + "——XMLServer:" + message);

        }
    }
}
