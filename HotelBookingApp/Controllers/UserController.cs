using AutoMapper;
using HotelBookingApp.DTO;
using HotelBookingApp.Models;
using HotelBookingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace HotelBookingApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Retrieve all users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(userDTOs);
        }

        [HttpGet("create")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Load the user creation form")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost("create")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create a new user")]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userDTO);
                user.UserId = Guid.NewGuid().ToString();
                await _userRepository.AddAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get a user by ID")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }



        [HttpGet("edit/{id}")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Load the user editing form")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost("edit/{id}")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Edit an existing user")]
        public async Task<IActionResult> Edit(string id, UserDTO userDTO)
        {
            if (id != userDTO.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userDTO);
                await _userRepository.UpdateAsync(user);
                TempData["SuccessMessage"] = "Οι αλλαγές αποθηκεύτηκαν επιτυχώς.";
                return NoContent();
            }

            TempData["ErrorMessage"] = "Υπήρξε κάποιο σφάλμα. Παρακαλώ ελέγξτε τα δεδομένα σας.";
            return BadRequest(ModelState);
        }

        [HttpGet("Details/{id}")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get user details by ID")]
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpGet("delete/{id}")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Load the user deletion confirmation form")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        [IgnoreAntiforgeryToken]
        //[ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete a user")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.RemoveAsync(user);
                return NoContent();
            }

            return NotFound();
        }
    }
}








