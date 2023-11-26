using System.Collections.Generic;
using WebApplication4.Data;
using WebApplication4.Model;
using WebApplication4.Services;

public class SchedulerService
{
    private readonly AppDbContext _context;

    private readonly UsersService _usersService;
    public SchedulerService(AppDbContext context, UsersService usersService)
    {
        _context = context;
        _usersService = usersService;
    }
    public Scheduler GetScheduler()
    {
        return _context.Scheduler.FirstOrDefault();
    }
}
