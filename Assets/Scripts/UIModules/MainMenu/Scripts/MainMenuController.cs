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
            _mainMenuUIView.startGameButton.onClick.AddListener(StartGameButtonClicked);
            _mainMenuUIView.settingsButton.onClick.AddListener(PlaySettingsButtonClicked);
            _mainMenuUIView.exitButton.onClick.AddListener(ExitButtonClicked);
            //_mainMenuUIView.shopButton.onClick.AddListener(OpenShopClicked);
            await _mainMenuUIView.Show();
            var result = await _completionSource.Task;
            result.Invoke();
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

        private void OpenInfoPlacardButtonClicked()
        {
            Application.OpenURL("https://t.me/+_Tb9ZgJXYcw4NTNi");
        }
        
        private void PlacardInfoCloseButtonClicked()
        {
            //_mainMenuUIView.informationPlacard.GetComponent<Popup>().Close();   
        }

        private void OpenShopClicked()
        {
            //_completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.Shop));
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