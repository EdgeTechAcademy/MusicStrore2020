using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicStrore2020.Data;
using MusicStrore2020.Models;

namespace MusicStrore2020.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Songs.Where(m => m.IsFeatured).ToList());
        }

        public IActionResult Albums()
        {
            var albums = _context.Songs.ToList().GroupBy(g => g.Album).Select(s => s.FirstOrDefault());
            return View(albums);
        }

        public IActionResult Recommendations(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.SingleOrDefault(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            List<Song> allSongs = _context.Songs.ToList();
            //  how would you only return songs with a high ranking
            List<Song> songs = allSongs.FindAll(s => s.Genre == customer.FavoriteGenre);
            ViewBag.Customer = customer;
            ViewBag.Me = "This is my name - Edge Tech Academy";
            ViewBag.time = new DateTime();
            return View(songs);
        }

        public string Hello()
        {
            return "...Edge Tech has you...";
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
