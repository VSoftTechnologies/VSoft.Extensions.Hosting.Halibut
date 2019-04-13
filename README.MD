## VSoft.Halibut.Hosting

This project addes support for [Microsoft.Extensions.Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting/) to [Halibut](https://github.com/OctopusDeploy/Halibut).

#### Usage

```c#
    static void Main(string[] args)
    {
        var host = new HostBuilder()
            .UseHalibut((options, services) =>
            {
                services.RegisterHalibutService<ICalculatorService, CalculatorService>();

                options.CertificateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HalibutServer.pfx");

                options.Mode = HalibutServerMode.Listening;
                //Listening
                options.Listen.EndPoint = new IPEndPoint(IPAddress.IPv6Any, 8433);
                options.Trust.Add("911B7F9E102486C4E1B299FA8D528FE30F129158");
                //Polling
                //options.Polling.BaseUri = "https://localhost:8433";
                //options.Polling.Subscription = "SQ-TENTAPOLL";
                //options.Polling.RemoteThumbPrint = "ABC.....";
                //WS Polling
                //options.Polling.BaseUri = "wss://localhost:8433/Halibut";
                //options.Polling.Subscription = "SQ-TENTAPOLL";
                //options.Polling.RemoteThumbPrint = "ABC..";

                //proxy
                //options.ProxyDetails = new ProxyDetails("myproxy", 9433, Transport.Proxy.ProxyType.HTTP);
            })
            .UseConsoleLifetime()
            .Build();

        host.Run();
    }

```
