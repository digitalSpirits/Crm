namespace Crm.Domain.Roles
{
    public interface IRoleNameUniquenessChecker
    {
        bool IsUnique(string name);
    }
}