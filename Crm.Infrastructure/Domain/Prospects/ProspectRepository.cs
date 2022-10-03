using Crm.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Crm.Domain.Prospects;

namespace Crm.Infrastructure.Domain.Prospects
{
    public class ProspectRepository : IProspectRepository
    {
        private readonly CrmContext _context;
        public ProspectRepository(CrmContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(Prospect prospect)
        {
            await _context.Prospects.AddAsync(prospect);
        }

        public async Task<Prospect> GetByIdAsync(ProspectId id)
        {
            var prospect = await _context.Prospects
                    .Include(ProspectEntityTypeConfiguration.EmailConfirmationList)
                    .SingleOrDefaultAsync(x => x.Id == id);

            return prospect;
        }
    }
}
