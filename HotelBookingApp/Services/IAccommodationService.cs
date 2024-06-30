using HotelBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingApp.Services
{
    public interface IAccommodationService
    {
        Task<IEnumerable<Accommodation>> GetAllAsync();
        Task<Accommodation?> GetAccommodationByIdAsync(int id);
        Task AddAsync(Accommodation accommodation);
        Task UpdateAsync(Accommodation accommodation);
        Task DeleteAccommodationAsync(int id);
    }
}


