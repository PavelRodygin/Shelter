using CodeBase.Core.UI.Popup;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace CodeBase.Implementation.Popup.Popups
{
    public class LoadingQuitDialogPopup : BasePopup
    {
        public Button closePageButton;
        public Button closeButton;
        
        public bool PlayerWantsToQuit { get; private set; }

        private void Awake()
        {
            closeButton.onClick.AddListener(() => Close().Forget());
            closePageButton.onClick.AddListener(IndicateClosePage);
        }
        
        private void IndicateClosePage() => OnCloseDecision(true);

        private void OnCloseDecision(bool wasClosed)
        {
            PlayerWantsToQuit = wasClosed;
            Close().Forget();
        }
    }
}