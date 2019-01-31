using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    internal class TaskRunner
    {
        private static async Task SaveAll()
        {
            Utility.RenderOutput("TaskRunnerSaveAll", Utility.StepStarted);

            // simulate a save routine by adding a little delay
            await Task.Delay(2000);

            Utility.RenderOutput("TaskRunnerSaveAll", Utility.StepDone);
        }

        public static async Task ProcessAsync()
        {
            // use Command pattern to execute tasks
            var taskManager = new TaskManager();

            // iterate through and simulate 10 iterations of tasks to run
            for (var i = 1; i <= 5; i++)
            {
                taskManager.AddTaskAsync(new DoTask1(i, 5000));
                taskManager.AddTaskAsync(new DoTask2(i, 3000));
            }

            // create fake people and add to the tasks to run
            for (var i = 1; i <= 10; i++)
            {
                taskManager.AddTaskAsync(new FakePersonRepository(i, 200));
            }

            // wait until all tasks are done executing
            await Task.WhenAll(taskManager.RunTasksAsync());

            // simulate a batch save
            await SaveAll();
        }
    }
}