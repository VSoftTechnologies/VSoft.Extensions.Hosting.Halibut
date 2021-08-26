using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;


namespace VSoft.Extensions.Hosting.Halibut
{
    internal class HalibutServiceRegistry : IServiceRegistry
    {
        private readonly Dictionary<string, Type> _lookup = new Dictionary<string, Type>();
        private readonly IServiceCollection _services;

        public HalibutServiceRegistry(IServiceCollection services)
        {
            _services = services;
        }

        public IReadOnlyList<Type> RegisteredServiceTypes => new List<Type>(_lookup.Values);

		public Type GetServiceType(string serviceName)
        {
            _lookup.TryGetValue(serviceName, out Type result);
            return result;
        }


        public void RegisterHalibutService<TContract, TImplementation>()
             where TContract : class
             where TImplementation : class, TContract

        {
            Type type = typeof(TContract);
            string name = type.Name;
            _lookup.Add(name, type);
            _services.AddScoped<TContract, TImplementation>();
        }
    }
}
