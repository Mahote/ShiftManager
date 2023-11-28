using Microsoft.AspNetCore.Mvc;
using WebApplication4.Model;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            try
            {
                _userService.CreateUser(user);
                return CreatedAtAction(nameof(GetUserById), new { userId = user.Id }, user);
            }
            catch (Exception ex)
            {
                // Log l'erreur ou retourne un message d'erreur approprié
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, User user)
        {
            _userService.UpdateUser(userId, user);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpPost("{userId}/assignshift/{shiftId}")]
        public IActionResult AssignShiftToUser(int userId, int shiftId)
        {
            _userService.AssignShiftToUser(userId, shiftId);
            
            return Ok($"L'utilisateur avec l'ID {userId} a été associé au quart de travail avec l'ID {shiftId}.");
        }
    }
}
