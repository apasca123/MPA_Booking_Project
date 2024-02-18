using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppModel.Models
{
    public class Venue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        // Alte proprietăți pentru locație (adresă, descriere etc.)

        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
