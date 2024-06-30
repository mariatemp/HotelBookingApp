using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HotelBookingApp.DTO
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int AccommodationId { get; set; }

        public string UserId { get; set; } = string.Empty;

        [Required]
        public string? Firstname { get; set; }

        [Required]
        public string? Lastname { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number of guests must be at least 1.")]
        public int NumberOfGuests { get; set; }

        [Required]
        public int PricePerNight { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Total Price must be a positive value.")]
        public int TotalPrice { get; set; }

        public AccommodationDTO? Accommodation { get; set; }
    }
}

