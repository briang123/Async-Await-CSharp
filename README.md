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

The results will look something like this in your command window...
```console
ProcessTasks.ProcessAllAsync started at 04:43:08.723 PM
DoTask1RunAsync started 1 at 04:43:09.756 PM
DoTask2RunAsync started 1 at 04:43:09.770 PM
DoTask1RunAsync started 2 at 04:43:09.811 PM
DoTask2RunAsync started 2 at 04:43:09.827 PM
DoTask1RunAsync started 3 at 04:43:09.828 PM
DoTask2RunAsync started 3 at 04:43:09.828 PM
DoTask1RunAsync started 4 at 04:43:09.850 PM
DoTask2RunAsync started 4 at 04:43:09.850 PM
DoTask1RunAsync started 5 at 04:43:09.850 PM
DoTask2RunAsync started 5 at 04:43:09.856 PM
DoTask1RunAsync started 6 at 04:43:09.871 PM
DoTask2RunAsync started 6 at 04:43:09.871 PM
DoTask1RunAsync started 7 at 04:43:09.888 PM
DoTask2RunAsync started 7 at 04:43:09.889 PM
DoTask1RunAsync started 8 at 04:43:09.890 PM
DoTask2RunAsync started 8 at 04:43:09.890 PM
DoTask1RunAsync started 9 at 04:43:09.952 PM
DoTask2RunAsync started 9 at 04:43:09.990 PM
DoTask1RunAsync started 10 at 04:43:10.011 PM
DoTask2RunAsync started 10 at 04:43:10.047 PM
DoTask1RunAsync done 1 at 04:43:10.901 PM
DoTask1RunAsync done 2 at 04:43:10.962 PM
DoTask1RunAsync done 3 at 04:43:10.976 PM
DoTask1RunAsync done 5 at 04:43:10.990 PM
DoTask1RunAsync done 4 at 04:43:11.030 PM
DoTask1RunAsync done 6 at 04:43:11.034 PM
DoTask1RunAsync done 8 at 04:43:11.048 PM
DoTask1RunAsync done 7 at 04:43:11.048 PM
DoTask1RunAsync done 9 at 04:43:11.110 PM
DoTask1RunAsync done 10 at 04:43:11.202 PM
DoTask2RunAsync done 1 at 04:43:14.037 PM
DoTask2RunAsync done 3 at 04:43:14.099 PM
DoTask2RunAsync done 2 at 04:43:14.105 PM
DoTask2RunAsync done 4 at 04:43:14.111 PM
DoTask2RunAsync done 5 at 04:43:14.126 PM
DoTask2RunAsync done 6 at 04:43:14.144 PM
DoTask2RunAsync done 8 at 04:43:14.159 PM
DoTask2RunAsync done 7 at 04:43:14.159 PM
DoTask2RunAsync done 9 at 04:43:14.252 PM
DoTask2RunAsync done 10 at 04:43:14.330 PM
Fetched workers at 04:43:15.452 PM: Andrew Grant
Fetched workers at 04:43:15.453 PM: Jazmin Schowalter
Fetched workers at 04:43:15.453 PM: Lincoln Leuschke
Fetched workers at 04:43:15.453 PM: Liliana Fadel
Fetched workers at 04:43:15.454 PM: Ariane Stokes
Fetched workers at 04:43:15.454 PM: Jeromy Stoltenberg
Fetched workers at 04:43:15.454 PM: Demarcus Murray
Fetched workers at 04:43:15.454 PM: Dr. Sid Marks
Fetched workers at 04:43:15.454 PM: Ali VonRueden
Fetched workers at 04:43:15.454 PM: Annalise Langosh
Fetched workers at 04:43:15.454 PM: Melyna Boyle
ProcessTasksSaveAll started at 04:43:15.455 PM
ProcessTasksSaveAll done at 04:43:17.467 PM
ProcessTasks.ProcessAllAsync done at 04:43:17.467 PM
Press any key to continue...
```