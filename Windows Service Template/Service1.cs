using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Windows_Service_Template
{
    internal class Service1
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private Timer timer;
        private int Interval = 30 * 1000;

        public Service1()
        {
            timer = new Timer();
            timer.Interval = Interval;
            timer.Elapsed += elasped;
        }

        public void Start()
        {
            timer.Start();
            logger.Info("Service Started!");
        }

        public void Stop()
        {
            timer.Stop();
            logger.Info("Service Stoped!");
        }

        private void elasped(object sender, ElapsedEventArgs e)
        {
            try
            {
                //TODO: Implement your service start routine.
                logger.Info("Service has terminated!");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}
