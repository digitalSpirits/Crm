using System;
using System.Collections.Generic;

namespace Crm.Application.Types
{
    public class CategoryRoleTypesDto
    {
        public string Category { get; set; }

        public List<RoleNameDto> Roles { get; set; }
    }
}
