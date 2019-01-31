using System;

namespace Async_Await_CSharp
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine(Utility.GenerateOutputMessage("ProcessTasks.ProcessAllAsync", Utility.StepStarted));

            var taskRunner = new TaskRunner();
            taskRunner.ProcessAsync().Wait();

            Console.WriteLine(Utility.GenerateOutputMessage("ProcessTasks.ProcessAllAsync", Utility.StepDone));

            Console.WriteLine(@"Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}