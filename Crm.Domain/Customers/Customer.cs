using Crm.Domain.SeedWork;
using Crm.Domain.Customers.BankAccounts;
using Crm.Domain.Customers.SubScriptions;
using System;
using System.Collections.Generic;
using Crm.Domain.Customers.SubScriptions.Events;
using System.Linq;
using Crm.Domain.Customers.Documents;
using Crm.Domain.Customers.BankAccounts.Events;
using Crm.Domain.Customers.Documents.Events;
using Crm.Domain.Customers.ApiKeys;
using Crm.Domain.Customers.ApiKeys.Events;
using Crm.Domain.Roles;
using Crm.Domain.Customers.BankAccounts.Transactions;
using Crm.Domain.Customers.BankAccounts.Transactions.Events;
using Crm.Domain.Customers.BankAccounts.Transfers;
using Crm.Domain.Customers.BankAccounts.Transfers.Events;
using Crm.Domain.Customers.Rules;
using TransactionStatus = Crm.Domain.Customers.BankAccounts.Transactions.TransactionStatus;

namespace Crm.Domain.Customers
{ 
    public class Customer: Entity, IAggregateRoot
    {
        public CustomerId Id { get; private set; }

        private string _username;

        private string _password;

        private string _token;

        private string _name;

        private string _surname;

        private string _email;

        private byte _type;

        private string _country;

        private string _city;

        private string _address;

        private string _phone;

        private string _profileRisk;

        private DateTime? _activationDate;
        
        private DateTime? _closeDate;

        private DateTime? _updateDate;

        private readonly List<Document>? _documents;

        private readonly List<SubScription>? _subScriptions;

        private readonly List<BankAccount>? _bankAccounts;

        private readonly List<ApiKey>? _apiKeys;

        private bool _isRemoved;

        private Customer()
        {
            _documents = new List<Document>();
            _subScriptions = new List<SubScription>();
            _bankAccounts = new List<BankAccount>();
            _apiKeys = new List<ApiKey>();
            _isRemoved = false;
        }

        // USER
        public Customer(string username, string password, string token, string name, string surname, string email, byte type, string country, string city, string address, string phone, string profileRisk, DateTime? activationDate, DateTime? closeDate, DateTime? updateDate)
        {
            Id = new CustomerId(Guid.NewGuid());
            _username = username;
            _password = password;
            _token = token;
            _name = name;
            _surname = surname;
            _email = email;
            _type = type;
            _country = country;
            _city = city;
            _address = address;
            _phone = phone;
    
            _profileRisk = profileRisk;
            _activationDate = activationDate;
            _closeDate = closeDate;
            _updateDate = updateDate;

            _documents = new List<Document>();
            _subScriptions = new List<SubScription>();
            _bankAccounts = new List<BankAccount>();
            _apiKeys = new List<ApiKey>();

            _isRemoved = false;

            AddDomainEvent(new CustomerRegisteredEvent(Id));
        }

        public static Customer CreateRegistered(string username, string password, string token, string name, string surname, string email, byte type, string country, string city, string address, string phone, string profileRisk, DateTime? activationDate, DateTime? closeDate, DateTime? updateDate)
        {

            // check here some logic , unique email...
            return new Customer(username, password, token, name, surname, email, type, country, city, address, phone, profileRisk, activationDate, closeDate, updateDate);
        }

        public void ChangeCustomer( string name, string surname, string email, byte type, string country, string city, string address, string phone, string profileRisk, DateTime? updateDate)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _type = type;
            _country = country;
            _city = city;
            _address = address;
            _phone = phone;
            _profileRisk = profileRisk;
            _updateDate = updateDate;

            AddDomainEvent(new CustomerChangedEvent(Id));
        }

        public void RefreshToken(string token)
        {
            _token = token;
            //AddDomainEvent(new MemberChangedEvent(Id));
        }

        public void Remove()
        {
            _isRemoved = true;
        }

        // END


        // ENTITY RELATIONS SUBSCRIPTIONS
        // CREATE SUBSCRIPTION 
        public SubScriptionId CreateSubScription(string name, string rev, decimal setupFee, decimal monthlyFee, decimal transactionFee, DateTime? updateDate)
        {
            var subscription = SubScription.CreateNew(name, rev, setupFee, monthlyFee, transactionFee, updateDate);

            _subScriptions.Add(subscription);

            AddDomainEvent(new SubScriptionRegisteredEvent(Id, subscription.Id));

            return subscription.Id;
        }

        // REMOVE ESUBSCRIPTION
        public void RemoveSubScription(SubScriptionId subScriptionId)
        {
            var subscription = _subScriptions.Single(x => x.Id == subScriptionId);

            subscription.Remove();

            AddDomainEvent(new SubScriptionRemovedEvent(subScriptionId));
        }

        // ENTITY RELATIONS BANKS
        // CREATE BANK 
        public BankAccountId CreateBankAccount(string currency, string description, string beneficiaryName, string bankName, string bankBrankName, string bankAddress, string bankSwiftBic, string bankAccountNumber, string iban, string intermediaryBankName, string intermediaryBankSwiftBic, string intermediaryBankAddress)
        {
            var bank = BankAccount.CreateNew(currency, description, beneficiaryName, bankName, bankBrankName, bankAddress, bankSwiftBic, bankAccountNumber, iban, intermediaryBankName, intermediaryBankSwiftBic, intermediaryBankAddress);

            _bankAccounts.Add(bank);

            AddDomainEvent(new BankAccountRegisteredEvent(Id, bank.Id));

            return bank.Id;
        }

