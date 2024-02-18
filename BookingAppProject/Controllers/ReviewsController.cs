using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingAppProject.Data;
using BookingAppProject.Models;
using BookingAppProject.Models.BookingViewModels;
using System.Security.Policy;

namespace BookingAppProject.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly BookingAppContext _context;

        public ReviewsController(BookingAppContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index( int? venueId)
        {
            var viewModel = new ReviewIndexData();
            viewModel.Reviews = _context.Reviews.AsNoTracking().Include(s => s.Venue);

            return View(viewModel);
        }



        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var publisher = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "ID", "Name");
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, Comment, Rating, VenueId")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = await _context.Reviews.AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "ID", "Name", review.VenueId);
            return View(review);
        }

        // POST: Publishers/Edit/5
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
            var reviewToUpdate = await _context.Reviews
            .Include(i => i.Venue)
            .FirstOrDefaultAsync(m => m.ID == id);
            
            if (await TryUpdateModelAsync<Review>(reviewToUpdate, "", s => s.Comment, s => s.Rating, s => s.VenueId))
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
            return View(reviewToUpdate);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reviews == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reviews == null)
            {
                return Problem("Entity set 'BookingContext.Publishers'  is null.");
            }
            var publisher = await _context.Reviews.FindAsync(id);
            if (publisher != null)
            {
                _context.Reviews.Remove(publisher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
          return (_context.Reviews?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
