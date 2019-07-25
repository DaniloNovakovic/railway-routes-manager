using System;
using System.Runtime.Serialization;

namespace Common
{
    [Serializable]
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : this("Invalid password!")
        {
        }

        public InvalidPasswordException(string message) : base(message)
        {
        }

        public InvalidPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}