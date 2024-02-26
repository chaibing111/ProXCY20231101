using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    public class AIANworkOrder
    {
        public string WONo = "";  //工单
        public string FCode = "064";   //工厂代码  广汽064
        public string PType = "M";   //产品类别  模组M  PACK:P
        public string CType = "E";   //电芯类型 三元E，铁锂B
        public string CSType = "00";   //自研00
        public string PCode = "A190";   //项目号A190
        public string VType = "H";       //车型H  P
        public string MCode = "0";        //广汽研发生产 0
        public string MType = "";           //模组区分 A / B   pack为数字

        public string STime = "";           //开始时间
        public string UTime = "";           //更新时间
        public string WType = "OPEN";               //工单状态  0：CLOSE，1：OPEN，2：PAUSE
    }


}
