using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MusicStrore2020.Data;
using MusicStrore2020.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System;

namespace MusicStrore2020.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SongsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Songs.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Artist,Album,ReleaseDate,Genre,ImagePath,Price,IsActive,IsFeatured")] Song song,
                        IFormFile file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = _env.WebRootPath + "\\uploads\\albums\\" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                song.ImagePath = "uploads/albums/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Artist,Album,ReleaseDate,Genre,ImagePath,Price,IsActive,IsFeatured")] Song song,
                        IFormFile file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = _env.WebRootPath + "\\uploads\\albums\\" + fileName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                song.ImagePath = "uploads/albums/" + fileName;
            }
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
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
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search2(string Search)
        {
            List<Song> allSongs = _context.Songs.ToList();
            List<Song> shortList = allSongs.FindAll(s => s.Album.ToUpper().Contains(Search.ToUpper())
                                                      || s.Title.ToUpper().Contains(Search.ToUpper())
                                                      || s.Artist.ToUpper().Contains(Search.ToUpper()));
            return View("ListSongs", shortList);
        }

        // GET: Songs/LoadSongs
        public IActionResult LoadSongs()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadSongs(IFormFile file)
        {
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = _env.WebRootPath + "\\uploads\\albums\\" + fileName;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                System.IO.StreamReader sfile = new System.IO.StreamReader(path);
                string line;
                sfile.ReadLine();
                while ((line = sfile.ReadLine()) != null)
                {
                    string[] properties = line.Split(",");
                    Song qSong = new Song
                    {
                        Title = properties[0],
                        Artist = properties[1],
                        Album = properties[2],
                        ImagePath = "uploads/albums/" + properties[2] + ".jpg",
                        ReleaseDate = DateTime.Parse(properties[3]),
                        Genre = properties[4]
                    };
                    decimal price;

                    decimal.TryParse(properties[5], out price);
                    qSong.Price = price;
                    _context.Add(qSong);
                }
                await _context.SaveChangesAsync();

                sfile.Close();
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Songs/Search?searchFor=The Eagles
        //  https://localhost:44366/songs/deleteall
        public IActionResult DeleteAll()
        {
            List<Song> allSongs = _context.Songs.ToList();
            allSongs.ForEach(s => _context.Songs.Remove(s));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // GET: Songs/GetGenre?searchFor=classic
        //  https://localhost:44366/songs/GetGenre?genre=classic
        public IActionResult GetGenre(string genre)
        {
            List<Song> allSongs = _context.Songs.ToList();

            var albums = (from s in allSongs
                          orderby s.Album
                          where s.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase)
                          select s.Album)
                          .Distinct()
                          .ToList();

            ViewBag.Genre = genre;
            return View("ListAlbums", albums);
        }

        // GET: Songs/Search?searchFor=The Eagles
        //  https://localhost:44366/songs/search?searchFor1=boys&field1=artist&searchfor2=rock&field2=genre
        public IActionResult Search(string searchFor1, string field1, string searchFor2, string field2)
        {
            if (searchFor1 == null)
            {
                return NotFound();
            }

            List<Song> songs;
            List<Song> allSongs = _context.Songs.ToList();
            songs = FilterSongs(allSongs, searchFor1, field1);
            songs = FilterSongs(songs, searchFor2, field2);

            //  how would you only return songs with a high ranking
            ViewBag.Artist = searchFor1;
            return View("ListSongs", songs);
        }

        private List<Song> FilterSongs(List<Song> someSongs, string searchFor, string field)
        {
            List<Song> songs;

            if (field == null) return someSongs;

            switch (field.ToLower())
            {
                case "genre":   songs = someSongs.FindAll(s => s.Genre.ToUpper().Contains(searchFor.ToUpper()));        break;
                case "artist":  songs = someSongs.FindAll(s => s.Artist.ToUpper().Contains(searchFor.ToUpper()));       break;
                case "album":   songs = someSongs.FindAll(s => s.Album.ToUpper().Contains(searchFor.ToUpper()));        break;
                case "date":    songs = someSongs.FindAll(s => s.ReleaseDate.CompareTo(DateTime.Parse(searchFor)) >= 0); break;
                default:        songs = someSongs; break;
            }
            return songs;
        }


    }
}
