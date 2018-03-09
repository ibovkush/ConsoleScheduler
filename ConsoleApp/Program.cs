using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                MapperInit.Init();

                Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };
                Scheduler.Runner.Instance.Start();
                Console.WriteLine("=================================");
                Console.WriteLine("Press 'Esc' to stop the scheduler");
                Console.WriteLine("=================================");

                var key = Console.ReadKey();

                while (key.Key != ConsoleKey.Escape)
                {
                    key = Console.ReadKey();
                }

                Scheduler.Runner.Instance.Stop();

                Console.WriteLine("Press any key to close the application");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Press any key to close the application");
                Console.ReadKey();
            }
        }
    }
}