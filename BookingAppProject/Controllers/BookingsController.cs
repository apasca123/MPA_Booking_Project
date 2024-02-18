using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingAppProject.Data;
using BookingAppProject.Models;

namespace BookingAppProject.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BookingAppContext _context;

        public BookingsController(BookingAppContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["DateSortParam"] = sortOrder == "ReservationDate" ? "date_desc" : "Date";
            ViewData["CurrentSort"] = sortOrder;
            
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var bookings = from b in _context.Bookings select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.Venue.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Date":
                    bookings = bookings.OrderBy(b => b.Date);
                    break;
                case "date_desc":
                    bookings = bookings.OrderByDescending(b => b.Date);
                    break;
                default:
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Booking>.CreateAsync(bookings.AsNoTracking().Include(s => s.Customer).Include(s => s.Venue), pageNumber ?? 1, pageSize));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .AsNoTracking()
                .Include(s => s.Customer)
                .Include(s => s.Venue)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "Name");
            ViewData["VenueId"] = new SelectList(_context.Venues, "ID", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,CustomerId,VenueId")] Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists ");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "Name");
            ViewData["VenueId"] = new SelectList(_context.Venues, "ID", "Name");
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Bookings.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "ID", "Name");
            ViewData["VenueId"] = new SelectList(_context.Venues, "ID", "Name");
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookingToUpdate = await _context.Bookings.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Booking>(bookingToUpdate, "", s => s.ID, s => s.Date, s => s.CustomerId, s => s.VenueId))
            {
                try
                {
                    await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                }
            }
            return View(bookingToUpdate);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangeErrors = false)
        {

            if (id == null)
            {
                return NotFound();
            }
            var book = await _context.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }
            if (saveChangeErrors.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'BookingContext.Bookings'  is null.");
            }
            var book = await _context.Bookings.FindAsync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Bookings.Remove(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool BookExists(int id)
        {
          return (_context.Bookings?.Any(e => e.ID == id)).GetValueOrDefault();
        }

    }
}
