using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    internal class TaskManager : ITaskManager
    {
        private readonly List<IDoTaskAsync> _tasks = new List<IDoTaskAsync>();

        public void AddTaskAsync(IDoTaskAsync task)
        {
            _tasks.Add(task);
        }

        public async Task RunTasksAsync()
        {
            foreach (var task in _tasks)
            {
                await task.RunAsync();
            }

            _tasks.Clear();
        }
    }
}