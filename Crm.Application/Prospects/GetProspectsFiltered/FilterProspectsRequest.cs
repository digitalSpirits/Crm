using System.ComponentModel.DataAnnotations;

namespace Crm.Application.Prospects.GetProspectsFiltered
{
    public class FilterProspectsRequest
    {
        [Required]
        public string Filter { get; set; }
    }
}
