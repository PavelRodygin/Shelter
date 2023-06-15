using System;
using Core.Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UIModules.MainMenu.Scripts
{
    public class MainMenuController : IController
    {
        private readonly IRootController _rootController;
        private readonly MainMenuUIView _mainMenuUIView;
        private readonly UniTaskCompletionSource<Action> _completionSource;

        public MainMenuController(IRootController rootController, MainMenuUIView mainMenuUIView)
        {
            _rootController = rootController;
            _mainMenuUIView = mainMenuUIView;
            _mainMenuUIView.gameObject.SetActive(false);
            _completionSource = new UniTaskCompletionSource<Action>();
        }
        
        public async UniTask Run(object param)
        {
            SetupEventListeners();
            await _mainMenuUIView.Show();
            var result = await _completionSource.Task;
            result.Invoke();
        }
        
        private void SetupEventListeners()
        {
            _mainMenuUIView.startGameButton.onClick.AddListener(StartGameButtonClicked);
            _mainMenuUIView.settingsButton.onClick.AddListener(PlaySettingsButtonClicked);
            _mainMenuUIView.exitButton.onClick.AddListener(ExitButtonClicked);
        }
        
        public async UniTask Stop()
        {
            await _mainMenuUIView.Hide();
        }
        
        private void StartGameButtonClicked()
        {
            _completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.GameScreen));
        }

        private void PlaySettingsButtonClicked()
        {
            _completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.Settings));
        }
        
        private void ExitButtonClicked()
        {
            Application.Quit();
        }
        
        public void Dispose()   
        {
            _mainMenuUIView.startGameButton.onClick.RemoveListener(StartGameButtonClicked);
            _mainMenuUIView.Dispose();
        }
    }
}