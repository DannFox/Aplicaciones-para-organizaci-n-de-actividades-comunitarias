using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestorActividades.Data;
using GestorActividades.Models;
using System.Collections;

namespace GestorActividades.Controllers
{
    public class ActividadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActividadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actividads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Actividades.Include(a => a.Organizador); 
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Actividads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividades
                .Include(a => a.Organizador)
                .Include(a => a.Participantes)
                .FirstOrDefaultAsync(m => m.ActividadID == id);
            if (actividad == null)
            {
                return NotFound();
            }

            return View(actividad);
        }

        // GET: Actividads/Create
        public IActionResult Create()
        {
            ViewBag.OrganizadorID = new SelectList(_context.Organizadores, "OrganizadorID", "Nombre");
            return View();
        }

        // POST: Actividads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActividadID,Titulo,Descripcion,Fecha,Ubicacion,OrganizadorID")] Actividad actividad)
        {
            await _context.Actividades.AddAsync(actividad);
            await _context.SaveChangesAsync();
            ViewBag.OrganizadorID = new SelectList(_context.Organizadores, "OrganizadorID", "Nombre", actividad.OrganizadorID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Actividads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Actividad actividad = await _context.Actividades.FirstAsync(e => e.ActividadID == id);
            if (actividad == null)
            {
                return NotFound();
            }
            ViewBag.OrganizadorID = new SelectList(_context.Organizadores, "OrganizadorID", "Nombre", actividad.OrganizadorID);
            return View(actividad);
        }

        // POST: Actividads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActividadID,Titulo,Descripcion,Fecha,Ubicacion,OrganizadorID")] Actividad actividad)
        {
            _context.Actividades.Update(actividad);
            await _context.SaveChangesAsync();
            ViewBag.OrganizadorID = new SelectList(_context.Organizadores, "OrganizadorID", "Nombre", actividad.OrganizadorID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Organizadors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividades
                .FirstOrDefaultAsync(m => m.ActividadID == id);
            if (actividad == null)
            {
                return NotFound();
            }

            return View(actividad);
        }

        // POST: Organizadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actividad = await _context.Actividades.FindAsync(id);
            if (actividad != null)
            {
                _context.Actividades.Remove(actividad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddParticipantes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actividad = _context.Actividades
                .Include(a => a.Participantes)
                .FirstOrDefault(m => m.ActividadID == id);

            if (actividad == null)
            {
                return NotFound();
            }

            ViewBag.Participantes = _context.Participantes.ToList();

            return View(actividad);
        }

        // POST: Actividads/AddParticipantes/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddParticipantes(int id, int[] selectedParticipantes)
        {
            var actividad = _context.Actividades.Include(a => a.Participantes)
                                                .FirstOrDefault(a => a.ActividadID == id);
            if (actividad == null)
            {
                return NotFound();
            }

            actividad.Participantes.Clear();
            foreach (var participanteId in selectedParticipantes)
            {
                var participante = _context.Participantes.Find(participanteId);
                if (participante != null)
                {
                    actividad.Participantes.Add(participante);
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
