using System;
using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    public class TaskBase : ITaskBase
    {
        public async Task RunAsyncTask(
            string classIdentifier, int num, int delayInMilliseconds, string message = null)
        {
            Console.WriteLine(
                Utility.GenerateOutputMessage(
                    classIdentifier,
                    Utility.StepStarted,
                    num,
                    message)
            );

            await Task.Delay(Utility.GetDelay(delayInMilliseconds));

            Console.WriteLine(
                Utility.GenerateOutputMessage(
                    classIdentifier,
                    Utility.StepDone,
                    num,
                    message)
            );

            Console.WriteLine("");
        }
    }
}