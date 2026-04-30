using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KitapYonetim.Models;

public class BooksController : Controller
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _context.Books
    .Include(b => b.Category)
    .ToListAsync();
        return View(books);
    }

    public IActionResult Add()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Book newBook)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(newBook);
        }

        _context.Books.Add(newBook);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Kitap başarıyla eklendi.";
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Update(int id)
    {
        ViewBag.Categories = _context.Categories.ToList();
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Book updatedBook)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View(updatedBook);
        } 

        _context.Books.Update(updatedBook);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Kitap güncellendi";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Kitap silindi";
        }
        return RedirectToAction("Index");
    }
}