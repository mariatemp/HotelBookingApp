using HotelBookingApp.Data;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingApp.Repositories
{
    public class AccommodationRepository : BaseRepository<Accommodation>, IAccommodationRepository
    {
        public AccommodationRepository(BookingContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Accommodation>> GetAllAsync()
        {
            return await _context.Accommodations.ToListAsync();
        }

        public new async Task<Accommodation?> GetByIdAsync(int id)
        {
            return await _context.Accommodations.FindAsync(id);
        }

        public new async Task AddAsync(Accommodation entity)
        {
            await _context.Accommodations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Accommodation entity)
        {
            _context.Accommodations.Update(entity);
            await _context.SaveChangesAsync();
        }

        public new async Task RemoveAsync(Accommodation entity)
        {
            _context.Accommodations.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Accommodation>> GetAccommodationsByUserIdAsync(string userId)
        {
            return await _context.Accommodations
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }
    }
}

