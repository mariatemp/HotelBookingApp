using HotelBookingApp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingApp.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetAllBookingsAsync();
        Task<BookingDTO> GetBookingByIdAsync(int id);
        Task CreateBookingAsync(BookingDTO bookingDto);
        Task UpdateBookingAsync(int id, BookingDTO bookingDto);
        Task DeleteBookingAsync(int id);
        Task<bool> BookAccommodationAsync(int accommodationId, string userId, DateTime startDate, DateTime endDate);
        int CalculateTotalPrice(DateTime startDate, DateTime endDate, int pricePerNight);
    }

}
