using acme.product.api.common;
using acme.product.repositories.implementation;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http;

namespace acme.product.web.api.IOC
{
    public class WebApiInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IConfigurationProvider>()
                .ImplementedBy<WebConfigurationProvider>().LifestyleSingleton());
            container.Register(Component.For<IDatabaseConfigurationService>()
                .ImplementedBy<DatabaseConfigurationService>().LifestyleSingleton());

            var databaseInitializer = new ProductDatabaseInitializer(container.Resolve<IDatabaseConfigurationService>());
            var productsContainer = databaseInitializer.CreateOrGetProuctsCollectionAsync();

            container.Register(
                Classes
                    .FromThisAssembly()
                    .BasedOn<ApiController>()
                    .LifestyleScoped()
                );
        }
    }
}