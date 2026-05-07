using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace File_Monitoring_Service
{
    public partial class FileMonitoringService : ServiceBase
    {
        private string SourceFolder;
        private string DestinationFolder;
        private string LogsFolder;
        private string FileName = "FileMonitoringLog.txt";
        private FileSystemWatcher fileWatcher;

        public FileMonitoringService()
        {
            /*
             متي ينده هذا الكونستركتور؟
                هذا الكونستركتور يتم استدعاؤه عندما يتم إنشاء مثيل جديد من فئة FileMonitoringService.
                في سياق خدمة ويندوز، يتم استدعاء هذا الكونستركتور عندما يتم تثبيت الخدمة أو تشغيلها بواسطة نظام التشغيل.

             OnInstalling & OnStarting:
                عند تثبيت الخدمة، يتم استدعاء هذا الكونستركتور لإنشاء مثيل من الخدمة.
                عند بدء تشغيل الخدمة، يتم استدعاء هذا الكونستركتور أيضًا لإنشاء مثيل جديد من الخدمة.
            */
            InitializeComponent();

            try
            {
                LogsFolder = ConfigurationManager.AppSettings["LogsFolder"];
                if (string.IsNullOrEmpty(LogsFolder))
                {
                    string defaultLogsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLogs");
                    LogsFolder = defaultLogsPath;
                    if (!Directory.Exists(LogsFolder))
                    {
                        Directory.CreateDirectory(LogsFolder);
                    }
                    throw new Exception("LogsFolder is not configured. Using default path: " + defaultLogsPath);
                }
                else if (!Directory.Exists(LogsFolder))
                {
                    Directory.CreateDirectory(LogsFolder);
                }

                SourceFolder = ConfigurationManager.AppSettings["SourceFolder"];
                if (string.IsNullOrEmpty(SourceFolder))
                {
                    throw new Exception("SourceFolder is not configured.");
                }
                else if (!Directory.Exists(SourceFolder))
                {
                    Directory.CreateDirectory(SourceFolder);
                }

                DestinationFolder = ConfigurationManager.AppSettings["DestinationFolder"];
                if (string.IsNullOrEmpty(DestinationFolder))
                {
                    throw new Exception("DestinationFolder is not configured.");
                }
                else if (!Directory.Exists(DestinationFolder))
                {
                    Directory.CreateDirectory(DestinationFolder);
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Error reading configuration OR creating directories: {ex.Message}\n");
                throw new Exception("Service initialization failed due to configuration error.", ex);
            }
        }
        private void LogMessage(string message)
        {
            message = $"[{DateTime.Now}]: {message}";

            if (Environment.UserInteractive)
            {
                Console.WriteLine(message);
                return;
            }

            string logFilePath = Path.Combine(LogsFolder, FileName);
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(message);
            }

        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            LogMessage($"File detected: {e.FullPath}");

            try
            {
 
                string GUID = Guid.NewGuid().ToString();
                string destinationFileName = $"{GUID}{Path.GetExtension(e.Name)}";

                string sourceFilePath = e.FullPath;
                string destinationFilePath = Path.Combine(DestinationFolder, destinationFileName);

                File.Move(sourceFilePath , destinationFilePath);

                LogMessage($"File moved: {sourceFilePath} to {destinationFilePath}");
            }
            catch (Exception ex)
            {
                LogMessage($"Error moving file: {ex.Message}");
            }
        }

        protected override void OnStart(string[] args)
        {
 
            fileWatcher = new FileSystemWatcher
            {
                Path = SourceFolder,
                Filter = "*.*",
                // monitor only creation of files
                NotifyFilter = NotifyFilters.FileName,
                EnableRaisingEvents = true,
                IncludeSubdirectories = false // monitor only the specified folder, not subfolders
            };

            fileWatcher.Created += OnCreated;

            LogMessage("Service started. Monitoring folder: " + SourceFolder);
        }

        protected override void OnStop()
        {
            fileWatcher.EnableRaisingEvents = false;
            fileWatcher.Dispose();
            LogMessage("Service stopped.\n");
        }

        public void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.WriteLine("Press any key to stop the service...");
            Console.ReadKey();
            this.OnStop();
        }
    }
}
