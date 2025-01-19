using Microsoft.AspNetCore.Mvc;
using DOTNET_Control.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        // Default route: /Admin will go to the Index action
        [Route("")]
        public IActionResult Index(string category)
        {
            // Fetch all data needed for dropdowns
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Publishers = _context.Publishers.ToList();

            // Fetch books with related data and filter by category if provided
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

        // Route for /Admin/CreateBook
        [Route("CreateBook")]
        public IActionResult CreateBook()
        {
            // Load data for authors, publishers, and categories
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Publishers = _context.Publishers.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("CreateBook")]
        public async Task<IActionResult> CreateBook(Book book, IFormFile ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    book.ImageUrl = $"images/{fileName}";
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Publishers = _context.Publishers.ToList();
                ViewBag.Categories = _context.Categories.ToList();
                return View(book);
            }
        }

        // DELETE: Admin/DeleteBook/5
        [HttpDelete]
        [Route("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            try
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        // POST: Admin/EditBook
        [HttpPost]
        [Route("EditBook")]
        public async Task<IActionResult> EditBook([FromBody] Book book)
        {
            try
            {
                var existingBook = await _context.Books.FindAsync(book.Id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                existingBook.Title = book.Title;
                existingBook.AuthorId = book.AuthorId;
                existingBook.PublisherId = book.PublisherId;
                existingBook.CategoryId = book.CategoryId;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
        [Route("Authors")]
        public async Task<IActionResult> ShowAuthors()
        {
            var authors = await _context.Authors.Include(a => a.Books).ToListAsync();
            return View(authors);
        }

        [HttpPost]
        [Route("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("EditAuthor")]
        public async Task<IActionResult> EditAuthor([FromBody] Author author)
        {
            try
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(author.Id))
                {
                    return NotFound();
                }
                throw;
            }
        }


        [HttpDelete]
        [Route("DeleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }

        [Route("Categories")]
        public async Task<IActionResult> ShowCategories()
        {
            var categories = await _context.Categories.Include(c => c.Books).ToListAsync();
            return View(categories);
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("EditCategory")]
        public async Task<IActionResult> EditCategory([FromBody] Category category)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
        [HttpGet]
        [Route("Publishers")]
        public async Task<IActionResult> ShowPublishers()
        {
            var publishers = await _context.Publishers.Include(p => p.Books).ToListAsync();
            
            return View(publishers);
        }

        [HttpPost]
        [Route("CreatePublisher")]
        public async Task<IActionResult> CreatePublisher([FromBody] Publisher publisher)
        {
            _context.Add(publisher);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPost]
        [Route("EditPublisher")]
        public async Task<IActionResult> EditPublisher([FromBody] Publisher publisher)
        {
            try
            {
                _context.Update(publisher);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(publisher.Id))
                {
                    return NotFound();
                }
                throw;
            }
        }
        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }
        [HttpDelete]
        [Route("DeletePublisher/{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [Route("Users")]
        public async Task<IActionResult> ShowUsers()
        {
            var users = await _context.Users
                .Include(u => u.Favorites)
                    .ThenInclude(f => f.Book)
                        .ThenInclude(b => b.Author)
                .ToListAsync();

            // Debugging: Log user favorites to the console
            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.UserName}");
                foreach (var favorite in user.Favorites)
                {
                    Console.WriteLine($" - Favorite Book: {favorite.Book?.Title}, Author: {favorite.Book?.Author?.Name}");
                }
            }

            return View(users);
        }


        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
