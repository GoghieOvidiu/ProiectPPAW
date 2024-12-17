using Autofac;
using Autofac.Integration.WebApi;

using BusinessLayer;
using BusinessLayer.CoreServices;
using BusinessLayer.CoreServices.Interfaces;
using BusinessLayer.Interface;

using Microsoft.Ajax.Utilities;

using NivelAccessDate;
using NivelAccessDate.Interfaces;

using Repository_CodeFirst;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace WebApiNet.Infrastructure
{
    public class ContainerConfigurer
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            // Register dependencies in controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register individual types
            builder.RegisterType<MemoryCacheService>()
                .As<ICache>();

            builder.RegisterType<ProiectContext>()
                .As<IProiectDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TipAbonamentAccesor>()
                .As<ITipAbonamentAccessor>();
            builder.RegisterType<TipAbonamentService>()
                .As<ITipAbonament>();

            builder.RegisterType<ClientAccesor>()
                .As<IClientAccessor>();
            builder.RegisterType<ClientService>()
                .As<IClient>();

            builder.RegisterType<AbonatiiAccesor>()
                .As<IAbonatiiAccessor>();
            builder.RegisterType<AbonatiiService>()
                .As<IAbonatii>();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}