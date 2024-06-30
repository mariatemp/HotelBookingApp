
using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.DTO
{
    public class AccommodationDTO
    {
        public int AccommodationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;


        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price per night must be at least 1.")]
        public int PricePerNight { get; set; }

        [Required]
        public DateTime AvailableFrom { get; set; }

        [Required]
        public DateTime AvailableTo { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}


