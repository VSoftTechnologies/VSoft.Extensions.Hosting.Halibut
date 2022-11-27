using Halibut;
using Halibut.ServiceModel;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace VSoft.Extensions.Hosting.Halibut
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
			var builder = new HalibutRuntimeBuilder()
				.WithServiceFactory(_serviceFactory)
				.WithServerCertificate(certificate)
				.WithTrustProvider(_trustProvider);

			if (_options.TypeRegistry != null)
				builder = builder.WithTypeRegistry(_options.TypeRegistry);

			_runtime = builder.Build();

			if (_options.Trust.Count > 0)
				foreach (var item in _options.Trust)
					_trustProvider.Add(item);

			_runtime.OnUnauthorizedClientConnect = _options.OnUnauthorizedClientConnect;

			switch (_options.Mode)
			{
				case HalibutServerMode.Listening:
					_runtime.Listen(_options.Listen.EndPoint);
					break;
				case HalibutServerMode.Polling:
					_runtime.Poll(new Uri($"poll://{_options.Polling.Subscription}"), new ServiceEndPoint(new Uri(_options.Polling.BaseUri), _options.Polling.RemoteThumbPrint, _options.ProxyDetails));

					break;
				case HalibutServerMode.WebSocketPolling:
					_runtime.Poll(new Uri($"poll://{_options.Polling.Subscription}"), new ServiceEndPoint(new Uri(_options.Polling.BaseUri), _options.Polling.RemoteThumbPrint, _options.ProxyDetails));
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
