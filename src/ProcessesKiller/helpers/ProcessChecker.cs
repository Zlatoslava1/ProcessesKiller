using System;
using System.Diagnostics;
using System.ComponentModel;

namespace ProcessesKiller
{
    //This method receives as a parametr process name and time in minutes 
    // and kills that process if it runs longer than that time 
    public class ProcessChecker
    {
        public void KillProcess(string processName, string maxLifetimeInMinutes) 
        {
            Process[] processlist = Process.GetProcesses();
            long maxLifetimeInMinutesL = 1;
            try
            {
                maxLifetimeInMinutesL = Convert.ToInt64(maxLifetimeInMinutes);
            }
            catch (FormatException e)
            {
                Console.WriteLine($"maxLifetimeMinutes parametr is not string. Exception appeared {e}");
            }
            foreach (Process theprocess in processlist)
            {
                try
                {
                    DateTime currentTime = DateTime.Now;
                    DateTime startTime = theprocess.StartTime;
                    var runningTime = currentTime - startTime;
                    string name = theprocess.ProcessName;
                    if ((runningTime.TotalMinutes > maxLifetimeInMinutesL) & (name.Equals(processName)))
                    {
                        Console.WriteLine($"Process {processName} is killing cos it is running {runningTime.TotalMinutes:N1} minutes.");
                        theprocess.Kill();
                    }
                }
                catch (Win32Exception)
                {
                }
            }
        }
    }
}

