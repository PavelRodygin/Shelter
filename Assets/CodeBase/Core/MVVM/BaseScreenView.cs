using CodeBase.Core.UI.Views.Animations;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Core.MVVM
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseScreenView : MonoBehaviour, IView
    {
        [SerializeField] private BaseAnimationElement animationElement;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public virtual async UniTask Show()
        {
            SetActive(true);
            
            //Растягиваем по SaveZone TODO
            RectTransform rectTransform = GetComponent<RectTransform>();

            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            
            if (animationElement != null) await animationElement.Show();
        }

        public virtual async UniTask Hide()
        {
            if (animationElement != null) await animationElement.Hide();
            SetActive(false);
        }
        
        protected void SetActive(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1 : 0;
            _canvasGroup.blocksRaycasts = isActive;
            _canvasGroup.interactable = isActive;
            
            gameObject.SetActive(isActive);
        }
        
        public void HideInstantly() => gameObject.SetActive(false);

        public virtual void Dispose() => Destroy(gameObject);
    }
}