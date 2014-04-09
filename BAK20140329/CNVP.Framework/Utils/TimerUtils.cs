using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CNVP.Framework.Utils
{
    public class TimerUtils
    {
        Timer m_objTimer;
        int m_nTimes;
        Boolean m_bIsWork;
        const int MAX_TIMES = 60;

        public TimerUtils()
        {
            m_objTimer = new Timer();
            m_objTimer.Tick += new EventHandler(m_objTimer_Tick);
        }

        void m_objTimer_Tick(object sender, EventArgs e)
        {
            m_nTimes++;
        }

        public void Wait(int nSec)
        {
            StartTime();
            Application.DoEvents();
            while (m_nTimes < nSec)
            {
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
                if (m_objTimer.Enabled == false)
                    break;
            }
            StopTime();
        }

        public void CancelWait()
        {
            StopTime();
        }

        private void StartTime()
        {
            m_bIsWork = true;
            m_nTimes = 0;
            m_objTimer.Interval = 1000;
            m_objTimer.Enabled = true;
            m_objTimer.Start();
        }

        private void StopTime()
        {
            m_bIsWork = false;
            m_objTimer.Stop();
            m_objTimer.Enabled = false;
        }
    }
}