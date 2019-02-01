using System.Threading.Tasks;

namespace Async_Await_CSharp.Interfaces
{
    public interface IDoTaskAsync
    {
        Task<string> RunAsync();
    }
}