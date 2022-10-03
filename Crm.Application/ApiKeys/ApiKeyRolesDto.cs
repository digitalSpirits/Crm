using Crm.Application.ApiRoles;
using System;
using System.Collections.Generic;

namespace Crm.Application.ApiKeys
{
    public class ApiKeyRolesDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<ApiRoleDto> Roles { get; set; }

    }
}
