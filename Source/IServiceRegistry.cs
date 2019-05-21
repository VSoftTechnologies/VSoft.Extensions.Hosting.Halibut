using System;


namespace VSoft.Extensions.Hosting.Halibut
{
    public interface IServiceRegistry
    {
        void RegisterHalibutService<TContract, TImplementation>()
             where TContract : class
             where TImplementation : class, TContract;

        Type GetServiceType(string serviceName);
    }
}
