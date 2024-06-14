using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EventPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        public HomeController(ILogger<HomeController> logger , DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calendar()
        {
            List<Evento> eventos = _context.Eventos.ToList();
            List<object> items = new List<object>();

            foreach(Evento evento in eventos)
            {
                var item = new
                {
                    id = evento.Id,
                    title = evento.Titulo,
                    start = evento.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = evento.Fecha.AddHours(1),
                    ubicacion = evento.Ubicacion,
                };
                items.Add(item);
            }

            ViewBag.Eventos = JsonConvert.SerializeObject(items);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
