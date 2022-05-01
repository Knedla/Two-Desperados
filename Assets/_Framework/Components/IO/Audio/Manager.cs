using UnityEngine;
using UnityEngine.Audio;

namespace Game.Components.IO.Audio
{
    public class Manager : IManager
    {
        const string musicVolume = "musicVol";
        const string sfxVolume = "sfxVol";

        public AudioMixer masterMixer;

        public void SetAudioMixer(AudioMixer masterMixer)
        {
            if (this.masterMixer != null)
                return;

            this.masterMixer = masterMixer;

            SetMusicOn();
            SetSfxOn();
        }

        void SetMusicOn()
        {
            if (Framework.AudioData.MusicOn)
                masterMixer.SetFloat(musicVolume, CalculateLvl(Framework.AudioData.MusicLvl));
            else
                masterMixer.SetFloat(musicVolume, Config.MinMusicVolume);
        }

        void SetSfxOn()
        {
            if (Framework.AudioData.SfxOn)
                masterMixer.SetFloat(sfxVolume, CalculateLvl(Framework.AudioData.SfxLvl));
            else
                masterMixer.SetFloat(sfxVolume, Config.MinSfxVolume);
        }

        public void ToggleMusic()
        {
            Framework.AudioData.MusicOn = !Framework.AudioData.MusicOn;
            SetMusicOn();
        }

        public void ToggleSfx()
        {
            Framework.AudioData.SfxOn = !Framework.AudioData.SfxOn;
            SetSfxOn();
        }

        public void SetMusicLvl(float lvl)
        {
            Framework.AudioData.MusicLvl = lvl;

            if (Framework.AudioData.MusicOn)
                masterMixer.SetFloat(musicVolume, CalculateLvl(lvl));
        }

        public void SetSfxLvl(float lvl)
        {
            Framework.AudioData.SfxLvl = lvl;

            if (Framework.AudioData.SfxOn)
                masterMixer.SetFloat(sfxVolume, CalculateLvl(lvl));
        }

        public void Pause(bool pause)
        {
            AudioListener.pause = pause;
        }

        public void Save()
        {
            Framework.AudioData.LocalSave();
        }

        float CalculateLvl(float linearValue)
        {
            return Mathf.Log10(linearValue) * 20;
        }
    }
}