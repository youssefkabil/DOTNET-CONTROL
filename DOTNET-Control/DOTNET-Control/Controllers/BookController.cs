using DOTNET_Control.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_Control.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;

        public BookController(LibraryDbContext context)
        {
            _context = context;
        }

        // Cette méthode récupère tous les livres
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Récupérer tous les livres disponibles
            var books = await _context.Books
                .Include(b => b.Author)     // Inclure les informations sur l'auteur
                .Include(b => b.Category)   // Inclure les informations sur la catégorie
                .ToListAsync();

            // Passer la liste des livres à la vue
            return View(books);
        }
    }
}
