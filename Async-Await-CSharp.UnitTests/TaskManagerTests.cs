using Async_Await_CSharp;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class TaskManagerTests
    {
        private Mock<ITaskManager> _taskManager;

        [SetUp]
        public void Setup()
        {
            _taskManager = new Mock<ITaskManager>();
        }

        //[Test]
        //public void AddTaskAsync_AddTask_ReturnsNewlyAddedTask()
        //{
        //    var task = new DoTask1(1, 500);

        //    //_taskManager.Setup(t => t.AddTaskAsync(It.IsAny<IDoTaskAsync>()));


        //}

    }
}