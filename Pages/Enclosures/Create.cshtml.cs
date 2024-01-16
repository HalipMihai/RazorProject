using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Enclosures
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
            return Page();
        }

        [BindProperty]
        public Enclosure Enclosure { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Enclosures.Add(Enclosure);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
