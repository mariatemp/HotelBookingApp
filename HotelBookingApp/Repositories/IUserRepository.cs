using HotelBookingApp.Models;

namespace HotelBookingApp.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByIdAsync(string id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetUserByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User entity);
    //    Task<bool> ValidateUserCredentialsAsync(string email, string password);
    }
}

