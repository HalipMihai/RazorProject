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

namespace RazorProject.Pages.Dogs
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public DetailsModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Dog Dog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dogs
                .Include(dog => dog.Enclosure)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dog == null)
            {
                return NotFound();
            }
            else
            {
                Dog = dog;
            }
            return Page();
        }
    }
}
