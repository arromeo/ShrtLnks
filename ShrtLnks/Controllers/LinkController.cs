using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShrtLnks.Data;
using ShrtLnks.Models;
using ShrtLnks.ViewModels;

namespace ShrtLnks.Controllers
{
    public class LinkController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LinkController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: link
        public async Task<IActionResult> Dashboard()
        {
            string currentUserId = _userManager.GetUserId(User);

            var links = await _context.Link.Where(l => l.OwnerId == currentUserId).ToListAsync();

            DashboardViewModel dashboardViewModel = new DashboardViewModel()
            {
                Links = links
            };

            return View(dashboardViewModel);
        }

        // GET: link/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: link/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("longUrl")] string longUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            string shortUrl = "";
            while (shortUrl == "")
            {
                string testString = RandomStringGenerator();
                bool doesExist = await _context.Link.AnyAsync(l => l.ShortUrl == testString);

                if (!doesExist)
                    shortUrl = testString;
            }

            Link link = new Link()
            {
                OwnerId = (currentUser == null) ? "Anonymous" : currentUser.Id,
                LongUrl = longUrl,
                ShortUrl = shortUrl,
                CreateAt = DateTime.Now
            };

            _context.Add(link);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = link.LinkId } );
        }

        // GET: link/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Link.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            LinkEditViewModel linkEditViewModel = new LinkEditViewModel()
            {
                Link = link
            };

            return View(linkEditViewModel);
        }

        // POST: link/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LinkId,OwnerId,LongUrl,ShortUrl,CreateAt")] Link link)
        {
            if (!ModelState.IsValid)
            {
                return View(link);
            }

            if (link == null)
            {
                return NotFound();
            }

            try
            {
                _context.Update(link);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkExists(link.LinkId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Dashboard));
        }

        // GET: link/delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Link.FirstOrDefaultAsync(l => l.LinkId == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // POST: link/delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var link = await _context.Link.FindAsync(id);
            _context.Link.Remove(link);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
        }

        //GET: links/details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var link = await _context.Link.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (link.OwnerId != currentUser.Id)
            {
                return RedirectToAction(nameof(Dashboard));
            }

            return View(link);
        }

        private bool LinkExists(int id)
        {
            return _context.Link.Any(e => e.LinkId == id);
        }

        private static string RandomStringGenerator()
        {
            Random random = new Random();

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 7)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}