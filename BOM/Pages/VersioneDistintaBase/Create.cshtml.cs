using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOM.Data;
using BOM.Model;

namespace BOM.Pages.VersioneDistintaBase
{
    public class CreateModel : PageModel
    {
        private readonly BOM.Data.BOMContext _context;

        public CreateModel(BOM.Data.BOMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BOM.Model.VersioneDistintaBase VersioneDistintaBase { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.VersioneDistintaBase.Add(VersioneDistintaBase);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
