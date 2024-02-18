using System;
using System.ComponentModel.DataAnnotations;

namespace BookingAppModel.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } 

        public int VenueId { get; set; }
        public Venue Venue { get; set; }
    }
}

