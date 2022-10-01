using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class GameScreen : MonoBehaviour
    {
        [SerializeField] public Button openButton;
        [SerializeField] public Button closeButton;
        [SerializeField] public Button interactButton;
        [SerializeField] public Button repairButton;
        [SerializeField] public Joystick walkJoystick;


        private void Start()
        {
            openButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);
            repairButton.gameObject.SetActive(false);
        }
    }
}