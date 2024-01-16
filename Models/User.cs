using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RazorProject.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [PersonalData]
        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}
