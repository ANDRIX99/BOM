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
            Console.WriteLine($"🟢 DEBUG: Richiesta ricevuta per Id = {id}");

            if (id == null)
            {
                Console.WriteLine("🚨 ERRORE: Id nullo, ritorno NotFound()");
                return NotFound();
            }

            // Query for fill ProductList
            var ProductList = await _context.Item.ToListAsync();
            Console.WriteLine("Product list count: " + ProductList.Count );
            Console.WriteLine("Product list: ");
            foreach ( var item in ProductList )
            {
                Console.WriteLine("Item id: " + item.Id + " " + "Item Name: " + item.Name);
            }

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

            Console.WriteLine($"✅ TROVATO: Id = {recordBase.Id}, Version = {recordBase.Version}, ProductId = {recordBase.ProductId}");

            // Adesso carichiamo con Include
            VersioneDistintaBase = await _context.VersioneDistintaBase
                .Include(v => v.Product)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (VersioneDistintaBase == null)
            {
                Console.WriteLine($"🚨 ERRORE: VersioneDistintaBase è NULL dopo Include! Questo è molto strano!");
                return NotFound();
            }

            Console.WriteLine($"✅ VersioneDistintaBase caricata: Id = {VersioneDistintaBase.Id}, Version = {VersioneDistintaBase.Version}");

            if (VersioneDistintaBase.Product == null)
            {
                Console.WriteLine($"🚨 ERRORE: Product è NULL nonostante Include!");
                return BadRequest();
            }

            Console.WriteLine($"🟢 DEBUG: VersioneDistintaBase è {(VersioneDistintaBase == null ? "NULL" : "OK")}");
            // Console.WriteLine(VersioneDistintaBase);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("???? OnPostAsync: OnPostAsync was call");

            // View error on ModelState
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Errore in {state.Key}: {error.ErrorMessage}");
                    }
                }
            }

            if (!ModelState.IsValid) return Page();
            Console.WriteLine("????? OnPostAsync ModelState: " + ModelState);

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
