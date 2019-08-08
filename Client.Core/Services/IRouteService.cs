using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface IRouteService
    {
        Task<IEnumerable<RouteModel>> GetAllRoutesAsync();
    }
}