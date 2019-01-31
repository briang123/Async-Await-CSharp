using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    public class DoTask1 : IDoTaskAsync
    {
        private readonly int _delayInMilliseconds;
        private readonly int _taskId;
        private const string ClassIdentifier = "DoTask1RunAsync";

        public DoTask1(int taskId, int delayInMilliseconds)
        {
            _taskId = taskId;
            _delayInMilliseconds = delayInMilliseconds;
        }

        public async Task RunAsync()
        {
            await Utility.RunAsyncTask(ClassIdentifier, _taskId, _delayInMilliseconds);
        }
    }
}