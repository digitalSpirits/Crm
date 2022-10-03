
using System.ComponentModel.DataAnnotations;


namespace Crm.Application.Customers.GetCustomersFiltered
{
    public class FilterCustomersRequest
    {

        [Required]
        public string Filter { get; set; }
    }
}
