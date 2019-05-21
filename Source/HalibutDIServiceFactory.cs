using System;
using Halibut.ServiceModel;
using Microsoft.Extensions.DependencyInjection;

namespace VSoft.Extensions.Hosting.Halibut
{
    //Uses the Host DI containerto resolve services
    internal class HalibutDIServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceRegistry _serviceRegistry;
        public HalibutDIServiceFactory(IServiceProvider serviceProvider, IServiceRegistry serviceRegistry)
        {
            _serviceProvider = serviceProvider;
            _serviceRegistry = serviceRegistry;
        }
        public IServiceLease CreateService(string serviceName)
        {
            Type serviceType = _serviceRegistry.GetServiceType(serviceName);
            //screate a scope per request, to ensure isolation.
            var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService(serviceType); //throws if not found!
            return new Lease(scope, service);
        }
        #region Nested type: Lease

        class Lease : IServiceLease
        {
            object _service;
            IServiceScope _scope;
                 

            public Lease(IServiceScope scope, object service)
            {
                this._scope = scope;
                this._service = service;
            }

            public object Service
            {
                get { return _service; }
            }

            public void Dispose()
            {
                _service = null;
                if (_service is IDisposable)
                {
                    //The DI container should dispose of the service instance.
//                    ((IDisposable)service).Dispose();
                }
                _scope.Dispose();
            }
        }

        #endregion


    }
}
