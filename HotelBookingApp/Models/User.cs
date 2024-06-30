using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; } = string.Empty;

    /*    [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; } = string.Empty; */

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = string.Empty;

        public ICollection<Accommodation>? Accommodations { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}


