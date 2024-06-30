using HotelBookingApp.Models;

namespace HotelBookingApp.Repositories
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        new Task<IEnumerable<Booking>> GetAllAsync();
        new Task<Booking> GetByIdAsync(int id);
        new Task AddAsync(Booking entity);
        Task UpdateAsync(Booking entity);
        Task DeleteAsync(Booking entity);
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId);
        Task<IEnumerable<Booking>> GetBookingByAccommodationIdAsync(int accommodationId);
        Task<bool> IsAccommodationAvailableAsync(int accommodationId, DateTime startDate, DateTime endDate);
    }
}

