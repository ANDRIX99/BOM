using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOM.Data;
using BOM.Model;

namespace BOM.Pages.DistintaBase
{
    public class DeleteModel : PageModel
    {
        private readonly BOM.Data.BOMContext _context;

        public DeleteModel(BOM.Data.BOMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BOM.Model.DistintaBase DistintaBase { get; set; } = default!;

        public IList<BOM.Model.Item> Items { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distintabase = await _context.DistintaBase.FirstOrDefaultAsync(m => m.Id == id);

            if (distintabase is not null)
            {
                Items = await _context.Item.ToListAsync();
                ViewData["ItemList"] = _context.Item.ToListAsync();
                ViewData["Id"] = id;

                DistintaBase = distintabase;

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

            var distintabase = await _context.DistintaBase.FindAsync(id);
            if (distintabase != null)
            {
                DistintaBase = distintabase;
                _context.DistintaBase.Remove(DistintaBase);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
