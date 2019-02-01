using System;
using Async_Await_CSharp.Interfaces;

namespace Async_Await_CSharp.Models
{
    public class FakePerson : IFakePerson
    {
        public string FullName { get; set; }
        public override String ToString()
        {
            return FullName;
        }
    }
}