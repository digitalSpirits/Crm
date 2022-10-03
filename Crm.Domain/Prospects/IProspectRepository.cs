using System.Threading.Tasks;

namespace Crm.Domain.Prospects
{
    public interface IProspectRepository
    {
        Task CreateAsync(Prospect prospect);

        Task<Prospect> GetByIdAsync(ProspectId id);
    }
}
