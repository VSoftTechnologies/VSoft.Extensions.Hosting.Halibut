using Halibut.ServiceModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace VSoft.Halibut.Hosting
{
    //Uses the asp.net container to res
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

            var service = _serviceProvider.GetRequiredService(serviceType); //throws if not found!
            return new Lease(service);
        }
        #region Nested type: Lease

        class Lease : IServiceLease
        {
            readonly object _service;

            public Lease(object service)
            {
                this._service = service;
            }

            public object Service
            {
                get { return _service; }
            }

            public void Dispose()
            {
                if (_service is IDisposable)
                {
                    //The DI container should dispose of the service instance.
//                    ((IDisposable)service).Dispose();
                }
            }
        }

        #endregion


    }
}
