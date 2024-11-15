using CodeBase.Core.UI.Buttons;
using UnityEngine;

namespace CodeBase.Implementation.UI.Buttons
{
    public class SwitchStateButton : SwitchButton
    {
        [SerializeField] private Transform activeState;
        [SerializeField] private Transform inactiveState;
        
        public override void Switch(bool isActive)
        {
            base.Switch(isActive);
            
            if (isActive)
            {
                activeState.gameObject.SetActive(true);
                inactiveState.gameObject.SetActive(false);
            }
            else
            {
                activeState.gameObject.SetActive(false);
                inactiveState.gameObject.SetActive(true);
            }
        }
    }
}