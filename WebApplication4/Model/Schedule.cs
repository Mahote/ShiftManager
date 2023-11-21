namespace WebApplication4.Model
{
    public class Schedule
    {
        public User User { get; set; }
        public List<ShiftWithCapacity> Shifts { get; set; }
    }
}
