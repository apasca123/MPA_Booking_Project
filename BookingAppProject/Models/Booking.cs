using System;
using System.ComponentModel.DataAnnotations;

namespace BookingAppProject.Models
{
    public class Booking
    {
        public int? ID { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }
        public Customer?  Customer{ get; set; }

        public int VenueId { get; set; }
        public Venue? Venue { get; set; }
    }
}

