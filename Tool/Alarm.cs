using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    public class Alarm
    {
        public static void Main_PLC_Alarm(bool[] b_State)
        {
            try
            {
                if (b_State.Length > 0)
                {
                    for (int i = 0; i < b_State.Length; i++)
                    {
                        if (b_State[i])
                        {
                            //处理有报警的状态if (b State[il)
                            Main_PLC_State(i + 1);
                        }
                    

                    }
                }
            }
            catch (Exception e)
            {
            }
         
        }
        /// <summary>
        /// PLC每个点的位的状态
        /// </summary>
        /// <param name="bitNum"></param>
        public static void Main_PLC_State(int bitNum)
        {
            switch (bitNum)
            {
                case 1:
                    Globals.LogRecord("【Main_PLC：M1201急停报警" + "】", true);
                    break;








            }

        }



    }
}
