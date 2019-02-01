using Async_Await_CSharp;
using NUnit.Framework;
using System.Threading.Tasks;
using Async_Await_CSharp.Interfaces;

namespace Tests
{
    [TestFixture]
    internal class TestRunnerTests
    {
        private ITaskRunner _taskRunner;

        [SetUp]
        public void Setup()
        {
            _taskRunner = new TaskRunner();
        }

        [Test]
        public async Task SaveAll_EnsureAllOperationsSave_ReturnsTrueIfSaved()
        {
            var result = await _taskRunner.SaveAll();

            Assert.That(result, Is.True);
        }

    }
}
