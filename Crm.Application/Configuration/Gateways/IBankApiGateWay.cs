using System;
using System.Collections.Generic;

namespace Crm.Application.Configuration.Gateways
{
    public interface IBankApiGateWay
    {
        List<KeyValuePair<string, string>> EndPoints { get; }
        List<KeyValuePair<string, string>> Headers { get;  }
        string Qonto { get; }
    }
}
