namespace Crm.Domain.Prospects
{
    public interface IProspectUniquenessChecker
    {
        bool IsUnique(string email);
    }
}