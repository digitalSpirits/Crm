using System;
using Crm.Domain.Customers;
using Crm.Domain.Customers.ApiKeys;
using Crm.Domain.Customers.BankAccounts;
using Crm.Domain.Customers.BankAccounts.Transactions;
using Crm.Domain.Customers.BankAccounts.Transfers;
using Crm.Domain.Customers.Documents;
using Crm.Domain.Customers.SubScriptions;
using Crm.Domain.Roles;
using Crm.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Crm.Infrastructure.Domain.Customers
{
    internal sealed class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {

        internal const string SubScriptionsList = "_subScriptions";
        internal const string BankAccountsList = "_bankAccounts";
        internal const string DocumentsList = "_documents";
        internal const string ApiKeysList = "_apiKeys";
        internal const string KeyRolesList = "_roles";
        internal const string TransactionsList = "_transactions";
        internal const string TransfersList = "_transfers";
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", SchemaNames.Crm);
            
            builder.HasKey(c => c.Id);

            builder.Property<string>("_username").HasColumnName("Username");
            builder.Property<string>("_password").HasColumnName("Password");
            builder.Property<string>("_token").HasColumnName("Token");
            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<string>("_surname").HasColumnName("Surname");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<byte>("_type").HasColumnName("Type");
            builder.Property<string>("_country").HasColumnName("Country");
            builder.Property<string>("_city").HasColumnName("City");
            builder.Property<string>("_address").HasColumnName("Address");
            builder.Property<string>("_phone").HasColumnName("Phone");
            builder.Property<string>("_profileRisk").HasColumnName("ProfileRisk");
            builder.Property<DateTime?>("_activationDate").HasColumnName("ActivationDate");
            builder.Property<DateTime?>("_closeDate").HasColumnName("CloseDate");
            builder.Property<DateTime?>("_updateDate").HasColumnName("UpdateDate");
            builder.Property<bool>("_isRemoved").HasColumnName("IsRemoved");

            builder.OwnsMany<SubScription>(SubScriptionsList, a =>
            {
                a.ToTable("Subscriptions", SchemaNames.Crm);
                a.HasKey("Id");
                a.Property<SubScriptionId>("Id");

                a.WithOwner().HasForeignKey("CustomerId");

                a.Property<string>("_name").HasColumnName("Name");
                a.Property<string>("_rev").HasColumnName("Rev");
                a.Property<decimal>("_setupFee").HasColumnName("SetupFee");
                a.Property<decimal>("_monthlyFee").HasColumnName("MonthlyFee");
                a.Property<decimal>("_transactionFee").HasColumnName("TransactionFee");
                a.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
                a.Property<DateTime?>("_updateDate").HasColumnName("UpdateDate");
            });

            builder.OwnsMany<BankAccount>(BankAccountsList, a =>
            {
                a.ToTable("BankAccounts", SchemaNames.Crm);
                a.HasKey("Id");
                a.Property<BankAccountId>("Id");

                a.WithOwner().HasForeignKey("CustomerId");

                a.Property<string>("_currency").HasColumnName("Currency");
                a.Property<string>("_description").HasColumnName("Description");
                a.Property<string>("_beneficiaryName").HasColumnName("BeneficiaryName");
                a.Property<string>("_bankName").HasColumnName("BankName");
                a.Property<string>("_bankBranchName").HasColumnName("BankBranchName");
                a.Property<string>("_bankAddress").HasColumnName("BankAddress");
                a.Property<string>("_bankSwiftBic").HasColumnName("BankSwiftBic");
                a.Property<string>("_bankAccountNumber").HasColumnName("BankAccountNumber");
                a.Property<string>("_iban").HasColumnName("Iban");
                a.Property<string>("_intermediaryBankName").HasColumnName("IntermediaryBankName");
                a.Property<string>("_intermediaryBankSwiftBic").HasColumnName("IntermediaryBankSwiftBic");
                a.Property<string>("_intermediaryBankAddress").HasColumnName("IntermediaryBankAddress");
                a.Property<bool>("_isRemoved").HasColumnName("IsRemoved");

                a.OwnsMany<Transaction>(TransactionsList, t =>
                {
                    t.ToTable("Transactions", SchemaNames.Crm);
                    t.HasKey("Id");
                    t.Property<TransactionId>("Id");

                    t.WithOwner().HasForeignKey("BankAccountId");

                    t.Property<string>("_currency").HasColumnName("Currency"); 
                    t.Property<decimal>("_amount").HasColumnName("Amount");
                    t.Property<DateTime>("_beginDate").HasColumnName("BeginDate");
                    t.Property<DateTime?>("_endDate").HasColumnName("EndDate");
                    t.Property<decimal?>("_expectedYield").HasColumnName("ExpectedYield");
                    t.Property<decimal?>("_actualYield").HasColumnName("ActualYield");
                    t.Property("_status").HasColumnName("Status").HasConversion(new EnumToNumberConverter<TransactionStatus, byte>());
                    t.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
                });

                a.OwnsMany<Transfer>(TransfersList, t =>
                {
                    t.ToTable("Transfers", SchemaNames.Crm);
                    t.HasKey("Id");
                    t.Property<TransferId>("Id");

                    t.WithOwner().HasForeignKey("BankAccountId");

                    t.Property<string>("PublicId").HasColumnName("PublicId");
                    t.Property<string>("_currency").HasColumnName("Currency");
                    t.Property<string>("_side").HasColumnName("Side");
                    t.Property<decimal>("_amount").HasColumnName("Amount");
                    t.Property("_status").HasColumnName("Status").HasConversion(new EnumToNumberConverter<TransferStatus, byte>());
                    t.Property("_type").HasColumnName("Type").HasConversion(new EnumToNumberConverter<TransferType, byte>());
                    t.Property<DateTime>("_date").HasColumnName("Date");
                    t.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
                });


            });

            builder.OwnsMany<Document>(DocumentsList, a =>
            {
                a.ToTable("Documents", SchemaNames.Crm);
                a.HasKey("Id");
                a.Property<DocumentId>("Id");

                a.WithOwner().HasForeignKey("CustomerId");
                a.Property<string>("_documentType").HasColumnName("DocumentType"); // .HasConversion(new EnumToNumberConverter<DocumentType, byte>());
                a.Property<byte[]>("_documentData").HasColumnName("DocumentData");
                a.Property<byte>("_documentStatus").HasColumnName("DocumentStatus");
                a.Property<string>("_auditedBy").HasColumnName("AuditedBy");
                a.Property<DateTime?>("_auditedDate").HasColumnName("AuditedDate");
                a.Property<bool>("_isRemoved").HasColumnName("IsRemoved");
            });

            builder.OwnsMany<ApiKey>(ApiKeysList, a =>
            {
                a.WithOwner().HasForeignKey("CustomerId");

                a.ToTable("ApiKeys", SchemaNames.Crm);
                a.HasKey("Id");
                a.Property<ApiKeyId>("Id");

                a.Property<string>("_name").HasColumnName("Name");
                a.Property<string>("_key").HasColumnName("Key");
                a.Property<bool>("_isRemoved").HasColumnName("IsRemoved");

                a.OwnsMany<KeyRole>(KeyRolesList, r =>
                {  
                    r.WithOwner().HasForeignKey("KeyId");

                    r.ToTable("ApiKeysRoles", SchemaNames.Crm);
                    r.HasKey("KeyId", "RoleId");
                    r.Property<ApiKeyId>("KeyId");
                    r.Property<RoleId>("RoleId");
                });
            });
        }
    }
}