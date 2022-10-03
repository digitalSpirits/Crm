
using Crm.Domain.Prospects;
using Crm.Domain.Prospects.EmailConfirmations;
using Crm.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Crm.Infrastructure.Domain.Prospects
{
    internal sealed class ProspectEntityTypeConfiguration : IEntityTypeConfiguration<Prospect>
    {
        internal const string EmailConfirmationList = "_emailConfirmations";
        public void Configure(EntityTypeBuilder<Prospect> builder)
        {
            builder.ToTable("Prospect", SchemaNames.Web);
            
            builder.HasKey(c => c.Id);
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_password").HasColumnName("Password");
            builder.Property<string>("_country").HasColumnName("Country");
            builder.Property<bool>("_emailConfirmedSent").HasColumnName("EmailConfirmedSent");

            builder.OwnsMany<EmailConfirmation>(EmailConfirmationList, a =>
            {
                a.ToTable("ProspectEmailConfirmation", SchemaNames.Web);
                a.HasKey("Id");
                a.Property<EmailConfirmationId>("Id");

                a.WithOwner().HasForeignKey("ProspectId");

                a.Property<string>("_token").HasColumnName("Token");
                a.Property<long>("_expireDate").HasColumnName("ExpireDate");
                a.Property<bool>("_activated").HasColumnName("Activated");
                a.Property<DateTime?>("_activatedDate").HasColumnName("ActivatedDate");
            });
 
        }
    }
}