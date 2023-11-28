using WebApplication4.Data;
using WebApplication4.Model;
using WebApplication4.Services;

public class SchedulerService
{
    private readonly AppDbContext _context;
    private readonly UserService _userService;
    public SchedulerService(AppDbContext context, UserService userService)
    {
        _context = context;
        _userService = userService;
    }
    public Scheduler GetScheduler()
    {
        return _context.Scheduler.FirstOrDefault();
    }
}
