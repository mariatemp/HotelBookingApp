using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApp.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Required]
        [ForeignKey("Accommodation")]
        public int AccommodationId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int NumberOfGuests { get; set; }

        [Required]
        public int PricePerNight { get; set; }

        [Required]
        public int TotalPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; } =  string.Empty;

        [Required]
        [StringLength(50)]
        public string Lastname { get; set; } = string.Empty; 

        public Accommodation? Accommodation { get; set; }
        public User? User { get; set; }
    }
}

