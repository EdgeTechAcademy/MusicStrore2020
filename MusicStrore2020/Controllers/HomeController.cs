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
            var grpBy = _context.Songs.GroupBy(m => m.Album).Select(s => s.FirstOrDefault());
            var groups = _context.Songs.GroupBy(p => p.Album);
            foreach (var group in groups)
            {
                //group.Key is the CategoryId value
                foreach (var song in group)
                {
                    // you can access individual product properties
                }
            }
            //            var select = grpBy.Select(grp => grp.First());
            //var list = select.ToList();
            return View(_context.Songs.Where(m => m.IsFeatured).ToList());
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
