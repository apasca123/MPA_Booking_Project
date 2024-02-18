﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingAppProject.Models.BookingViewModels;
using BookingAppProject.Data;
using BookingAppProject.Models;

namespace BookingAppProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookingAppContext _context;

    public HomeController(BookingAppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<ActionResult> Statistics()
    {
        IQueryable<BookingGroup> data =
        from booking in _context.Bookings
        group booking by booking.Date.Date into dateGroup
        select new BookingGroup()
        {
            BookingDate = dateGroup.Key,
            BookingsCount = dateGroup.Count()
        };

        return View(await data.AsNoTracking().ToListAsync());
    }

}

