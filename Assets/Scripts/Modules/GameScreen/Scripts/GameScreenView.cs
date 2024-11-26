using Core.Views;
using SimpleInputNamespace;
using TMPro;
using UnityEngine;

namespace Modules.GameScreen.Scripts
{
    public class GameScreenView : UIView
    {
        [SerializeField] public TextMeshProUGUI messageText;
        [SerializeField] public GameObject interactTipView;
        [SerializeField] public GameObject crouchTipView;
        [SerializeField] public GameObject dropItemTipView;
        // [SerializeField] public Joystick walkJoystick;
    }
}