using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Modules.GameScreen.Scripts
{
    public class GameMessageManager
    {
        private GameScreenView _gameScreenView;
        private const float MessageFadeTime = 0.25f;
        
        public void Initialize(GameScreenView gameScreenView) => 
            _gameScreenView = gameScreenView;

        public async UniTask ShowMessagesSequentially(Dictionary<string, int> messages)
        {
            foreach (var message in messages) await ShowMessage(message.Key, message.Value);
        }

        public async UniTask ShowMessage(string message, int time)
        {
            _gameScreenView.messageText.gameObject.SetActive(true);
            _gameScreenView.messageText.text = message;
            _gameScreenView.messageText.DOColor(Color.white, 0); 
            await _gameScreenView.messageText.DOFade(1f, MessageFadeTime);
            await UniTask.Delay(time);
            await _gameScreenView.messageText.DOFade(0f, MessageFadeTime);  
            _gameScreenView.messageText.gameObject.SetActive(false);
        }
    }
}