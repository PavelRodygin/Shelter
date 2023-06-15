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
           
        }
    }
}