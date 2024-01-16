using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Dogs
{
    public class IndexModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public IndexModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Dog> Dogs { get;set; } = default!;

        [BindProperty]
        public bool ShowOnlyAdoptable { get; set; } = true;

        [BindProperty]
        public bool ShowOnlyVaccinated { get; set; } = false;

        [BindProperty]
        public bool ShowOnlyMixedRaces{ get; set; } = false;

        public async Task GetDogs()
        {
            IEnumerable<Dog> dogs = await _context.Dogs
                .Include(dog => dog.Enclosure)
                .ToListAsync();

            if (ShowOnlyAdoptable)
                dogs = dogs.Where(dog => dog.AdoptionContractId == null);

            if (ShowOnlyVaccinated)
                dogs = dogs.Where(dog => dog.Vaccinated! == true);

            if (ShowOnlyMixedRaces)
            {
                var knownRaces = Enum.GetValues<Race>();
                dogs = dogs.Where(dog => !knownRaces.Contains(dog.Race));
            }

            Dogs = dogs.ToList();
        }

        public async Task OnGetAsync()
        {
            await GetDogs();
        }

        public async Task OnPostAsync()
        {
            await GetDogs();
        }
    }
}
