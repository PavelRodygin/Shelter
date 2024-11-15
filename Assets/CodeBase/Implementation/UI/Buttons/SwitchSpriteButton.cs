using CodeBase.Core.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Implementation.UI.Buttons
{
    public class SwitchSpriteButton : SwitchButton
    {
        [SerializeField] private Sprite inactiveSprite;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Image image;

        public override void Switch(bool isActive)
        {
            button.interactable = !isActive;
            image.sprite = isActive ? activeSprite : inactiveSprite;
        }
    }
}