using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    public class DoTask1 : IDoTaskAsync
    {
        private const string ClassIdentifier = "DoTask1RunAsync";

        public async Task RunAsync(int num, int delayInMilliseconds)
        {
            await Utility.RunAsyncTask(ClassIdentifier, num, delayInMilliseconds);
        }
    }
}