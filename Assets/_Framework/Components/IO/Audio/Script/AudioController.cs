using Game.Components.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.IO.Audio
{
    public class AudioController : MonoBehaviour
    {
        public Sprite MusicSpriteOn;
        public Sprite MusicSpriteOff;

        public Sprite SfxSpriteOn;
        public Sprite SfxSpriteOff;

        public Button MusicToggleButton;
        public Button SfxToggleButton;

        public CustomSlider MusicLvlSlider;
        public CustomSlider SfxLvlSlider;

        void Awake()
        {
            InitSliders();

            SetMusicToggleButtonSprite();
            SetSfxToggleButtonSprite(true);
        }

        void OnEnable()
        {
            MusicToggleButton.onClick.AddListener(ToggleMusic);
            SfxToggleButton.onClick.AddListener(ToggleSfx);
        }

        void OnDisable()
        {
            MusicToggleButton.onClick.RemoveAllListeners();
            SfxToggleButton.onClick.RemoveAllListeners();
        }

        void InitSliders()
        {
            MusicLvlSlider.onValueChanged.AddListener(delegate { SetMusicLvl(MusicLvlSlider.value); });
            MusicLvlSlider.OnEndDragAction += OnEndDrag;

            SfxLvlSlider.onValueChanged.AddListener(delegate { SetSfxLvl(SfxLvlSlider.value); });
            SfxLvlSlider.OnEndDragAction += OnEndDrag;

            MusicLvlSlider.maxValue = Config.NormalizedMaxMusicLvl;
            SfxLvlSlider.maxValue = Config.NormalizedMaxSfxLvl;

            MusicLvlSlider.value = Framework.AudioData.MusicLvl;
            SfxLvlSlider.value = Framework.AudioData.SfxLvl;

            SetMusicLvlSlider();
            SfxLvlSlidernSlider();
        }

        void SetMusicLvlSlider()
        {
            if (Framework.AudioData.MusicOn)
                MusicLvlSlider.interactable = true;
            else
                MusicLvlSlider.interactable = false;
        }

        void SfxLvlSlidernSlider()
        {
            if (Framework.AudioData.SfxOn)
                SfxLvlSlider.interactable = true;
            else
                SfxLvlSlider.interactable = false;
        }

        void SetMusicToggleButtonSprite()
        {
            if (Framework.AudioData.MusicOn)
                MusicToggleButton.image.sprite = MusicSpriteOn;
            else
                MusicToggleButton.image.sprite = MusicSpriteOff;
        }

        void SetSfxToggleButtonSprite(bool init = false)
        {
            if (Framework.AudioData.SfxOn)
            {
                SfxToggleButton.image.sprite = SfxSpriteOn;

                if (!init)
                    Framework.EventManager.TriggerEvent(System.Event.SystemListener.OnButtonClick);
            }
            else
                SfxToggleButton.image.sprite = SfxSpriteOff;
        }

        void ToggleMusic()
        {
            Framework.AudioManager.ToggleMusic();
            SetMusicToggleButtonSprite();
            SetMusicLvlSlider();
        }

        void ToggleSfx()
        {
            Framework.AudioManager.ToggleSfx();
            SetSfxToggleButtonSprite();
            SfxLvlSlidernSlider();
        }

        public void SetMusicLvl(float lvl)
        {
            Framework.AudioManager.SetMusicLvl(lvl);
        }

        public void SetSfxLvl(float lvl)
        {
            Framework.AudioManager.SetSfxLvl(lvl);
        }

        void OnEndDrag()
        {
            Framework.AudioManager.Save();
        }
    }
}