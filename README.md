# Async-Await-CSharp

An Async/Await example in C# where we have a console application startup method calling the ```TaskRunner.ProcessAsync()``` static method to initiate and kick off the async Tasks.

Not all classes are shown below, but just a subset, for brevity.

```C#
public class Program
{
    public static void Main()
    {
        Utility.RenderOutput("ProcessTasks.ProcessAllAsync", Utility.StepStarted);

        TaskRunner.ProcessAsync().Wait();

        Utility.RenderOutput("ProcessTasks.ProcessAllAsync", Utility.StepDone);

        Console.WriteLine(@"Press any key to continue...");
        Console.ReadKey(true);
    }
}
```

The TaskRunner class is responsible for setting up the tasks to be run using the Command design pattern.
```c#
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
```

The TaskManager class accepts a task to manage, then provides a method to run all the tasks in an async manner.
```C#
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
```

An example of one of the Tasks classes (DoTask1 and DoTask2 are identical)
```C#
public interface IDoTaskAsync
{
    Task RunAsync();
}

public class DoTask1 : IDoTaskAsync
{
    private readonly int _delayInMilliseconds;
    private readonly int _taskId;
    private const string ClassIdentifier = "DoTask1RunAsync";

    public DoTask1(int taskId, int delayInMilliseconds)
    {
        _taskId = taskId;
        _delayInMilliseconds = delayInMilliseconds;
    }

    public async Task RunAsync()
    {
        await Utility.RunAsyncTask(ClassIdentifier, _taskId, _delayInMilliseconds);
    }
}
```

Here is where we fetch our Fake person data...
```c#
internal class FakePersonRepository : IDoTaskAsync
{
    private readonly int _delayInMilliseconds;
    private readonly int _taskId;
    private const string ClassIdentifier = "FakePersonRepository";

    public FakePersonRepository(int taskId, int delayInMilliseconds)
    {
        _taskId = taskId;
        _delayInMilliseconds = delayInMilliseconds;
    }

    public async Task RunAsync()
    {
        var fakePerson = new FakePerson()
        {
            FullName = Faker.Name.FullName()
        };

        await Utility.RunAsyncTask(
            ClassIdentifier,
            _taskId,
            _delayInMilliseconds,
            fakePerson.FullName);
    }
}
```

Our Utility class of helper methods...
```C#
public class Utility
{
    public static string StepStarted = "started";
    public static string StepDone = "done";

    public static string GetDate()
    {
        return DateTime.Now.ToString("hh:mm:ss.fff tt");
    }

    public static void RenderOutput(string taskName, string stepName)
    {
        Console.WriteLine(@"{0} {1} at {2}", taskName, stepName.ToLower(), GetDate());
    }

    public static void RenderOutput(string taskName, string stepName, int num, string message)
    {
        if (message != null)
        {
            Console.WriteLine(@"{0} {1} {2} [Message: {3}] at {4}", taskName, stepName.ToLower(), num, message,
                GetDate());
        }
        else
        {
            Console.WriteLine(@"{0} {1} {2} at {3}", taskName, stepName.ToLower(), num, GetDate());
        }
    }

    public static int GetDelay(int ms)
    {
        return new Random(ms).Next(0, ms);
    }

    public static async Task RunAsyncTask(string classIdentifier, int num, int delayInMilliseconds, string message = null)
    {
        RenderOutput(classIdentifier, StepStarted, num, message);

        await Task.Delay(GetDelay(delayInMilliseconds));

        RenderOutput(classIdentifier, StepDone, num, message);

        Console.WriteLine("");
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