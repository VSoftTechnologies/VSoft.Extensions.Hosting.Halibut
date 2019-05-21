namespace VSoft.Extensions.Hosting.Halibut
{
    public class HalibutPollingOptions
    {
        public string BaseUri { get; set; } = "https://localhost:8443";

        public string Subscription { get; set; }

        public string RemoteThumbPrint { get; set; }
    }
}
