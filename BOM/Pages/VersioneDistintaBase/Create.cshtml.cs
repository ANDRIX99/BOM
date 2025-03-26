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

namespace BOM.Pages.VersioneDistintaBase
{
    public class CreateModel : PageModel
    {
        private readonly BOM.Data.BOMContext _context;

        public IList<Item> Product { get; set; }

        public CreateModel(BOM.Data.BOMContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Product = await _context.Item.ToListAsync();
            if (Product == null || !Product.Any()) Console.WriteLine("Product is null or is empty");

            return Page();
        }

        [BindProperty]
        public BOM.Model.VersioneDistintaBase VersioneDistintaBase { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("OnPostAsync in VersioneDistintaBase/Create");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("!ModelState.IsValid == false");
                // return Page();
            }

            _context.VersioneDistintaBase.Add(VersioneDistintaBase);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
