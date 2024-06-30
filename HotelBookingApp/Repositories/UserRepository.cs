using HotelBookingApp.Data;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HotelBookingApp.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly BookingContext _context;

        public UserRepository(BookingContext context, ILogger<UserRepository> logger) : base(context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            _logger.LogInformation("Fetching user by email: {Email}", email);
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

     /*   public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            _logger.LogInformation("Validating credentials for user with email: {Email}", email);
            var user = await GetUserByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found", email);
                return false;
            }

            bool isValid = EncryptionUtil.IsValidPassword(password, user.PasswordHash);
            _logger.LogInformation("Password validation result for user with email {Email}: {IsValid}", email, isValid);
            return isValid;
        } */

        public async Task AddAsync(User user)
        {
            if (user.UserId == null)
            {
                user.UserId = Guid.NewGuid().ToString();
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _logger.LogInformation("Updating user with ID: {UserId}", entity.UserId);
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public new async Task RemoveAsync(User user)
        {
            _logger.LogInformation("Removing user with ID: {UserId}", user.UserId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


    }
}





