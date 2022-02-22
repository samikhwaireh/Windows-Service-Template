using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Windows_Service_Template
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // This will ensure that future calls to Directory.GetCurrentDirectory()
            // returns the actual executable directory and not something like C:\Windows\System32 
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Service1>(s =>
                {

                    s.ConstructUsing(service => new Service1());
                    // the start and stop methods for the service
                    s.WhenStarted(service => new Service1().Start());
                    s.WhenStopped(service => new Service1().Stop());

                });

                x.RunAsLocalSystem();

                //x.RunAs("username", "password"); // predefined user
                //x.RunAsPrompt(); // when service is installed, the installer will prompt for a username and password
                //x.RunAsNetworkService(); // runs as the NETWORK_SERVICE built-in account
                //x.RunAsLocalSystem(); // run as the local system account
                x.RunAsLocalService(); // run as the local service account

                //=> Service Instalation - These configuration options are used during the service instalation

                x.StartAutomatically(); // Start the service automatically
                                        //x.StartAutomaticallyDelayed(); // Automatic (Delayed) -- only available on .NET 4.0 or later
                                        //x.StartManually(); // Start the service manually
                                        //x.Disabled(); // install the service as disabled

                //=> Service Configuration

                //x.EnablePauseAndContinue(); // Specifies that the service supports pause and continue.
                //x.EnableShutdown(); //Specifies that the service supports the shutdown service command.

                //=> Service Dependencies
                //=> Service dependencies can be specified such that the service does not start until the dependent services are started.

                //x.DependsOn("SomeOtherService");
                //x.DependsOnMsmq(); // Microsoft Message Queueing
                //x.DependsOnMsSql(); // Microsoft SQL Server
                //x.DependsOnEventLog(); // Windows Event Log
                //x.DependsOnIis(); // Internet Information Server


                // Specify the base name, display name and description for the service, as it is registered in the services control manager.
                // This information is visible through the Windows Service Monitor
                x.SetDescription("Description");
                x.SetDisplayName("Displayname");
                x.SetServiceName("Service Name");

            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
