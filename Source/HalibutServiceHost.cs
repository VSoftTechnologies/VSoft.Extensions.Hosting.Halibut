using Halibut;
using Halibut.ServiceModel;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VSoft.Halibut.Hosting
{
    public class HalibutServiceHost : IHostedService
    {
        private readonly HalibutHostOptions _options;
        private readonly IServiceFactory _serviceFactory;
        private readonly ITrustProvider _trustProvider;
        private HalibutRuntime _runtime;

        public HalibutServiceHost(HalibutHostOptions options, IServiceFactory serviceFactory, ITrustProvider trustProvider)
        {
            _options = options;
            _serviceFactory = serviceFactory;
            _trustProvider = trustProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            var certificate = new X509Certificate2(_options.CertificateFile);
            _runtime = new HalibutRuntime(_serviceFactory, certificate);

            _trustProvider.OnAdded = (s) => _runtime.Trust(s);
            _trustProvider.OnRemoved = (s) => _runtime.RemoveTrust(s);
            _trustProvider.OnTrustOnly = (list) => _runtime.TrustOnly(list);

            if (_options.Trust.Count > 0)
            {
                foreach (var item in _options.Trust)
                {
                    _trustProvider.Add(item);
                }
            }

            switch (_options.Mode)
            {
                case HalibutServerMode.Listening:
                    _runtime.Listen(_options.Listen.EndPoint);
                    break;
                case HalibutServerMode.Polling:
                    _runtime.Poll(new Uri($"poll://{_options.Polling.Subscription}"), new ServiceEndPoint(new Uri(_options.Polling.BaseUri), _options.Polling.RemoteThumbPrint, _options.ProxyDetails));

                    break;
                case HalibutServerMode.WebSocketPolling:
                    _runtime.Poll(new Uri($"poll://{_options.Polling.Subscription}"), new ServiceEndPoint(new Uri(_options.Polling.BaseUri), _options.Polling.RemoteThumbPrint,_options.ProxyDetails));
                    break;
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _runtime.Dispose();
            _runtime = null;

            return Task.CompletedTask;
        }
    }
}
