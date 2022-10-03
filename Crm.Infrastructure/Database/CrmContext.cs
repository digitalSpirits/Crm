
using Crm.Domain.Customers;
using Crm.Domain.Prospects;
using Crm.Domain.Roles;
using Crm.Infrastructure.Processing.InternalCommands;
using Crm.Infrastructure.Processing.Outbox;
using Microsoft.EntityFrameworkCore;
using System;

namespace Crm.Infrastructure.Database
{
    public class CrmContext : DbContext
    {
        public DbSet<Prospect> Prospects { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public CrmContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .LogTo(Console.WriteLine)
                .EnableDetailedErrors();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrmContext).Assembly);
        }
    }
}
