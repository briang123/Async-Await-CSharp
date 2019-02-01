using System.Collections.Generic;
using System.Threading.Tasks;
using Async_Await_CSharp.Interfaces;

namespace Async_Await_CSharp
{
    public class TaskManager : ITaskManager
    {
        private readonly List<IDoTaskAsync> _tasks = new List<IDoTaskAsync>();

        public void AddTaskAsync(IDoTaskAsync task)
        {
            _tasks.Add(task);
        }

        public async Task<bool> RunTasksAsync()
        {
            foreach (var task in _tasks)
            {
                await task.RunAsync();
            }

            _tasks.Clear();

            return _tasks.Count == 0;
        }
    }
}