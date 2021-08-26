using System;
using System.Collections.Generic;

namespace VSoft.Extensions.Hosting.Halibut
{
    public interface IServiceRegistry
    {
        void RegisterHalibutService<TContract, TImplementation>()
             where TContract : class
             where TImplementation : class, TContract;

        Type GetServiceType(string serviceName);

        IReadOnlyList<Type> RegisteredServiceTypes { get; }
    }
}
