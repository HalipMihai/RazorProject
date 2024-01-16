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

namespace RazorProject.Pages.Dogs
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public EditModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dog Dog { get; set; } = default!;

        [BindProperty]
        public Race FirstRace { get; set; } = default!;

        [BindProperty]
        public Race SecondRace { get; set; } = default!;

        void GetEnclosures()
        {
            // find all enclosures which still have available space
            var enclosuresWithSpace = _context.Enclosures
                .Where(e => (e.Dogs!.Sum(dog => (int)dog.Size) <= e.Capacity))
                .OrderBy(e => e.Capacity - (e.Dogs!.Sum(dog => (int)dog.Size)))
                .ToList();
            var enclosures = enclosuresWithSpace.Select(e => new SelectListItem(e.Name, e.ID.ToString()));

            ViewData["Enclosures"] = enclosures;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GetEnclosures();
            var dog =  await _context.Dogs.FirstOrDefaultAsync(m => m.ID == id);
            if (dog == null)
            {
                return NotFound();
            }
            Dog = dog;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // check if the selected enclosure has enough space for this dog
            var enclosure = _context.Enclosures
                .Where(e => e.ID == Dog.EnclosureId).First()!;
            var usedCapacity = enclosure.Dogs != null ? enclosure.Dogs!.Sum(dog => (int)dog.Size) : 0;
            var capacity = enclosure.Capacity;
            if (capacity - usedCapacity < (int)Dog.Size)
            {
                ModelState.AddModelError(nameof(Dog.EnclosureId), "The selected enclosure does not have enough space for this dog");
            }

            if (!ModelState.IsValid)
            {
                GetEnclosures();
                return Page();
            }

            Dog.Race = FirstRace | SecondRace;

            _context.Attach(Dog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogExists(Dog.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DogExists(int id)
        {
            return _context.Dogs.Any(e => e.ID == id);
        }
    }
}
