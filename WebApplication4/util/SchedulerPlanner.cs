using WebApplication4.Model;

public class SchedulerPlanner
{
    public Dictionary<User, List<Shift>> AssignShifts(List<User> users, List<Shift> shifts)
    {
        Dictionary<User, List<Shift>> schedule = new Dictionary<User, List<Shift>>();
        
        foreach (var shift in shifts.OrderByDescending(s => s.Capacity))
        {
            Console.WriteLine(shift.Id);
            foreach (var user in users)
            {
                foreach (var userShift in user.Shifts)
                {
                    // Vérifier si le créneau de l'utilisateur correspond au créneau de la liste
                    if (AreShiftsCompatible(userShift, shift))
                    {
                        if (!schedule.ContainsKey(user))
                            schedule[user] = new List<Shift>();

                        schedule[user].Add(shift);
                        shift.Capacity--; // Réduction de la capacité du créneau
                        
                    }
                }
            }
        }

        return schedule;
    }

    private bool AreShiftsCompatible(Shift userShift, Shift shift)
    {

        // Mettez ici la logique pour vérifier si les créneaux sont compatibles
        // Cela pourrait être basé sur la correspondance des horaires, par exemple
        // Retourne true si les créneaux sont compatibles, sinon false
        // Exemple simplifié :
        return userShift.Start == shift.Start && userShift.End == shift.End && shift.Capacity > 0;
    }
}
