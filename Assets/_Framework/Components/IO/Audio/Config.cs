namespace Game.Components.IO.Audio
{
    public class Config
    {
        public const float MinMasterVolume = -80;
        public const float MaxMasterVolume = 0;

        public const float MinMusicVolume = -80;
        public const float MaxMusicVolume = 0;

        public const float MinSfxVolume = -80;
        public const float MaxSfxVolume = 0;

        public const float NormalizedMaxMusicLvl = 0.99f;
        public const float NormalizedMaxSfxLvl = 0.99f;

        public const float NormalizedMinMusicLvl = 0.0001f;
        public const float NormalizedMinSfxLvl = 0.0001f;
    }
}