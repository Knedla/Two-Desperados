namespace Game.Components.Experience
{
    public class Manager
    {
        public int CalculateExperiene(IExperienceable experienceable) // treba ovde proslediti i neki objekat igraca koji kaze koji je on lvl da bi mogao modifier na razliku levela da se primeni... mislim za ovo je dovoljno, al moze to drasticno bolje - MMORPG style
        {
            return experienceable.Level;
        }
    }
}
