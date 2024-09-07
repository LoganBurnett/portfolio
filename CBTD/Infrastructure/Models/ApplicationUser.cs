using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        [DisplayName("Street Address")]
        public string? StreetAddress { get; set; }

        public string? City { get; set; }
        public string? State { get; set; }
        [DisplayName("Postal Code")]
        public string? PostalCode { get; set; }

        // NotMapped means don't map it to the database
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
    }

}
