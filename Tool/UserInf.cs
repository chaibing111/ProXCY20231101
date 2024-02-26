using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    [Serializable]
    class UserInf
    {
        public UserInf()
        {
           ;//因为外面是直接使用这个对象，不需要创建，所以我们在这个地方必须直接实例化
        }
        public string Admin { get; set; }
        public string OP { get; set; }
        public string ENG { get; set; }
        public string Admin_PSW { get; set; }
        public string OP_PSW { get; set; }

        public string ENG_PSW { get; set; }
    }
}
