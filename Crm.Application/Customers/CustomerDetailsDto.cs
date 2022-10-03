using Crm.Application.ApiKeys;
using Crm.Application.BankAccounts;
using Crm.Application.Documents;
using Crm.Application.SubScriptions;
using System;
using System.Collections.Generic;

namespace Crm.Application.Customers.GetCustomerDetails
{
    public class CustomerDetailsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Type { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string ProfileRisk { get; set; }

        public DateTime? ActivationDate { get; set; }

        public CurrentPositionDto CurrentPosition { get; set; }

        public List<SubScriptionDto> SubScriptions { get; set; }

        public List<BankAccountDto> BankAccounts { get; set; }

        public List<DocumentDetailsDto> Documents { get; set; }

        public List<ApiKeyRolesDto> ApiKeysRoles { get; set; }

    }
}
