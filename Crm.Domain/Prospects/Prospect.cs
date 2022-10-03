
using Crm.Domain.Prospects.EmailConfirmations;
using Crm.Domain.Prospects.EmailConfirmations.Events;
using Crm.Domain.Prospects.Rules;
using Crm.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Crm.Domain.Prospects
{
    public class Prospect: Entity, IAggregateRoot
    {
        public ProspectId Id { get; private set; }

        private string _email;

        private string _password;

        private string _country;

        private bool _emailConfirmedSent;

        private readonly List<EmailConfirmation> _emailConfirmations;

        private bool _isRemoved;

        private Prospect()
        {
            _emailConfirmations = new List<EmailConfirmation>();
            _emailConfirmedSent = false;
            _isRemoved = false;
        }

        public Prospect(string email, string password, string country)
        {
            Id = new ProspectId(Guid.NewGuid());
            _email = email;
            _password = password;
            _country = country;
            _emailConfirmations = new List<EmailConfirmation>();

            AddDomainEvent(new ProspectRegisteredEvent(Id));
        }

        public static Prospect CreateRegistered(string email, string password, string country, IProspectUniquenessChecker prospectUniquenessChecker)
        {
            // check here some logic , unique email...

            CheckRule(new ProspectEmailMustBeUniqueRule(prospectUniquenessChecker, email));

            return new Prospect(email, password, country);
        }

        public bool IsTokenValid(string token)
        {
            var emailConfirmation = _emailConfirmations.Find(x => x._token == token);
            return emailConfirmation.IsValid();
        }

        public void MarkAsConfirmedByEmail()
        {
            _emailConfirmedSent = true;
        }

        public void MarkProspectEmailAsConfirmed(string token)
        {

            var emailConfirmation = _emailConfirmations.Find(x => x._token == token);
            emailConfirmation.MarkAsConfirmed();

            AddDomainEvent(new EmailConfirmedEvent(Id));
        }

        public void Remove()
        {
            _isRemoved = true;
        }

        // CREATE ENTITY RELATIONS
        public EmailConfirmationId CreateEmailConfirmation(string token, long expireDate)
        {
            var emailConfirmation = EmailConfirmation.CreateNew(token, expireDate, false, null);

            _emailConfirmations.Add(emailConfirmation);

            AddDomainEvent(new EmailConfirmationRegisteredEvent(Id, emailConfirmation.Id));

            return emailConfirmation.Id;

        }
    }
}
