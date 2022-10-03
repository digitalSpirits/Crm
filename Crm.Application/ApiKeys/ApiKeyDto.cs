using System;

namespace Crm.Application.ApiKeys
{
    public class ApiKeyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }
    }
}
