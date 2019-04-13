using System;
using System.Collections.Generic;
using System.Text;

namespace VSoft.Halibut.Hosting
{
    public interface IServiceRegistry
    {
        void RegisterHalibutService<TContract, TImplementation>()
             where TContract : class
             where TImplementation : class, TContract;

        Type GetServiceType(string serviceName);
    }
}
