using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    public class StationInfo
    {
        //modBusWeld{"batNo":"0","palletCode":"0","modCode":"221026AH100723","weldTime":"2023-03-07 09:39:59","weldData":"L1","batBatch":"000",
        //"Software":"电芯上料","EquipmentCode":"MLNE002","SubEquipmentCode":"02","WorkStationCode":"02","parmCode1":"OP020-M-01-001"}
        public static StationInfo ins = new StationInfo();

        public string title = "BusBar焊接工站";
        public string Software = "BUSBAR焊接";    //软件名称
        public string EquipmentCode = "1234567890";  //设备编号
        public string SubEquipmentCode = "1";        //子设备编号
        public string WorkStationCode = "1";         //站点编号
        public string parmCode1 = "1245";           //参数编号
        public string batBatch = "";            //工单号
        public List<string> weldDocs = new List<string>();    //焊接文档


        public static void StationInfoInit()
        {
            string mesMgr_path = AppDomain.CurrentDomain.BaseDirectory + "SysData\\" + "MesMgr.json";
            string mesMethodMgr_path = AppDomain.CurrentDomain.BaseDirectory + "SysData\\" + "MesMethodMgr.json";
            DataUploadHelper.MyMes.MesMgr_path = mesMgr_path;
            DataUploadHelper.MyMes.MesMethodMgr_path = mesMethodMgr_path;
            DataUploadHelper.MyMes.MesInit();
            DataUploadHelper.MyMes.MethodInit();

            ins = FileHelper.InfoRead<StationInfo>(PublicFunction.stationInfo_path);
            PublicFunction.WorkOrderUpdata();
        }
    }
}
