using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MediatR;
using System.Collections.Generic;

namespace MessageProcessingWebJob.Infrastructure.CastleWindsor.Installers
{
    public class InfrastructureInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterAutoMapper(container);
            RegisterMediatR(container);
        }

        private void RegisterAutoMapper(IWindsorContainer container)
        {
            container.Register
            (
                Component.For<IConfigurationProvider>()
                         .UsingFactoryMethod(ctx => new MapperConfiguration(cfg => cfg.AddProfiles(GetType().Assembly))),
                
                Component.For<IMapper>()
                         .UsingFactoryMethod(ctx => new Mapper(ctx.Resolve<IConfigurationProvider>()))
            );
        }

        private void RegisterMediatR(IWindsorContainer container)
        {
            container.Register
            (
                Classes.FromAssemblyContaining<Program>()
                       .BasedOn(typeof(IAsyncRequestHandler<>))
                       .OrBasedOn(typeof(IAsyncRequestHandler<,>))
                       .OrBasedOn(typeof(IRequestHandler<>))
                       .OrBasedOn(typeof(IRequestHandler<,>))
                       .WithServiceAllInterfaces(),

                Component.For<SingleInstanceFactory>()
                         .UsingFactoryMethod<SingleInstanceFactory>(ctx => t => ctx.HasComponent(t) ? ctx.Resolve(t) : null)
                         .LifestyleSingleton(),

                Component.For<MultiInstanceFactory>()
                         .UsingFactoryMethod<MultiInstanceFactory>(ctx => t => (IEnumerable<object>)ctx.ResolveAll(t))
                         .LifestyleSingleton(),

                Component.For<IMediator>()
                          .ImplementedBy<Mediator>()
                          .LifestyleSingleton()
            );
        }
    }
}
