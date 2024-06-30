using HotelBookingApp.Data;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        private new readonly BookingContext _context;

        public BookingRepository(BookingContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public new async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id) ?? throw new InvalidOperationException("Booking not found.");
        }

        public new async Task AddAsync(Booking entity)
        {
            if (entity.Accommodation != null)
            {
                _context.Entry(entity.Accommodation).State = EntityState.Detached;
            }

            await _context.Bookings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Booking entity)
        {
            _context.Bookings.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingByAccommodationIdAsync(int accommodationId)
        {
            return await _context.Bookings
                .Where(b => b.AccommodationId == accommodationId)
                .ToListAsync();
        }

        public async Task<bool> IsAccommodationAvailableAsync(int accommodationId, DateTime startDate, DateTime endDate)
        {
            return !await _context.Bookings
               .AnyAsync(b => b.AccommodationId == accommodationId &&
                              ((b.StartDate <= startDate && b.EndDate >= startDate) ||
                               (b.StartDate <= endDate && b.EndDate >= endDate)));
        }
    }
}

