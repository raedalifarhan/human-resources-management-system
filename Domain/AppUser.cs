
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        [DisplayName("First Name"), MaxLength(50)]
        public string FirstName { get; set; } = default!;


        [DisplayName("Last Name"), MaxLength(50)]
        public string LastName { get; set; } 
            = string.Empty;
    }
}
