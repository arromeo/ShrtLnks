using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShrtLnks.Data;

namespace ShrtLnks.Controllers
{
    public class RedirectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RedirectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Link(string shortUrl)
        {
            var link = _context.Link.FirstOrDefault(l => l.ShortUrl == shortUrl);

            if (link == null)
            {
                return NotFound();
            }

            return Redirect(link.LongUrl);
        }
    }
}