using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTControllers.Controllers
{
    public interface ITempRepository
    {
        Task AddAsync(string deviceId, TemperatureModel model);
        Task AddManyAsync(string deviceId, List<TemperatureModel> models);
    }
}