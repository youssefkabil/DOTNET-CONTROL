using DOTNET_Control.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DOTNET_Control.Controllers
{
    [Authorize]
    public class FavorisController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavorisController(LibraryDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavoris(int bookId)
        {
            var userId = _userManager.GetUserId(User);

            // Check if the book is already in the user's favorites
            if (!_context.Favorites.Any(f => f.BookId == bookId && f.UserId == userId))
            {
                // Find the ApplicationUser associated with the userId
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    var favoris = new Favoris
                    {
                        BookId = bookId,
                        UserId = userId,
                        User = user, // Link the ApplicationUser entity
                        AddedDate = DateTime.Now
                    };

                    _context.Favorites.Add(favoris);
                    await _context.SaveChangesAsync();
                }
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavoris(int bookId)
        {
            var userId = _userManager.GetUserId(User);

            var favoris = _context.Favorites
                .Include(f => f.User) // Include the related ApplicationUser
                .FirstOrDefault(f => f.BookId == bookId && f.UserId == userId);

            if (favoris != null)
            {
                _context.Favorites.Remove(favoris);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Get the ID of the logged-in user
            var userId = _userManager.GetUserId(User);

            // Retrieve the user's list of favorite books
            var favoris = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Book) // Include book details
                .ThenInclude(b => b.Author) // Include author details (optional)
                .Include(f => f.User) // Include user details (optional for debugging)
                .ToListAsync();

            return View(favoris);
        }
    }
}
