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

            // Eseguiamo la query senza Include per vedere i dati base
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        public async Task<IActionResult> OnPostAsync()
        {
            // Debugging
            Console.WriteLine("???? OnPostAsync: OnPostAsync was call");
            Console.WriteLine($"???? OnPostAsync: VersioneDistintaBase.ProductId = {VersioneDistintaBase.ProductId}");
            Console.WriteLine($"???? OnPostAsync: VersioneDistintaBase.CreationTime = {VersioneDistintaBase.CreationTime}");

            // Find existing entity on the database
            var versioneDistintaBaseDb = await _context.VersioneDistintaBase
                .Include(v => v.Product)
                .FirstOrDefaultAsync(v => v.Id == VersioneDistintaBase.Id);

            if (versioneDistintaBaseDb == null) return NotFound();

            // Updating entitys properties
            versioneDistintaBaseDb.ProductId = VersioneDistintaBase.ProductId;
            versioneDistintaBaseDb.Version = VersioneDistintaBase.Version;
            versioneDistintaBaseDb.CreationTime = VersioneDistintaBase.CreationTime;

            // View error on ModelState
            if (!ModelState.IsValid)
            {
                Console.WriteLine("OnPostAsync: !ModelState.IsValid");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                    }
                }
                Console.WriteLine("OnPostAsync: !ModelState.IsValie get new product list");
                ViewData["ProductList"] = await _context.Item.ToListAsync();
            }

            // I do this query to be sure to populate ViewData["ProductList"]
            // and in case is empty I repopulate with the list
            // Query for fill ProductList
            // var ProductList = await _context.Item.ToListAsync();
            //Console.WriteLine("Product list count: " + ProductList.Count);
            //Console.WriteLine("Product list: ");
            //foreach (var item in ProductList)
            //{
            //    Console.WriteLine("Item id: " + item.Id + " " + "Item Name: " + item.Name);
            //}

            // ViewData["ProductList"] = ProductList;

            if (!ModelState.IsValid)
            {
                Console.WriteLine("???? OnPostAsync: Return the same page with some error error = idk");
                return Page();
            }
            Console.WriteLine("????? OnPostAsync ModelState: " + ModelState);

            Console.WriteLine($"???? OnPostAsync CreationTime: {VersioneDistintaBase.CreationTime}");

            _context.Attach(VersioneDistintaBase).State = EntityState.Modified;

            try
            {
                // Save changes
                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"???? OnPostAsync: Number of records affected = {result}");
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("OnPostAsync: DbUpdateConcurrencyException ex");
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
