using System;
using System.Collections.Generic;

namespace Crm.Domain.Customers
{
    public interface IAuthenticateSecurity
    {
        string CreateHashedSaltedPassword(string email, string password);

        KeyValuePair<string, long> GetAuthToken(Guid id);

        string GetApiToken(Guid id, IEnumerable<string> roles);

        string GetPublicId();
    }
}
