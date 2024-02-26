using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace sunyvpp
{
    class JsonHelper
    {
        public static readonly Lazy<JsonHelper> instance = new Lazy<JsonHelper>(() => new JsonHelper()); // new iniHelper();

        private JsonHelper()
        {
        }

        public static JsonHelper Instance { get; } = instance.Value;

        //写入一个Json文件
        public  void ObjectToJsonFile(string path,object o)
        {
            string str = JsonConvert.SerializeObject(o);
            using (FileStream fs=new FileStream(path,FileMode.Create) )
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(str);
                sw.Flush();
                sw.Close();
            }

        }
        //读取文件反序列化到对象
        public  object JsonFileToObject(string path, object o)
        {
            string str = string.Empty;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs);
                str = sr.ReadToEnd();
                sr.Close();
                return JsonConvert.DeserializeObject(str,o.GetType());
            }
        }
    }




}
