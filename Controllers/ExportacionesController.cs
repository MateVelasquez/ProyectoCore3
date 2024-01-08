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
    public class ExportacionesController : Controller
    {
        private readonly CoreContext _context;

        public ExportacionesController(CoreContext context)
        {
            _context = context;
        }

        // GET: Exportaciones
        public async Task<IActionResult> Index()
        {
            var coreContext = _context.Exportacions.Include(e => e.IdTrabajadorNavigation);
            return View(await coreContext.ToListAsync());
        }

        // GET: Exportaciones/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Exportacions == null)
            {
                return NotFound();
            }

            var exportacion = await _context.Exportacions
                .Include(e => e.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdExportacion == id);
            if (exportacion == null)
            {
                return NotFound();
            }

            return View(exportacion);
        }

        // GET: Exportaciones/Create
        public IActionResult Create()
        {
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula");
            return View();
        }

        // POST: Exportaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdExportacion,IdTrabajador,Cantidad,FechaExportacion")] Exportacion exportacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exportacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", exportacion.IdTrabajador);
            return View(exportacion);
        }

        

        // GET: Exportaciones/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Exportacions == null)
            {
                return NotFound();
            }

            var exportacion = await _context.Exportacions.FindAsync(id);
            if (exportacion == null)
            {
                return NotFound();
            }
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", exportacion.IdTrabajador);
            return View(exportacion);
        }

        // POST: Exportaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdExportacion,IdTrabajador,Cantidad,FechaExportacion")] Exportacion exportacion)
        {
            if (id != exportacion.IdExportacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exportacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportacionExists(exportacion.IdExportacion))
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
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", exportacion.IdTrabajador);
            return View(exportacion);
        }

        // GET: Exportaciones/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Exportacions == null)
            {
                return NotFound();
            }

            var exportacion = await _context.Exportacions
                .Include(e => e.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdExportacion == id);
            if (exportacion == null)
            {
                return NotFound();
            }

            return View(exportacion);
        }

        // POST: Exportaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Exportacions == null)
            {
                return Problem("Entity set 'CoreContext.Exportacions'  is null.");
            }
            var exportacion = await _context.Exportacions.FindAsync(id);
            if (exportacion != null)
            {
                _context.Exportacions.Remove(exportacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExportacionExists(byte id)
        {
          return _context.Exportacions.Any(e => e.IdExportacion == id);
        }
    }
}
