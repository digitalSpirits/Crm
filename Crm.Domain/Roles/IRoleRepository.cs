using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crm.Domain.Roles
{
    public interface IRoleRepository
    {
        Task CreateAsync(Role role);

        Task<List<Role>> GetAsync(List<RoleId> ids);

        Task<Role> GetByIdAsync(RoleId id);
    }
}
