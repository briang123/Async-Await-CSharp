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

    }
}