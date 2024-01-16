using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorProject.Data;
using RazorProject.Models;

namespace RazorProject.Pages.Enclosures
{
    public class IndexModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public IndexModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Enclosure> Enclosure { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Enclosure = await _context.Enclosures
                .Include(e => e.Dogs)
                .ToListAsync();
        }
    }
}
