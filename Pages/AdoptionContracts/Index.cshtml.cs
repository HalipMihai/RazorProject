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

namespace RazorProject.Pages.AdoptionContracts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly RazorProject.Data.ApplicationDbContext _context;

        public IndexModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AdoptionContract> AdoptionContract { get;set; } = default!;

        public async Task OnGetAsync()
        {
            AdoptionContract = await _context.AdoptionContracts
                .Include(a => a.Person).ToListAsync();
        }
    }
}
