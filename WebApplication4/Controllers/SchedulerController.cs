using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication4.Model;

namespace VotreNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchedulerController : ControllerBase
    {
        private readonly SchedulerService _schedulerService;

        public SchedulerController(SchedulerService schedulerService)
        {
            _schedulerService = schedulerService;
        }

        [HttpGet("run")]
        public IActionResult RunScheduler()
        {

            try
            {
                Dictionary<User, List<ShiftWithCapacity>> schedule = _schedulerService.RunScheduler();

                // Convertir le dictionnaire en une liste de paires clé-valeur sérialisable
                var serializableSchedule = schedule.Select(pair => new Schedule
                {
                    User = pair.Key,
                    Shifts = pair.Value
                }).ToList();

                return Ok(serializableSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Scheduler execution failed: {ex.Message}");
            }
        }
    }
}