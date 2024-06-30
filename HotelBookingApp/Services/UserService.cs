using AutoMapper;
using HotelBookingApp.Data;
using HotelBookingApp.DTO;
using HotelBookingApp.Exceptions;
using HotelBookingApp.Models;
using HotelBookingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HotelBookingApp.Services
{
    public class UserService : IUserService
    {
        private readonly BookingContext _context;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(BookingContext context, ILogger<UserService> logger, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var user = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(user);
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                throw new EntityNotFoundException($"User with ID {id} not found.");
            }
            return _mapper.Map<UserDTO>(user);
        }


        public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUserAsync(string id, UserDTO userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new EntityNotFoundException($"User with ID {id} not found.");
            }
            _mapper.Map(userDto, user);
            await _context.SaveChangesAsync();
            return userDto;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new EntityNotFoundException($"User with ID {id} not found.");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}


