using AutoMapper;
using HotelBookingApp.DTO;
using HotelBookingApp.Models;
using HotelBookingApp.Repositories;
using HotelBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly BookingContext _context;
        private readonly ILogger<BookingService> _logger;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, BookingContext context, ILogger<BookingService> logger)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
        }

        public async Task<BookingDTO> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Accommodation)
                .FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found");
            }
            return new BookingDTO
            {
                BookingId = booking.BookingId,
                AccommodationId = booking.AccommodationId,
                UserId = booking.UserId,
                Firstname = booking.Firstname,
                Lastname = booking.Lastname,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                NumberOfGuests = booking.NumberOfGuests,
                PricePerNight = booking.Accommodation?.PricePerNight ?? 0,
                TotalPrice = booking.TotalPrice
            };
        }

        public async Task CreateBookingAsync(BookingDTO bookingDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);

            if (booking.Accommodation != null)
            {
                _context.Entry(booking.Accommodation).State = EntityState.Detached;
            }

            await _bookingRepository.AddAsync(booking);
        }

        public async Task UpdateBookingAsync(int id, BookingDTO bookingDTO)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found");
            }

            _logger.LogInformation("Updating booking with ID {BookingId}", id);
            _logger.LogInformation("New Booking Data: {@BookingDto}", bookingDTO);

            booking.Firstname = bookingDTO.Firstname ?? string.Empty;
            booking.Lastname = bookingDTO.Lastname ?? string.Empty;
            booking.StartDate = bookingDTO.StartDate;
            booking.EndDate = bookingDTO.EndDate;
            booking.NumberOfGuests = bookingDTO.NumberOfGuests;
            booking.TotalPrice = bookingDTO.TotalPrice;

            _context.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                throw new KeyNotFoundException("Booking not found");
            }

            await _bookingRepository.RemoveAsync(booking);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(string userId)
        {
            return await _bookingRepository.GetBookingsByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Booking>> GetBookingByAccommodationIdAsync(int accommodationId)
        {
            return await _bookingRepository.GetBookingByAccommodationIdAsync(accommodationId);
        }

        public async Task<bool> BookAccommodationAsync(int accommodationId, string userId, DateTime startDate, DateTime endDate)
        {
            var accommodation = await _context.Accommodations.FindAsync(accommodationId);
            if (accommodation == null)
            {
                return false;
            }

            var booking = new Booking
            {
                AccommodationId = accommodationId,
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = CalculateTotalPrice(startDate, endDate, accommodation.PricePerNight)
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return true;
        }

        public int CalculateTotalPrice(DateTime startDate, DateTime endDate, int pricePerNight)
        {
            _logger.LogInformation("Start Date: {StartDate}", startDate);
            _logger.LogInformation("End Date: {EndDate}", endDate);
            _logger.LogInformation("Price Per Night: {PricePerNight}", pricePerNight);

            int days = (endDate - startDate).Days;
            int totalPrice = days * pricePerNight;

            _logger.LogInformation("Total Price Calculated: {TotalPrice}", totalPrice);

            return totalPrice;
        }
    }
}






