namespace Game.Components.Experience
{
    public interface IExperienceable // sadrzi sve podatke na osnovu kojih se obracunava xp za taj ovjekat
    {
        int Level { get; }
    }
}
