using System;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Configuration.Commands;
using Crm.Application.Configuration.Data;
using Crm.Domain.Customers;
using Crm.Domain.SeedWork;
using Dapper;
using MediatR;

namespace Crm.Application.Prospects.IntegrationHandlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Unit>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IAuthenticateSecurity _authenticateSecurity;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IAuthenticateSecurity authenticateSecurity, ISqlConnectionFactory sqlConnectionFactory)
        {
            _authenticateSecurity = authenticateSecurity;
            _sqlConnectionFactory = sqlConnectionFactory;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string userNameSql = "SELECT UserName FROM [Crm].[Customers]";
            var userNameList = connection.QueryAsync<string>(userNameSql).Result.AsList();

            var userName = string.Empty; 
            bool isValid = false;

            while (!isValid)
            { 
                userName = _authenticateSecurity.GetPublicId();
                isValid = !userNameList.Contains(userName);
            }

            const string prospectSql = "SELECT [Id], [Email], [Password], [Country] FROM [Web].[Prospect] WHERE [Prospect].[Id] = @Id ";

            var prospect = await connection.QuerySingleOrDefaultAsync<ProspectDetailsDto>(prospectSql, new { Id = command.ProspectId.Value });

            var customer = Customer.CreateRegistered(userName, prospect.Password, string.Empty, string.Empty, string.Empty, prospect.Email, 0, prospect.Country, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.UtcNow, null, null);

            await _customerRepository.CreateAsync(customer);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}