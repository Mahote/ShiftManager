using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication4.Model;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly ShiftsService _shiftService;

        public ShiftsController(ShiftsService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Shift>> GetShifts()
        {
            var shifts = _shiftService.GetShifts();
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public ActionResult<Shift> GetShiftById(int id)
        {
            var shift = _shiftService.GetShiftById(id);

            if (shift == null)
            {
                return NotFound();
            }

            return Ok(shift);
        }

        [HttpPost]
        public ActionResult<Shift> CreateShift([FromBody] Shift shift)
        {
            if (shift == null)
            {
                return BadRequest("Invalid shift data");
            }
            var createdShift = _shiftService.CreateShift(shift);
            if (createdShift)
            {
                return StatusCode(201, createdShift);
            }
            else
            {
                return StatusCode(500, "Failed to create shift");
            }
            
        }

        [HttpPut()]
        public IActionResult UpdateShift([FromBody] Shift updatedShift)
        {
            if (updatedShift == null || updatedShift.Id == 0)
            {
                return BadRequest("Invalid user data or missing user ID");
            }

            var shiftToUpdate = _shiftService.GetShiftById(updatedShift.Id);
            if (shiftToUpdate == null)
            {
                return NotFound();
            }

            var updated = _shiftService.UpdateShift(shiftToUpdate);
            if(updated == null)
            {
                return BadRequest("Failed to update shift");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShift(int id)
        {
            var deleted = _shiftService.DeleteShift(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
