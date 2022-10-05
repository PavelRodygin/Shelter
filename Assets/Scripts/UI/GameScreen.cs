using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] public Button openButton;
        [SerializeField] public Button closeButton;
        [SerializeField] public Button interactButton;
        [SerializeField] public Button repairButton;
        [SerializeField] public Button jumpButton;
        [SerializeField] public Button crouchButton;
        [SerializeField] public Button getUpButton;
        [SerializeField] public Button throwButton;
        [SerializeField] public Button grabButton;
        [SerializeField] public Button switchButton;
        [SerializeField] public Joystick walkJoystick;
        

        private void Awake()
        { 
            openButton.onClick.AddListener(() => openButton.gameObject.SetActive(false));
            closeButton.onClick.AddListener(() => closeButton.gameObject.SetActive(false));
            interactButton.onClick.AddListener(() => interactButton.gameObject.SetActive(false));
            getUpButton.gameObject.SetActive(false);
            openButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);
            repairButton.gameObject.SetActive(false);
            
        }
    }
}