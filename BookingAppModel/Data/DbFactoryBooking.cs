using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace BookingAppModel.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<BookingAppContext>
    {
        public BookingAppContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<BookingAppContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookingDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new BookingAppContext(optionsBuilder.Options);
        }
    }
}
