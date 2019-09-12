using Autofac;
using IoTControllers.Controllers;
using System;

namespace IoTControllers
{
    public class IotControllersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TempIoTController>();
        }
    }
}
