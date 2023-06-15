using System;
using Core.Controllers;
using Cysharp.Threading.Tasks;
using Systems.AudioSystem;
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
            _completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.MainMenu));
        }

        private void PlaySoundsSwitchButtonClicked()
        {
           
        }

        private void PlayMusicSwitchButtonClicked()
        {
            
        }

        private void ChangeSoundsVolumeSlider(float value)
        {
         
        }

        private void ChangeMusicVolumeSlider(float value)
        {
           
        }

        public void Dispose()
        {
            _settingsUIView.saveButton.onClick.RemoveListener(PlaySaveButtonClicked);
            _settingsUIView.saveButton.onClick.RemoveListener(PlayBackButtonClicked);
            _settingsUIView.Dispose();
        }
    }
}