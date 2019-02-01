using System.Threading.Tasks;
using Async_Await_CSharp.Interfaces;

namespace Async_Await_CSharp.Repositories
{
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
}