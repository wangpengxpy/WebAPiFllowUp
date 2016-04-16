using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebAPiFllowUp.Filters
{
    public class BasicAuthenticationIdentity : GenericIdentity
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }     
        public BasicAuthenticationIdentity(string name, string password)
            : base(name, "Basic")
        {
            this.UserName = name;
            this.UserPassword = password;
        }
    }
}