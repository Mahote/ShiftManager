using Microsoft.AspNetCore.Mvc;
using WebApplication4.Model;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly ShiftService _shiftService;

        public ShiftController(ShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet]
        public IActionResult GetAllShifts()
        {
            var shifts = _shiftService.GetAllShifts();
            return Ok(shifts);
        }

        [HttpGet("{shiftId}")]
        public IActionResult GetShiftById(int shiftId)
        {
            var shift = _shiftService.GetShiftById(shiftId);
            if (shift == null)
            {
                return NotFound();
            }
            return Ok(shift);
        }

        [HttpPost]
        public IActionResult CreateShift(Shift shift)
        {
            _shiftService.CreateShift(shift);
            return CreatedAtAction(nameof(GetShiftById), new { shiftId = shift.Id }, shift);
        }

        [HttpPut("{shiftId}")]
        public IActionResult UpdateShift(int shiftId, Shift shift)
        {
            _shiftService.UpdateShift(shiftId, shift);
            return NoContent();
        }

        [HttpDelete("{shiftId}")]
        public IActionResult DeleteShift(int shiftId)
        {
            _shiftService.DeleteShift(shiftId);
            return NoContent();
        }
    }
}
