using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.AdoptionContracts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public CreateModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Persons"] = _context.Persons.Select(person => new SelectListItem($"{person.FirstName} {person.LastName}", person.ID.ToString()));

            ViewData["Dogs"] = _context.Dogs
                .Include(dog => dog.Enclosure)
                .Where(dog => dog.AdoptionContractId == null)
                .Select(dog => new SelectListItem($"Name: {dog.Name} / Race: {dog.Race} / Enclosure: {dog.Enclosure!.Name}", dog.ID.ToString()));

            return Page();
        }

        [BindProperty]
        public AdoptionContract AdoptionContract { get; set; } = default!;

        [BindProperty]
        public int DogId { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AdoptionContract.Dog = _context.Dogs.First(dog => dog.ID == DogId);

            _context.AdoptionContracts.Add(AdoptionContract);

            // remove the adopted dog from enclosure
            var enclosure = _context.Enclosures.First(enclosure => enclosure.Dogs != null && enclosure.Dogs.Select(dog => dog.ID).Contains(DogId));
            var dog = enclosure.Dogs!.First(dog => dog.ID == DogId);
            enclosure.Dogs!.Remove(dog);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
