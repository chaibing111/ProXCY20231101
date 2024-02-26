using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    public static class PublicFunction
    {
        public static string stationInfo_path= AppDomain.CurrentDomain.BaseDirectory + "parm\\StationInfo.json";

        public static void MesMethodCheck(string methodName)
        {
            if (!DataUploadHelper.MyMes.MethodExist(methodName))
            {
                DataUploadHelper.MyMes.List_Method_Par.Add(new DataUploadHelper.Method_Par()
                {
                    MethodName = methodName,
                    NodeDic = new Dictionary<string, Dictionary<int, List<DataUploadHelper.Json_Par>>>()
                });
                DataUploadHelper.MyMes.MethodSave();
            }

        }


        public static void WorkOrderUpdata()
        {
            if (DataUploadHelper.MyMes.List_Mes_Par.Count <= 0) return;
            if(!DataUploadHelper.MyMes.PingIpOrDomainName(DataUploadHelper.MyMes.List_Mes_Par[0].url))
            {
                Log("服务器未在线，工单更新失败。");
                return;
            }
            MesMethodCheck("workOrderGet");

            string jsonTable = DataUploadHelper.MyMes.MesSend(0, "workOrderGet", "{}");
            if (jsonTable.IndexOf("{\"status\":\"NG\"") >= 0 || jsonTable.IndexOf("Error") >=0)
            {
                return;
            }
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonTable);

            if (dt.Rows.Count > 0)
            {
                StationInfo.ins.batBatch = dt.Rows[0]["WONo"].ToString();

                FileHelper.InfoSave(StationInfo.ins, PublicFunction.stationInfo_path);
            }
        }

        public static void ImageSave(Bitmap img,bool isOK)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "LogImage\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = path + "\\Img_" + DateTime.Now.ToString("yyyyMMddHHmmssff") + ".jpg";
                img.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                Log("图像保存出错" + ex.Message);
            }
        }


        public static void Log(string str)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "LogTmp\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = path + "Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string conTent = DateTime.Now.ToString("G") + ":" + str + Environment.NewLine;
            File.AppendAllText(fileName, conTent);
        }
    }
}
