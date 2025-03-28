using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOM.Data;
using BOM.Model;
using Microsoft.EntityFrameworkCore;

namespace BOM.Pages.DistintaBase
{
    public class CreateModel : PageModel
    {
        private readonly BOM.Data.BOMContext _context;

        public CreateModel(BOM.Data.BOMContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BOM.Model.DistintaBase DistintaBase { get; set; }

        [BindProperty]
        public IList<BOM.Model.VersioneDistintaBase> VersionList { get; set; }

        [BindProperty]
        public int VersioneDistintaBaseId { get; set; }

        [BindProperty]
        public int ItemId { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewData["VersionList"] = await _context.VersioneDistintaBase.ToListAsync();
            ViewData["ItemList"] = await _context.Item.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If model is not valid, I repopulate the list to avoid the error in the view
                ViewData["VersionList"] = await _context.VersioneDistintaBase.ToListAsync();
                ViewData["ItemList"] = await _context.Item.ToListAsync();
                // return Page();
            }

            // Create new object DistintaBase without edit class structure
            var nuovaDistintaBase = new BOM.Model.DistintaBase
            {
                VersioneDistintaBaseId = VersioneDistintaBaseId,
                FiglioId = ItemId,
                Amount = Quantity
            };

            try
            {
                _context.DistintaBase.Add(nuovaDistintaBase);
                await _context.SaveChangesAsync();
            } catch (DbUpdateException ex)
            {
                Console.WriteLine("Error during updating database");
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
