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

        [HttpGet("scheduler")]
        public IActionResult RunScheduler()
        {
            Scheduler scheduler = _schedulerService.GetScheduler();

            if (scheduler != null)
            {
                return Ok(scheduler); // Retourne un code HTTP 200 avec l'objet Scheduler
            }
            else
            {
                return NotFound(); // Ou un autre code d'erreur selon la logique métier
            }
        }
    }
}