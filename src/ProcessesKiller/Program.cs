using System;

namespace ProcessesKiller
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                ActionRepeater actionRepeater = new ActionRepeater(args[0], args[1], args[2]);
                actionRepeater.RunActionRepeater();
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"Program required 3 parameters. Less paramenters were received: {e}");
            }

        }
    }
}
