using Game.System.PersistentData;

namespace Game.Components.IO.Audio
{
    public interface IData : IPersistentData
    {
        float MasterLvl { get; }
        float MusicLvl { get; set; }
        float SfxLvl { get; set; }

        bool MasterOn { get; }
        bool MusicOn { get; set; }
        bool SfxOn { get; set; }
    }
}