
using HotelBookingApp.Data;

namespace HotelBookingApp.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingContext _context;

        public UnitOfWork(BookingContext context,
            IAccommodationRepository accommodationRepository,
            IBookingRepository bookingRepository,
            IUserRepository userRepository)
        {
            _context = context;
            Accommodations = accommodationRepository;
            Bookings = bookingRepository;
            Users = userRepository;
        }

        public IAccommodationRepository Accommodations { get; private set; }
        public IBookingRepository Bookings { get; private set; }
        public IUserRepository Users { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
