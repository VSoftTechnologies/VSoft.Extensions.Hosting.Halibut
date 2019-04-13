using System;
using System.Collections.Generic;
using System.Text;

namespace VSoft.Halibut.Hosting
{
    public interface ITrustProvider
    {
        void Add(string clientThumbprint);

        //replaces any existing
        void TrustOnly(IReadOnlyList<string> thumbprints);

        void Remove(string clientThumbprint);

        //for internal use only.. if Octpus merge my PR we can remove this.
        Action<string> OnAdded { get; set; }

        Action<string> OnRemoved { get; set; }
            
        Action<IReadOnlyList<string>> OnTrustOnly { get; set; }
    }
}
