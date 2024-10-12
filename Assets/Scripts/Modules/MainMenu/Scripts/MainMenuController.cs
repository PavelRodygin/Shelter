using System;
using Core.Controllers;
using Core.Systems;
using Cysharp.Threading.Tasks;
using GameScripts;
using Modules.GameScreen.Scripts;
using UnityEngine;

namespace UIModules.MainMenu.Scripts
{
    public class MainMenuController : IController
    {
        private readonly IRootController _rootController;
        private readonly AudioSystem _audioSystem;
        private readonly LevelManager _levelManager;
        private readonly MainMenuUIView _mainMenuUIView;
        private readonly UniTaskCompletionSource<Action> _completionSource;

        public MainMenuController(IRootController rootController, MainMenuUIView mainMenuUIView, AudioSystem audioSystem,
            LevelManager levelManager)
        {
            _rootController = rootController;
            _mainMenuUIView = mainMenuUIView;
            _mainMenuUIView.gameObject.SetActive(false);    
            _audioSystem = audioSystem;
            _levelManager = levelManager;
            _completionSource = new UniTaskCompletionSource<Action>();
        }
        
        public async UniTask Run(object param)
        {
            SetupEventListeners();
            await _mainMenuUIView.Show();
            _audioSystem.PlayMainMenuMelody();
            var result = await _completionSource.Task;
            result.Invoke();
        }
        
        private void SetupEventListeners()
        {
            _mainMenuUIView.settingsButton.onClick.AddListener(ShowSettingsPopup);
            _mainMenuUIView.settingsPopup.closeButton.onClick.AddListener(HideSettingsPopup);
            _mainMenuUIView.settingsPopup.musicSwitch.OnSwitchToggled += OnMusicSwitchToggled;
            _mainMenuUIView.settingsPopup.musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolumeSlider);
            _mainMenuUIView.settingsPopup.soundsSwitch.OnSwitchToggled += OnSoundsSwitchToggled;
            _mainMenuUIView.settingsPopup.soundsVolumeSlider.onValueChanged.AddListener(ChangeSoundsVolumeSlider);
            _mainMenuUIView.settingsPopup.sensitivitySlider.onValueChanged.AddListener(ChangeSensitivitySlider);
            _mainMenuUIView.playButton.onClick.AddListener(PlayButtonClicked);
            _mainMenuUIView.quitButton.onClick.AddListener(ExitButtonClicked);
        }
        
        private void PlayButtonClicked()
        {   
            _audioSystem.StopMainMenuMelody();
            _completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.GameScreen));
        }

        private async void ShowSettingsPopup()
        {
            _mainMenuUIView.settingsPopup.gameObject.SetActive(true);
            _mainMenuUIView.settingsPopup.musicVolumeSlider.value = _audioSystem.MusicVolume;
            _mainMenuUIView.settingsPopup.soundsVolumeSlider.value = _audioSystem.SoundsVolume;
            _mainMenuUIView.settingsPopup.sensitivitySlider.value = _levelManager.CameraSensitivity;
            await _mainMenuUIView.settingsPopup.Show();
        }
        
        private void ExitButtonClicked() => Application.Quit();
        
        #region SettingsPopup

        private void OnMusicSwitchToggled(bool switchEnabled)
        {
            if (switchEnabled)
            {
                if (_mainMenuUIView.settingsPopup.musicVolumeSlider.value == 0)
                    _mainMenuUIView.settingsPopup.musicVolumeSlider.value = .5f;
            }
            else
                _mainMenuUIView.settingsPopup.musicVolumeSlider.value = 0;
        }
        
        private void ChangeMusicVolumeSlider(float volume)
        {
            if (volume > 0)
            {
                if (!_mainMenuUIView.settingsPopup.musicSwitch.SwitchEnabled)
                    _mainMenuUIView.settingsPopup.musicSwitch.Toggle();  //Enable switch
                _audioSystem.SetMusicVolume(volume);
            }
            else
            {
                if (_mainMenuUIView.settingsPopup.musicSwitch.SwitchEnabled)
                    _mainMenuUIView.settingsPopup.musicSwitch.Toggle();  //Disable switch
                _audioSystem.SetMusicVolume(0);
            }
        }

        private void OnSoundsSwitchToggled(bool switchEnabled)
        {
            if (switchEnabled)
            {
                if (_mainMenuUIView.settingsPopup.soundsVolumeSlider.value == 0)
                    _mainMenuUIView.settingsPopup.soundsVolumeSlider.value = .5f;
            }
            else
                _mainMenuUIView.settingsPopup.soundsVolumeSlider.value = 0;
        } 
        
        private void ChangeSoundsVolumeSlider(float volume)
        {
            if (volume > 0)
            {
                if (!_mainMenuUIView.settingsPopup.soundsSwitch.SwitchEnabled)
                    _mainMenuUIView.settingsPopup.soundsSwitch.Toggle();  //Enable switch
                _audioSystem.SetSoundsVolume(volume);
            }
            else
            {
                if (_mainMenuUIView.settingsPopup.soundsSwitch.SwitchEnabled)
                    _mainMenuUIView.settingsPopup.soundsSwitch.Toggle();  //Disable switch
                _audioSystem.SetSoundsVolume(0);
            }
        }
        
        private void ChangeSensitivitySlider(float sensitivity) => _levelManager.CameraSensitivity = sensitivity;

        private async void HideSettingsPopup() => await _mainMenuUIView.settingsPopup.Hide();
        
        #endregion
        
        public async UniTask Stop() => await _mainMenuUIView.Hide();

        public void Dispose()   
        {
            _mainMenuUIView.playButton.onClick.RemoveListener(PlayButtonClicked);
            _mainMenuUIView.Dispose();
        }
    }
}