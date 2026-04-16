using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KutuphaneKitapYonetimi.Models;

namespace KutuphaneKitapYonetimi.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book newBook)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(newBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newBook);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Book updatedBook)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Update(updatedBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(updatedBook);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}