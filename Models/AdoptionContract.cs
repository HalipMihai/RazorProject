using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace RazorProject.Models
{
    public class AdoptionContract
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        public Dog? Dog { get; set; }

        public int? PersonId { get; set; }
        public Person? Person { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateTime >= DateTime.Now.Add(TimeSpan.FromDays(7)))
                yield return new ValidationResult("DateTime cannot be too long in the future", new[] { nameof(DateTime) });
            if (DateTime < DateTime.Now.Subtract(TimeSpan.FromDays(30)))
                yield return new ValidationResult("DateTime cannot be too long in the past!", new[] { nameof(DateTime) });
        }
    }
}
