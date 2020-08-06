using System;
using System.Timers;


namespace ProcessesKiller
{
    public class ActionRepeater
    {
        public ActionRepeater(string processName1, string maxLifetimeInMinutes1, string monitoringFrequency1)
        {
            processName = processName1;
            maxLifetimeInMinutes = maxLifetimeInMinutes1;
            monitoringFrequency = monitoringFrequency1;
        }
        private static System.Timers.Timer aTimer;
        static string processName;
        static string maxLifetimeInMinutes;
        string monitoringFrequency;
        public void RunActionRepeater()
        {
            SetTimer(maxLifetimeInMinutes);
            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();

        }
        private static void SetTimer(string maxLifetimeInMinutes)
        {
            long maxLifetimeInMiliSeconds = 100500;
            try
            {
                maxLifetimeInMiliSeconds = Convert.ToInt64(maxLifetimeInMinutes) * 60 * 1000;
            }
            catch (FormatException e)
            {
                Console.WriteLine($"maxLifetimeMinutes parametr is not string. Exception appeared {e}");
            }
            aTimer = new System.Timers.Timer(maxLifetimeInMiliSeconds);
            KillingProcess();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void KillingProcess()
        {
            ProcessChecker processChecker = new ProcessChecker();
            processChecker.KillProcess(processName, maxLifetimeInMinutes);
            Console.WriteLine("Procesess checked.");
        }
        private static void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            KillingProcess();
        }
    }
}