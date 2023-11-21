using Microsoft.AspNetCore.Mvc;
using WebApplication4.Model;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _userService;

        public UsersController(UsersService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var deleted = _userService.DeleteByUserId(userId);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User updatedUser)
        {
            if (updatedUser == null || updatedUser.Id == 0)
            {
                return BadRequest("Invalid user data or missing user ID");
            }

            var userToUpdate = _userService.GetById(updatedUser.Id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.Name = updatedUser.Name;
            userToUpdate.Surname = updatedUser.Surname;

            var updated = _userService.UpdateUser(userToUpdate);
            if (!updated)
            {
                return BadRequest("Failed to update user");
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid user data");
            }

            var userCreated = _userService.CreateUser(newUser);

            if (userCreated != null)
            {
                return StatusCode(201, userCreated);
            }
            else
            {
                return StatusCode(500, "Failed to create user");
            }
        }

        [HttpPost("{userId}/shift")]
        public IActionResult AddShiftToUser(int userId, [FromBody] Shift shift)
        {
            var shiftadded = _userService.AddShiftToUser(userId, shift);
            if (!shiftadded)
            {
                return NotFound();
            }

            return Ok(shiftadded);
        }

        [HttpGet("{userId}/shift")]
            public IActionResult GetUsersShifts(int userId)
        {
            var shifts = _userService.GetUsersShifts(userId);
            if (shifts == null)
            {
                return NotFound();
            }

            return Ok(shifts);
        }
    }
}
