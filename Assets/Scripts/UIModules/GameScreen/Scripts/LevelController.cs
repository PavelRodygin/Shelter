using System;
using Core.Controllers;
using Cysharp.Threading.Tasks;
using GameScripts.Level;
using UnityEngine;
using Zenject;

namespace UIModules.GameScreen.Scripts
{
    public class LevelController : IController
    {
        private readonly IRootController _rootController;
        private readonly GameScreenUIView _gameScreenUIView;
        private readonly GameplayModule _gameplayModule;
        private readonly UniTaskCompletionSource<Action> _completionSource;
        
        public LevelController(IRootController rootController, GameScreenUIView gameScreenUIView, GameplayModule gameplayModule)
        {
            _rootController = rootController;
            _gameScreenUIView = gameScreenUIView;
            _gameScreenUIView.gameObject.SetActive(false);
            _gameplayModule = gameplayModule;
            _completionSource = new UniTaskCompletionSource<Action>();
        }
        
        public async UniTask Run(object param)
        {
            _gameplayModule.StartGame();
            await _gameScreenUIView.Show();
            var result = await _completionSource.Task;
            result.Invoke();
        }

        public async UniTask Stop()
        {
            await _gameScreenUIView.Hide();
        }
        
        public void Dispose()
        {
            _gameScreenUIView.Dispose();
        }
    }
}