using System.Diagnostics;
using System.IO;
using System.Threading;
using Xunit;

namespace ProcessesKiller.Tests
{
    public class ProcessCheckerTests
    {
        [Fact]
        //This is a positive test which checks that KillProcess method close specified process after specified time 
        public void KillProcessPositiveTest()
        {
            Process.Start("notepad.exe");
            Thread.Sleep(61000);
            string name = "notepad";
            string maxLifetimeInMinutesL = "1";
            int processCount = 0;
            ProcessChecker processChecker = new ProcessChecker();
            processChecker.KillProcess(name, maxLifetimeInMinutesL);
            Thread.Sleep(5000);
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName == name)
                {
                    processCount++;
                }

            }
            Assert.Equal(0, processCount);
        }

        [Fact]
        //This test checks that KillProcess metchod does not crash if there is not required process
        public void KillProcessNoProcessTest()
        {
            string name = "notepad";
            string maxLifetimeInMinutesL = "1";
            int unhandledExceptionCount = 0;
            try
            {
                ProcessChecker processChecker = new ProcessChecker();
                processChecker.KillProcess(name, maxLifetimeInMinutesL);
            }
            catch (IOException)
            {
                unhandledExceptionCount++;
            }
            Assert.True(unhandledExceptionCount == 0);
        }

        [Fact]
        //This test checks application does not crash if LifetimeInMinutes parametr is not string
        public void KillProcessInvalidTimeTest()
        {
            string name = "notepad";
            string maxLifetimeInMinutesL = "1se";
            int unhandledExceptionCount = 0;
            try
            {
                ProcessChecker processChecker = new ProcessChecker();
                processChecker.KillProcess(name, maxLifetimeInMinutesL);
            }
            catch (IOException)
            {
                unhandledExceptionCount++;
            }
            Assert.True(unhandledExceptionCount == 0);
        }

        [Fact]
        //This test checks application does not crach if parameters are empty
        public void KillProcessNoArgumentsTest()
        {
            int unhandledExceptionCount = 0;
            try
            {
                ProcessChecker processChecker = new ProcessChecker();
                processChecker.KillProcess("", "");
            }
            catch (IOException)
            {
                unhandledExceptionCount++;
            }
            Assert.True(unhandledExceptionCount == 0);
        }

        [Fact]
        //This test checks that application does not kill another processes
        public void KillProcessWithCorrectNameTest()
        {
            Process.Start("notepad.exe");
            string name1 = "notepad++";
            string name2 = "notepad";
            string maxLifetimeInMinutesL = "1";
            int processName2Count = 0;
            ProcessChecker processChecker = new ProcessChecker();
            processChecker.KillProcess(name1, maxLifetimeInMinutesL);
            Thread.Sleep(5000);
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                if (theprocess.ProcessName == name2)
                {
                    processName2Count++;
                }
            }
            //Killing process in the end of the test
            processChecker.KillProcess(name2, "0");
            Assert.True(processName2Count > 0);
        }
    }
}
