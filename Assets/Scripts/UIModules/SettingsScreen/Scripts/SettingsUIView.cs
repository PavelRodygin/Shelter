using Core.Views;
using Modules.Settings.Scripts;
using Systems.AudioSystem;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace UIModules.SettingsScreen.Scripts
{
    public class SettingsUIView : UIView
    {
        [Inject] private AudioSystem _audioSystem;

        public TMP_Dropdown musicDropDown;
        public TMP_Dropdown languageDropDown;
        
        public AudioSpriteSwitch soundsVolumeSpriteSwitch;
        public AudioSpriteSwitch musicVolumeSpriteSwitch;

        public Button saveButton;
        public Button backButton;

        public Slider musicVolumeSlider;
        public Slider soundsVolumeSlider;

        public Button musicSwitch;
        public Button soundSwitch;

        public bool isMusicOn = true;
        public bool isSoundsOn = true;
        
        private void OnEnable()
        {
            musicDropDown.ClearOptions();
            musicDropDown.AddOptions(_audioSystem.songsNames);
            musicDropDown.value = _audioSystem.ReturnCurrentSongIndex();
            musicDropDown.onValueChanged.AddListener(songIndex => _audioSystem.PlaySong(songIndex));
            

            soundsVolumeSlider.value = _audioSystem.soundsVolume*100;
            musicVolumeSlider.value = _audioSystem.musicVolume*100;
            if (_audioSystem.soundsVolume > 0)
            {
                isSoundsOn = true;
                soundsVolumeSpriteSwitch.ToggleEnabled();
            }
            else
            {
                isSoundsOn = false;
                soundsVolumeSpriteSwitch.ToggleDisable();
            }
            
            if (_audioSystem.musicVolume > 0)
            {
                isMusicOn = true;
                musicVolumeSpriteSwitch.ToggleEnabled();
            }
            else
            {
                isMusicOn = false;
                musicVolumeSpriteSwitch.ToggleDisable();
            }
            isSoundsOn = _audioSystem.soundsVolume > 0;
            isMusicOn = _audioSystem.musicVolume > 0;
        }
    }
}