using Game.System.PersistentData;

namespace Game.Components.Experience
{
    public interface IExperience : IPersistentData
    {
        int Value { get; }
        void Add(int value);
    }
}
