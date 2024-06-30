namespace HotelBookingApp.Repositories
{
    public interface IUnitOfWork :IDisposable
    {
        IAccommodationRepository Accommodations { get; }
        IBookingRepository Bookings { get; }
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }
}
