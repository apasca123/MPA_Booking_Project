using BookingAppModel.Models;

public class ReviewIndexData
{
    public IEnumerable<Booking>? Bookings { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
    public IEnumerable<Venue>? Venues { get; set; }
}

