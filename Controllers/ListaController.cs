using Microsoft.AspNetCore.Mvc;
using lista.Data;
using lista.Models;
using Microsoft.EntityFrameworkCore;

namespace lista.Controllers
{
    public class ListaController : Controller
    {
        private readonly AppDbContext _context;

        public ListaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Lista
        public async Task<IActionResult> Index()
        {
            var listas = await _context.Listas.ToListAsync();
            return View(listas);
        }

        // GET: Lista/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lista/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListaModel lista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lista);
        }

        // GET: Lista/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var lista = await _context.Listas.FindAsync(id);
            if (lista == null) return NotFound();

            return View(lista);
        }

        // POST: Lista/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ListaModel lista)
        {
            if (id != lista.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Listas.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lista);
        }

        // GET: Lista/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var lista = await _context.Listas.FirstOrDefaultAsync(m => m.Id == id);
            if (lista == null) return NotFound();

            return View(lista);
        }

        // POST: Lista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lista = await _context.Listas.FindAsync(id);
            if (lista != null)
            {
                _context.Listas.Remove(lista);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}