using CodeBase.Core.MVVM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Implementation.Modules.Base.PrivacyPolicy
{
    public class PrivacyPolicyScreenView : BaseScreenView
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button acceptButton;
        
        public void SetupEventListeners(UnityAction onBackButtonClicked)
        {
            backButton.onClick.AddListener(onBackButtonClicked);
            acceptButton.onClick.AddListener(onBackButtonClicked);
        }

        public override void Dispose()
        {
            RemoveEventListeners();
            base.Dispose();
        }

        private void RemoveEventListeners()
        {
            backButton.onClick.RemoveAllListeners();
            acceptButton.onClick.RemoveAllListeners();
        }
    }
}