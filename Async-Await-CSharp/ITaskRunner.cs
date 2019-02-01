using System.Threading.Tasks;

namespace Async_Await_CSharp
{
    public interface ITaskRunner
    {
        Task<bool> SaveAll();
        Task ProcessAsync();
    }
}