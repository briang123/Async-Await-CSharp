using System;

namespace Async_Await_CSharp
{
    public class Utility
    {
        public static string StepStarted = "started";
        public static string StepDone = "done";

        public static string GetFormattedDate(DateTime date)
        {
            return date.ToString("hh:mm:ss.fff tt");
        }

        public static string GenerateOutputMessage(string taskName, string stepName)
        {
            return $"{taskName} {stepName.ToLower()} at {GetFormattedDate(DateTime.Now)}";
        }

        public static string GenerateOutputMessage(string taskName, string stepName, int num, string message)
        {
            return message != null ?
                $"{taskName} {stepName.ToLower()} {num} [Message: {message}] at {GetFormattedDate(DateTime.Now)}" :
                $"{taskName} {stepName.ToLower()} {num} at {GetFormattedDate(DateTime.Now)}";
        }

        public static int GetDelay(int ms)
        {
            return new Random(ms).Next(0, ms);
        }
    }
}