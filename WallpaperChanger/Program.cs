using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quartz;
using Quartz.Impl;

namespace WallpaperChanger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CreateSchedule();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
           

        }

        private static void CreateSchedule()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            IJobDetail job = JobBuilder.Create<Update>().WithIdentity("Update").Build();

            var trigger = TriggerBuilder.Create().StartNow()
                .WithDailyTimeIntervalSchedule(s => s.WithIntervalInHours(24))
                .Build();
            sched.ScheduleJob(job, trigger);
        } 
    }
}
