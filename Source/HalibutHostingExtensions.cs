using System;
using Halibut.ServiceModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace VSoft.Halibut.Hosting
{
    public static class HalibutHostingExtensions
    {
        public static IHostBuilder UseHalibut(this IHostBuilder builder, Action<HalibutHostOptions, IServiceRegistry> configure)
        {
            if (configure == null)
                throw new ArgumentNullException("configure action");

            
            HalibutHostOptions options = new HalibutHostOptions();
           

            builder.ConfigureServices((hostContext, services) =>
            {
                IServiceRegistry serviceRegistry = new HalibutServiceRegistry(services);

                configure(options, serviceRegistry);


                services.AddSingleton<ITrustProvider, DefaultTrustProvider>();
                services.AddSingleton(options);
                services.AddSingleton<IServiceRegistry>(serviceRegistry);
                services.AddSingleton<IServiceFactory, HalibutDIServiceFactory>();
                services.AddHostedService<HalibutServiceHost>();
            });

            return builder;
        }
    }
}
