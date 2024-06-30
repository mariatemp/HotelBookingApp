using AutoMapper;
using HotelBookingApp.Data;
using HotelBookingApp.DTO;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace HotelBookingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IAccommodationService _accommodationService;
        private readonly BookingContext _context;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger, IMapper mapper, IAccommodationService accommodationService, IUserService userService, BookingContext context)
        {
            _bookingService = bookingService;
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
            _accommodationService = accommodationService;
            _context = context;
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Retrieve all bookings")]
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get booking details by ID")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Accommodation)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            var bookingDetails = new BookingDTO
            {
                BookingId = booking.BookingId,
                AccommodationId = booking.AccommodationId,
                UserId = booking.UserId,
                Firstname = booking.Firstname,
                Lastname = booking.Lastname,
                NumberOfGuests = booking.NumberOfGuests,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                TotalPrice = booking.TotalPrice,
                Accommodation = new AccommodationDTO
                {
                    Description = booking.Accommodation.Description,
                    Location = booking.Accommodation.Location,
                    City = booking.Accommodation.City,
                    Address = booking.Accommodation.Address,
                    PricePerNight = booking.Accommodation.PricePerNight
                }
            };

            return Ok(bookingDetails);
        }


        [HttpGet("edit/{id}")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Load the booking editing form")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Accommodation)
                .FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(new BookingDTO
            {
                BookingId = booking.BookingId,
                AccommodationId = booking.AccommodationId,
                UserId = booking.UserId,
                Firstname = booking.Firstname,
                Lastname = booking.Lastname,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                NumberOfGuests = booking.NumberOfGuests,
                PricePerNight = booking.Accommodation.PricePerNight,
                TotalPrice = booking.TotalPrice
            });
        }


        [HttpPost("edit/{id}")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Edit an existing booking")]
        public async Task<IActionResult> Edit(int id, [FromBody] BookingDTO bookingDTO)
        {
            if (id != bookingDTO.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var accommodation = await _accommodationService.GetAccommodationByIdAsync(bookingDTO.AccommodationId);
                    if (accommodation != null)
                    {
                        bookingDTO.PricePerNight = accommodation.PricePerNight;
                        bookingDTO.TotalPrice = _bookingService.CalculateTotalPrice(bookingDTO.StartDate, bookingDTO.EndDate, accommodation.PricePerNight);
                    }

                    await _bookingService.UpdateBookingAsync(id, bookingDTO);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(bookingDTO.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return BadRequest(ModelState);
        }



        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }

        [HttpGet("delete/{id}")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       [SwaggerOperation(Summary = "Load the booking deletion confirmation form")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }


        [HttpPost("delete/{id}"), ActionName("Delete")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete a booking")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _bookingService.DeleteBookingAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the booking.");
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the booking.");
                return BadRequest(ModelState);
            }
        }
    }
}









