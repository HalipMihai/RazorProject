using System.ComponentModel.DataAnnotations;

namespace RazorProject.Models
{
    public class Enclosure
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z][A-Za-z\d\s]*$")]
        // starts with Upper case letter, contains: upper case, lower case, digits and spaces
        public string Name { get; set; }

        [Required]
        [Range(0, 30)]
        public int Capacity { get; set; }

        public ICollection<Dog>? Dogs { get; set; }
    }
}