        // CHANGE BANK
        public void ChangeBankAccount(BankAccountId bankId, string currency, string description, string beneficiaryName, string bankName, string bankBrankName, string bankAddress, string bankSwiftBic, string bankAccountNumber, string iban, string intermediaryBankName, string intermediaryBankSwiftBic, string intermediaryBankAddress)
        {
            var bank = _bankAccounts.Single(x => x.Id == bankId);

            bank.Change(currency, description, beneficiaryName, bankName, bankBrankName, bankAddress, bankSwiftBic, bankAccountNumber, iban, intermediaryBankName, intermediaryBankSwiftBic, intermediaryBankAddress);

            AddDomainEvent(new BankAccountChangeEvent(bank.Id));
        }

        // REMOVE BANK
        public void RemoveBank(BankAccountId bankId)
        {
            var bank = _bankAccounts.Single(x => x.Id == bankId);

            bank.Remove();

            AddDomainEvent(new BankAccountRemovedEvent(bank.Id));
        }

        // ENTITY RELATION BANK TRANSACTIONS
        // CREATE TRANSACTION 
        public TransactionId CreateBankAccountTransaction(BankAccountId bankId, string currency, decimal amount, DateTime beginDate, DateTime? endDate, byte? expectedYield, byte? actualYield, TransactionStatus status)
        {
            var bank = _bankAccounts.Single(x => x.Id == bankId);

            var transaction = bank.CreateNewTransaction(currency, amount, beginDate, endDate, expectedYield, actualYield, status);

            AddDomainEvent(new TransactionRegisteredEvent(Id, bankId, transaction.Id));

            return transaction.Id;
        }

        public void ChangeTransactionStatus(BankAccountId bankId, TransactionId transactionId, TransactionStatus status)
        {
            var bank = _bankAccounts.Single(x => x.Id == bankId);
            bank.ChangeTransactionStatus(transactionId, status);
        }

        // REMOVE TRANSACTION
        public void RemoveBankAccountTransaction(TransactionId transactionId)
        {
            var bank = _bankAccounts.Single(x => x.Id == transactionId);

            bank.RemoveTransaction(transactionId);

           // AddDomainEvent(new BankAccountRemovedEvent(bank.Id));
        }

        // ENTITY RELATION BANK TRANSFER
        // CREATE TRANSFER 
        public TransferId CreateBankAccountTransfer(BankAccountId bankId, string publicId, string currency, string side, decimal amount, TransferStatus status, TransferType type, DateTime date, ICustomerLiquidyChecker customerLiquidyChecker)
        {
            var bank = _bankAccounts.Single(x => x.Id == bankId);

            CheckRule(new CustomerBankAccountMustHaveLiquidityRule(customerLiquidyChecker, bankId.Value, amount));

            var transfer = bank.CreateNewTransfer(publicId, currency, side, amount, status, type, date);

            AddDomainEvent(new TransferRegisteredEvent(Id, bank.Id, transfer.Id));

            return transfer.Id;
        }

        public void ChangeBankAccountTransfer(BankAccountId bankId, TransferId transferId, TransferStatus status)
        {
            var bank = _bankAccounts.Single(x => x.Id == bankId);

             bank.ChangeTransfer(transferId, status);
        }

        // REMOVE TRANSFER
        public void RemoveBankAccountTransfer(BankAccountId bankId, TransferId transferId)
        {
            var bank = _bankAccounts.Single(x => x.Id == bankId);

            bank.RemoveTransfer(transferId);

            // AddDomainEvent(new BankAccountRemovedEvent(bank.Id));
        }

        // DOCUMENTS
        // CREATE ENTITY RELATIONS
        public DocumentId CreateDocument(string documentType, byte[] documentData, byte documentStatus, string auditedBy, DateTime? auditedDate)
        {
            var document = Document.CreateNew(documentType, documentData, documentStatus, auditedBy, auditedDate);

            _documents.Add(document);

            AddDomainEvent(new DocumentRegisterEvent(Id, document.Id));

            return document.Id;
        }

        // REMOVE ENTITY RELATIONS
        public void RemoveDocument(DocumentId documentId)
        {
            var document = _subScriptions.Single(x => x.Id == documentId);

            document.Remove();

            AddDomainEvent(new DocumentRemovedEvent(documentId));
        }

        // CREATE API KEY 
        public ApiKeyId CreateApiKey(string name, string key, List<RoleId> roleIds)
        {
            var apiKey = ApiKey.CreateNew(name, key);

            if (roleIds != null && roleIds.Count > 0)
            {
                foreach (var roleId in roleIds)
                {
                    apiKey.AddRole(roleId);
                }
            }

            _apiKeys.Add(apiKey);

            AddDomainEvent(new ApiKeyRegisteredEvent(Id, apiKey.Id));

            return apiKey.Id;
        }

        // CHANGE API KEY

        public void RemoveApiKeyRoles(ApiKeyId apiKeyId)
        {
            var apiKey = _apiKeys.Single(x => x.Id == apiKeyId);

            apiKey.RemoveRoles();

        }
        public void ChangeApiKey(ApiKeyId apiKeyId, string name, string key, List<RoleId> roleIds)
        {
            var apiKey = _apiKeys.Single(x => x.Id == apiKeyId);

            apiKey.Change(name,key, roleIds);

            AddDomainEvent(new ApiKeyChangedEvent(apiKey.Id));
        }

        // REMOVE API KEY 
        public void RemoveApiKey(ApiKeyId apiKeyId)
        {
            var apiKey = _apiKeys.Single(x => x.Id == apiKeyId);

            apiKey.Remove();

            AddDomainEvent(new ApiKeyRemovedEvent(apiKey.Id));
        }
    }
}
