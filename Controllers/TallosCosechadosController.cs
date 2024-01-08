using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCore.Models;

namespace ProyectoCore.Controllers
{
    public class TallosCosechadosController : Controller
    {
        private readonly CoreContext _context;

        public TallosCosechadosController(CoreContext context)
        {
            _context = context;
        }

        // GET: TallosCosechados
        public async Task<IActionResult> Index()
        {
            var coreContext = _context.TallosCosechados.Include(t => t.IdTrabajadorNavigation);
            return View(await coreContext.ToListAsync());
        }

        // GET: TallosCosechados/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.TallosCosechados == null)
            {
                return NotFound();
            }

            var tallosCosechado = await _context.TallosCosechados
                .Include(t => t.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdTallosCosechados == id);
            if (tallosCosechado == null)
            {
                return NotFound();
            }

            return View(tallosCosechado);
        }

        // GET: TallosCosechados/Create
        public IActionResult Create()
        {
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula");
            return View();
        }

        // POST: TallosCosechados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTallosCosechados,IdTrabajador,Cantidad,FechaCosecha")] TallosCosechado tallosCosechado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tallosCosechado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", tallosCosechado.IdTrabajador);
            return View(tallosCosechado);
        }

        // GET: TallosCosechados/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.TallosCosechados == null)
            {
                return NotFound();
            }

            var tallosCosechado = await _context.TallosCosechados.FindAsync(id);
            if (tallosCosechado == null)
            {
                return NotFound();
            }
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", tallosCosechado.IdTrabajador);
            return View(tallosCosechado);
        }

        // POST: TallosCosechados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdTallosCosechados,IdTrabajador,Cantidad,FechaCosecha")] TallosCosechado tallosCosechado)
        {
            if (id != tallosCosechado.IdTallosCosechados)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tallosCosechado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TallosCosechadoExists(tallosCosechado.IdTallosCosechados))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", tallosCosechado.IdTrabajador);
            return View(tallosCosechado);
        }

        // GET: TallosCosechados/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.TallosCosechados == null)
            {
                return NotFound();
            }

            var tallosCosechado = await _context.TallosCosechados
                .Include(t => t.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdTallosCosechados == id);
            if (tallosCosechado == null)
            {
                return NotFound();
            }

            return View(tallosCosechado);
        }

        // POST: TallosCosechados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.TallosCosechados == null)
            {
                return Problem("Entity set 'CoreContext.TallosCosechados'  is null.");
            }
            var tallosCosechado = await _context.TallosCosechados.FindAsync(id);
            if (tallosCosechado != null)
            {
                _context.TallosCosechados.Remove(tallosCosechado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TallosCosechadoExists(byte id)
        {
          return _context.TallosCosechados.Any(e => e.IdTallosCosechados == id);
        }
    }
}
