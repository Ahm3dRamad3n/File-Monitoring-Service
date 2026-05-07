using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace File_Monitoring_Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                FileMonitoringService service = new FileMonitoringService();
                Console.WriteLine("Starting service in interactive mode...");
                service.TestStartupAndStop(null);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new FileMonitoringService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
