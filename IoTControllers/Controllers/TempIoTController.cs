using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoTControllers.Controllers
{
    public class TempIoTController : IoTController
    {


        private readonly ITempRepository _tempRepository;
        public TempIoTController(ITempRepository tempRepository)
        {
            _tempRepository = tempRepository;
        }

        public async Task AddAsync(string deviceId, JObject model)
        {
            await _tempRepository.AddAsync(deviceId, model.ToObject<TemperatureModel>());
        }
        public async Task AddManyAsync(string deviceId, JArray models)
        {
            await _tempRepository.AddManyAsync(deviceId, models.ToObject<List<TemperatureModel>>());
        }

    }

    public class TemperatureModel
    {
        public decimal Wind { get; set; }
        public decimal Humidity { get; set; }
        public decimal  Precipitation { get; set; }
    }
}
