using System.ComponentModel.DataAnnotations;

namespace OrganikHaberlesme.Application.Models.Identity
{
    public class RegistrationRequest
    {
        //[Required]
        //public string FirstName { get; set; }

        //[Required]
        //public string LastName { get; set; }


        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
      
        [Required]
        [EmailAddress]
        public string Email { get; set; }

          [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
