﻿namespace WebApplication4.Model
{
    public class Shift
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime  End { get; set; }
        public int Capacity { get; set; }
        public List<User> Users { get; set; }
    }


}
