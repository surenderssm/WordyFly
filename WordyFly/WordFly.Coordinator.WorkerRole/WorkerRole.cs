using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

namespace WordFly.Coordinator.WorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("WordFly.Coordinator.WorkerRole is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            Trace.TraceInformation("WordFly.Coordinator.WorkerRole has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WordFly.Coordinator.WorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WordFly.Coordinator.WorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            //while (!cancellationToken.IsCancellationRequested)
            //{
            while (true)
            {
                try
                {
                    Task[] Tasks = new Task[]{ 
                         Task.Factory.StartNew(() => GameCoordinator.StartCoordinating(), TaskCreationOptions.LongRunning)
                        ,Task.Factory.StartNew(() => Print(), TaskCreationOptions.LongRunning)
                        //,Task.Factory.StartNew(() => ArchiveStaleSessions(), TaskCreationOptions.LongRunning)
                        //,Task.Factory.StartNew(() => DynamicAllocationToSubGroups(), TaskCreationOptions.LongRunning)
                        //,Task.Factory.StartNew(() => ProcessEventHub(), TaskCreationOptions.LongRunning)
                        //,Task.Factory.StartNew(() => ClearLocalCache(), TaskCreationOptions.LongRunning)
                        };
                    Task.WaitAll(Tasks);
                }
                catch (Exception)
                {
                    Console.WriteLine("exc");                    
                }
               

            }
            //}
        }

        private static void Print()
        {
            long ctr = 0;
            int sleep = 10;
            while (true)
            {
                ctr++;
                Console.WriteLine("ctr"+ ctr);
                Thread.Sleep(sleep * 1000);
            }
        }
    }
}
