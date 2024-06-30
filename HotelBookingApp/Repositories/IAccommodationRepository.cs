using HotelBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingApp.Repositories
{
    public interface IAccommodationRepository
    {
        Task<IEnumerable<Accommodation>> GetAllAsync();
        Task<Accommodation?> GetByIdAsync(int id);
        Task AddAsync(Accommodation entity);
        Task UpdateAsync(Accommodation entity);
        Task RemoveAsync(Accommodation entity);
    }
}

