using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorProject.Models;

namespace RazorProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RazorProject.Models.Dog> Dogs { get; set; } = default!;

        public DbSet<RazorProject.Models.AdoptionContract> AdoptionContracts { get; set; } = default!;

        public DbSet<RazorProject.Models.Person> Persons {  get; set; } = default!;

        public DbSet<RazorProject.Models.Enclosure> Enclosures { get; set; } = default!;
    }
}
