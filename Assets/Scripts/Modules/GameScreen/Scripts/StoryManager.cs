using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Modules.GameScreen.Scripts
{
    public class StoryManager : MonoBehaviour
    {
        [Inject] private GameMessageManager _gameMessageManager;
        
        public async void StartGame()
        {
            await UniTask.Delay(1000);
            var messages = new Dictionary<string, int>
            {
                { "That's was a typical day", 3000},
                { "But...", 1000*2},
            }; 
            await _gameMessageManager.ShowMessagesSequentially(messages);
            //Sound of siren on, timer on
            await UniTask.Delay(1000 * 60); //1 minute
            //DetonateBomb();
        }
        
        private void PlayInsiderShelter()
        {
            var messages = new Dictionary<string, int>()
            {
                { "Welcome to the shelter", 2000},
                { "Now you need to close all the doors", 2500},
                //  Add task CloseDoors
            };
            _gameMessageManager.ShowMessagesSequentially(messages).Forget();
        }
        

    }
}