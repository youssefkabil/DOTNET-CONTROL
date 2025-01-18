using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DOTNET_Control.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DOTNET_Control.Controllers
{
    [Route("Admin")]
    [ServiceFilter(typeof(AdminOnlyAttribute))]
    public class AdminController : Controller
    {
        private readonly LibraryDbContext _context;

        public AdminController(LibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string category)
        {
            // Fetch all categories for the dropdown
            ViewBag.Categories = _context.Categories.ToList();

            // Fetch books with related data (Author and Publisher) and filter by category if provided
            var books = string.IsNullOrEmpty(category)
                ? _context.Books
                    .Include(b => b.Category)
                    .Include(b => b.Author)
                    .Include(b => b.Publisher)
                    .ToList()
                : _context.Books
                    .Include(b => b.Category)
                    .Include(b => b.Author)
                    .Include(b => b.Publisher)
                    .Where(b => b.Category.Name == category)
                    .ToList();

            return View(books);
        }





    }

}
