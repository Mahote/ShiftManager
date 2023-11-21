using WebApplication4.Model;

public class Scheduler
{
    public Dictionary<User, List<ShiftWithCapacity>> AssignShifts(List<User> users, List<ShiftWithCapacity> shifts)
    {
        Dictionary<User, List<ShiftWithCapacity>> schedule = new Dictionary<User, List<ShiftWithCapacity>>();

        foreach (var shift in shifts.OrderByDescending(s => s.Capacity))
        {
            foreach (var user in users)
            {
                foreach (var userShift in user.Shifts)
                {
                    // Vérifier si le créneau de l'utilisateur correspond au créneau de la liste
                    if (AreShiftsCompatible(userShift, shift))
                    {
                        if (!schedule.ContainsKey(user))
                            schedule[user] = new List<ShiftWithCapacity>();

                        schedule[user].Add(shift);
                        shift.Capacity--; // Réduction de la capacité du créneau
                        
                    }
                }
            }
        }

        return schedule;
    }

    private bool AreShiftsCompatible(Shift userShift, ShiftWithCapacity shift)
    {
        // Mettez ici la logique pour vérifier si les créneaux sont compatibles
        // Cela pourrait être basé sur la correspondance des horaires, par exemple
        // Retourne true si les créneaux sont compatibles, sinon false
        // Exemple simplifié :
        return userShift.Start == shift.Start && userShift.End == shift.End;
    }
}
