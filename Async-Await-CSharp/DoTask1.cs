using System.Threading.Tasks;
using Async_Await_CSharp.Interfaces;

namespace Async_Await_CSharp
{
    public class DoTask1 : TaskBase, IDoTaskAsync
    {
        private readonly int _delayInMilliseconds;
        private readonly int _taskId;
        private const string ClassIdentifier = "DoTask1RunAsync";

        public DoTask1(int taskId, int delayInMilliseconds)
        {
            _taskId = taskId;
            _delayInMilliseconds = delayInMilliseconds;
        }

        public async Task<string> RunAsync()
        {
            return await RunAsyncTask(ClassIdentifier, _taskId, _delayInMilliseconds);
        }
    }
}