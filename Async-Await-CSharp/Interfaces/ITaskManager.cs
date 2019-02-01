using System.Threading.Tasks;

namespace Async_Await_CSharp.Interfaces
{
    public interface ITaskManager
    {
        void AddTaskAsync(IDoTaskAsync task);
        Task<bool> RunTasksAsync();
    }
}