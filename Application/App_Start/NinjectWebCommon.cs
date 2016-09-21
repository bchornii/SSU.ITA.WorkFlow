using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using SSU.ITA.WorkFlow.Application.Web;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Application.Web.ExceptionHandlers;
using System.Web.Mvc;
using System.Collections.Generic;
using SSU.ITA.WorkFlow.DataAccess.EF.Repository.Repositories;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace SSU.ITA.WorkFlow.Application.Web
{
    public static class ServiceLocator
    {
        private static IKernel _kernel;
        public static bool isKernerInitialized { get; private set; }
        public static T GetService<T>()
        {
            return _kernel.Get<T>();
        }

        public static void Initialize(Func<IKernel> initialize)
        {
            _kernel = initialize();
            isKernerInitialized = true;
        }
    }

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        private static IKernel _kernel;

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            ServiceLocator.Initialize(CreateKernel);
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
            _kernel.Dispose();
            _kernel = null;
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            if (_kernel == null)
            {
                _kernel = new StandardKernel();
            }

            try
            {
                if (!ServiceLocator.isKernerInitialized)
                {
                    _kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                    _kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                    RegisterServices(_kernel);
                }
                return _kernel;
            }

            catch
            {
                _kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IWorkFlowDbContextFactory>().To<WorkFlowDbContextFactory>();
            kernel.Bind<IHasher>().To<Hasher>();
            kernel.Bind<IEmployeeService>().To<EmployeeService>();
            kernel.Bind<IProjectsService>().To<ProjectsService>();
            kernel.Bind<IUserRolesService>().To<UserRolesService>();
            kernel.Bind<ISelfRegistrationService>().To<SelfRegistrationService>();
            kernel.Bind<IInvitationService>().To<InvitationService>();
            kernel.Bind<IRegistrationTokenService>().To<RegistrationTokenService>();
            kernel.Bind<ITaskService>().To<TasksService>();
            kernel.Bind<IExceptionHandlerCustomizer>().To<ExceptionHandlerCustomizer>();
            kernel.Bind<IProjectRepository>().To<ProjectRepository>();
            kernel.Bind<IReportRepository>().To<ReportRepository>();
            kernel.Bind<IReportService>().To<ReportService>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
        }
    }
}