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

            if (!_context.Favorites.Any(f => f.BookId == bookId && f.UserId == userId))
            {
                var favoris = new Favoris
                {
                    BookId = bookId,
                    UserId = userId,
                    AddedDate = DateTime.Now
                };

                _context.Favorites.Add(favoris);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavoris(int bookId)
        {
            var userId = _userManager.GetUserId(User);

            var favoris = _context.Favorites.FirstOrDefault(f => f.BookId == bookId && f.UserId == userId);
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
            // Récupérer l'ID de l'utilisateur connecté
            var userId = _userManager.GetUserId(User);

            // Récupérer la liste des livres favoris de cet utilisateur
            var favoris = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Book) // Inclure les informations sur le livre
                .ThenInclude(b => b.Author) // Inclure les informations sur l'auteur (facultatif)
                .ToListAsync();

            return View(favoris);
        }
    }
}
