using System;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface ICommandManager
    {
        event EventHandler CommandExecuted;

        bool CanRedo();

        bool CanUndo();

        Task ExecuteAsync(IUndoableCommand command);

        Task RedoAsync();

        Task UndoAsync();
    }
}