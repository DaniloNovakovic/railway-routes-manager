using System.ServiceModel;

namespace Common
{
    [ServiceContract]
    public interface ICountryService : IServiceBase<int, CountryDto>
    {
    }
}