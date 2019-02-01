using Async_Await_CSharp;
using NUnit.Framework;
using System.Threading.Tasks;
using Async_Await_CSharp.Interfaces;

namespace Tests
{
    [TestFixture]
    internal class TaskBaseTests
    {
        private ITaskBase _taskBase;
        private string _classIdentifier;
        private int _iterationNumber;
        private int _delayInMilliseconds;

        [SetUp]
        public void Setup()
        {
            _classIdentifier = "a";
            _iterationNumber = 1;
            _delayInMilliseconds = 10;
            _taskBase = new TaskBase();
        }

        [Test]
        [TestCase("a:1")]
        [TestCase("a:1:b", "b")]
        public async Task RunAsyncTask_DoTasks_ReturnsGeneratedString(
            string expectedMessage, string message = null)
        {
            var result = await _taskBase.RunAsyncTask(
                _classIdentifier,
                _iterationNumber,
                _delayInMilliseconds,
                message);

            Assert.That(result, Is.EqualTo(expectedMessage));
        }
    }
}