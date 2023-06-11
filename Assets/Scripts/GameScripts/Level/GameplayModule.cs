using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameScripts.PlayerScripts;
using Interfaces;
using UIModules.GameScreen.Scripts;
using UnityEngine;
using Zenject;

namespace GameScripts.Level
{
    public class GameplayModule : MonoBehaviour
    {
        [Inject] private GameScreenUIView _gameScreenUIView;
        [SerializeField] private GameObject levelWalls;
        [SerializeField] private Core.AbstractClasses.OpenClosable door;
        //[SerializeField] private List<Breakable> breakables = new();  
        //[SerializeField] private float breakingTime = 20000f;
        //[SerializeField] private float breakTimeMultiplier = 1.2f;
        [SerializeField] private float fadeTime = 0.5f;
        [SerializeField] private int showTime = 2000;
        public Player player;
        private PlayerMoveController _playerMoveController;
        private bool _surviveStarted;
        //private bool _allIsFine = true;
        private IBreakable _currentBroken; 
        //private List<ITask> _tasks;
  

        public async void StartGame()
        {
            var messages = new Dictionary<string, int>
            {
                { "That's was a typical day", 1000*2},
                { "But...", 1000*2},
            }; 
            await ShowGameMessage(messages);
            //Sound of siren on, timer on
            await UniTask.Delay(1000 * 60); //1 minute
            //DetonateBomb();
        }

        private void GenerateLevel()
        {
            Instantiate(levelWalls);
            Instantiate(door);
        }

        // private void DetonateBomb()
        // {
        //     if (player.MoveController)
        //     {
        //         
        //     }
        // }

        private void OnPlayerInsideShelter()
        {
            var messages = new Dictionary<string, int>()
            {
                { "Welcome to the shelter", 2000},
                { "Now you need to close all the doors", 2000},
                //добавляем в таблицу задач закрытие дверей
            }; 
        }
        
        // private async void StartSurvive()
        // {
        //     
        // }
        
        // private async void BreakSomething()
        // {
        //     while (player.MoveController.IsAlive)
        //     {
        //         int index = Random.Range(0, breakables.Count); 
        //         //breakables[index]Break();
        //         await UniTask.Delay((int)(breakingTime * breakTimeMultiplier));
        //     }
        // }
        
        public async UniTask ShowGameMessage(Dictionary<string, int> messages)
        {
            foreach (var message in messages)
            {
                _gameScreenUIView.messageText.gameObject.SetActive(true);
                _gameScreenUIView.messageText.text = message.Key;
                _gameScreenUIView.messageText.DOColor(Color.white, 0); 
                await _gameScreenUIView.messageText.DOFade(1f, fadeTime);
                await UniTask.Delay(showTime);
                await _gameScreenUIView.messageText.DOFade(0f, fadeTime);  
                _gameScreenUIView.messageText.gameObject.SetActive(false);
            }
        }

        public void Show()
        {
            //gameObject.SetActive(true); ?????
            GenerateLevel();
            player.gameObject.SetActive(true);
            _playerMoveController.OnPlayerInsideShelter += OnPlayerInsideShelter;
        }

        public void Hide()
        {
            _playerMoveController.OnPlayerInsideShelter -= OnPlayerInsideShelter;
            player.gameObject.SetActive(false);
            Destroy(levelWalls);
        }
    }
}
