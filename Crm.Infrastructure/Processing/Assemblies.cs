using Crm.Application.Customers.RegisterCustomer;
using System.Reflection;

namespace Crm.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(RegisterCustomerCommand).Assembly;
    }
}