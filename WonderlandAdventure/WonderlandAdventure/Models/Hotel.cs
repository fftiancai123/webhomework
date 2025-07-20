using System;
using System.ComponentModel.DataAnnotations;

namespace WonderlandAdventure.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string PriceRange { get; set; }
        public string ImageUrl { get; set; }
        public string[] Amenities { get; set; }
    }

    public class BookingViewModel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string GuestName { get; set; }

        [Required(ErrorMessage = "Please enter check-in date")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Please enter check-out date")]
        public DateTime CheckOutDate { get; set; }

        [Required(ErrorMessage = "Please select number of guests")]
        [Range(1, 10, ErrorMessage = "Number of guests must be between 1 and 10")]
        public int NumberOfGuests { get; set; }

        [Required(ErrorMessage = "Please select a room type")]
        public int SelectedRoomTypeId { get; set; }

        public RoomType[] RoomTypes { get; set; }

        public bool IncludeBreakfast { get; set; }
    }

    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
    }
}