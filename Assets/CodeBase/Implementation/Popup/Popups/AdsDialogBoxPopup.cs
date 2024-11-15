using CodeBase.Core.UI.Popup;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace CodeBase.Implementation.Popup.Popups
{
    public class AdsDialogBoxPopup : BasePopup
    {
        public Button watchAdsButton;
        public Button closeButton;
        
        public bool WasAdFullyWatched { get; private set; }

        private void Awake()
        {
            closeButton.onClick.AddListener(() => Close().Forget());
            watchAdsButton.onClick.AddListener(ShowAd);
        }
        
        private void ShowAd()
        {
            // AdsController.Instance.ShowRewardAd(result => OnAdWatched(true));
        }

        private void OnAdWatched(bool wasWatched)
        {
            WasAdFullyWatched = wasWatched;
            Close().Forget();
        }
    }
}