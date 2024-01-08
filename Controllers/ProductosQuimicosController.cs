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
    [Authorize(Roles = "GerenteTecnico")]
    public class ProductosQuimicosController : Controller
    {
        private readonly CoreContext _context;

        public ProductosQuimicosController(CoreContext context)
        {
            _context = context;
        }

        // GET: ProductosQuimicos
        public async Task<IActionResult> Index()
        {
            var coreContext = _context.ProductoQuimicos.Include(p => p.IdExportacionNavigation).Include(p => p.IdTrabajadorNavigation);
            return View(await coreContext.ToListAsync());
        }

        // GET: ProductosQuimicos/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.ProductoQuimicos == null)
            {
                return NotFound();
            }

            var productoQuimico = await _context.ProductoQuimicos
                .Include(p => p.IdExportacionNavigation)
                .Include(p => p.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdProductoQuimico == id);
            if (productoQuimico == null)
            {
                return NotFound();
            }

            return View(productoQuimico);
        }

        // GET: ProductosQuimicos/Create
        public IActionResult Create()
        {
            ViewData["IdExportacion"] = new SelectList(_context.Exportacions, "IdExportacion", "IdExportacion");
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "IdTrabajador");
            return View();
        }

        // POST: ProductosQuimicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProductoQuimico,IdTrabajador,IdExportacion,Nombre,Costo")] ProductoQuimico productoQuimico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoQuimico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdExportacion"] = new SelectList(_context.Exportacions, "IdExportacion", "IdExportacion", productoQuimico.IdExportacion);
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "IdTrabajador", productoQuimico.IdTrabajador);
            return View(productoQuimico);
        }

        // GET: ProductosQuimicos/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.ProductoQuimicos == null)
            {
                return NotFound();
            }

            var productoQuimico = await _context.ProductoQuimicos.FindAsync(id);
            if (productoQuimico == null)
            {
                return NotFound();
            }
            ViewData["IdExportacion"] = new SelectList(_context.Exportacions, "IdExportacion", "IdExportacion", productoQuimico.IdExportacion);
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "IdTrabajador", productoQuimico.IdTrabajador);
            return View(productoQuimico);
        }

        // POST: ProductosQuimicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdProductoQuimico,IdTrabajador,IdExportacion,Nombre,Costo")] ProductoQuimico productoQuimico)
        {
            if (id != productoQuimico.IdProductoQuimico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoQuimico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoQuimicoExists(productoQuimico.IdProductoQuimico))
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
            ViewData["IdExportacion"] = new SelectList(_context.Exportacions, "IdExportacion", "IdExportacion", productoQuimico.IdExportacion);
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "IdTrabajador", productoQuimico.IdTrabajador);
            return View(productoQuimico);
        }

        // GET: ProductosQuimicos/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.ProductoQuimicos == null)
            {
                return NotFound();
            }

            var productoQuimico = await _context.ProductoQuimicos
                .Include(p => p.IdExportacionNavigation)
                .Include(p => p.IdTrabajadorNavigation)
                .FirstOrDefaultAsync(m => m.IdProductoQuimico == id);
            if (productoQuimico == null)
            {
                return NotFound();
            }

            return View(productoQuimico);
        }

        // POST: ProductosQuimicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.ProductoQuimicos == null)
            {
                return Problem("Entity set 'CoreContext.ProductoQuimicos'  is null.");
            }
            var productoQuimico = await _context.ProductoQuimicos.FindAsync(id);
            if (productoQuimico != null)
            {
                _context.ProductoQuimicos.Remove(productoQuimico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoQuimicoExists(byte id)
        {
          return (_context.ProductoQuimicos?.Any(e => e.IdProductoQuimico == id)).GetValueOrDefault();
        }
    }
}
