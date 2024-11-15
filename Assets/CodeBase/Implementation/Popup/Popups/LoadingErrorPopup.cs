using CodeBase.Core.UI.Popup;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Implementation.Popup.Popups
{
    public class LoadingErrorPopup : BasePopup
    {
        [SerializeField] private Button retryButton;
        
        protected virtual void Awake() =>
            retryButton.onClick.AddListener(() => Close().Forget());

        public override UniTask Open<T>(T param)
        {
            if (!(param is UnityAction onRetryButtonClicked)) return base.Open(param);

            retryButton.onClick.AddListener(onRetryButtonClicked);

            return base.Open(param);
        }

        public override UniTask Close()
        {
            retryButton.onClick.RemoveAllListeners();
            return base.Close();
        }
    }
}