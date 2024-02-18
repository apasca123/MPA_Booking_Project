using System;
using Microsoft.EntityFrameworkCore;
using BookingAppModel.Models;

namespace BookingAppModel.Data
{
    public class BookingAppContext : DbContext
    {
        public BookingAppContext(DbContextOptions<BookingAppContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Booking>().ToTable("Bookings")
                .Property(o => o.Date);
            modelBuilder.Entity<Review>().ToTable("Reviews");
            modelBuilder.Entity<Venue>().ToTable("Venues");
        }

    }
}
