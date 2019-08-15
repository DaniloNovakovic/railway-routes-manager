namespace Client.Core
{
    public interface IUndoableCommand
    {
        void Execute();

        void UnExecute();
    }
}