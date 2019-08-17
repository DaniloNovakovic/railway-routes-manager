using System.Threading.Tasks;

namespace Client.Core
{
    public interface IUndoableCommand
    {
        Task ExecuteAsync();

        Task UnExecuteAsync();
    }
}