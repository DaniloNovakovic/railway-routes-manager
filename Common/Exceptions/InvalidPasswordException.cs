using System;
using System.ServiceModel;

namespace Common
{
    [Serializable]
    public class InvalidPasswordException : FaultException
    {
        public InvalidPasswordException() : base("Invalid password!")
        {
        }

        public InvalidPasswordException(string reason) : base(reason)
        {
        }

        public InvalidPasswordException(FaultReason reason) : base(reason)
        {
        }

        public InvalidPasswordException(string reason, FaultCode code) : base(reason, code)
        {
        }

        public InvalidPasswordException(FaultReason reason, FaultCode code) : base(reason, code)
        {
        }

        public InvalidPasswordException(string reason, FaultCode code, string action) : base(reason, code, action)
        {
        }

        public InvalidPasswordException(FaultReason reason, FaultCode code, string action) : base(reason, code, action)
        {
        }

        public InvalidPasswordException(System.ServiceModel.Channels.MessageFault fault) : base(fault)
        {
        }

        public InvalidPasswordException(System.ServiceModel.Channels.MessageFault fault, string action) : base(fault, action)
        {
        }
    }
}