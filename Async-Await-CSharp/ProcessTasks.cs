using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Await_CSharp
{

    internal class ProcessTasks
    {
        private static async Task SaveAll()
        {
            Utility.RenderOutput("ProcessTasksSaveAll", Utility.StepStarted);

            await Task.Delay(2000);

            Utility.RenderOutput("ProcessTasksSaveAll", Utility.StepDone);
        }

        public static async Task ProcessAllAsync()
        {
            // Create list and get worker data
            var tasks = new List<Task> { FakeWorkerRepository.GetFakeWorkersAsync() };

            // iterate through and simulate 10 iterations of tasks to run
            for (var i = 1; i <= 10; i++)
            {
                tasks.Add(new DoTask1().RunAsync(i, 2000));
                tasks.Add(new DoTask2().RunAsync(i, 5000));
            }

            // when all worker data is retrieved and tasks are done
            await Task.WhenAll(tasks);

            // simulate a batch save
            await SaveAll();
        }
    }
}