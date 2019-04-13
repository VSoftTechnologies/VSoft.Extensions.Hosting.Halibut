using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace VSoft.Halibut.Hosting
{
    public class HalibutListenOptions
    {
        public IPEndPoint EndPoint { get; set; } = new IPEndPoint(IPAddress.IPv6Any, 8443);
    }
}
