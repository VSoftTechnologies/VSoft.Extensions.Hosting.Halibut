using Halibut;
using System;
using System.Collections.Generic;
using System.Text;


namespace VSoft.Halibut.Hosting
{
    public class HalibutHostOptions
    {
        public int Port { get; set; } = 50576;

        public string CertificateFile { get; set; }

        public HalibutServerMode Mode { get; set; } = HalibutServerMode.Listening;

        public HalibutListenOptions Listen { get; private set; } = new HalibutListenOptions();

        public HalibutPollingOptions Polling { get; private set; } = new HalibutPollingOptions();

        public ProxyDetails ProxyDetails { get; set; }

        public List<string> Trust { get; private set; } = new List<string>();

        public Func<string, string, UnauthorizedClientConnectResponse> OnUnauthorizedClientConnect { get; set; }
    }
}
