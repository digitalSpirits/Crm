using System;

namespace Crm.Application.ApiRoles
{
    public class ApiKeyRoleDto
    {
        public Guid KeyId { get; set; }

        public Guid RoleId { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }
    }
}
