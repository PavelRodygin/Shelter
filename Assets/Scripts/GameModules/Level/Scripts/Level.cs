using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameModules.PlayerScripts.Scripts;
using Interfaces;
using UIModules.GameScreen.Scripts;
using UnityEngine;
using Zenject;

namespace GameModules
{
    public class Level : MonoBehaviour
    {
        [Inject] private GameScreen _gameScreen;
        [SerializeField] private MoveController playerMoveController;
        [SerializeField] private List<GameObject> breakables;  //TODO
        //[SerializeField] private List<IBreakable> breakables = new List<IBreakable>();  //TODO
        [SerializeField] private float breakingTime = 20000f;
        [SerializeField] private float breakTimeMultiplier = 0.9f;
        private bool _surviveStarted;
        private bool _allIsFine = true;
        private IBreakable _currentBroken; 
        //private List<ITask> _tasks;

    
        private void Awake()
        {
            StartGame();
            //_playerMoveController.SurvivalStarted += StartSurvive;
        }

        private void Update()
        {
        
        }   

        private async void BreakSomething()
        {
            // while (_playerMoveController.IsAlive)
            // {
            //     int index = Random.Range(0, breakables.Count); 
            //     //breakables[index]Break();
            //     await UniTask.Delay((int)(breakingTime * breakTimeMultiplier));
            // }
        }
    
        private async void StartGame()
        {
            var messages = new Dictionary<string, int>
            {
                { "That's was a typical day", 1000*2},
                { "But...", 1000*2},
            }; 
            await _gameScreen.ShowGameMessage(messages);
            //Sound of siren on, timer on
            await UniTask.Delay(1000 * 60 * 3); //3 minutes
            //DetonateBomb();
            StartSurvive();
        }


        // private void DetonateBomb()
        // {
        //     if (_player.Inside the)
        //     {
        //         
        //     }
        // }
        
        private async void StartSurvive()
        {
            var messages = new Dictionary<string, int>()
            {
                { "Welcome to the shelter", 2000},
                { "Now you need to close all the doors", 2000},
                //добавляем в таблицу задач закрытие дверей
            }; 
        }
    }
}
