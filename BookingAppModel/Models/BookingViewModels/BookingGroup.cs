using System;
using System.ComponentModel.DataAnnotations;
namespace BookingAppModel.Models.BookingViewModel
{
    public class BookingGroup
    {
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }
        public int BookingsCount { get; set; }
    }
}