namespace Server.Core
{
    public interface ILogger
    {
        void Debug(string message);

        void Exception(string message);

        void Info(string message);

        void Warn(string message);
    }
}