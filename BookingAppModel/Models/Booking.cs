using System;
using System.ComponentModel.DataAnnotations;

namespace BookingAppModel.Models
{
    public class Booking
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }
        public Customer  Customer{ get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; }
    }
}

