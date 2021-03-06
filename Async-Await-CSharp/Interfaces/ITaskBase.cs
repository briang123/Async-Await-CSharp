﻿using System.Threading.Tasks;

namespace Async_Await_CSharp.Interfaces
{
    public interface ITaskBase
    {
        Task<string> RunAsyncTask(
            string classIdentifier, int num, int delayInMilliseconds, string message = null);
    }
}