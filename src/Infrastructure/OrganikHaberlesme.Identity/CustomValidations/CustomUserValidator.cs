using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using OrganikHaberlesme.Identity.Models;

namespace OrganikHaberlesme.Identity.CustomValidations
{
    public class CustomUserValidator : IUserValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            string[] Digits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            foreach (var item in Digits)
            {
                if (user.UserName[0].ToString() == item)
                {
                    errors.Add(new IdentityError() { Code = "UserNameContainsFirstLetterDigitContains", Description = "Kullanıcı adının ilk karakteri sayısal karakter içeremez" });
                }
            }

            var duplicatePhone = manager.Users.Any(x => x.PhoneNumber == user.PhoneNumber);

            if (duplicatePhone)
            {
                errors.Add(new IdentityError() { Code = "UserPhoneNumberDuplicate", Description = $"Phone '{user.PhoneNumber}' already exists." });
            }

            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
