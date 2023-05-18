using System.Collections.Generic;
using Core.Views;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UIModules.GameScreen.Scripts
{
    public class GameScreenUIView : UIView
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] public Button interactButton;
        [SerializeField] public Button repairButton;
        [SerializeField] public Button jumpButton;
        [SerializeField] public Button crouchButton;
        [SerializeField] public Button getUpButton;
        [SerializeField] public Button throwButton;
        [SerializeField] public Button grabButton;
        [SerializeField] public Joystick walkJoystick;
        
        private void OnEnable()
        {
            SetDisableButtons();
            getUpButton.gameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);
            grabButton.gameObject.SetActive(false);
            repairButton.gameObject.SetActive(false);
        }

        private void SetDisableButtons()
        {
            interactButton.onClick.AddListener(() => interactButton.gameObject.SetActive(false));
            grabButton.onClick.AddListener(() => grabButton.gameObject.SetActive(false));
            throwButton.onClick.AddListener(() => throwButton.gameObject.SetActive(false));
        }
        
        public async UniTask ShowGameMessage(Dictionary<string, int> messages)
        {
            float fadeTime = 0.5f;
            int showTime = 2000;
            foreach (var message in messages)
            {
                messageText.gameObject.SetActive(true);
                messageText.text = message.Key;
                messageText.DOColor(Color.white, 0); 
                await messageText.DOFade(1f, fadeTime);
                await UniTask.Delay(showTime);
                await messageText.DOFade(0f, fadeTime);  
                messageText.gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            interactButton.onClick.RemoveAllListeners();
            grabButton.onClick.RemoveAllListeners();
            throwButton.onClick.RemoveAllListeners();
        }
    }
}