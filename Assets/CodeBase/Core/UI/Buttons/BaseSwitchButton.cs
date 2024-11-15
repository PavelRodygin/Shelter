using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Core.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class SwitchButton : MonoBehaviour
    {
        public Button button;
        
        public bool IsActive { get; private set; }

        public virtual void Switch(bool isActive)
        {
            IsActive = isActive;
        }
    }
}