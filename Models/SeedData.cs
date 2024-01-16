using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RazorProject.Data;

namespace RazorProject.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context == null)
                {
                    throw new ArgumentNullException("Null ApplicationDbContext");
                }

                if (context.Enclosures.IsNullOrEmpty())
                {
                    context.Enclosures.AddRange(
                        new Enclosure() { Name = "Left pen", Capacity = 5 },
                        new Enclosure() { Name = "Right pen", Capacity = 5 },
                        new Enclosure() { Name = "Small pen", Capacity = 1 },
                        new Enclosure() { Name = "Temporary fenced pen", Capacity = 3 },
                        new Enclosure() { Name = "Garden court", Capacity = 6 },
                        new Enclosure() { Name = "Front yard", Capacity = 3 },
                        new Enclosure() { Name = "Back yard", Capacity = 3 },
                        new Enclosure() { Name = "Caged area", Capacity = 2 }
                    );
                    context.SaveChanges();
                }

                if (context.Persons.IsNullOrEmpty())
                {
                    for (int i = 1; i < 10; i++)
                    {
                        context.Persons.Add(new Person()
                        {
                            Address = $"Str. Cluj, nr. {i}",
                            FirstName = $"Prenume {i}",
                            LastName = $"Nume {i}",
                            PhoneNumber = $"+4070000000{i}"
                        });
                    }
                    context.SaveChanges();
                }

                if (context.Dogs.IsNullOrEmpty())
                {
                    context.Dogs.AddRange(
                        new Dog() { EnclosureId = 1, Size = Size.Medium, Age = 5, ArrivalDate = DateTime.UnixEpoch, Race = Race.Bulldog, Vaccinated = false, Name = "Dog #1" },
                        new Dog() { EnclosureId = 2, Size = Size.Small, Age = 1, ArrivalDate = DateTime.UnixEpoch, Race = Race.Terrier, Name = "Dog #2" },
                        new Dog() { EnclosureId = 3, Size = Size.Small, Age = 8, ArrivalDate = DateTime.UnixEpoch, Race = Race.GermanShepherd | Race.Rottweiler, Vaccinated = true, Name = "Dog #3" },
                        new Dog() { EnclosureId = 4, Size = Size.Medium, Age = 1, ArrivalDate = DateTime.UnixEpoch, Race = Race.WienerDog | Race.Unknown, Vaccinated = true, Name = "???" },
                        new Dog() { EnclosureId = 5, Size = Size.Small, Age = 3, ArrivalDate = DateTime.UnixEpoch, Race = Race.Husky, Name = "" },
                        new Dog() { EnclosureId = 6, Size = Size.Small, Age = 2, ArrivalDate = DateTime.UnixEpoch, Race = Race.Akita | Race.BorderCollie, Vaccinated = false, Name = "Test4" },
                        new Dog() { EnclosureId = 7, Size = Size.Medium, Age = 2, ArrivalDate = DateTime.UnixEpoch, Race = Race.BorderCollie, Vaccinated = false, Name = "Test3" }
                    );
                    context.SaveChanges();
                }

            }
        }
    }
}
