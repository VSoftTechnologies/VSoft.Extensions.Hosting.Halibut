using System;
using System.Collections.Generic;
using System.Text;

namespace VSoft.Halibut.Hosting
{
    public enum HalibutServerMode
    {
        Listening = 0,
        Polling = 1,
        WebSocketPolling = 2
    }
}
