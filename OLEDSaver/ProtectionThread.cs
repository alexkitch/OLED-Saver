using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLEDSaver2
{
    public class ProtectionOptions
    {
        public double IntervalMins { get; set; }

        public double DurationSecs { get; set; }
    }

    public class ProtectionThread
    {
        private static ProtectionOptions options = new ProtectionOptions
        {
            IntervalMins = 5,
            DurationSecs = 10
        };      

        public static void Work(MainForm form)
        {
            try
            {
                while (Thread.CurrentThread.IsAlive)
                {
                    // Wait a while before beginning protection
                    Thread.Sleep(TimeSpan.FromMinutes(options.IntervalMins));

                    // Enable protection
                    form.Invoke((MethodInvoker)delegate
                    {
                        form.SetProtection(true);
                    });
                    Thread.Sleep(TimeSpan.FromSeconds(options.DurationSecs));

                    // Disable protection
                    form.Invoke((MethodInvoker)delegate
                    {
                        form.SetProtection(false);
                    });
                }
            }
            catch (ThreadAbortException e)
            {
                return;
            }
        }
    }
}
