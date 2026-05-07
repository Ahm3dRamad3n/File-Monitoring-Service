using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace File_Monitoring_Service
{
    [RunInstaller(true)]
    public partial class MyServiceInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;
        public MyServiceInstaller()
        {
            InitializeComponent();

            serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "FileMonitoringService",
                DisplayName = "File Monitoring Service",
                Description = "Monitors a folder and moves new files to a destination folder.",
                StartType = ServiceStartMode.Manual,  // It's preferable to make it automatic if you use it frequently
                ServicesDependedOn = new string[] { "RpcSs", "EventLog", "LanmanWorkstation" } // FileSystemWatcher dependencies
                /**************
                * Should you write dependencies here, ensure that the dependent services are available on the target system before installation. 
                ***************/
            };

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);

        }
    }
}
