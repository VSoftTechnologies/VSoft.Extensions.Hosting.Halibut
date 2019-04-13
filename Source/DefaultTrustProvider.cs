using System;
using System.Collections.Generic;

namespace VSoft.Halibut.Hosting
{
    public class DefaultTrustProvider : ITrustProvider
    {
        public void Add(string clientThumbprint)
        {
            this.OnAdded?.Invoke(clientThumbprint);
        }

        public void Remove(string clientThumbprint)
        {
            this.OnRemoved?.Invoke(clientThumbprint);
        }

        public void TrustOnly(IReadOnlyList<string> thumbprints)
        {
            this.OnTrustOnly?.Invoke(thumbprints);
        }

        public Action<string> OnAdded { get; set; }

        public Action<string> OnRemoved { get; set; }

        public Action<IReadOnlyList<string>> OnTrustOnly { get; set; }

    }
}
