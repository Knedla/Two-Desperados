using System;

namespace Game.Components.IO.Audio
{
    [Serializable]
    public class Data : IData
    {
        public void OnDataChange()
        {
            Framework.PlayerPreferenceData.OnDataChange();
        }

        public float MasterLvl { get; set; }
        public float MusicLvl { get; set; }
        public float SfxLvl { get; set; }

        bool masterOn;
        public bool MasterOn
        {
            get { return masterOn; }
            set
            {
                if (masterOn != value)
                {
                    masterOn = value;
                    OnDataChange();
                }
            }
        }

        bool musicOn;
        public bool MusicOn
        {
            get { return musicOn; }
            set
            {
                if (musicOn != value)
                {
                    musicOn = value;
                    OnDataChange();
                }
            }
        }

        bool sfxOn;
        public bool SfxOn
        {
            get { return sfxOn; }
            set
            {
                if (sfxOn != value)
                {
                    sfxOn = value;
                    OnDataChange();
                }
            }
        }

        public void SetDefaultValues()
        {
            MusicLvl = Config.NormalizedMaxMusicLvl;
            SfxLvl = Config.NormalizedMaxSfxLvl;

            musicOn = true;
            sfxOn = true;
        }

        public void LocalSave()
        {
            Framework.PlayerPreferenceData.LocalSave();
        }

        public void UploadToCloud()
        {
            Framework.PlayerPreferenceData.UploadToCloud();
        }
    }
}