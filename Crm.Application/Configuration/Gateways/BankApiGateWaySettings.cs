
using System.Collections.Generic;

namespace Crm.Application.Configuration.Gateways
{
    public class BankApiGateWaySettings
    {

        public string Login { get; set; } = null!;

        public string Secret { get; set; } = null!;

        public string Iban { get; set; } = null!;

        public List<EndPoint> Domains { get; set; } = null!;

        public List<EndPoint> EndPoints { get; set; } = null!;
    }

    public class EndPoint
    {
        public string Name { get; set; } = null!;

        public string Path { get; set; } = null!;
    }
}
