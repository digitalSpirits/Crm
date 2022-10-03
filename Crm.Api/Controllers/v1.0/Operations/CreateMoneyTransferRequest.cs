namespace Crm.Api.Customers
{

    public class CreateMoneyTransferRequest
    {
        public Creditor Creditor { get; set; }
        public string ExecutionDate { get; set; }
        public string Uri { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsInstant { get; set; }
        public string FeeType { get; set; }
        public string FeeAccountId { get; set; }
        public TaxRelief TaxRelief { get; set; }

    }
    public class Creditor
    {
        public string Name { get; set; }
        public Account Account { get; set; }
        public Address Address { get; set; }
    }

    public class Account
    {
        public string AccountCode { get; set; }
        public string BicCode { get; set; }
    }

    public class Address
    {
        public object Street { get; set; }
        public object City { get; set; }
        public object CountryCode { get; set; }
    }

    public class TaxRelief
    {
        public string TaxReliefId { get; set; }
        public bool IsCondoUpgrade { get; set; }
        public string CreditorFiscalCode { get; set; }
        public string BeneficiaryType { get; set; }
        public NaturalPersonBeneficiary NaturalPersonBeneficiary { get; set; }
        public LegalPersonBeneficiary LegalPersonBeneficiary { get; set; }
    }

    public class NaturalPersonBeneficiary
    {
        public string FiscalCode1 { get; set; }
        public object FiscalCode2 { get; set; }
        public object FiscalCode3 { get; set; }
        public object FiscalCode4 { get; set; }
        public object FiscalCode5 { get; set; }
    }

    public class LegalPersonBeneficiary
    {
        public object FiscalCode { get; set; }
        public object LegalRepresentativeFiscalCode { get; set; }
    }
}
