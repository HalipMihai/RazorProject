using System.ComponentModel.DataAnnotations;

namespace RazorProject.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[A-Z][a-z]*$")]
        // starts with Upper case letter, contains: lower case, then a space, then another upper case letter and lower case letters
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[A-Z][a-z]*$")]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber {  get; set; }

        [Required]
        [StringLength(100, MinimumLength = 9)]
        public string Address { get; set; }

        public ICollection<AdoptionContract>? AdoptionContracts { get; set; }
    }
}
