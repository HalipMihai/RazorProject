using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorProject.Data;
using RazorProject.Models;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace RazorProject.Pages.AdoptionContracts
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        static DetailsModel()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        private readonly RazorProject.Data.ApplicationDbContext _context;

        public DetailsModel(RazorProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public AdoptionContract AdoptionContract { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptioncontract = await _context.AdoptionContracts
                .Include(contract => contract.Dog)
                .Include(contract => contract.Person)
                .FirstOrDefaultAsync(m => m.ID == id);
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

            var adoptioncontract = await _context.AdoptionContracts
                .Include(contract => contract.Dog)
                .Include(contract => contract.Person)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (adoptioncontract == null)
            {
                return NotFound();
            }
            else
            {
                AdoptionContract = adoptioncontract;
            }

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(16));

                    page.Header()
                        .DefaultTextStyle(x => x.SemiBold())
                        .Column(x =>
                        {
                            x.Item().Text($"Contract #{AdoptionContract.ID}");
                            x.Item().Text($"Date: {AdoptionContract.DateTime.ToShortDateString()}");
                        });
                        

                    page.Content()
                        .PaddingVertical(2, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().AlignCenter();
                            x.Item().Text($"Adoption contract").Bold();

                            x.Item().AlignLeft();
                            x.Item().Text($"We, the undersigned,");
                            x.Item().Text(t =>
                            {
                                t.Span($"{AdoptionContract.Person!.FirstName} {AdoptionContract.Person!.LastName}").Underline();
                                t.Span($", located at address ");
                                t.Span($"{AdoptionContract.Person!.Address}").Underline();
                                t.Span($", with phone number ");
                                t.Span($"{AdoptionContract.Person!.PhoneNumber}").Underline();
                                t.Span($",");
                            }); 
                            x.Item().Text(t =>
                            {
                                t.Span($"and Dog Shelter, located in Cluj, 10 Libertatii Street");
                                t.Span($",");
                            });
                            x.Item().Text(t =>
                            {
                                t.Span($"have agreed to transfer the rescued dog named \"");
                                t.Span($"{AdoptionContract.Dog!.Name}").Underline();
                                t.Span($"\", aged {AdoptionContract.Dog!.Age}, race {AdoptionContract.Dog!.Race}");
                            });

                            x.Item().AlignLeft();
                            x.Item().Text($"Date and time: {AdoptionContract.DateTime}");
                            x.Item().PaddingTop(20);
                            x.Item().Text($"Signature of {AdoptionContract.Person!.FirstName} {AdoptionContract.Person!.LastName}");
                            x.Item().PaddingTop(20);
                            x.Item().Text($"Signature of shelter representative");
                        });
                });
            });

            var bytes = doc.GeneratePdf();
            return new FileStreamResult(new MemoryStream(bytes), "application/pdf");
        }
    }
}
