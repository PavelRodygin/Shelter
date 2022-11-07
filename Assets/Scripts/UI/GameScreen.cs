using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] public Button openButton;
        [SerializeField] public Button closeButton;
        [SerializeField] public Button interactButton;
        [SerializeField] public Button repairButton;
        [SerializeField] public Button jumpButton;
        [SerializeField] public Button crouchButton;
        [SerializeField] public Button getUpButton;
        [SerializeField] public Button throwButton;
        [SerializeField] public Button grabButton;
        [SerializeField] public Joystick walkJoystick;


        private void Awake()
        {
            SetDisableButtons();
            getUpButton.gameObject.SetActive(false);
            openButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);
            repairButton.gameObject.SetActive(false);
        }

        private void SetDisableButtons()
        {
            openButton.onClick.AddListener(() => openButton.gameObject.SetActive(false));
            closeButton.onClick.AddListener(() => closeButton.gameObject.SetActive(false));
            interactButton.onClick.AddListener(() => interactButton.gameObject.SetActive(false));
            grabButton.onClick.AddListener(() => grabButton.gameObject.SetActive(false));
            throwButton.onClick.AddListener(() => throwButton.gameObject.SetActive(false));
        }

        public async void ShowGameMessage(string message, int time)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = message;
            messageText.DOColor(Color.white, 0f);
            messageText.DOFade(1f, 2f);
            await UniTask.Delay(time);
            messageText.DOFade(0f, 2f);
            await UniTask.Delay(2000);
            messageText.gameObject.SetActive(false);
        }

        public async void ShowGameException(string message)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = message;
            messageText.DOFade(1f, 2f);
            messageText.DOColor(Color.red, 0f);
            await UniTask.Delay(6000);
            messageText.DOFade(0f, 2f);
            await UniTask.Delay(2000);
            messageText.gameObject.SetActive(false);
        }
    }
}