using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookingAppProject.Models;

namespace BookingAppProject.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookingAppContext(serviceProvider.GetRequiredService<DbContextOptions<BookingAppContext>>()))
            {
                if (context.Venues.Any())
                {
                    return;
                }

                var venues = new Venue[]
                {
                new Venue { Name = "Pensiunea Ponor" },
                new Venue { Name = "Nedei Spa" },
                new Venue { Name = "Hotel Elenor" }
                };
                context.Venues.AddRange(venues);
                context.SaveChanges();


                context.SaveChanges();

                var customers = new Customer[] {
                    new Customer { Name = "Popescu Marcela" },
                    new Customer { Name = "Mihailescu Cornel" }
                 };

                context.Customers.AddRange(customers);
                context.SaveChanges();
/*
                var orders = new Booking[]{
                    new Booking{BookID=books[0].ID,CustomerID=customers[0].CustomerID,OrderDate=DateTime.Parse("2021-02-25") },
                    new Booking{BookID=books[1].ID,CustomerID=customers[0].CustomerID,OrderDate=DateTime.Parse("2021-09-28") },
                    new Booking{BookID=books[2].ID,CustomerID=customers[1].CustomerID,OrderDate=DateTime.Parse("2021-10-28") },
                    new Booking{BookID=books[3].ID,CustomerID=customers[0].CustomerID,OrderDate=DateTime.Parse("2021-09-28") },
                    new Booking{BookID=books[4].ID,CustomerID=customers[1].CustomerID,OrderDate=DateTime.Parse("2021-09-28") },
                    new Booking{BookID=books[0].ID,CustomerID=customers[1].CustomerID,OrderDate=DateTime.Parse("2021-10-28") }
                };
                foreach (Booking e in orders)
                {
                    context.Orders.Add(e);
                }
                context.SaveChanges();

                var publishers = new Publisher[]
                {
                    new Publisher{PublisherName="Humanitas",Adress="Str. Aviatorilor, nr. 40, Bucuresti"},
                    new Publisher{
                        PublisherName = "Nemira", Adress = "Str. Plopilor, nr. 35, Ploiesti"},
                    new Publisher{PublisherName="Paralela 45",Adress="Str. Cascadelor, nr. 22, Cluj-Napoca"},
                };
                foreach (Publisher p in publishers)
                {
                    context.Publishers.Add(p);
                }
              
                context.SaveChanges();
                var publishedbooks = new Review[]
                {
                    new Review {
                        BookID = books.Single(c => c.Title == "Maytrei" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Humanitas").ID
                    },
                    new Review {
                        BookID = books.Single(c => c.Title == "Ion" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Humanitas").ID
                    },
                    new Review {
                        BookID = books.Single(c => c.Title == "Baltagul" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Nemira").ID
                    },
                    new Review {
                        BookID = books.Single(c => c.Title == "Salut" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Paralela 45").ID
                    },
                    new Review {
                        BookID = books.Single(c => c.Title == "Enigma Otiliei" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Paralela 45").ID
                    },
                    new Review {
                        BookID = books.Single(c => c.Title == "Maytrei" ).ID,
                        PublisherID = publishers.Single(i => i.PublisherName == "Paralela 45").ID
                    }
                };

                foreach (Review pb in publishedbooks)
                {
                    context.PublishedBooks.Add(pb);
                }
                context.SaveChanges();*/
            }
        }
    }
}