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

        public BOM.Model.DistintaBase DistintaBase { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var distintabase = await _context.DistintaBase.FirstOrDefaultAsync(m => m.Id == id);

            if (distintabase is not null)
            {
                DistintaBase = distintabase;

                return Page();
            }

            return NotFound();
        }
    }
}
