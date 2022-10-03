using Crm.Domain.SeedWork;
using System;

namespace Crm.Domain.Prospects.EmailConfirmations
{
    public class EmailConfirmation : Entity
    {
        internal EmailConfirmationId Id;

        internal string _token;

        private long _expireDate;

        private bool _activated;

        private DateTime? _activatedDate;

        public EmailConfirmation(string token, long expireDate, bool activated, DateTime? activatedDate)
        { 
            Id = Id = new EmailConfirmationId(Guid.NewGuid());
            _token = token;
            _expireDate = expireDate;
            _activated = activated;
            _activatedDate = activatedDate;
        }

        private EmailConfirmation() {
        
        }

        internal static EmailConfirmation CreateNew(string token, long expireDate, bool activated, DateTime? activatedDate)
        {
            return new EmailConfirmation(token, expireDate, activated, activatedDate); 
        }

        internal void MarkAsConfirmed()
        {
            _activated = true;
            _activatedDate = DateTime.UtcNow;
        }

        internal bool IsValid()
        {
            return _expireDate >= DateTime.UtcNow.Ticks;
        }
    }
}
