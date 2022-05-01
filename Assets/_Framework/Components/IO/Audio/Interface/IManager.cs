using UnityEngine.Audio;

namespace Game.Components.IO.Audio
{
    public interface IManager
    {
        void SetAudioMixer(AudioMixer masterMixer);

        void ToggleMusic();
        void ToggleSfx();

        void SetMusicLvl(float lvl);
        void SetSfxLvl(float lvl);

        void Pause(bool pause);

        void Save();
    }
}