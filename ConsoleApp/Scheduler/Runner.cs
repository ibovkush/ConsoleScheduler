using ConsoleApp.Scheduler.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Scheduler
{
    internal sealed class Runner
    {
        static readonly Lazy<Runner> lazy = new Lazy<Runner>(() => new Runner(), true);
        public static Runner Instance => lazy.Value;

        private IScheduler scheduler;
        private int delay;

        private Runner() {
          
            var factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler();

            delay = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SchedulerDelayInMinutes"] ?? "1");
        }

        public void Start()
        { 
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<DownloadJob>()
                .WithIdentity("downloader", "simpleGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("periodicalTrigger", "simpleGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(delay)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger);
        }

        public void Stop()
        {
            scheduler.Shutdown();
        }

    }
}
