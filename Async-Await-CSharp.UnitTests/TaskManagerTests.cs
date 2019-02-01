using Async_Await_CSharp;
using NUnit.Framework;
using System.Threading.Tasks;
using Async_Await_CSharp.Interfaces;

namespace Tests
{
    [TestFixture]
    internal class TaskManagerTests
    {
        private ITaskManager _taskManager;
        private int _taskId;
        private int _delayInMilliseconds;

        [SetUp]
        public void Setup()
        {
            _taskId = 1;
            _delayInMilliseconds = 10;
            _taskManager = new TaskManager();
        }

        [Test]
        public async Task RunTasksAsync_ProcessAllTasks_ReturnsTrue()
        {
            _taskManager.AddTaskAsync(new DoTask1(_taskId, _delayInMilliseconds));

            var result = await _taskManager.RunTasksAsync();

            Assert.That(result, Is.True);
        }
    }
}