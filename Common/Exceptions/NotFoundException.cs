using System;
using System.ServiceModel;

namespace Common
{
    [Serializable]
    public class NotFoundException : FaultException
    {
        public NotFoundException() : base("Requested resource could not be found!")
        {
        }

        public NotFoundException(string reason) : base(reason)
        {
        }

        public NotFoundException(FaultReason reason) : base(reason)
        {
        }

        public NotFoundException(string reason, FaultCode code) : base(reason, code)
        {
        }

        public NotFoundException(FaultReason reason, FaultCode code) : base(reason, code)
        {
        }

        public NotFoundException(string reason, FaultCode code, string action) : base(reason, code, action)
        {
        }

        public NotFoundException(FaultReason reason, FaultCode code, string action) : base(reason, code, action)
        {
        }

        public NotFoundException(System.ServiceModel.Channels.MessageFault fault) : base(fault)
        {
        }

        public NotFoundException(System.ServiceModel.Channels.MessageFault fault, string action) : base(fault, action)
        {
        }
    }
}