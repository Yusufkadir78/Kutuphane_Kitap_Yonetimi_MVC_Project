using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Book newBook)
    {
        if (!ModelState.IsValid)
            return View(newBook);

        _context.Books.Add(newBook);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Kitap eklendi";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Book updatedBook)
    {
        if (!ModelState.IsValid)
            return View(updatedBook);

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