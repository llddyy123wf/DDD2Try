﻿using Autofac;
using Autofac.Configuration;

namespace DDD2Try.Application
{
    public class ApplicationFactory
    {
        public static T Create<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<T>();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            using (var container = builder.Build())
            {
                return container.Resolve<T>();
            }
        }
    }
}