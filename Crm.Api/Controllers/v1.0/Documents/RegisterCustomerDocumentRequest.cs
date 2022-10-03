using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace Crm.Api.Customers
{
    public class RegisterCustomerDocumentRequest
    {

        [Required]
        public string DocumentType { get; set; }

        [Required]
        public IFormFile DocumentData { get; set; }

        public byte? DocumentStatus { get; set; }

        public string AuditedBy { get; set; }

        public DateTime? AuditedDate { get; set; }
    }
}
