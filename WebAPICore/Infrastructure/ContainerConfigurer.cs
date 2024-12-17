using Autofac;

using Autofac.Integration.WebApi;

using BusinessLayer;
using BusinessLayer.Interface;

using Repository_CodeFirst;

using System.Reflection;
using System.Web.Http;

namespace WebAPICore.Infrastructure
{
    public class ContainerConfigurer
    {
        public static void ConfigureContainer()
        {
            var builder=new ContainerBuilder();
            // Register dependencies in controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ProiectContext>()
                .As<IProiectDbContext>()
                .SingleInstance();

            builder.RegisterType<ClientService>()
                .As<IClient>();

            // Get your HttpConfiguration.
          //  var config = GlobalConfiguration.Configuration;

            // Set the dependency resolver to be Autofac.
           // var container = builder.Build();
           // config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
