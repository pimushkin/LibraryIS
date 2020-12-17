using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.CrossCutting.Configuration
{
    public class AuthenticationOptions
    {
        public const string JsonKey = "ClientAuthentication";
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
