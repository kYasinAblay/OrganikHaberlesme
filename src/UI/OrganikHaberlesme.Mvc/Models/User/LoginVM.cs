
using System.ComponentModel.DataAnnotations;

namespace OrganikHaberlesme.Mvc.Models.User
{
    public class LoginVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set;}
    }
}
