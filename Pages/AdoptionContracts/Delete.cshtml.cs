using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.AdoptionContracts
{
    public class DeleteModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public DeleteModel(RazorProject.Data.ApplicationDbContext context)
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

            var adoptioncontract = await _context.AdoptionContracts.FirstOrDefaultAsync(m => m.ID == id);

            if (adoptioncontract == null)
            {
                return NotFound();
            }
            else
            {
                AdoptionContract = adoptioncontract;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptioncontract = await _context.AdoptionContracts.FindAsync(id);
            if (adoptioncontract != null)
            {
                AdoptionContract = adoptioncontract;
                _context.AdoptionContracts.Remove(AdoptionContract);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
