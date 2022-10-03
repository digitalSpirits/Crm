using Crm.Domain.Roles;
using Crm.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Infrastructure.Domain.Roles
{
    internal sealed class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", SchemaNames.Crm );
            
            builder.HasKey(c => c.Id);
            builder.Property<string>("_category").HasColumnName("Category");
            builder.Property<string>("Name").HasColumnName("Name");
        }
   }
}