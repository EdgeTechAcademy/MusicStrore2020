using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MusicStore.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            Random rnd = new Random();
            ViewBag.Age = (int)rnd.Next(100000);
            ViewBag.Name = "Edge Tech Academy";
            ViewBag.Date = DateTime.Now;
            return View();
        }
        public IActionResult Me()
        {
            string[] ar = { "Julius", "Mark", "Gary", "Edge Tech Academy" };
            ViewBag.List = ar;

            Random rnd = new Random();
            ViewBag.Age = (int)rnd.Next(100);
            ViewBag.Name = "Gary James";
            ViewBag.Date = DateTime.Now;
            return View();
        }

        public string Hello()
        {
            return "...Edge Tech has you...";
        }
    }
}