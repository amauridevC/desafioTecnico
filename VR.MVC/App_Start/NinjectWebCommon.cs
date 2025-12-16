using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using VR.Application.Interfaces;
using VR.Application.Services;
using VR.Domain.Interfaces;
using VR.Infra.Data.Context;
using VR.Infra.Data.Repositories;
using VR.Infra.Data.UnitOfWork;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VR.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(VR.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace VR.MVC.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => bootstrapper.Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                System.Web.Mvc.DependencyResolver.SetResolver(new Ninject.Web.Mvc.NinjectDependencyResolver(kernel));

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<VRContext>().ToSelf().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<IFabricanteVeiculoRepository>().To<FabricanteVeiculoRepository>();
            kernel.Bind<IFabricanteVeiculoAppService>().To<FabricanteVeiculoAppService>();

            kernel.Bind<IVeiculoRepository>().To<VeiculoRepository>();
            kernel.Bind<IVeiculoAppService>().To<VeiculoAppService>();

            kernel.Bind<IClienteRepository>().To<ClienteRepository>();
            kernel.Bind<IClienteAppService>().To<ClienteAppService>();

            kernel.Bind<IVendaRepository>().To<VendaRepository>();
            kernel.Bind<IVendaAppService>().To<VendaAppService>();

        }
    }
}
