using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Azure.WebJobs.Host;

namespace MessageProcessingWebJob.Infrastructure.CastleWindsor
{
    public class WindsorJobActivator : IJobActivator
    {
        private readonly IWindsorContainer _container;

        public WindsorJobActivator()
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
        }

        public T CreateInstance<T>() =>
            _container.Resolve<T>();
    }
}
