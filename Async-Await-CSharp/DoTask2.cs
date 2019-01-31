﻿using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    public class DoTask2 : IDoTaskAsync
    {
        private readonly int _delayInMilliseconds;
        private readonly int _taskId;
        private const string ClassIdentifier = "DoTask2RunAsync";

        public DoTask2(int taskId, int delayInMilliseconds)
        {
            _taskId = taskId;
            _delayInMilliseconds = delayInMilliseconds;
        }

        public async Task RunAsync()
        {
            await Utility.RunAsyncTask(ClassIdentifier, _taskId, _delayInMilliseconds);
        }
    }
}