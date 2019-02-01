using System;
using System.Threading.Tasks;
using Async_Await_CSharp.Helpers;
using Async_Await_CSharp.Interfaces;
using Async_Await_CSharp.Models;
using Async_Await_CSharp.Repositories;

namespace Async_Await_CSharp
{
    public class TaskRunner : ITaskRunner
    {
        public async Task<bool> SaveAll()
        {
            Console.WriteLine(Utility.GenerateOutputMessage("TaskRunnerSaveAll", Utility.StepStarted));

            // simulate a save routine by adding a little delay
            await Task.Delay(2000);

            Console.WriteLine(Utility.GenerateOutputMessage("TaskRunnerSaveAll", Utility.StepDone));

            return true;
        }

        public async Task ProcessAsync()
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
                var fakePerson = new FakePerson()
                {
                    FullName = Faker.Name.FullName()
                };

                taskManager.AddTaskAsync(new FakePersonRepository(i, 200, fakePerson));
            }

            // wait until all tasks are done executing
            await Task.WhenAll(taskManager.RunTasksAsync());

            // simulate a batch save
            await SaveAll();
        }

    }
}