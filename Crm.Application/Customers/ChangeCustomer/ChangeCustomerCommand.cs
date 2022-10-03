using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.Customers.ChangeCustomer
{
    public class ChangeCustomerCommand : CommandBase<Unit>
    {
        public Guid CustomerId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public byte Type { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string ProfileRisk { get; set; }

        public DateTime? UpdateDate { get; set; }

        public ChangeCustomerCommand(Guid id, string name, string surname, string email, byte type, string country, string city, string address, string phone, string profileRisk, DateTime? updateDate)
        {
            CustomerId = id;
            Name = name;
            Surname = surname;
            Email = email;
            Type = type;
            Country = country;
            City = city;
            Address = address;
            Phone = phone;
            ProfileRisk = profileRisk;
            UpdateDate = updateDate;
        }
    }
}
