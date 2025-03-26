using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOM.Data;
using BOM.Model;

namespace BOM.Pages.VersioneDistintaBase
{
    public class EditModel : PageModel
    {
        private readonly BOM.Data.BOMContext _context;

        public EditModel(BOM.Data.BOMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BOM.Model.VersioneDistintaBase VersioneDistintaBase { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var versionedistintabase =  await _context.VersioneDistintaBase
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (versionedistintabase == null) return NotFound();

            if (VersioneDistintaBase is null) return NotFound();
            if (VersioneDistintaBase.Product is null) return BadRequest();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(VersioneDistintaBase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VersioneDistintaBaseExists(VersioneDistintaBase.Id)) return NotFound();
                else throw;
            }

            return RedirectToPage("./Index");
        }

        private bool VersioneDistintaBaseExists(int id)
        {
            return _context.VersioneDistintaBase.Any(e => e.Id == id);
        }
    }
}
