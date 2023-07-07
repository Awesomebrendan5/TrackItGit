using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using CliWrap;
using System.IO;
using System.Reflection;

namespace TheTracker
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(String[] args)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(path);

            var exitCode = HostFactory.Run(x =>
            {
                x.Service<TheTrackerService>(s =>
                {
                    s.ConstructUsing(tracker => new TheTrackerService());
                    s.WhenStarted(tracker => tracker.Start());
                    s.WhenStopped(tracker => tracker.stop());
                });
                x.RunAsLocalSystem();
                x.SetServiceName("TrackItService");
                x.SetDisplayName("TrackIt Service");
                x.SetDescription("The TrackIt screentime tracker");
            });
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
