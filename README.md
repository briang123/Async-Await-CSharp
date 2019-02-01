# Async-Await-CSharp

An Async/Await example in C# where we have a console application startup method calling the ```TaskRunner.ProcessAsync()``` static method to initiate and kick off the async Tasks.

Not all classes are shown below, but just a subset, for brevity.

```C#
public class Program
{
  public static void Main()
  {
      Console.WriteLine(Utility.GenerateOutputMessage("ProcessTasks.ProcessAllAsync", Utility.StepStarted));

      TaskRunner.ProcessAsync().Wait();

      Console.WriteLine(Utility.GenerateOutputMessage("ProcessTasks.ProcessAllAsync", Utility.StepDone));

      Console.WriteLine(@"Press any key to continue...");
      Console.ReadKey(true);
  }
}
```

The TaskRunner class is responsible for setting up the tasks to be run using the Command design pattern.
```c#
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
```

The TaskManager class accepts a task to manage, then provides a method to run all the tasks in an async manner.
```C#
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
```

An example of one of the Tasks classes (DoTask1 and DoTask2 are identical)
```C#
public interface IDoTaskAsync
{
    Task<string> RunAsync();
}

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
```

Here is where we fetch our Fake person data...
```c#
public class FakePersonRepository : TaskBase, IDoTaskAsync
{
    private readonly int _delayInMilliseconds;
    private readonly IFakePerson _fakePerson;
    private readonly int _taskId;
    private const string ClassIdentifier = "FakePersonRepository";

    public FakePersonRepository(int taskId, int delayInMilliseconds, IFakePerson fakePerson)
    {
        _taskId = taskId;
        _delayInMilliseconds = delayInMilliseconds;
        _fakePerson = fakePerson;
    }

    public async Task<string> RunAsync()
    {
        return await RunAsyncTask(
            ClassIdentifier,
            _taskId,
            _delayInMilliseconds,
            _fakePerson.FullName);
    }
}
```

Our Utility class of helper methods...
```C#
public class Utility
{
    public static string StepStarted = "started";
    public static string StepDone = "done";

    public static string GetFormattedDate(DateTime date)
    {
        return date.ToString("hh:mm:ss.fff tt");
    }

    public static string GenerateOutputMessage(string taskName, string stepName)
    {
        return $"{taskName} {stepName.ToLower()} at {GetFormattedDate(DateTime.Now)}";
    }

    public static string GenerateOutputMessage(string taskName, string stepName, int num, string message)
    {
        return message != null ?
            $"{taskName} {stepName.ToLower()} {num} [Message: {message}] at {GetFormattedDate(DateTime.Now)}" :
            $"{taskName} {stepName.ToLower()} {num} at {GetFormattedDate(DateTime.Now)}";
    }

    public static int GetDelay(int ms)
    {
        return new Random(ms).Next(0, ms);
    }
}
```

The results will look something like this in your command window...
```console
ProcessTasks.ProcessAllAsync started at 08:54:02.943 AM
DoTask1RunAsync started 1 at 08:54:03.329 AM
DoTask1RunAsync done 1 at 08:54:07.856 AM

DoTask2RunAsync started 1 at 08:54:07.858 AM
DoTask2RunAsync done 1 at 08:54:07.864 AM

DoTask1RunAsync started 2 at 08:54:07.864 AM
DoTask1RunAsync done 2 at 08:54:12.138 AM

DoTask2RunAsync started 2 at 08:54:12.138 AM
DoTask2RunAsync done 2 at 08:54:12.156 AM

DoTask1RunAsync started 3 at 08:54:12.156 AM
DoTask1RunAsync done 3 at 08:54:16.430 AM

DoTask2RunAsync started 3 at 08:54:16.430 AM
DoTask2RunAsync done 3 at 08:54:16.448 AM

DoTask1RunAsync started 4 at 08:54:16.448 AM
DoTask1RunAsync done 4 at 08:54:20.717 AM

DoTask2RunAsync started 4 at 08:54:20.717 AM
DoTask2RunAsync done 4 at 08:54:20.735 AM

DoTask1RunAsync started 5 at 08:54:20.735 AM
DoTask1RunAsync done 5 at 08:54:25.010 AM

DoTask2RunAsync started 5 at 08:54:25.010 AM
DoTask2RunAsync done 5 at 08:54:25.028 AM

FakePersonRepository started 1 [Message: Miss Rickie Weber] at 08:54:25.084 AM
FakePersonRepository done 1 [Message: Miss Rickie Weber] at 08:54:25.131 AM

FakePersonRepository started 2 [Message: Giovani Cormier] at 08:54:25.132 AM
FakePersonRepository done 2 [Message: Giovani Cormier] at 08:54:25.174 AM

FakePersonRepository started 3 [Message: Haleigh Kunze] at 08:54:25.174 AM
FakePersonRepository done 3 [Message: Haleigh Kunze] at 08:54:25.217 AM

FakePersonRepository started 4 [Message: Dr. Oran Fahey] at 08:54:25.218 AM
FakePersonRepository done 4 [Message: Dr. Oran Fahey] at 08:54:25.260 AM

FakePersonRepository started 5 [Message: Hassan Schamberger] at 08:54:25.260 AM
FakePersonRepository done 5 [Message: Hassan Schamberger] at 08:54:25.314 AM

FakePersonRepository started 6 [Message: Laron Hauck] at 08:54:25.314 AM
FakePersonRepository done 6 [Message: Laron Hauck] at 08:54:25.357 AM

FakePersonRepository started 7 [Message: Davon Metz] at 08:54:25.357 AM
FakePersonRepository done 7 [Message: Davon Metz] at 08:54:25.399 AM

FakePersonRepository started 8 [Message: Tiara Willms] at 08:54:25.399 AM
FakePersonRepository done 8 [Message: Tiara Willms] at 08:54:25.452 AM

FakePersonRepository started 9 [Message: Freeda Schowalter] at 08:54:25.452 AM
FakePersonRepository done 9 [Message: Freeda Schowalter] at 08:54:25.495 AM

FakePersonRepository started 10 [Message: Estefania Swift] at 08:54:25.497 AM
FakePersonRepository done 10 [Message: Estefania Swift] at 08:54:25.541 AM

TaskRunnerSaveAll started at 08:54:25.543 AM
TaskRunnerSaveAll done at 08:54:27.553 AM
ProcessTasks.ProcessAllAsync done at 08:54:27.553 AM
Press any key to continue...
```