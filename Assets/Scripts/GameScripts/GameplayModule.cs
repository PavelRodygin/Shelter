using System.Collections.Generic;
using Core.AbstractClasses;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameScripts.PlayerScripts;
using Interfaces;
using UIModules.GameScreen.Scripts;
using UnityEngine;
using Zenject;

namespace GameScripts
{
    public class GameplayModule : MonoBehaviour
    {
        [Inject] private Camera _camera;
        [SerializeField] private GameObject levelWalls;
        [SerializeField] private Core.AbstractClasses.OpenClosable door;
        [SerializeField] private List<Breakable> _breakables = new();  
        [SerializeField] private float breakingTime = 20000f;
        [SerializeField] private float breakTimeMultiplier = 1.2f;
        [SerializeField] private float fadeTime = 0.5f;
        [SerializeField] private int showTime = 2000;
        public Player player;
        private GameScreenUIView _gameScreenUIView;
        private IBreakable _currentBroken;
        private bool _surviveStarted;
        private bool _allIsFine = true;
        //private List<ITask> _tasks;

        public void Initialize(GameScreenUIView gameScreenUIView)
        {
            _gameScreenUIView = gameScreenUIView;
            player = Instantiate(player, transform).GetComponent<Player>();
            player.Initialize(gameScreenUIView, _camera);
        }

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
            player.transform.localPosition = new Vector3(-0.3f, 5f, -5f);
            levelWalls = Instantiate(levelWalls, transform);
            door = Instantiate(door, transform);
            door.transform.position = new Vector3(0f, 0.482f, 0f);
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
            ShowGameMessage(messages).Forget();
        }

        // private async void BreakSomething()
        // {
        //     while (player.MoveController.IsAlive)
        //     {
        //         int index = Random.Range(0, breakables.Count); 
        //         //breakables[index]Break();
        //         await UniTask.Delay((int)(breakingTime * breakTimeMultiplier));
        //     }
        // }
        
        private async UniTask ShowGameMessage(Dictionary<string, int> messages)
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
            GenerateLevel();
            player.MoveModule.OnPlayerInsideShelter += OnPlayerInsideShelter;
        }

        public void Hide()
        {
            player.MoveModule.OnPlayerInsideShelter -= OnPlayerInsideShelter;
            player.Hide();
            Destroy(player);
            Destroy(door);
            Destroy(levelWalls);
        }
    }
}
