using Async_Await_CSharp;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    internal class TaskBaseTests
    {
        private Mock<ITaskBase> _taskBase;

        [SetUp]
        public void Setup()
        {
            _taskBase = new Mock<ITaskBase>();

            _taskBase
                .Setup(t => t.RunAsyncTask("a", 1, 10, null));

        }

    }
}
