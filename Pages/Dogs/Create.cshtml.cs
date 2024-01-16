using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Dogs
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public CreateModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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

        public IActionResult OnGet()
        {
            GetEnclosures();
            return Page();
        }

        [BindProperty]
        public Dog Dog { get; set; } = default!;

        [BindProperty]
        public Race FirstRace { get; set; } = default!;

        [BindProperty]
        public Race SecondRace { get; set; } = default!;

        [BindProperty]
        public int EnclosureId { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // check if the selected enclosure has enough space for this dog
            var enclosure = _context.Enclosures
                .Where(e => e.ID == EnclosureId).First()!;
            var usedCapacity = enclosure.Dogs != null? enclosure.Dogs!.Sum(dog => (int)dog.Size) : 0;
            var capacity = enclosure.Capacity;
            if (capacity - usedCapacity < (int)Dog.Size)
            {
                ModelState.AddModelError(nameof(EnclosureId), "The selected enclosure does not have enough space for this dog");
            }

            if (!ModelState.IsValid)
            {
                GetEnclosures();
                return Page();
            }

            Dog.Race = FirstRace | SecondRace;
            Dog.EnclosureId = EnclosureId;

            _context.Dogs.Add(Dog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
