using Autofac;
using IoTControllers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IoTListenerFunction
{
    public static class AutofacContainerFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>IContainer.</returns>
        public static IContainer Create()
        {
            var builder = new ContainerBuilder();
            Register_Services(builder);
            var container = builder.Build();
            return container;
        }
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void Register_Services(ContainerBuilder builder)
        {
            var path = Directory.GetCurrentDirectory();
            builder.Register(context => new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json").Build()).As<IConfiguration>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            builder.RegisterAssemblyModules(assemblies);
            builder.RegisterModule<IotControllersModule>();
        }
    }
}
