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
    public class DetailsModel : PageModel
    {
        private readonly BOM.Data.BOMContext _context;

        public DetailsModel(BOM.Data.BOMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BOM.Model.DistintaBase DistintaBase { get; set; } = default!;

        [BindProperty]
        public IList<BOM.Model.DistintaBase> ListDistintaBase { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var distintabase = await _context.DistintaBase
                .Include(d => d.VersioneDistintaBase)
                .Include(d => d.Figlio)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["ItemList"] = await _context.Item.ToListAsync();
            ViewData["VersioneDistintaBase"] = await _context.VersioneDistintaBase.ToListAsync();
            // ViewData["DistintaBase"] = await _context.DistintaBase.ToListAsync();
            ViewData["Id"] = id;
            ListDistintaBase = await _context.DistintaBase
                .Where(d => d.VersioneDistintaBaseId == id)
                .Include(d => d.Figlio)
                .Include(d => d.VersioneDistintaBase)
                .ToListAsync();
            DistintaBase = distintabase;

            Console.WriteLine($"DistintaBases count: {ListDistintaBase?.Count ?? 0}");

            //if (distintabase is null) return NotFound();

            return Page();
        }
    }
}
