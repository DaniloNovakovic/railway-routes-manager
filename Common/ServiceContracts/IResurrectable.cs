using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface IResurrectable<TKey>
    {
        [OperationContract]
        void Resurrect(TKey key);
    }
}