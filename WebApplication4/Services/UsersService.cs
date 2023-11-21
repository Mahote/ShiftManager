using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Model;

namespace WebApplication4.Services
{
    public class UsersService
    {
        private readonly AppDbContext _context;

        public UsersService(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            var users = _context.Users.Include(u => u.Shifts).ToList();

            return users;
        }

        public User GetById(int userId)
        {
            var user = _context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            return user;
        }

        public bool DeleteByUserId(int userId)
        {
            var userToDelete = _context.Users.Include(u => u.Shifts).FirstOrDefault(u => u.Id == userId);

            if (userToDelete != null)
            {
                _context.Shifts.RemoveRange(userToDelete.Shifts);
                _context.Users.Remove(userToDelete);

                _context.SaveChanges();
                return true; // Indicates successful deletion
            }

            return false; // Indicates user not found
        }

        public bool UpdateUser(User updatedUser)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);

            if (existingUser != null)
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Surname = updatedUser.Surname;

                _context.SaveChanges();
                return true; // Indicates successful update
            }

            return false; // Indicates user not found
        }

        public User CreateUser(User user)
        {
            if (user == null)
            {
                return null; // Indicates if the user is null
            }

            try
            {
                _context.Users.Add(user);
                _context.SaveChanges(); 
                return user; 
            }
            catch (Exception ex)
            {
               
                return null; // Indicates internal server error
            }
        }

        public bool AddShiftToUser(int userId, Shift newShift)
        {
            var userToAddShift = _context.Users.Include(u => u.Shifts).FirstOrDefault(u => u.Id == userId);

            if (userToAddShift != null)
            {
                userToAddShift.Shifts.Add(new Shift
                {
                    Start = newShift.Start,
                    End = newShift.End,
                });

                _context.SaveChanges();
                return true; // Indicates successful addition of shift
            }

            return false; // Indicates user not found
        }

        public List<Shift> GetUsersShifts(int userId)
        {
            var user = _context.Users.Include(u => u.Shifts).FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return user.Shifts;
            }

            return null;
        }
        


    }
}