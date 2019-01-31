# Async-Await-CSharp

An Async/Await example in C# where we have a console application startup method calling the ```ProcessTasks.ProcessAllAsync()``` static method to initiate and kick off the async Tasks.

Not all classes are shown below, but just a subset, for brevity.

```C#
public class Program
{
    public static void Main()
    {
        Utility.RenderOutput("ProcessTasks.ProcessAllAsync", Utility.StepStarted);

        ProcessTasks.ProcessAllAsync().Wait();

        Utility.RenderOutput("ProcessTasks.ProcessAllAsync", Utility.StepDone);

        Console.WriteLine(@"Press any key to continue...");
        Console.ReadKey(true);
    }
}
```

The ProcessTasks class initiates and kicks off the Tasks with the help of a Utility class that encapsulates some common usage.
```C#
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
```

An example of one of the Tasks classes
```C#
public interface IDoTaskAsync
{
    Task RunAsync(int num, int delayInMilliseconds);
}

public class DoTask1 : IDoTaskAsync
{
    private const string ClassIdentifier = "DoTask1RunAsync";

    public async Task RunAsync(int num, int delayInMilliseconds)
    {
        await Utility.RunAsyncTask(ClassIdentifier, num, delayInMilliseconds);
    }
}
```

Here is where we fetch our Fake worker data...
```c#
internal class FakeWorkerRepository
{
    public static async Task GetFakeWorkersAsync()
    {
        var workerData = new List<FakeWorker>();

        for (var i = 0; i <= 10; i++)
        {
            await Task.Delay(500);

            workerData.Add(new FakeWorker()
            {
                FullName = Faker.Name.FullName()
            });
        }

        workerData.ForEach(s => Console.WriteLine(@"Fetched workers at {0}: {1}", Utility.GetDate(), s));
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

    public static void RenderOutput(string taskName, string stepName, int num)
    {
        Console.WriteLine(@"{0} {1} {2} at {3}", taskName, stepName.ToLower(), num.ToString(), GetDate());
    }

    public static void RenderOutput(string taskName, string stepName)
    {
        Console.WriteLine(@"{0} {1} at {2}", taskName, stepName.ToLower(), GetDate());
    }

    public static int GetDelay(int ms)
    {
        return new Random(ms).Next(0, ms);
    }

    public static async Task RunAsyncTask(string classIdentifier, int num, int delayInMilliseconds)
    {
        RenderOutput(classIdentifier, StepStarted, num);

        var delay = GetDelay(delayInMilliseconds);
        await Task.Delay(delay);

        RenderOutput(classIdentifier, StepDone, num);
    }
}
```