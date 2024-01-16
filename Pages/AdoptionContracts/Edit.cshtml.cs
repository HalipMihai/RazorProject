using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.AdoptionContracts
{
    public class EditModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public EditModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AdoptionContract AdoptionContract { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptioncontract =  await _context.AdoptionContracts.FirstOrDefaultAsync(m => m.ID == id);
            if (adoptioncontract == null)
            {
                return NotFound();
            }
            AdoptionContract = adoptioncontract;
           ViewData["PersonId"] = new SelectList(_context.Persons, "ID", "ID");
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

            _context.Attach(AdoptionContract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdoptionContractExists(AdoptionContract.ID))
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

        private bool AdoptionContractExists(int id)
        {
            return _context.AdoptionContracts.Any(e => e.ID == id);
        }
    }
}
