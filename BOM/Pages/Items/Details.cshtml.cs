using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOM.Data;
using BOM.Model;

namespace BOM.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly BOM.Data.BOMContext _context;

        public DetailsModel(BOM.Data.BOMContext context)
        {
            _context = context;
        }

        public Item Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FirstOrDefaultAsync(m => m.Id == id);

            if (item is not null)
            {
                Item = item;

                return Page();
            }

            return NotFound();
        }
    }
}
