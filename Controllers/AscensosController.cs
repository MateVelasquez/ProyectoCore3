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
    public class AscensosController : Controller
    {
        private readonly CoreContext _context;

        public AscensosController(CoreContext context)
        {
            _context = context;
        }

        // GET: Ascensos
        public async Task<IActionResult> Index()
        {
            var coreContext = _context.Ascensos.Include(a => a.IdTrabajadorNavigation);
            return View(await coreContext.ToListAsync());
        }

        // GET: Ascensos/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Ascensos == null)
            {
                return NotFound();
            }

            var ascenso = await _context.Ascensos
                .Include(a => a.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdAscenso == id);
            if (ascenso == null)
            {
                return NotFound();
            }

            return View(ascenso);
        }

        // GET: Ascensos/Create
        public IActionResult Create()
        {
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula");
            return View();
        }

        // POST: Ascensos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAscenso,IdTrabajador,Sustentacion")] Ascenso ascenso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ascenso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", ascenso.IdTrabajador);
            return View(ascenso);
        }

        // GET: Ascensos/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Ascensos == null)
            {
                return NotFound();
            }

            var ascenso = await _context.Ascensos.FindAsync(id);
            if (ascenso == null)
            {
                return NotFound();
            }
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", ascenso.IdTrabajador);
            return View(ascenso);
        }

        // POST: Ascensos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdAscenso,IdTrabajador,Sustentacion")] Ascenso ascenso)
        {
            if (id != ascenso.IdAscenso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ascenso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AscensoExists(ascenso.IdAscenso))
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
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", ascenso.IdTrabajador);
            return View(ascenso);
        }

        // GET: Ascensos/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Ascensos == null)
            {
                return NotFound();
            }

            var ascenso = await _context.Ascensos
                .Include(a => a.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdAscenso == id);
            if (ascenso == null)
            {
                return NotFound();
            }

            return View(ascenso);
        }

        // POST: Ascensos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Ascensos == null)
            {
                return Problem("Entity set 'CoreContext.Ascensos'  is null.");
            }
            var ascenso = await _context.Ascensos.FindAsync(id);
            if (ascenso != null)
            {
                _context.Ascensos.Remove(ascenso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AscensoExists(byte id)
        {
          return _context.Ascensos.Any(e => e.IdAscenso == id);
        }
    }
}
