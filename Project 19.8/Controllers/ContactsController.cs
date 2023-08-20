using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using Project_19._8.Models;

namespace Project_19._8.Controllers;

public class ContactsController : Controller
{
    private readonly ContactsContext _context;

    public ContactsController(ContactsContext context)
    {
        _context = context;
    }

    // GET: Contacts
    public async Task<IActionResult> Index()
    {
          return _context.Contact != null ? 
                      View(await _context.Contact.ToListAsync()) :
                      Problem("Entity set 'ContactsContext.Contact'  is null.");
    }

    // GET: Contacts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Contact == null)
        {
            return NotFound();
        }

        var contact = await _context.Contact
            .FirstOrDefaultAsync(m => m.ID == id);
        if (contact == null)
        {
            return NotFound();
        }

        return View(contact);
    }

    // GET: Contacts/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Contacts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,Patronymic,MobileNumber,Address,Description")] Contact contact)
    {
        if (ModelState.IsValid)
        {
            _context.Add(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(contact);
    }

    // GET: Contacts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Contact == null)
        {
            return NotFound();
        }

        var contact = await _context.Contact.FindAsync(id);
        if (contact == null)
        {
            return NotFound();
        }
        return View(contact);
    }

    // POST: Contacts/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,Patronymic,MobileNumber,Address,Description")] Contact contact)
    {
        if (id != contact.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(contact);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(contact.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(contact);
    }

    // GET: Contacts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Contact == null)
        {
            return NotFound();
        }

        var contact = await _context.Contact
            .FirstOrDefaultAsync(m => m.ID == id);
        if (contact == null)
        {
            return NotFound();
        }

        return View(contact);
    }

    // POST: Contacts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Contact == null)
        {
            return Problem("Entity set 'ContactsContext.Contact'  is null.");
        }
        var contact = await _context.Contact.FindAsync(id);
        if (contact != null)
        {
            _context.Contact.Remove(contact);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ContactExists(int id)
    {
      return (_context.Contact?.Any(e => e.ID == id)).GetValueOrDefault();
    }
}
