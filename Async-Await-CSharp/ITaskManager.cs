using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    public interface ITaskManager
    {
        void AddTaskAsync(IDoTaskAsync task);
        Task RunTasksAsync();
    }
}