using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    internal interface ITaskManager
    {
        void AddTaskAsync(IDoTaskAsync task);
        Task RunTasksAsync();
    }
}