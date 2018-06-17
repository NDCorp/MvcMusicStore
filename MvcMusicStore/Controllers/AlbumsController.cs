using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MvcMusicStoreContext _context;

        public AlbumsController(MvcMusicStoreContext context)
        {
            _context = context;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var mvcMusicStoreContext = _context.Album.Include(a => a.Artist).Include(a => a.Genre).OrderBy(b => b.Title);

            //IIS has a finite number of process threads
            //A very active site will either ignore, drop or backlog requests
            //Users get "Timeout", "Unavailable", "Forbidden" or "Not Found" errors
            //By default, generated actions fork asynchronous tasks
            //Releasing resources while waiting on requests to the database server
            //This allows sites to scale to a very large number of users
            return View(await mvcMusicStoreContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist.OrderBy(a => a.Name), "ArtistId", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreId");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "Name", album.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreId", album.GenreId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album.SingleOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "Name", album.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (id != album.AlbumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.AlbumId))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "Name", album.ArtistId);
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Album
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefaultAsync(m => m.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Album.SingleOrDefaultAsync(m => m.AlbumId == id);
            _context.Album.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Album.Any(e => e.AlbumId == id);
        }

        public ActionResult Search(string query, string searchType)
        {
            if (searchType == "title")
            {
                //LiNQ to get the list of albums
                var albums = _context.Album.Include(a => a.Artist)
                            .Where(b => b.Title.Contains(query));

                ViewData["albumNbr"] = albums.Count();
                return View(albums);
            }
            else if (searchType == "artist")
            {
                //LiNQ to get the list of albums
                var albums = _context.Album.Include(a => a.Artist)
                            .Where(b => b.Artist.Name.Contains(query));

                ViewData["albumNbr"] = albums.Count();
                return View(albums);
            }
            else
            {
                //LiNQ to get the list of albums
                var albums = _context.Album.Include(a => a.Artist)
                            .Where(b => b.Title.Contains(query) || b.Artist.Name.Contains(query));

                ViewData["albumNbr"] = albums.Count();
                return View(albums);
            }

            //LiNQ to get the list of albums
            //var albums = _context.Album.Include(a => a.Artist)
            //            .Where(b => b.Title.Contains(query) || b.Artist.Name.Contains(query));

            //return View(albums);
        }

        public async Task<IActionResult> MyEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //faster way to get the element using the primary key _context.Album.SingleOrDefaultAsync instead of using .Where
            var album = await _context.Album.SingleOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreId", album.GenreId);

            return View(album);
        }

        // POST: Albums/MyEdit/5
        //Receive the object to save in the Database +  Some validations
        [HttpPost, ActionName("MyEdit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyEdit(Album album)
        {
            if (album.Price < 0)
            {
                //Don't commit and show message
                ModelState.TryAddModelError("Price", "price cannot be nagative");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", $"Exception thrown on update: { e.GetBaseException().Message}");

                //Console.WriteLine(e);
                //throw;
            }

            ViewBag.GenreId = new SelectList(_context.Genre, "GenreId", "GenreId");
            return View(album);
        }
    }
}
