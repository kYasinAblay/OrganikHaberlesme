using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganikHaberlesme.Application.Models.Identity
{
    public class AuthOptions
    {

        public string Provider { get; set; }

        public string Code { get; set; }

        public bool IsPersistence { get; set; }
        public bool RememberClient { get; set; }
    }
}

