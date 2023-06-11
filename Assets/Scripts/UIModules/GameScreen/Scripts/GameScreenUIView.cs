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
        [SerializeField] public TextMeshProUGUI messageText;
        [SerializeField] public Button interactButton;
        [SerializeField] public Button jumpButton;
        [SerializeField] public Button crouchButton;
        [SerializeField] public Button throwButton;
        [SerializeField] public Button grabButton;
        [SerializeField] public Joystick walkJoystick;

        
        private void OnEnable()
        {
            SetDisableButtons();
            interactButton.gameObject.SetActive(false);
            grabButton.gameObject.SetActive(false);
        }

        private void SetDisableButtons()
        {
            interactButton.onClick.AddListener(() => interactButton.gameObject.SetActive(false));
            grabButton.onClick.AddListener(() => grabButton.gameObject.SetActive(false));
            throwButton.onClick.AddListener(() => throwButton.gameObject.SetActive(false));
        }

        private void OnDisable()
        {
            interactButton.onClick.RemoveAllListeners();
            grabButton.onClick.RemoveAllListeners();
            throwButton.onClick.RemoveAllListeners();
        }
    }
}