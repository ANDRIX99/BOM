using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOM.Data;
using BOM.Model;

namespace BOM.Pages.VersioneDistintaBase
{
    public class DeleteModel : PageModel
    {
        private readonly BOM.Data.BOMContext _context;

        public DeleteModel(BOM.Data.BOMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BOM.Model.VersioneDistintaBase VersioneDistintaBase { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var versionedistintabase = await _context.VersioneDistintaBase.FirstOrDefaultAsync(m => m.Id == id);

            if (versionedistintabase is not null)
            {
                VersioneDistintaBase = versionedistintabase;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var versionedistintabase = await _context.VersioneDistintaBase.FindAsync(id);
            if (versionedistintabase != null)
            {
                VersioneDistintaBase = versionedistintabase;
                _context.VersioneDistintaBase.Remove(VersioneDistintaBase);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
