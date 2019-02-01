using Async_Await_CSharp;
using NUnit.Framework;
using System;
using Async_Await_CSharp.Helpers;

namespace Tests
{
    [TestFixture]
    internal class UtilityTests
    {
        private DateTime _currentDate;
        private string _currentFormattedDate;
        private string _taskName;
        private int _iterationNumber;
        private string _message;

        [SetUp]
        public void Setup()
        {
            _currentDate = new DateTime(2019, 1, 1, 12, 0, 0);
            _currentFormattedDate = "12:00:00.000 PM";
            _taskName = "task";
            _iterationNumber = 1;
            _message = "message";
        }

        [Test]
        public void GenerateOutputMessage_StepDone_ReturnsBeginningOfDoneMessageTextToWrite()
        {
            var result = Utility.GenerateOutputMessage(_taskName, Utility.StepDone);

            StringAssert.StartsWith("task done at", result);
        }

        [Test]
        public void GenerateOutputMessage_StepStart_ReturnsBeginningOfStartingMessageTextToWrite()
        {
            var result = Utility.GenerateOutputMessage(_taskName, Utility.StepStarted);

            StringAssert.StartsWith("task started at", result);
        }

        [Test]
        public void
            GenerateOutputMessage_StepStartWithIterationWithMessage_ReturnsBeginningOfStartedMessageTextWithMessageIncluded()
        {
            var result = Utility.GenerateOutputMessage(_taskName, Utility.StepStarted, _iterationNumber, _message);

            StringAssert.StartsWith("task started 1 [Message: message] at", result);
        }

        [Test]
        public void
            GenerateOutputMessage_StepStartWithIterationWithNoMessage_ReturnsBeginningOfStartedMessageTextWithoutMessageIncluded()
        {
            var result = Utility.GenerateOutputMessage(_taskName, Utility.StepStarted, _iterationNumber, null);

            StringAssert.StartsWith("task started 1 at", result);
        }

        [Test]
        public void GetDate_CheckCurrentDate_ReturnsExpectedFormat()
        {
            var result = Utility.GetFormattedDate(_currentDate);

            Assert.That(result, Is.EqualTo(_currentFormattedDate));
        }

        [Test]
        public void GetDelay_SetDelay_DelayIsGreaterThanOrEqualToZeroMilliseconds()
        {
            var result = Utility.GetDelay(10);

            Assert.That(result, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void GetDelay_SetDelay_DelayIsLessThanOrEqualToMaxMilliseconds()
        {
            var result = Utility.GetDelay(10);

            Assert.That(result, Is.LessThanOrEqualTo(10));
        }

        [Test]
        public void GetDelay_SetNoDelay_DelayIsZeroMilliseconds()
        {
            var result = Utility.GetDelay(0);

            Assert.That(result, Is.EqualTo(0));
        }
    }
}