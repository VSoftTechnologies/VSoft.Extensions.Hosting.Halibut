using System.Net;

namespace VSoft.Extensions.Hosting.Halibut
{
    public class HalibutListenOptions
    {
        public IPEndPoint EndPoint { get; set; } = new IPEndPoint(IPAddress.IPv6Any, 8443);
    }
}
