using System.Threading.Tasks;

namespace Client.Core
{
    public interface ICommandManager
    {
        Task ExecuteAsync(IUndoableCommand command);

        Task RedoAsync();

        Task UndoAsync();
    }
}