using Crm.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Crm.Domain.Roles;
using System.Collections.Generic;
using System.Linq;

namespace Crm.Infrastructure.Domain.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CrmContext _context;
        public RoleRepository(CrmContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task<List<Role>> GetAsync(List<RoleId> ids)
        {

            try
            {
               return  await _context.Roles.Where(x => ids.Contains(x.Id)).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<Role> GetByIdAsync(RoleId id)
        {
            var role = await _context.Roles
                    .SingleOrDefaultAsync(x => x.Id == id);

            return role;
        }
    }
}
