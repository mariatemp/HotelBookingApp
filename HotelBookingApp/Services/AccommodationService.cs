using HotelBookingApp.Data;
using HotelBookingApp.Models;
using HotelBookingApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingApp.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly BookingContext _context;

        public AccommodationService(IAccommodationRepository accommodationRepository, BookingContext context)
        {
            _accommodationRepository = accommodationRepository;
            _context = context;
        }

        public async Task<IEnumerable<Accommodation>> GetAllAsync()
        {
            return await _accommodationRepository.GetAllAsync();
        }

        public async Task<Accommodation?> GetAccommodationByIdAsync(int id)
        {
            return await _accommodationRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Accommodation accommodation)
        {
            await _accommodationRepository.AddAsync(accommodation);
        }

        public async Task UpdateAsync(Accommodation accommodation)
        {
            await _accommodationRepository.UpdateAsync(accommodation);
        }

        public async Task DeleteAccommodationAsync(int id)
        {
            var accommodation = await _accommodationRepository.GetByIdAsync(id);
            if (accommodation != null)
            {
                await _accommodationRepository.RemoveAsync(accommodation);
            }
        }
    }
}
