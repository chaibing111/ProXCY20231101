using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    //http://124.71.225.135/wcfhttpservice
    //http://124.71.225.135/wcfhttpservice?wsdl
    /*
     接口详细释义：
        方法名：SendMessage 
        CommandId--接口名字
        MessageGuid--每次生成一个GUID
        RequestDate--当前日期
        CommandRequestJson--具体的报文内容
     格式化：
    {
	   "MessageGuid": "7c5f4159-1fff-4738-abb1-c433c43a279d",
	   "RequestDate": "2021-04-23T00:23:14.5088Z",
	   "CommandId": "EqptRun",
	   "CommandRequestJson": ""
    }
    */

    public class MessageObj
    {
        public string MessageGuid = Guid.NewGuid().ToString();
        public string RequestDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffffZ");      //2021-04-23T00:23:14.5088Z  //ToUniversalTime(). //这种格式是 英国时间( +0 时区 ). 比中国时间少了8个小时. 中国是+8时区
        public string CommandId = "EqptRun";
        public string CommandRequestJson = "{}";
    }

    public class MessageRequestObj
    {
        public string CommandId = "";
        public string CommandResponseJson = "";
        public string ErrorCode = "";
        public string ErrorMessage = "";
        public string MessageGuid = "";
        public string ResponseDate = "";
        public bool Success = true;
        
    }


    /// <summary>
    /// 1.	工单申请
    /// </summary>
    public class WipOrderRequest
    {
        public string Software = "";       //上位机软件名称
        public string EquipmentCode = "";       //设备编号
        public string WorkStationCode = "";     //站点编号
    }

    public class WipOrderRequestMsg
    {
        public bool ResultFlag = true;              //结果
        public string Code = "0";                   //
        public string MOMMessage = "";              //消息
        public List<WipOrderBase> WipOrder = new List<WipOrderBase>(); //工单信息信息  value:WipOrderNo:工单号
    }

    public class WipOrderBase
    {
        public string WipOrderNo = "";          //工单号
        public string FactoryCode = "";          //工厂代码
        public string PlantType = "";          //类型 模组/PACK
        public string CellType = "";          //电芯类别
        public string CellSupplyType = "";          //
        public string ProjectCode = "";          //工程代码
        public string VehicleType = "";          //
        public string ManufacturerCode = "";          //
        public string ModuleType = "";          //模组类别
    }

    /// <summary>
    /// 2.	工艺路线申请
    /// </summary>
    public class ProcessRequest
    {
        public string Software = "";            //上位机软件名称
        public string EquipmentCode = "";       //设备编号
        public string WorkStationCode = "";     //站点编号
        public string WipOrderNo = "";          //工单号
    }

    public class ProcessRequestMsg
    {
        public bool ResultFlag = true;          //结果
        public string Code = "0";                   //
        public string MOMMessage = "";          //消息
        public string ProductDesc = "";         //产品名称
        public string FirstArticleNum = "";       //首件数量
        public string DebugNum = "";            //调机数量
        public string ParamVersion = "";       //参数版本
        public bool ParamRefreshFlag = true;          //是否需要读取参数
        public List<ProcessInfoBase> ProcessInfo = new List<ProcessInfoBase>();             //工序参数
        public List<OprsequencenoInfoBase> OprsequencenoInfo = new List<OprsequencenoInfoBase>();  //工位信息
    }

    public class ProcessInfoBase
    {
        public string EquipmentCode = "";            //设备编号
        public string ProcessCode = "";             //工序编号
        public List<ParameterInfoBase> ParameterInfo = new List<ParameterInfoBase>();       //参数信息
    }
    public class ParameterInfoBase
    {
        public string ParameterCode = "";               //参数编码
        public string ParameterType = "";               //参数类型
        public string TargetValue = "";                 //目标值
        public string UOMCode = "";                     //单位
        public string UpperControlLimit = "";            //控制上限值
        public string LowerControlLimit = "";             //控制下限值
        public string UpperSpecificationsLimit = "";            //规格上限值
        public string LowerSpecificationsLimit = "";             //规格下限值
        public string UpperLimit = "";                      //上限值
        public string LowerLimit = "";                      //上限值
        public string Description = "";                     //参数描述

    }

    public class OprsequencenoInfoBase
    {
        public string WorkCenter = "";     //
        public string Oprsequenceno = "";  //
        public string EquipmentCode = "";  //
    }


    /// <summary>
    /// 3.	工单BOM申请流程
    /// </summary>
    public class WipOrderBomRequest
    {
        public string Software = "";       //上位机软件名称
        public string EquipmentCode = "";       //设备编号
        public string WipOrderNo = "";       //工单号
    }

    public class WipOrderBomRequestMsg
    {
        public bool ResultFlag = true;              //结果
        public string Code = "";                    //
        public string MOMMessage = "";              //消息
        public List<ProcessInfoBase2> ProcessInfo = new List<ProcessInfoBase2>();             //工序信息
        

    }

    public class ProcessInfoBase2
    {
        public string EquipmentCode = "";            //设备编号
        public string ProcessCode = "";             //工序编号
        public List<MaterialInfoBase> MaterialInfo = new List<MaterialInfoBase>();             //物料信息
    }
    public class MaterialInfoBase
    {
        public string ProductNo = "";               //物料编码
        public string ProductName = "";               //物料名称
        public string ProductType = "";                 //物料类型代码
        public string Location = "";                     //安装位置
        public string Quantity = "";            //系统数量
        public string UomCode = "";             //单位
        public string AvailableFlag = "";            //是否可用
        public string AvailableMessage = "";             //信息

    }

    /// <summary>
    /// 4.	工序信息回传MES,ModuleOutput/PACKOutput
    /// </summary>
    public class Module_PACKOutput
    {
        public string Software = "";       //上位机软件名称
        public string EquipmentCode = "";       //设备编号
        public List<SerialNosBase> SerialNos = new List<SerialNosBase>();       //模组/PACK号组
    }

    public class SerialNosBase
    {
        public string SerialNo = "";        //模组/PACK号
        public string ProductType = "";        //AB电芯标识
        public string PassFlag = "";           //电芯的OK和NG
        public List<ProcessInfoBase3> ProcessInfo = new List<ProcessInfoBase3>();       //工序信息
        public List<PartInfoBase> PartInfo = new List<PartInfoBase>();        //零部件信息  {PartID} //零部件流水号（唯一码）
        public List<MaterialInfoBase2> MaterialInfo = new List<MaterialInfoBase2>();      //物料信息
        public List<ParameterInfoBase2> Parameters = new List<ParameterInfoBase2>();          //参数
        public List<EquipmentInfosBase> EquipmentInfos = new List<EquipmentInfosBase>();        //设备信息
    }

    public class PartInfoBase
    {
        public string PartID = "";                  //零部件流水号（唯一码）
    }

    public class MaterialInfoBase2
    {
        public string LabelNo = "";               //物料流水号（标签号）
        public string Quantity = "";               //已经消耗的物料数量
    }

    public class ProcessInfoBase3
    {
        public string EquipmentCode = "";       //设备编号
        public string ProcessCode = "";         //工序编号
        public string StartDate = "";           //入站时间  (北京时间, 格式yyyy-mm-dd hh:mm:ss)
        public string EndDate = "";           //入站时间  (北京时间, 格式yyyy-mm-dd hh:mm:ss)
    }

    public class ParameterInfoBase2
    {
        public string ParameterCode = "";               //参数编码
        public string ParameterType = "";               //参数类型
        public string TargetValue = "";                 //目标值
        public string UOMCode = "";                     //单位
        public string UpperControlLimit = "";            //控制上限值
        public string LowerControlLimit = "";             //控制下限值
        public string UpperSpecificationsLimit = "";            //规格上限值
        public string LowerSpecificationsLimit = "";             //规格下限值
        public string UpperLimit = "";                      //上限值
        public string LowerLimit = "";                      //上限值
        public string Value = "";                           //值
        public string ParameterResult = "";                      //返回值
        public string Description = "";                     //参数描述

    }



    public class ModuleOutputMsg
    {
        public bool ResultFlag = true;              //结果
        public string Code = "";                    //
        public string MOMMessage = "";              //消息
        public List<SerialNos2Base> SerialNos = new List<SerialNos2Base>();       //模组/PACK号组
    }

    public class SerialNos2Base
    {
        public string SerialNo = "";              //模组/PACK号
        public string Result = "";              //结果
        public string SerialMessage = "";              //模组/PACK信息
        public string SerialNoCode = "";            //
    }

    /// <summary>
    /// 5.	不良信息回传MES CellNGOutput/ModuleReworkOutput/PACKReworkOutput
    /// </summary>
    public class NGReOutput
    {
        public string Software = "";       //上位机软件名称
        public string EquipmentCode = "";       //设备编号
        public string SubEquipmentCode="";       //子设备设备编号
        public List<SerialNos3Base> SerialNos = new List<SerialNos3Base>();             //模组/PACK号组
        public List<EquipmentInfosBase> EquipmentInfos = new List<EquipmentInfosBase>();        //设备信息
    }

    public class SerialNos3Base
    {
        public string SerialNo = "";        //模组/PACK号
        public string ProductType = "";        //AB电芯标识
        public string PassFlag = "";           //电芯的OK和NG
        public List<ProcessInfoBase4> ProcessInfo = new List<ProcessInfoBase4>();             //工序信息        
        
    }


    public class ProcessInfoBase4
    {
        public string EquipmentCode = "";       //设备编号
        public string ProcessCode = "";         //工序编号
        public string StartDate = "";           //入站时间(北京时间, 格式yyyy - mm - dd hh: mm: ss)
        public string EndDate = "";             //出站时间(北京时间, 格式yyyy - mm - dd hh: mm: ss)
        public string NGDescription = "";       //NG描述
        public List<MaterialInfoBase2> MaterialInfo = new List<MaterialInfoBase2>();      //物料信息
        public List<ParameterInfoBase2> ParameterInfo = new List<ParameterInfoBase2>();          //参数

    }

    public class EquipmentInfosBase
    {
        public string EquipmentCode = "";       //设备编号
        public string StartDate = "";           //入站时间  (北京时间, 格式yyyy-mm-dd hh:mm:ss)
        public string EndDate = "";           //入站时间  (北京时间, 格式yyyy-mm-dd hh:mm:ss)
        public List<EquipmentBasicInfosBase> EquipmentBasicInfos = new List<EquipmentBasicInfosBase>();           //设备上位机缓存离线数据  EquipmentBasic:设备上位机缓存离线数据明细
    }

    public class EquipmentBasicInfosBase
    {
        public string EquipmentBasic = "";      //EquipmentBasic:设备上位机缓存离线数据明细
    }

    public class NGReOutputMsg
    {
        public bool ResultFlag = true;              //结果
        public string Code = "";                    //
        public string MOMMessage = "";              //消息
        public List<SerialNoBase2> SerialNos = new List<SerialNoBase2>();
    }

    public class SerialNoBase2
    {
        public string SerialNo = "";        //模组/PACK号
        public string Result = "";        //结果
        public string SerialNoMessage = "";        //模组/PACK信息
        public string SerialNoCode = "";        //
    }

    /// <summary>
    /// 6.	单电芯属性获取 CellState
    /// {"Software":"誉辰 2022-06-17","EquipmentCode":"P06GW0101","SerialNo":"0H9CB052B21023C6F0000042"} 
    /// 接口功能：1.返回电芯的电芯号，电芯状态   2.MES进行电芯与栈板、托盘解绑流程
    /// </summary>
    public class ModuleCellState
    {
        public string Software = "";       //上位机软件名称
        public string EquipmentCode = "";       //设备编号
        public string SerialNo = "";             //电芯号
    }

    public class ModuleCellStateMsg
    {//{"ResultFlag":true,"MOMMessage":"OK","SerialNo":"0H9CB052B21023C6F0000042","SerialNoStatus":"1"} 
        public bool ResultFlag = true;       //电芯状态
        public string Code = "";                //
        public string MOMMessage = "OK";       //消息
        public string SerialNo = "";             //电芯号
        public string SerialNoStatus = "";      //电芯状态  1:OK   2:NG  4:REWORK
    }

    public class ModuleTrayCellsStatus
    {
        public string Software = "";       //上位机软件名称
        public string EquipmentCode = "";       //设备编号
        public string TrayBarcode = "";             //TrayBarcode
    }

    public class ModuleTrayCellsStatusMsg
    {//{"ResultFlag":true,"MOMMessage":"OK","SerialNo":"0H9CB052B21023C6F0000042","SerialNoStatus":"1"} 
        public bool ResultFlag = true;       //电芯状态
        public string Code = "";                //
        public string MOMMessage = "OK";       //消息
        public string TrayBarcode = "";             //电芯号
        public List<SerialNoBase3> SerialNos =new List< SerialNoBase3>();      
    }

    public class SerialNoBase3
    {
        public string SerialNo = "";            //
        public string PositionNo = "";          //
        public string SerialNoStatus = "";      //
        public string SerialNoMessage = "";     // 模组/PACK信息",
        public string SerialNoCode = "";
    }


    /// <summary>
    /// 7.	整盘电芯属性获取 TrayCellsStatus
    /// {"Software":"TPOCVSoft","EquipmentCode":"P06HC2101","TrayBarcode":"ZN180 072HC000213"}  
    /// 接口功能：1.返回盘内电芯的电芯号，通道号，及电芯状态   2.MES进行电芯与栈板、托盘解绑流程
    /// </summary>
    public class TrayCellsStatus
    {
        public string Software = "";       //上位机软件名称
        public string EquipmentCode = "";       //设备编号
        public string TrayBarcode = "";             //托盘号
    }

    public class TrayCellsStatusMsg
    {//{"ResultFlag":true,"MOMMessage":"OK","TrayBarcode":null,"SerialNos":[{"SerialNo":"0H9CE054B41023C810000550","PositionNo":"26","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810000699","PositionNo":"37","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810000941","PositionNo":"4","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810000946","PositionNo":"24","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810000962","PositionNo":"40","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001219","PositionNo":"46","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001261","PositionNo":"28","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001321","PositionNo":"38","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001323","PositionNo":"20","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001362","PositionNo":"10","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001374","PositionNo":"13","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001377","PositionNo":"15","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001387","PositionNo":"36","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001390","PositionNo":"34","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001400","PositionNo":"6","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001401","PositionNo":"5","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001408","PositionNo":"47","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001420","PositionNo":"45","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001442","PositionNo":"18","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001452","PositionNo":"31","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001454","PositionNo":"29","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001467","PositionNo":"27","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001470","PositionNo":"25","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001479","PositionNo":"12","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001481","PositionNo":"19","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001482","PositionNo":"23","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001483","PositionNo":"21","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001485","PositionNo":"17","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001490","PositionNo":"22","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001491","PositionNo":"9","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001492","PositionNo":"11","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001494","PositionNo":"14","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001495","PositionNo":"16","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001501","PositionNo":"41","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001502","PositionNo":"43","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001508","PositionNo":"3","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001509","PositionNo":"1","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001521","PositionNo":"35","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001522","PositionNo":"33","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001523","PositionNo":"39","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001526","PositionNo":"7","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001527","PositionNo":"8","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001529","PositionNo":"32","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001530","PositionNo":"30","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B41023C810001549","PositionNo":"44","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B42023C7Y0000825","PositionNo":"2","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B42023C810000090","PositionNo":"42","SerialNoStatus":"2"},{"SerialNo":"0H9CE054B42023C810000096","PositionNo":"48","SerialNoStatus":"2"}]} 
        public bool ResultFlag = true;       //电芯状态
        public string MOMMessage = "OK";       //消息
        public string TrayBarcode = null;             //托盘号
        public List<SerialNoBase> SerialNos = new List<SerialNoBase>();             //电芯号组
        public string SerialNoStatus = "";      
    }
    public class SerialNoBase
    {
        public string SerialNo = "";        //电芯号
        public string PositionNo = "0";
        public string SerialNoStatus = "0";     //电芯状态  1:OK   2:NG  4:REWORK
    }



    /// <summary>
    /// 10.	气密数据采集 LeakTestData
    /// </summary>
    public class LeakTestData
    {
        public string Station = "";                         // 站点
        public string Instrument = "";                       // 仪器名称
        public string Fixture = "";                          // 夹具名称
        public string LineID = "";                          // 产线
        public string Operator = "";                         // 操作员
        public string WorkOrder = "";                       // 工单
        public string StationNo = "";                       // SN
        public string TestItem = "";                         // 测试项目
        public string SubTestItem = "";                      // 子测试项目（低压，高压）
        public string LowLimit = "";                        // 压力下限
        public string HighLimit = "";                        // 压力上限
        public string TestValue = "";                        // 测试结果值
        public string Unit = "";                             // 测试单位
        public string Status = "";                           // 状态结果
        public string TimeElapsed = "";                      // 耗时
        public DateTime TestTime = DateTime.Now;             // 测试时间
        public string ErrorMSG = "";                         // 错误信息
    }


}
