namespace DOTNET_Control.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        public bool isAdmin { get; set; } // Add this property
        public ICollection<UserBook> UserBooks { get; set; }
    }
}
