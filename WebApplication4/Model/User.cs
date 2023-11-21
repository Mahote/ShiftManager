namespace WebApplication4.Model
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; } 
        public List<Shift> Shifts { get; set; }
    }
}
