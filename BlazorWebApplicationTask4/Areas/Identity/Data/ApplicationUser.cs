using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebApplicationTask4.Areas.Identity.Data {
    public class ApplicationUser : IdentityUser {
        public ApplicationUser() {

        }
        public string SocialName { get; set; }
        public DateTime FirstLoginDate { get; set; }
        public DateTime LastLogoutDate { get; set; }
        public string Status { get; set; }
    }
}
