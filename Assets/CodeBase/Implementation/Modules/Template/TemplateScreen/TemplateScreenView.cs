using CodeBase.Core.MVVM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Implementation.Modules.Template.TemplateScreen
{
    public class TemplateScreenView : BaseScreenView
    {
        [SerializeField] private Button backButton;
        
        public void SetupEventListeners(UnityAction onBackButtonClicked)
        {
            backButton.onClick.AddListener(onBackButtonClicked);
        }
        
        private void RemoveEventListeners()
        {
            backButton.onClick.RemoveAllListeners();
        }

        public override void Dispose()
        {
            RemoveEventListeners();
            base.Dispose();
        }
    }
}