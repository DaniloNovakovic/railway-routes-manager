using System.Collections.Generic;

namespace Client.Core
{
    public interface ICommandManager
    {
        void Execute(IUndoableCommand command);

        void Redo();

        void Undo();
    }
}