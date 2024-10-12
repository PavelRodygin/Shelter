using Core.Views;
using SimpleInputNamespace;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Modules.GameScreen.Scripts
{
    public class GameScreenView : UIView
    {
        [SerializeField] public TextMeshProUGUI messageText;
        [SerializeField] public Button interactButton;
        [SerializeField] public Button crouchButton;
        [SerializeField] public Button dropButton;
        [SerializeField] public Joystick walkJoystick;
    }
}