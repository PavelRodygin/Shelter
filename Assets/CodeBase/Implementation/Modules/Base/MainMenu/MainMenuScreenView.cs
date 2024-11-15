using CodeBase.Core.MVVM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Implementation.Modules.Base.MainMenu
{
    public class MainMenuScreenView : BaseScreenView
    {
        [SerializeField] private Button addonListButton;
        [SerializeField] private Button favoritesButton;
        [SerializeField] private Button instructionButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button privacyPolicyButton;
        [SerializeField] private Toggle soundToggle;

        public void SetupEventListeners(
            UnityAction onAddonListButtonClicked,
            UnityAction onFavoritesButtonClicked,
            UnityAction onInstructionButtonClicked,
            UnityAction onSettingsButtonClicked,
            UnityAction<bool> onSoundToggleClicked,
            UnityAction onPrivacyPolicyButtonClicked)
        {
            addonListButton.onClick.AddListener(onAddonListButtonClicked);
            favoritesButton.onClick.AddListener(onFavoritesButtonClicked);
            instructionButton.onClick.AddListener(onInstructionButtonClicked);
            settingsButton.onClick.AddListener(onSettingsButtonClicked);
            
            //Elements below may be unused, due to the App design

            if (soundToggle != null) 
                soundToggle.onValueChanged.AddListener(onSoundToggleClicked);
            
            if (privacyPolicyButton != null) 
                privacyPolicyButton.onClick.AddListener(onPrivacyPolicyButtonClicked);
        }

        public void InitializeToggle(bool isSoundEnabled)
        {
            if (soundToggle != null) soundToggle.isOn = !isSoundEnabled;
        }

        public override void Dispose()
        {
            RemoveEventListeners();
            base.Dispose();
        }

        private void RemoveEventListeners()
        {
            addonListButton.onClick.RemoveAllListeners();
            favoritesButton.onClick.RemoveAllListeners();
            instructionButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            
            //Elements below may be unused, due to the App design
            
            if (privacyPolicyButton != null) 
                privacyPolicyButton.onClick.RemoveAllListeners();
            
            if (soundToggle != null) 
                soundToggle.onValueChanged.RemoveAllListeners();
        }
    }
}