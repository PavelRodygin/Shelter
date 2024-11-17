using System;
using Core.Controllers;
using Cysharp.Threading.Tasks;

namespace Modules.GameScreen.Scripts
{
    public class GameScreenController : IController
    {
        private readonly IRootController _rootController;
        private readonly GameScreenView _gameScreenView;
        private readonly LevelManager _levelManager;
        private readonly GameMessageManager _gameMessageManager;
        // private readonly StoryManager _gameStoryManager;
        private readonly UniTaskCompletionSource<Action> _completionSource;

        public GameScreenController(IRootController rootController, GameScreenView gameScreenView,
            LevelManager levelManager, GameMessageManager gameMessageManager)
        {
            _rootController = rootController;
            _gameScreenView = gameScreenView;
            _gameScreenView.gameObject.SetActive(false);
            _levelManager = levelManager;
            _gameMessageManager = gameMessageManager;
            // _gameStoryManager = gameStoryManager;
            _completionSource = new UniTaskCompletionSource<Action>();
        }
        
        public async UniTask Run(object param)
        {
            await _gameScreenView.Show();
            SetupEventListeners();
            _levelManager.gameObject.SetActive(true);
            _levelManager.Initialize(_gameScreenView);
            await _levelManager.Show();
            _gameMessageManager.Initialize(_gameScreenView);
            // _gameStoryManager.StartGame();
            
            var result = await _completionSource.Task;
            result.Invoke();
        }
        
        private void SetupEventListeners()
        {
            _gameScreenView.interactButton.onClick.AddListener(() => _gameScreenView.interactButton.gameObject.SetActive(false));
            _gameScreenView.dropButton.onClick.AddListener(() => _gameScreenView.dropButton.gameObject.SetActive(false));
        }

        public async UniTask Stop()
        {
            var task1 = _levelManager.Hide();
            var task2 = _gameScreenView.Hide();
            await UniTask.WhenAll(task1, task2);
        }
        
        public void Dispose()
        {
            _gameScreenView.Dispose();
        }
    }
}