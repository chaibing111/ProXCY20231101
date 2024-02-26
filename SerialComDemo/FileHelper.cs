using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SerialComDemo
{
    public static class FileHelper
    {
		[System.Runtime.InteropServices.DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
		[System.Runtime.InteropServices.DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

		/// <summary>
		/// deepcopy
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="t"></param>
		/// <returns></returns>
        public static T ClassDeepCopy<T>(T t)where T : new()
        {
            string jsontxt = JsonConvert.SerializeObject(t);
            if (string.IsNullOrEmpty(jsontxt)) return new T();
            var outt = JsonConvert.DeserializeObject<T>(jsontxt);
            return outt;
        }

        //[Serializable]
        public static T InfoRead<T>(string filePath) where T : new()
        {
                                                                                   
            string fileName = Path.GetFileName(filePath);
            string bankFullPath = AppDomain.CurrentDomain.BaseDirectory + "ParmBackup\\" + fileName;
            string str = FileReadAll(filePath);
            if (string.IsNullOrEmpty(str) || str.IndexOf("\0") >= 0 || str.Length < 2)
            {
                str = FileReadAll(bankFullPath);
            }
            try
            {
                if (string.IsNullOrEmpty(str) || str.IndexOf("\0") >= 0 || str.Length < 2)
                {
                    return new T(); // default(T);
                }
                else
                {
                    var t = JsonConvert.DeserializeObject<T>(str);
                    if (File.Exists(bankFullPath))
                    {
                        File.Delete(bankFullPath);
                    }
                    InfoSave(t, bankFullPath);
                    return t;
                }
            }
            catch (Exception ex)
            {
                //PublicLog.MsgShow(2003, true, false, false, filePath);
            }
            return new T(); // default(T);
        }
        public static void InfoSave<T>(T t, string filePath)
        {
            string str = JsonConvert.SerializeObject(t);
            if (str.IndexOf("\0") >= 0 || str.Length < 2 || str == "null")
            {
                //PublicLog.MsgShow(2002, true, false, false, filePath);
            }
            else
            {
                FileWriteAll(str, filePath);
            }
        }

        /// <summary>
        /// FileReadAll
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string FileReadAll(string filepath)
        {
            if (File.Exists(filepath))
            {
                return System.IO.File.ReadAllText(filepath);
            }
            else
            {
                return "";
            }
        }

		/// <summary>
		/// FileWriteAll
		/// </summary>
		/// <param name="txt"></param>
		/// <param name="filepath"></param>
		public static void FileWriteAll(string txt,string filepath)
        {
            if (!File.Exists(filepath))
            {
                string path = Path.GetDirectoryName(filepath);
                CreateFile(path);
            }
            try
            {
                File.WriteAllText(filepath, txt);
            }
            catch // (Exception ex)
            {
                //throw ex;
            }
            
        }

		/// <summary>
		/// FileWriteLine
		/// </summary>
		/// <param name="txt"></param>
		/// <param name="filepath"></param>
		public static void FileWriteLine(string txt,string filepath)
        {
            if (!File.Exists(filepath))
            {
                string path = Path.GetDirectoryName(filepath);
                CreateFile(filepath);
            }
            File.AppendAllText(filepath, txt);
        }

		/// <summary>
		/// DataTableToCSVstr
		/// </summary>
		/// <param name="dtData"></param>
		/// <returns></returns>
		public static string DataTableToCSVstr(System.Data.DataTable dtData)
        {
            StringBuilder sb = new StringBuilder();
            //标题
            for (int i = 0; i < dtData.Columns.Count; i++)
            {
                if (i == dtData.Columns.Count - 1)
                    sb.AppendLine(dtData.Columns[i].ColumnName);
                else
                    sb.Append(dtData.Columns[i].ColumnName + "\t");
            }
            //内容
            for (int j = 0; j < dtData.Rows.Count; j++)
                for (int i = 0; i < dtData.Columns.Count; i++)
                {
                    if (i == dtData.Columns.Count - 1)
                        sb.AppendLine(dtData.Rows[j][i].ToString());
                    else
                        sb.Append(dtData.Rows[j][i].ToString() + "\t");
                }
            return sb.ToString();
        }


		/// <summary>
		/// 写入数据至INI文件
		/// </summary>
		/// <param name="section"></param>
		/// <param name="key"></param>
		/// <param name="Value"></param>
		/// <param name="Path"></param>
		public static void WriteINI(string section, string key, string Value, string Path)
		{
			WritePrivateProfileString(section, key, Value, Path);
		}

		/// <summary>
		/// 从INI文件读取数据
		/// </summary>
		/// <param name="section"></param>
		/// <param name="key"></param>
		/// <param name="Standard"></param>
		/// <param name="Path"></param>
		/// <returns></returns>
		public static string GetINI(string section, string key, string Standard, string Path)
		{
			StringBuilder temp = new StringBuilder(255);
			GetPrivateProfileString(section, key, Standard, temp, 255, Path);
			return temp.ToString();
		}

		/// <summary>
		/// 查询文件路径是否存在，不存在则根据路径创建文件
		/// </summary>
		/// <param name="FilePath"></param>
		public static void CreateFile(string FilePath)
        {
            try
            {
                if (null == FilePath || string.IsNullOrEmpty(FilePath.Trim()))
                {
                    System.Windows.Forms.MessageBox.Show("CreateFile Path is Null");
                    return;
                }

                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("CreateFile error:" + ex.Message.ToString());
            }
        }

    }
}
