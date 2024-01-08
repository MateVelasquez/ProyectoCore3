using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCore.Models;

namespace ProyectoCore.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TrabajadoresController : Controller
    {
        
        private readonly CoreContext _context;

        public TrabajadoresController(CoreContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index()
        {
              return _context.Trabajadors != null ? 
                          View(await _context.Trabajadors.ToListAsync()) :
                          Problem("Entity set 'CoreContext.Trabajadors'  is null.");
        }

        // GET: Trabajadores/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Trabajadors == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadors
                .FirstOrDefaultAsync(m => m.IdTrabajador == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTrabajador,Cedula,Nombre,Email,Salario,FechaContratacion,AreaTrabajo")] Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trabajador);
        }

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Trabajadors == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadors.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }
            return View(trabajador);
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdTrabajador,Cedula,Nombre,Email,Salario,FechaContratacion,AreaTrabajo")] Trabajador trabajador)
        {
            if (id != trabajador.IdTrabajador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadorExists(trabajador.IdTrabajador))
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
            return View(trabajador);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Trabajadors == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadors
                .FirstOrDefaultAsync(m => m.IdTrabajador == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Trabajadors == null)
            {
                return Problem("Entity set 'CoreContext.Trabajadors'  is null.");
            }
            var trabajador = await _context.Trabajadors.FindAsync(id);
            if (trabajador != null)
            {
                _context.Trabajadors.Remove(trabajador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadorExists(byte id)
        {
          return (_context.Trabajadors?.Any(e => e.IdTrabajador == id)).GetValueOrDefault();
        }
    }
}
