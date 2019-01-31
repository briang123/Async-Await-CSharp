using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    public class DoTask2 : IDoTaskAsync
    {
        private const string ClassIdentifier = "DoTask2RunAsync";

        public async Task RunAsync(int num, int delayInMilliseconds)
        {
            await Utility.RunAsyncTask(ClassIdentifier, num, delayInMilliseconds);
        }
    }
}