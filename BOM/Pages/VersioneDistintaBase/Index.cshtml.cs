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
    public class IndexModel : PageModel
    {
        private readonly BOM.Data.BOMContext _context;

        public IndexModel(BOM.Data.BOMContext context)
        {
            _context = context;
        }

        public IList<BOM.Model.VersioneDistintaBase> VersioneDistintaBase { get;set; } = default!;

        public async Task OnGetAsync()
        {
            VersioneDistintaBase = await _context.VersioneDistintaBase.ToListAsync();

            ViewData["ItemList"] = await _context.Item.ToListAsync();
        }
    }
}
