using Crm.Domain.Customers.BankAccounts.Transactions;
using Crm.Domain.Customers.BankAccounts.Transfers;
using Crm.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crm.Domain.Customers.BankAccounts
{
    public class BankAccount : Entity
    {
        internal BankAccountId Id;

        private string _currency;

        private string _description;

        private string _beneficiaryName;

        private string _bankName;

        private string _bankBranchName;

        private string _bankAddress;

        private string _bankSwiftBic;

        private string _bankAccountNumber;

        private string _iban;

        private string _intermediaryBankName;

        private string _intermediaryBankSwiftBic;

        private string _intermediaryBankAddress;

        private readonly List<Transaction>? _transactions;

        private readonly List<Transfer>? _transfers;

        private bool _isRemoved;

        private BankAccount()
        {
            _transactions = new List<Transaction>();
            _transfers = new List<Transfer>();
            _isRemoved = false;
        }
        public BankAccount(string currency, string description, string beneficiaryName, string bankName, string bankBranchName, string bankAddress, string bankSwiftBic, string bankAccountNumber, string iban, string intermediaryBankName, string intermediaryBankSwiftBic, string intermediaryBankAddress)
        {
            Id = new BankAccountId(Guid.NewGuid());
            _currency = currency;
            _description = description;
            _beneficiaryName = beneficiaryName;
            _bankName = bankName;
            _bankBranchName = bankBranchName;
            _bankAddress = bankAddress;
            _bankSwiftBic = bankSwiftBic;
            _bankAccountNumber = bankAccountNumber;
            _iban = iban;
            _intermediaryBankName = intermediaryBankName;
            _intermediaryBankSwiftBic = intermediaryBankSwiftBic;
            _intermediaryBankAddress = intermediaryBankAddress;
            _isRemoved = false;
        }

        internal static BankAccount CreateNew(string currency, string description, string beneficiaryName, string bankName, string bankBranchName, string bankAddress, string bankSwiftBic, string bankAccountNumber, string iban, string intermediaryBankName, string intermediaryBankSwiftBic, string intermediaryBankAddress)
        {
            return new BankAccount(currency, description, beneficiaryName, bankName, bankBranchName, bankAddress, bankSwiftBic, bankAccountNumber, iban, intermediaryBankName, intermediaryBankSwiftBic, intermediaryBankAddress);
        }

        internal void Change(string currency, string description, string beneficiaryName, string bankName, string bankBranchName, string bankAddress, string bankSwiftBic, string bankAccountNumber, string iban, string intermediaryBankName, string intermediaryBankSwiftBic, string intermediaryBankAddress)
        {
            _currency = currency;
            _description = description;
            _beneficiaryName = beneficiaryName;
            _bankName = bankName;
            _bankBranchName = bankBranchName;
            _bankAddress = bankAddress;
            _bankSwiftBic = bankSwiftBic;
            _bankAccountNumber = bankAccountNumber;
            _iban = iban;
            _intermediaryBankName = intermediaryBankName;
            _intermediaryBankSwiftBic = intermediaryBankSwiftBic;
            _intermediaryBankAddress = intermediaryBankAddress;
        }

        internal void Remove()
        {
            _isRemoved = true;
        }

        // TRANSACTION
        // CREATE TRANSACTION
        internal Transaction CreateNewTransaction(string currency, decimal amount, DateTime beginDate, DateTime? endDate, byte? expectedYield, byte? actualYield, TransactionStatus status)
        {
            var transaction = Transaction.CreateNew(currency, amount, beginDate, endDate, expectedYield, actualYield, status);

            _transactions.Add(transaction);

            return transaction;
        }

        // CHANGE TRANSACTION STATUS
        internal void ChangeTransactionStatus(TransactionId transactionId, TransactionStatus status)
        {
            var transaction = _transactions.Single(x => x.Id == transactionId);
            transaction.ChangeStatus(status);
        }

        // REMOVE TRANSACTION
        internal void RemoveTransaction(TransactionId transactionId)
        {
            var transaction = _transactions.Single(x => x.Id == transactionId);

            transaction.Remove();
        }

        // TRANSFER
        // CREATE ENTITY RELATIONS 
        internal Transfer CreateNewTransfer(string publicId, string currency, string side, decimal amount, TransferStatus status, TransferType type, DateTime date)
        {
            var transfer = Transfer.CreateNew(publicId, currency, side, amount, status, type, date);

            _transfers.Add(transfer);

            return transfer;
        }

        internal void ChangeTransfer(TransferId transferId, TransferStatus status)
        {
            var transfer = _transfers.Single(x => x.Id == transferId);

            transfer.ChangeStatus(status);
        }

        internal void RemoveTransfer(TransferId transferId)
        {
            var transfer = _transfers.Single(x => x.Id == transferId);

            transfer.Remove();
        }
    }
}
