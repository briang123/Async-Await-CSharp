using System;
using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    public class Utility
    {
        public static string StepStarted = "started";
        public static string StepDone = "done";

        public static string GetDate()
        {
            return DateTime.Now.ToString("hh:mm:ss.fff tt");
        }

        public static void RenderOutput(string taskName, string stepName, int num)
        {
            Console.WriteLine(@"{0} {1} {2} at {3}", taskName, stepName.ToLower(), num.ToString(), GetDate());
        }

        public static void RenderOutput(string taskName, string stepName)
        {
            Console.WriteLine(@"{0} {1} at {2}", taskName, stepName.ToLower(), GetDate());
        }

        public static int GetDelay(int ms)
        {
            return new Random(ms).Next(0, ms);
        }

        public static async Task RunAsyncTask(string classIdentifier, int num, int delayInMilliseconds)
        {
            RenderOutput(classIdentifier, StepStarted, num);

            var delay = GetDelay(delayInMilliseconds);
            await Task.Delay(delay);

            RenderOutput(classIdentifier, StepDone, num);
        }
    }
}