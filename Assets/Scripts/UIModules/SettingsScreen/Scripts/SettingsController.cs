using System;
using Core.Controllers;
using Cysharp.Threading.Tasks;
using Systems.AudioSystem;
using UltimateClean;
using UnityEngine;

namespace UIModules.SettingsScreen.Scripts
{
    public class SettingsController : IController
    {
        private readonly IRootController _rootController;
        private readonly SettingsUIView _settingsUIView;
        private readonly UniTaskCompletionSource<Action> _completionSource;
        private readonly AudioSystem _audioSystem;
        private readonly float _savedMusicVolume;
        private readonly float _savedSoundVolume;
        private readonly int _savedSongIndex;
        private int _savedLanguage;

        public SettingsController(IRootController rootController, SettingsUIView settingsUIView, AudioSystem audioSystem)
        {
            _audioSystem = audioSystem;
            _rootController = rootController;
            _settingsUIView = settingsUIView;
            _completionSource = new UniTaskCompletionSource<Action>();
            _savedMusicVolume = _audioSystem.musicVolume;
            _savedSoundVolume = _audioSystem.soundsVolume;
            _savedSongIndex = _settingsUIView.musicDropDown.value;
        }

        public async UniTask Run(object param)
        {
            _savedLanguage = _settingsUIView.languageDropDown.value;
            _settingsUIView.saveButton.onClick.AddListener(PlaySaveButtonClicked);
            _settingsUIView.backButton.onClick.AddListener(PlayBackButtonClicked);
            _settingsUIView.soundSwitch.onClick.AddListener(PlaySoundsSwitchButtonClicked);
            _settingsUIView.soundsVolumeSlider.onValueChanged.AddListener(ChangeSoundsVolumeSlider);
            _settingsUIView.musicSwitch.onClick.AddListener(PlayMusicSwitchButtonClicked);
            _settingsUIView.musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolumeSlider);

            await _settingsUIView.Show();
            Debug.Log("Показали");
            var result = await _completionSource.Task;
            result.Invoke();
        }

        public async UniTask Stop()
        {
            await _settingsUIView.Hide();
        }

        private void PlaySaveButtonClicked()
        {
            _savedLanguage = _settingsUIView.languageDropDown.value;
            _completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.MainMenu));
        }
        private void PlayBackButtonClicked()
        {
            _settingsUIView.languageDropDown.value = _savedLanguage;    
            _audioSystem.ChangeMusicVolume(_savedMusicVolume);
            _audioSystem.soundsVolume = _savedSoundVolume;
            if (_savedSongIndex != _settingsUIView.musicDropDown.value)
                _audioSystem.PlaySong(_savedSongIndex);
            _completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.MainMenu));
        }

        private void PlaySoundsSwitchButtonClicked()
        {
            if (_audioSystem.soundsVolume > 0)
            {
                _audioSystem.soundsVolume = 0;
                _settingsUIView.isSoundsOn = false;
                _settingsUIView.soundsVolumeSlider.value = 0;
                _settingsUIView.soundsVolumeSpriteSwitch.ToggleDisable();
            }
            else if (_audioSystem.soundsVolume == 0)
            {
                _audioSystem.soundsVolume = 0.5f;
                _settingsUIView.isSoundsOn = true;
                _settingsUIView.soundsVolumeSlider.value = 50f;
                _settingsUIView.soundsVolumeSpriteSwitch.ToggleEnabled();
            }
        }

        private void PlayMusicSwitchButtonClicked()
        {
            if (_audioSystem.musicVolume > 0)
            {
                _audioSystem.ChangeMusicVolume(0);
                _settingsUIView.isMusicOn = false;
                _settingsUIView.musicVolumeSlider.value = 0;
                _settingsUIView.musicVolumeSpriteSwitch.ToggleDisable();
            }
            else if (_audioSystem.musicVolume == 0)
            {
                _audioSystem.ChangeMusicVolume(.5f);
                _settingsUIView.isMusicOn = true;
                _settingsUIView.musicVolumeSlider.value = 50f;
                _settingsUIView.musicVolumeSpriteSwitch.ToggleEnabled();
            }
        }

        private void ChangeSoundsVolumeSlider(float value)
        {
            _audioSystem.soundsVolume = _settingsUIView.soundsVolumeSlider.value / 100;
            if (!_settingsUIView.isSoundsOn && _settingsUIView.soundsVolumeSlider.value > 0)
            {
                _settingsUIView.soundSwitch.GetComponent<Switch>().Toggle();
                _settingsUIView.isSoundsOn = true;
                _settingsUIView.soundsVolumeSpriteSwitch.ToggleEnabled();
            }
            if (_settingsUIView.soundsVolumeSlider.value == 0 && _settingsUIView.isSoundsOn)
            {
                _settingsUIView.soundSwitch.GetComponent<Switch>().Toggle();
                _settingsUIView.isSoundsOn = false;
                _settingsUIView.soundsVolumeSpriteSwitch.ToggleDisable();
            }
        }

        private void ChangeMusicVolumeSlider(float value)
        {
            _audioSystem.ChangeMusicVolume(_settingsUIView.musicVolumeSlider.value / 100);
            if (!_settingsUIView.isMusicOn && _settingsUIView.musicVolumeSlider.value > 0)
            {
                _settingsUIView.musicSwitch.GetComponent<Switch>().Toggle();
                _settingsUIView.isMusicOn = true;
                _settingsUIView.musicVolumeSpriteSwitch.ToggleEnabled();

            }
            if (_settingsUIView.musicVolumeSlider.value == 0 && _settingsUIView.isMusicOn)
            {
                _settingsUIView.musicSwitch.GetComponent<Switch>().Toggle();
                _settingsUIView.isMusicOn = false;
                _settingsUIView.musicVolumeSpriteSwitch.ToggleDisable();
            }
        }

        public void Dispose()
        {
            _settingsUIView.saveButton.onClick.RemoveListener(PlaySaveButtonClicked);
            _settingsUIView.saveButton.onClick.RemoveListener(PlayBackButtonClicked);
            _settingsUIView.Dispose();
        }
    }
}