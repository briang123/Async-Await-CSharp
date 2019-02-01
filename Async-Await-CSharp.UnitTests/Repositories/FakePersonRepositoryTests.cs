using Async_Await_CSharp;
using NUnit.Framework;
using System.Threading.Tasks;
using Async_Await_CSharp.Interfaces;
using Async_Await_CSharp.Models;
using Async_Await_CSharp.Repositories;

namespace Tests
{
    [TestFixture]
    internal class FakePersonRepositoryTests
    {
        private IFakePerson _fakePerson;
        private IDoTaskAsync _fakePersonRepository;
        private int _taskId;
        private int _delayInMilliseconds;
        private string _classIdentifier;

        [SetUp]
        public void Setup()
        {
            _taskId = 1;
            _delayInMilliseconds = 10;
            _classIdentifier = "FakePersonRepository";
            _fakePerson = new FakePerson { FullName = "john doe" };
            _fakePersonRepository = new FakePersonRepository(_taskId, _delayInMilliseconds, _fakePerson);
        }

        [Test]
        public async Task RunAsync_DoTasks_ReturnsGeneratedString()
        {
            var expectedMessage = $"{_classIdentifier}:{_taskId}:{_fakePerson.FullName}";

            var result = await _fakePersonRepository.RunAsync();

            Assert.That(result, Is.EqualTo(expectedMessage));
        }
    }
}