using System;
using Core.Controllers;
using Cysharp.Threading.Tasks;

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
        
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public UniTask Run(object param)
        {
            throw new System.NotImplementedException();
        }

        public UniTask Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}