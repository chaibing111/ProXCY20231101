using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    class iniHelper
    {

        //程序运行时创建一个静态只读的辅助对象
        private readonly static object lockCard = new object();
        public static readonly Lazy<iniHelper> instance = new Lazy<iniHelper>(()=> new iniHelper());// new iniHelper();
        private iniHelper() { }
        //private static iniHelper instance = new iniHelper();
        public static iniHelper Instance { get; } = instance.Value;
        //public static iniHelper Instance { get { return GetInstance(); } }
        /// <summary>
        /// 懒汉式单例模式
        /// </summary>
        /// <returns></returns>
        //private static iniHelper GetInstance()
        //{
        //    //第一重判断，先判断实例是否存在，不存在再加锁处理
        //    if (null != instance)
        //    {
        //        //加锁的程序在某一时刻只允许一个线程访问
        //        lock (lockCard)
        //        {
        //            //第二重判断
        //            if (null != instance)
        //            {
        //                instance = new iniHelper();//创建单例实例
        //            }
        //        }

        //    }
        //    return instance;
        //}


        #region 功能声明变量
        public string path = System.AppDomain.CurrentDomain.BaseDirectory + "Save_File.ini";
        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        /// <summary>
        /// 自定义读取INI文件中的内容方法
        /// </summary>
        /// <param name="Section">键</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        private string ContentValue(string Section, string key, string strFilePath)
        {

            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            return temp.ToString();
        }
        #endregion


        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public  void SaveConfig(string key,string value)
        {
            WritePrivateProfileString("information", key, value, path);
            //直接获取系统当前路径
            //string path=System.AppDomain.CurrentDomain.BaseDirectory+"Save_File.ini";
            //try
            //{
            //    //WritePrivateProfileString("information","name",this.textBox1.Text,path);
            //    WritePrivateProfileString("information", "name", this.textBox1.Text, path);
            //    WritePrivateProfileString("information", "sex", this.textBox2.Text, path);
            //    WritePrivateProfileString("information", "age", this.textBox3.Text, path);
            //    WritePrivateProfileString("information", "address", this.textBox4.Text, path);
            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message.ToString());
            //}
        }
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string  ReadConfig(string key)
        {

            if (File.Exists(path))
            {
                return  ContentValue("information", key, path);

                //if (File.Exists(path))
                //{
                //    this.textBox1.Text = ContentValue("information", "name", path);
                //    this.textBox2.Text = ContentValue("information", "sex", path);
                //    this.textBox3.Text = ContentValue("information", "age", path);
                //    this.textBox4.Text = ContentValue("information", "address", path);

                //}
                //else
                //{
                //    MessageBox.Show("");
                //}
            }
            else
            {
                return "error";
            }

        }




    }
}
