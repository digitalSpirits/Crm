using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Crm.Api.Authentication
{
    [DataContract]
    public class AuthenticationRequest
    {
        [StringLength(30, MinimumLength = 8, ErrorMessage = "UserName must have a minimum length of {0} and maximum length of {1} ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserName is required")]
        public string UserName { get; set; }


        [StringLength(60, MinimumLength = 8, ErrorMessage = "Password must have a minimum length of {1} and maximum length of {0} ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
