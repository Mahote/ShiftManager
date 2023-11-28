using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Model;

namespace WebApplication4.Services
{
    public class UserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
        }

        public void CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(int userId, User updatedUser)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (existingUser != null)
            {
                existingUser.Name = updatedUser.Name;
                existingUser.Surname = updatedUser.Surname;
                // Mettre à jour d'autres propriétés si nécessaire

                _dbContext.SaveChanges();
            }
        }

        public void DeleteUser(int userId)
        {
            var userToRemove = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (userToRemove != null)
            {
                _dbContext.Users.Remove(userToRemove);
                _dbContext.SaveChanges();
            }
        }
        public void AssignShiftToUser(int userId, int shiftId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            var shift = _dbContext.Shifts.Include(s => s.Users).FirstOrDefault(s => s.Id == shiftId);

            if (user != null && shift != null)
            {
                // Vérifier si l'utilisateur n'est pas déjà associé au shift
                if (!shift.Users.Contains(user))
                {
                    shift.Users.Add(user);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
