using System;
using System.Collections.Generic;
using System.Linq;

using WebApplication4.Model;
using WebApplication4.Data; // Assurez-vous d'importer le bon namespace pour votre DbContext

namespace WebApplication4.Services
{
    public class ShiftsService
    {
        private readonly AppDbContext _context;

        public ShiftsService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Shift> GetShifts()
        {
            return _context.Shifts.ToList();
        }

        public Shift GetShiftById(int id)
        {
            var shift = _context.Shifts.FirstOrDefault(s => s.Id == id);
            if (shift == null)
            {
                return null;
            }

            return shift;

            
        }

        public bool CreateShift(Shift shift)
        {
            if (shift == null)
            {
                return false; // Indicates if the user is null
            }

            try
            {
                _context.Shifts.Add(shift);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false; // Indicates internal server error
            }
        }

        public Shift UpdateShift(Shift updatedShift)
        {
            var existingShift = _context.Shifts.FirstOrDefault(s => s.Id == updatedShift.Id);

            if (existingShift != null)
            {
                existingShift.Start = updatedShift.Start;
                existingShift.End = updatedShift.End;

                _context.SaveChanges();
                return updatedShift;
            }
            return null;
            
        }

        public bool DeleteShift(int id)
        {
            var shiftToRemove = _context.Shifts.FirstOrDefault(s => s.Id == id);

            if (shiftToRemove == null)
            {
                return false;
            }

            _context.Shifts.Remove(shiftToRemove);
            _context.SaveChanges();

            return true;
        }
    }
}
