using System.ComponentModel.DataAnnotations;

namespace OrganikHaberlesme.Mvc.Models.User
{
    public class RegisterVm
    {
        //[Required]
        //public string FirstName { get; set; }

        //[Required]
        //public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
