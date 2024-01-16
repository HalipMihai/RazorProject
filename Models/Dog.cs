using System.ComponentModel.DataAnnotations;

namespace RazorProject.Models
{
    [Flags]
    public enum Race
    {
        Unknown         = 0x000,
        Bulldog         = 0x001,
        Husky           = 0x002,
        Akita           = 0x004,
        BorderCollie    = 0x008,
        Labrador        = 0x010,
        Terrier         = 0x020,
        WienerDog       = 0x040,
        GermanShepherd  = 0x080,
        Rottweiler      = 0x100,
    }

    public enum Size
    {
        Small   = 1, // a small  dog needs 1x space
        Medium  = 2, // a medium dog needs 2x space
        Large   = 3  // a large  dog needs 3x space
    }

    public class Dog : IValidatableObject
    {
        public int ID { get; set; }

        [Required]
        [Range(0, 20)]
        public uint Age { get; set; }

        public bool? Vaccinated { get; set; }

        [Required]
        public Race Race { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalDate { get; set; }

        [Required]
        public Size Size { get; set; }

        [StringLength(30)]
        [RegularExpression(@"^[A-Z][a-z\s]*$")]
        // starts with Upper case letter, contains: upper case, lower case, spaces
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Comments { get; set; }

        public int? AdoptionContractId { get; set; }
        public AdoptionContract? AdoptionContract { get; set; }

        public int? EnclosureId { get; set; }
        public Enclosure? Enclosure { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ArrivalDate >= DateTime.Now.Add(TimeSpan.FromDays(7)))
                yield return new ValidationResult("ArrivalDate cannot be too long in the future", new[] { nameof(ArrivalDate) });
            if (ArrivalDate < DateTime.Now.Subtract(TimeSpan.FromDays(30)))
                yield return new ValidationResult("ArrivalDate cannot be too long in the past!", new[] { nameof(ArrivalDate) });
        }
    }
}
