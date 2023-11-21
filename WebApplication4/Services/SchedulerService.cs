using System.Collections.Generic;
using WebApplication4.Data;
using WebApplication4.Model;
using WebApplication4.Services;

public class SchedulerService
{
    private readonly Scheduler _scheduler;

    private readonly AppDbContext _context;

    private readonly UsersService _usersService;
    public SchedulerService(Scheduler scheduler,AppDbContext context, UsersService usersService)
    {
        _context = context;
        _scheduler = scheduler;
        _usersService = usersService;
    }

    public Dictionary<User, List<ShiftWithCapacity>> RunScheduler()
    {

        List<User> users = _usersService.GetUsers();

        List<ShiftWithCapacity> shifts = _context.ShiftWithCapacity.ToList();

        // Utilisation de l'algorithme Scheduler pour attribuer les créneaux
        var schedule = _scheduler.AssignShifts(users, shifts);

        // Traitement ou retour des résultats selon votre cas d'utilisation
        foreach (var userSchedule in schedule)
        {
            Console.WriteLine($"User: {userSchedule.Key.Id}");
            foreach (var shift in userSchedule.Value)
            {
                Console.WriteLine($"   Shift ID: {shift.Id}, Start: {shift.Start}, End: {shift.End}, Capacity: {shift.Capacity}");
            }
            Console.WriteLine();
        }
        if ( schedule !=  null)
        {
            return schedule;
        }
        return null;

        
    }
}
