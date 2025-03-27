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
using Microsoft.EntityFrameworkCore.Storage;

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

        [BindProperty]
        public List<BOM.Model.Item> ProductList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Console.WriteLine($"🟢 DEBUG: Richiesta ricevuta per Id = {id}");

            if (id == null)
            {
                Console.WriteLine("🚨 ERRORE: Id nullo, ritorno NotFound()");
                return NotFound();
            }

            // Query for fill ProductList
            var ProductList = await _context.Item.ToListAsync();
            //Console.WriteLine("Product list count: " + ProductList.Count );
            //Console.WriteLine("Product list: ");
            //foreach ( var item in ProductList )
            //{
            //    Console.WriteLine("Item id: " + item.Id + " " + "Item Name: " + item.Name);
            //}

            ViewData["ProductList"] = ProductList;

            // Query without Include() to see data
            var recordBase = await _context.VersioneDistintaBase
                .Where(v => v.Id == id)
                .Select(v => new
                {
                    v.Id,
                    v.Version,
                    v.ProductId
                })
                .FirstOrDefaultAsync();

            if (recordBase == null)
            {
                Console.WriteLine($"🚨 ERRORE: Nessun record trovato con Id = {id}");
                return NotFound();
            }

            // Console.WriteLine($"✅ TROVATO: Id = {recordBase.Id}, Version = {recordBase.Version}, ProductId = {recordBase.ProductId}");

            // Adesso carichiamo con Include
            VersioneDistintaBase = await _context.VersioneDistintaBase
                .Include(v => v.Product)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (VersioneDistintaBase == null)
            {
                Console.WriteLine($"🚨 ERRORE: VersioneDistintaBase è NULL dopo Include! Questo è molto strano!");
                return NotFound();
            }

            // Console.WriteLine($"✅ VersioneDistintaBase caricata: Id = {VersioneDistintaBase.Id}, Version = {VersioneDistintaBase.Version}");

            if (VersioneDistintaBase.Product == null)
            {
                Console.WriteLine($"🚨 ERRORE: Product è NULL nonostante Include!");
                return BadRequest();
            }

            // Console.WriteLine($"🟢 DEBUG: VersioneDistintaBase è {(VersioneDistintaBase == null ? "NULL" : "OK")}");
            // Console.WriteLine(VersioneDistintaBase);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Debugging: verify passed values
            // Console.WriteLine("OnPostAsync: OnPostAsync was called.");
            // Console.WriteLine($"OnPostAsync: VersioneDistintaBase.ProductId = {VersioneDistintaBase.ProductId}");

            // Recover entity from database
            var versioneDistintaBaseDb = await _context.VersioneDistintaBase
                .Include(v => v.Product)  // Carica la relazione con Product
                .FirstOrDefaultAsync(v => v.Id == VersioneDistintaBase.Id);

            // Console.WriteLine("???? OnPostAsync: Are you here 1");

            if (versioneDistintaBaseDb == null)
            {
                return NotFound();
            }

            // Console.WriteLine("???? OnPostAsync: Are you here 2");

            // Verify ProductId is not null and then verify with a valid Id on Item table
            if (VersioneDistintaBase.ProductId == 0 || !await _context.Item.AnyAsync(i => i.Id == VersioneDistintaBase.ProductId))
            {
                ModelState.AddModelError("VersioneDistintaBase.ProductId", "The Product is required.");
                ViewData["ProductList"] = await _context.Item.ToListAsync();
                return Page();
            }

           //  Console.WriteLine("???? OnPostAsync: Are you here 3");

            // Assign new value (is not necessary to touch 'Product')
            versioneDistintaBaseDb.ProductId = VersioneDistintaBase.ProductId;
            versioneDistintaBaseDb.Version = VersioneDistintaBase.Version;
            versioneDistintaBaseDb.CreationTime = VersioneDistintaBase.CreationTime;

            // Console.WriteLine("???? OnPostAsync: Are you here 3.1");

            // Verify errors in ModelState
            if (!ModelState.IsValid)
            {
                // Console.WriteLine("???? OnPostAsync: Are you here 3.2");
                ViewData["ProductList"] = await _context.Item.ToListAsync();
                // return Page();
            }

            // Console.WriteLine("???? OnPostAsync: Are you here 4");

            // Mark the entity as modified
            _context.Attach(versioneDistintaBaseDb).State = EntityState.Modified;


            // Console.WriteLine($"???? OnPostAsync: Entity state before save: {_context.Entry(versioneDistintaBaseDb).State}");

            try
            {
                //Console.WriteLine("???? OnPostAsync: Are you here 5");
                // Save changes
                var result = await _context.SaveChangesAsync();
                // Console.WriteLine($"OnPostAsync: Number of records affected = {result}");
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("???? OnPostAsync: Are you here 6");
                if (!VersioneDistintaBaseExists(VersioneDistintaBase.Id)) return NotFound();
                else throw;
            }

            // Console.WriteLine("???? OnPostAsync: Are you here 7");
            // Return to Index page
            return RedirectToPage("./Index");
        }

        private bool VersioneDistintaBaseExists(int id)
        {
            return _context.VersioneDistintaBase.Any(e => e.Id == id);
        }
    }
}
