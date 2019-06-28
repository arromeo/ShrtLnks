using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShrtLnks.Data;
using ShrtLnks.Models;

namespace ShrtLnks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("longUrl")] string longUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string shortUrl = await RandomStringGenerator();

            Link link = new Link()
            {
                OwnerId = "Anonymous",
                LongUrl = longUrl,
                ShortUrl = shortUrl,
                CreateAt = DateTime.Now
            };

            _context.Add(link);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = link.LinkId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var link = await _context.Link.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }

            if (link.OwnerId != "Anonymous")
            {
                return RedirectToAction(nameof(Index));
            }

            return View(link);
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string> RandomStringGenerator()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string shortUrl = "";
            while (shortUrl == "")
            {
                Random random = new Random();
                string testString = new string(Enumerable.Repeat(chars, 7).Select(s => s[random.Next(s.Length)]).ToArray());
                bool doesExist = await _context.Link.AnyAsync(l => l.ShortUrl == testString);

                if (!doesExist)
                    shortUrl = testString;
            }

            return shortUrl;
        }
    }
}
