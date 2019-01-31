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

        public static async Task ProcessTasksAsync()
        {
            // Create task list to store all our tasks we need to run
            var tasks = new List<Task>();

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

            // we will add our DoTask to the current list of Tasks using the command pattern
            tasks.Add(taskManager.RunTasksAsync());

            // when all worker data is retrieved and tasks are done
            await Task.WhenAll(tasks);

            // simulate a batch save
            await SaveAll();
        }
    }
}