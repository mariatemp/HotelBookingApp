using AutoMapper;
using HotelBookingApp.Data;
using HotelBookingApp.DTO;
using HotelBookingApp.Models;
using HotelBookingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace HotelBookingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccommodationController : Controller
    {
        private readonly IAccommodationService _accommodationService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<AccommodationController> _logger;
        private readonly BookingContext _context;

        public AccommodationController(
            IAccommodationService accommodationService,
            IBookingService bookingService,
            IUserService userService,
            IMapper mapper,
            ILogger<AccommodationController> logger,
            BookingContext context)
        {
            _accommodationService = accommodationService;
            _bookingService = bookingService;
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retrieve all accommodations")]
        public async Task<IActionResult> Index()
        {
            var accommodations = await _accommodationService.GetAllAsync();
            var accommodationDTOs = _mapper.Map<IEnumerable<AccommodationDTO>>(accommodations);
            return Ok(accommodationDTOs);
        }

        [HttpGet("create")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Load the accommodation creation form")]
        public async Task<IActionResult> Create()
        {
            var users = await _userService.GetAllUsersAsync();
            var userSelectList = new SelectList(users, "UserId", "Username");
            return Ok(userSelectList);
        }

        [HttpPost("create")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create a new accommodation")]
        public async Task<IActionResult> Create([FromBody] AccommodationDTO accommodationDTO)
        {
            if (accommodationDTO == null)
            {
                _logger.LogError("Accommodation data is null.");
                return BadRequest("Accommodation data is null.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is invalid: {ModelState}", ModelState);
                return BadRequest(ModelState);
            }
            try
            {
             /*  var userId = accommodationDTO.UserId;
                if (User.Identity.IsAuthenticated)
                {
                    userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                } */

                var accommodation = _mapper.Map<Accommodation>(accommodationDTO);
             //   accommodation.UserId = userId ?? accommodationDTO.UserId;

                await _accommodationService.AddAsync(accommodation);

                return CreatedAtAction(nameof(Details),
                    new { id = accommodation.AccommodationId },
                    accommodationDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating accommodation");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("edit/{id}")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Load the accommodation editing form")]
        public async Task<IActionResult> Edit(int id)
        {
            var accommodation = await _accommodationService.GetAccommodationByIdAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            var accommodationDTO = _mapper.Map<AccommodationDTO>(accommodation);
            var users = await _userService.GetAllUsersAsync();
            var userSelectList = new SelectList(users, "UserId", "Username", accommodationDTO.UserId);
            return Ok(new { accommodationDTO, userSelectList });
        }

        [HttpPost("edit/{id}")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Edit an existing accommodation")]
        public async Task<IActionResult> Edit(int id, [FromBody] AccommodationDTO accommodationDTO)
        {
            if (id != accommodationDTO.AccommodationId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accommodation = await _accommodationService.GetAccommodationByIdAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            try
            {
                _mapper.Map(accommodationDTO, accommodation);
                await _accommodationService.UpdateAsync(accommodation);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating accommodation");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get accommodation details by ID")]
        public async Task<IActionResult> Details(int id)
        {
            var accommodation = await _accommodationService.GetAccommodationByIdAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            var accommodationDTO = _mapper.Map<AccommodationDTO>(accommodation);
            return Ok(accommodationDTO);
        }

        [HttpGet("delete/{id}")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Load the accommodation deletion confirmation form")]
        public async Task<IActionResult> Delete(int id)
        {
            var accommodation = await _accommodationService.GetAccommodationByIdAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            var accommodationDTO = _mapper.Map<AccommodationDTO>(accommodation);
            return Ok(accommodationDTO);
        }

        [HttpPost("delete/{id}")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete an accommodation")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accommodation = await _accommodationService.GetAccommodationByIdAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }

            try
            {
                await _accommodationService.DeleteAccommodationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting accommodation");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("book/{id}")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Load the booking form for an accommodation")]
        public async Task<IActionResult> Book(int id)
        {
            var accommodation = await _accommodationService.GetAccommodationByIdAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            var accommodationDto = _mapper.Map<AccommodationDTO>(accommodation);
            var bookingDto = new BookingDTO
            {
                AccommodationId = accommodation.AccommodationId,
                Accommodation = accommodationDto,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                PricePerNight = accommodation.PricePerNight
            };
            return Ok(bookingDto);
        }

        [HttpPost("book")]
        [IgnoreAntiforgeryToken]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create a booking for an accommodation")]
        public async Task<IActionResult> Book([FromBody] BookingDTO bookingDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var accommodation = await _accommodationService.GetAccommodationByIdAsync(bookingDTO.AccommodationId);
                if (accommodation == null)
                {
                    return NotFound();
                }

             /*   var userId = bookingDTO.UserId;
               if (User.Identity.IsAuthenticated)
                {
                    userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                } */

                bookingDTO.Accommodation = null;
                bookingDTO.PricePerNight = accommodation.PricePerNight;
                bookingDTO.TotalPrice = _bookingService.CalculateTotalPrice(bookingDTO.StartDate, bookingDTO.EndDate, bookingDTO.PricePerNight);
             //   bookingDTO.UserId = userId ?? bookingDTO.UserId;

                await _bookingService.CreateBookingAsync(bookingDTO);

                var createdBooking = _mapper.Map<Booking>(bookingDTO);
                return CreatedAtAction(nameof(Details), new { id = createdBooking.BookingId }, createdBooking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}









