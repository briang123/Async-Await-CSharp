using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Await_CSharp
{
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
}