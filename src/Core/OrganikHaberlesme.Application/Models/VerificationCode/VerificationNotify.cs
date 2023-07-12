using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganikHaberlesme.Application.Models.VerificationCode
{
    public class VerificationNotify
    {
        public string MailTo { get; set; }

        public string Code { get; set; }
        public string Message { get; set; }
        public string PhoneNumber { get; set; }
    }
}
