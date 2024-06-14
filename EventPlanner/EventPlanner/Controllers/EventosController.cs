using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Controllers
{
    public class EventosController : Controller
    {
        private readonly DataContext _context;

        public EventosController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {            
            return View(await _context.Eventos.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Evento evento)
        {
            if (ModelState.IsValid) 
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Evento creado exitosamente!!!";
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound(); ;
            }

            var evento = await _context.Eventos.FindAsync(id);

            if (evento == null)
            {
                return NotFound(); 
            }

            return View(evento);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Evento evento)
        {
            if (ModelState.IsValid)
            {
                var eventoEncontrado = await _context.Eventos.FindAsync(evento.Id);
                if (eventoEncontrado == null)
                {
                    return NotFound();
                }

                eventoEncontrado.Titulo = evento.Titulo;
                eventoEncontrado.Fecha = evento.Fecha;
                eventoEncontrado.Descripcion = evento.Descripcion;
                eventoEncontrado.Ubicacion = evento.Ubicacion;

                _context.Update(eventoEncontrado);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Evento editado exitosamente!!!";
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        public async Task<IActionResult> Delete (int? id)
        {
            if (id == null || _context.Eventos == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id);

            if(evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Evento eliminado exitosamente!!!";
            return RedirectToAction("Index");
        }
    }
}
