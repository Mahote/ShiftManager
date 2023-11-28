using WebApplication4.Data;
using WebApplication4.Model;

namespace WebApplication4.Services
{
    public class ShiftService
    {
        private readonly AppDbContext _dbContext;

        public ShiftService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Shift> GetAllShifts()
        {
            return _dbContext.Shifts.ToList();
        }

        public Shift GetShiftById(int shiftId)
        {
            return _dbContext.Shifts.FirstOrDefault(s => s.Id == shiftId);
        }

        public void CreateShift(Shift shift)
        {
            _dbContext.Shifts.Add(shift);
            _dbContext.SaveChanges();
        }

        public void UpdateShift(int shiftId, Shift updatedShift)
        {
            var existingShift = _dbContext.Shifts.FirstOrDefault(s => s.Id == shiftId);
            if (existingShift != null)
            {
                existingShift.Start = updatedShift.Start;
                existingShift.End = updatedShift.End;
                existingShift.Capacity = updatedShift.Capacity;
                // Mettre à jour d'autres propriétés si nécessaire

                _dbContext.SaveChanges();
            }
        }

        public void DeleteShift(int shiftId)
        {
            var shiftToDelete = _dbContext.Shifts.FirstOrDefault(s => s.Id == shiftId);
            if (shiftToDelete != null)
            {
                _dbContext.Shifts.Remove(shiftToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
