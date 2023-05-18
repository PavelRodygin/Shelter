using System;
using Core.Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UIModules.GameScreen.Scripts
{
    public class GameScreenController : IController
    {
        private readonly IRootController _rootController;
        private readonly GameScreenUIView _gameScreenUIView;
        private readonly UniTaskCompletionSource<Action> _completionSource;
        
        public GameScreenController(IRootController rootController, GameScreenUIView gameScreenUIView)
        {
            _rootController = rootController;
            _gameScreenUIView = gameScreenUIView;
            _gameScreenUIView.gameObject.SetActive(false);
            _completionSource = new UniTaskCompletionSource<Action>();
        }
        
        public async UniTask Run(object param)
        {
            await _gameScreenUIView.Show();
            Debug.Log("Показали");
            var result = await _completionSource.Task;
            result.Invoke();
        }

        public async UniTask Stop()
        {
            await _gameScreenUIView.Hide();
        }
        
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}