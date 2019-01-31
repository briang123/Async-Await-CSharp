using System;

namespace Async_Await_CSharp
{
    public class Program
    {
        public static void Main()
        {
            Utility.RenderOutput("ProcessTasks.ProcessAllAsync", Utility.StepStarted);

            ProcessTasks.ProcessAllAsync().Wait();

            Utility.RenderOutput("ProcessTasks.ProcessAllAsync", Utility.StepDone);

            Console.WriteLine(@"Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}