using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    internal interface ITaskRunner
    {
        Task SaveAll();
        Task ProcessAsync();
    }
}