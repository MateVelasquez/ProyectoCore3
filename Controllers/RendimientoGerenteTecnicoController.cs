using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoCore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCore.Controllers
{
    public class RendimientoGerenteTecnicoController : Controller
    {
        private readonly CoreContext _context;

        public RendimientoGerenteTecnicoController(CoreContext context)
        {
            _context = context;
        }

        // GET: RendimientoGerenteTecnico
        public async Task<IActionResult> Index()
        {
            var rendimientos = await _context.RendimientoGerenteTecnicos
                .Include(r => r.IdExportacionNavigation)
                .Include(r => r.IdProyeccionAnualNavigation)
                .Include(r => r.IdTrabajadorNavigation)
                .ToListAsync();

            return View(rendimientos);
        }

        // POST: RendimientoGerenteTecnico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRendimientoGerenteTecnico,IdProyeccionAnual,IdExportacion,IdTrabajador,Cantidad")] RendimientoGerenteTecnico rendimientoGerenteTecnico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rendimientoGerenteTecnico);
                await _context.SaveChangesAsync();

                // Redireccionar a la acción Index después de agregar un rendimiento
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdExportacion"] = new SelectList(_context.Exportacions, "IdExportacion", "IdExportacion", rendimientoGerenteTecnico.IdExportacion);
            ViewData["IdProyeccionAnual"] = new SelectList(_context.ProyeccionAnuals, "IdProyeccionAnual", "IdProyeccionAnual", rendimientoGerenteTecnico.IdProyeccionAnual);
            ViewData["IdTrabajador"] = new SelectList(_context.Trabajadors, "IdTrabajador", "Cedula", rendimientoGerenteTecnico.IdTrabajador);
            return View(rendimientoGerenteTecnico);
        }
    }
}