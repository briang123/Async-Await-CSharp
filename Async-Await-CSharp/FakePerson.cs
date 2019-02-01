using System;

namespace Async_Await_CSharp
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