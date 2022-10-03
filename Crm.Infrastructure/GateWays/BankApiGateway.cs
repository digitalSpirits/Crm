
using System.Collections.Generic;
using Crm.Application.Configuration.Gateways;
using Crm.Domain.Customers.BankAccounts;

namespace Crm.Infrastructure.GateWays
{
    public class BankApiGateway : IBankApiGateWay
    {
        private readonly List<KeyValuePair<string, string>> _domains;

        private readonly string _login;

        private readonly string _secretKey;

        private readonly string _iban;
        public List<KeyValuePair<string, string>> EndPoints { get; private set; }
        public List<KeyValuePair<string, string>> Headers { get; private set; }

        public BankApiGateway(List<KeyValuePair<string, string>> domains, string login, string secretKey, string iban, List<KeyValuePair<string, string>> endPoints)
        {
            _domains = domains;
            _login = login;
            _secretKey = secretKey;
            _iban = iban;
            EndPoints = endPoints;
            Headers = GetHeaders();
        }

        public string Qonto => _domains.Find(x => x.Key == BankDomains.QONTO).Value;


        private List<KeyValuePair<string, string>> GetHeaders()
        {
            if (Headers != null)
                return Headers;

            Headers = new List<KeyValuePair<string, string>>();
            Headers.Add(new KeyValuePair<string, string>("Authorization", _login + " : " + _secretKey));
            Headers.Add(new KeyValuePair<string, string>("Content-Type", "application/json"));

            return Headers;
        }
    }
}
