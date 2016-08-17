using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSOSimulator.Models
{
    public class LoginResultViewModel
    {
        public string UserName { get; set; }

        public string EncryptedPassword { get; set; }

        public string LicenseeName { get; set; }
    }
}