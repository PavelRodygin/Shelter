using System;
using Core.Controllers;
using Cysharp.Threading.Tasks;
using Modules.MainMenu.Scripts;
using UltimateClean;
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
            _mainMenuUIView.showAllExercisesButton.onClick.AddListener(PlayCategoryButtonClicked);
            _mainMenuUIView.startQuickWarmUpButton.onClick.AddListener(PlayQuickWarmUpButtonClicked);
            _mainMenuUIView.settingsButton.onClick.AddListener(PlaySettingsButtonClicked);
            _mainMenuUIView.exitButton.onClick.AddListener(ExitButtonClicked);
            _mainMenuUIView.shopButton.onClick.AddListener(OpenShopClicked);
            _mainMenuUIView.parametersButton.onClick.AddListener(PlayParametersButtonClicked);
            await _mainMenuUIView.Show();
            var result = await _completionSource.Task;
            result.Invoke();
        }
        
        public async UniTask Stop()
        {
            await _mainMenuUIView.Hide();
        }
        
        private void PlayCategoryButtonClicked()
        {
            //_completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.Сategory));
        }
        
        private void PlayQuickWarmUpButtonClicked()
        {
            //_completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.WarmUpCustomization));
        }

        private void PlaySettingsButtonClicked()
        {
            _completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.Settings));
        }

        private void PlayParametersButtonClicked()
        {
            //_completionSource.TrySetResult(() => _rootController.RunController(ControllerMap.Parameters));
        }

        private void PlacardInfoOpenButtonClicked()
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
            _mainMenuUIView.showAllExercisesButton.onClick.RemoveListener(PlayCategoryButtonClicked);
            _mainMenuUIView.Dispose();
        }
    }
}