using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sunyvpp
{
    public class Delay
    {
        private int _startTime = 0;

        [DllImport("kernel32.dll")]
        public static extern int GetTickCount();

        public Delay()
        {
            _startTime = GetTickCount();
        }

        public void InitialTime()
        {
            _startTime = GetTickCount();
        }

        public bool TimeIsUp(int delayTime)
        {
            if (GetTickCount() - _startTime > delayTime)
            {
                return true;
            }
            return false;
        }
    }
}
