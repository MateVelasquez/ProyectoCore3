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
    public class ProyeccionAnualController : Controller
    {
        private readonly CoreContext _context;

        public ProyeccionAnualController(CoreContext context)
        {
            _context = context;
        }

        // GET: ProyeccionAnual
        public async Task<IActionResult> Index()
        {
            var coreContext = _context.ProyeccionAnuals.Include(p => p.IdExportacionNavigation).Include(p => p.IdTrabajadorNavigation);
            return View(await coreContext.ToListAsync());
        }

        // GET: ProyeccionAnual/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.ProyeccionAnuals == null)
            {
                return NotFound();
            }

            var proyeccionAnual = await _context.ProyeccionAnuals
                .Include(p => p.IdExportacionNavigation)
                .Include(p => p.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdProyeccionAnual == id);
            if (proyeccionAnual == null)
            {
                return NotFound();
            }

            return View(proyeccionAnual);
        }

        // GET: ProyeccionAnual/Create
        public IActionResult Create()
        {
            ViewData["IdExportacion"] = new SelectList(_context.Exportacions, "IdExportacion", "IdExportacion");
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula");
            return View();
        }

        // POST: ProyeccionAnual/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProyeccionAnual,IdExportacion,IdTrabajador,Cantidad")] ProyeccionAnual proyeccionAnual)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyeccionAnual);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdExportacion"] = new SelectList(_context.Exportacions, "IdExportacion", "IdExportacion", proyeccionAnual.IdExportacion);
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", proyeccionAnual.IdTrabajador);
            return View(proyeccionAnual);
        }

        // GET: ProyeccionAnual/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.ProyeccionAnuals == null)
            {
                return NotFound();
            }

            var proyeccionAnual = await _context.ProyeccionAnuals.FindAsync(id);
            if (proyeccionAnual == null)
            {
                return NotFound();
            }
            ViewData["IdExportacion"] = new SelectList(_context.Exportacions, "IdExportacion", "IdExportacion", proyeccionAnual.IdExportacion);
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", proyeccionAnual.IdTrabajador);
            return View(proyeccionAnual);
        }

        // POST: ProyeccionAnual/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdProyeccionAnual,IdExportacion,IdTrabajador,Cantidad")] ProyeccionAnual proyeccionAnual)
        {
            if (id != proyeccionAnual.IdProyeccionAnual)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyeccionAnual);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyeccionAnualExists(proyeccionAnual.IdProyeccionAnual))
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
            ViewData["IdExportacion"] = new SelectList(_context.Exportacions, "IdExportacion", "IdExportacion", proyeccionAnual.IdExportacion);
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", proyeccionAnual.IdTrabajador);
            return View(proyeccionAnual);
        }

        // GET: ProyeccionAnual/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.ProyeccionAnuals == null)
            {
                return NotFound();
            }

            var proyeccionAnual = await _context.ProyeccionAnuals
                .Include(p => p.IdExportacionNavigation)
                .Include(p => p.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdProyeccionAnual == id);
            if (proyeccionAnual == null)
            {
                return NotFound();
            }

            return View(proyeccionAnual);
        }

        // POST: ProyeccionAnual/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.ProyeccionAnuals == null)
            {
                return Problem("Entity set 'CoreContext.ProyeccionAnuals'  is null.");
            }
            var proyeccionAnual = await _context.ProyeccionAnuals.FindAsync(id);
            if (proyeccionAnual != null)
            {
                _context.ProyeccionAnuals.Remove(proyeccionAnual);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyeccionAnualExists(byte id)
        {
          return _context.ProyeccionAnuals.Any(e => e.IdProyeccionAnual == id);
        }
    }
}
