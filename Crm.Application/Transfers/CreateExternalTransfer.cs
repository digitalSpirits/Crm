using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Crm.Application.Transfers
{

    public class CreateExternalTransfer
    {
        [JsonPropertyName("external_transfer")]
        public ExternalTransfer ExternalTransfer { get; set; }
    }

    public class ExternalTransfer
    {
        [JsonPropertyName("beneficiary_id")]
        public string BeneficiarId { get; set; }

        [JsonPropertyName("debit_iban")]
        public string DebitIban { get; set; }

        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("scheduled_date")]
        public DateOnly ScheduledDate { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("attachment_ids")]
        public List<string> AttachmentIds { get; set; }

    }
}
