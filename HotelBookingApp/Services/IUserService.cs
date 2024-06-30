using HotelBookingApp.DTO;
using HotelBookingApp.Models;

namespace HotelBookingApp.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<UserDTO> CreateUserAsync(UserDTO userDto);
        Task<UserDTO> UpdateUserAsync(string id, UserDTO userDto);
        Task<bool> DeleteUserAsync(string id);
    }
}
