using System;
using System.Threading.Tasks;
using Async_Await_CSharp.Helpers;
using Async_Await_CSharp.Interfaces;

namespace Async_Await_CSharp
{
    public class TaskBase : ITaskBase
    {
        public async Task<string> RunAsyncTask(
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

            return $"{classIdentifier}:{num.ToString()}{(message != null ? ":" + message : "")}";
        }
    }
}