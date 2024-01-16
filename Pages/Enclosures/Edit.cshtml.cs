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

namespace RazorProject.Pages.Enclosures
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
        public Enclosure Enclosure { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enclosure =  await _context.Enclosures.FirstOrDefaultAsync(m => m.ID == id);
            if (enclosure == null)
            {
                return NotFound();
            }
            Enclosure = enclosure;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Enclosure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnclosureExists(Enclosure.ID))
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

        private bool EnclosureExists(int id)
        {
            return _context.Enclosures.Any(e => e.ID == id);
        }
    }
}
