using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Await_CSharp
{
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
}