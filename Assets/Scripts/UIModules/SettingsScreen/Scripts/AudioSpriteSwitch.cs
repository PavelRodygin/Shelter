using UnityEngine;
using UnityEngine.UI;

namespace Modules.Settings.Scripts
{
    public class AudioSpriteSwitch : MonoBehaviour
    {
        [SerializeField] private Image enabledIcon;
        [SerializeField] private Image disabledIcon;

        public void ToggleEnabled()
        {
            enabledIcon.gameObject.SetActive(true);
            disabledIcon.gameObject.SetActive(false);
        }
        
        public void ToggleDisable()
        {
            enabledIcon.gameObject.SetActive(false);
            disabledIcon.gameObject.SetActive(true);
        }
    }
}