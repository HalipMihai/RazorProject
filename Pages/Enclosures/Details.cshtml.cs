using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Enclosures
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public DetailsModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Enclosure Enclosure { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enclosure = await _context.Enclosures
                .Include(e => e.Dogs)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (enclosure == null)
            {
                return NotFound();
            }
            else
            {
                Enclosure = enclosure;
            }
            return Page();
        }
    }
}
