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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Console.WriteLine($"🟢 DEBUG: Richiesta ricevuta per Id = {id}");

            if (id == null)
            {
                Console.WriteLine("🚨 ERRORE: Id nullo, ritorno NotFound()");
                return NotFound();
            }

            // Eseguiamo la query senza Include per vedere i dati base
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


            return Page();
        }

        /*public async Task<IActionResult> OnGetAsync(int? id)
        {
            /*var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM VersioneDistintaBase WHERE Id = @id";
            var param = command.CreateParameter();
            param.ParameterName = "@id";
            param.Value = id;
            command.Parameters.Add(param);

            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                Console.WriteLine($"Record trovato manualmente: Id={reader["Id"]}, Version={reader["Version"]}");
            }
            else
            {
                Console.WriteLine("Nessun record trovato con la query manuale");
            }
            await connection.CloseAsync();

            Console.WriteLine($"Id received: {id}");
            if (id == null) return NotFound();

            // Console.WriteLine($"VersioneDistintaBase: {(VersioneDistintaBase != null ? "Found" : "Not Found")}");

            /*var versionedistintabase =  await _context.VersioneDistintaBase
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            var record = await _context.VersioneDistintaBase
                .Where(v => v.Id == id)
                .Select(v => new
                {
                    v.Id,
                    v.Version,
                    v.ProductId
                })
                .FirstOrDefaultAsync();

            if (record == null)
            {
                Console.WriteLine($"⚠️ VersioneDistintaBase con Id={id} non trovata!");
            }
            else
            {
                Console.WriteLine($"✅ VersioneDistintaBase trovata: Id={record.Id}, Version={record.Version}, ProductId={record.ProductId}");
            }

            var versionedistintabase = await _context.VersioneDistintaBase
                .Include(v => v.Product)
                .FirstOrDefaultAsync();

            //var query = _context.VersioneDistintaBase
            //    .Include(m => m.Product)
            //    .Where(m => m.Id == id)
            //    .ToQueryString();

            //Console.WriteLine(query);

            Console.WriteLine($"VersioneDistintaBase: {(VersioneDistintaBase != null ? "Found" : "Not Found")}");

            if (VersioneDistintaBase == null)
            {
                Console.WriteLine($"⚠️ VersioneDistintaBase con Id={id} non trovata!");
            }
            else
            {
                Console.WriteLine($"✅ VersioneDistintaBase trovata: Id={VersioneDistintaBase.Id}, Version={VersioneDistintaBase.Version}");

                if (VersioneDistintaBase.Product == null)
                {
                    Console.WriteLine($"⚠️ Product è NULL per VersioneDistintaBase con Id={VersioneDistintaBase.Id}!");
                }
                else
                {
                    Console.WriteLine($"✅ Product caricato: Id={VersioneDistintaBase.Product.Id}, Name={VersioneDistintaBase.Product.Name}");
                }
            }

            if (versionedistintabase == null) Console.WriteLine("id not found"); // return NotFound();

            if (VersioneDistintaBase is null)
            {

                Console.WriteLine($"VersioneDistintaBase not found for Id: {id}");
            }

            if (VersioneDistintaBase.Product is null)
            {
                Console.WriteLine("Errore: Product è null per questa VersioneDistintaBase");
                return BadRequest();
            }

            Console.WriteLine("Dati caricati con successo");
            return Page();
        }*/

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

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
