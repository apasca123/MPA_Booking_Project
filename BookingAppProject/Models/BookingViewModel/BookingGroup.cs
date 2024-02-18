using System;
using System.ComponentModel.DataAnnotations;
namespace BookingAppProject.Models.BookingViewModels
{
    public class BookingGroup
    {
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }
        public int BookingsCount { get; set; }
    }
}