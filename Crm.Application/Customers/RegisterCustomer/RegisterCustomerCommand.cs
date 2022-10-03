using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.Customers.RegisterCustomer
{
    public class RegisterCustomerCommand : CommandBase<CustomerDto>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public byte Type { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string ProfileRisk { get; set; }

        public DateTime? ActivationDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public RegisterCustomerCommand(string username, string password, string token, string name, string surname, string email, byte type, string country, string city, string address, string phone, string profileRisk, DateTime? activationDate, DateTime? closeDate, DateTime? updateDate)
        {
            Username = username;
            Password = password;
            Token = token;
            Name = name;
            Surname = surname;
            Email = email;
            Type = type;
            Country = country;
            City = city;
            Address = address;
            Phone = phone;
            ProfileRisk = profileRisk;
            ActivationDate = activationDate;
            CloseDate = closeDate;
            UpdateDate = updateDate;
        }

    }
}
