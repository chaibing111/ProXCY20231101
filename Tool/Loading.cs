using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sunyvpp
{

    public class Loading
    {
        private Thread _ShowThread = null;
        private FrmBar _Loadfrm = new FrmBar();
        public Loading()
        {
            //    ParentFrm.Visible = true;
        }

        public void ShowLoading()
        {
            if (_ShowThread == null)
                _ShowThread = new Thread(new ThreadStart(LoadingProcess));
            if (!_ShowThread.IsAlive)
                _ShowThread.Start();
        }

        private void LoadingProcess()
        {
            try
            {

                _Loadfrm.ShowDialog();
            }
            catch (Exception e)
            {
                ;
            }
        }

        public void SetMessage(string message)
        {
            _Loadfrm.SetMessage(message);
        }

        public void SetProcess(int value)
        {
            _Loadfrm.SetProcess(value);
        }

        public void EndLoading()
        {
            try
            {
               
            }
            catch (Exception e)
            {

            }
       finally
            {
                Thread.Sleep(500);
                _Loadfrm.EndProcess();
                if (_ShowThread != null)
                    _ShowThread.Abort();
            }
        }
    }




}
