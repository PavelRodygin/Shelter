using System.Threading.Tasks;
using CodeBase.Core.UI.Views.Animations;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Core.UI.Popup
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BasePopup : MonoBehaviour
    {
        [Inject] private RootCanvas _rootCanvas;
        [SerializeField] private float bgShowHideTime = 0.25f;
        [SerializeField] private BaseAnimationElement animationElement;
        
        private GameObject _popupBackground;    
        private TaskCompletionSource<bool> _tcs;
        
        public Color backgroundColor = new Color(10.0f / 255.0f, 10.0f / 255.0f, 10.0f / 255.0f, 0.6f);

        public virtual async UniTask Open<T>(T param)
        {
            gameObject.SetActive(true);
            AddBackground();
            await animationElement.Show();
        }

        public virtual async UniTask Close()
        {
            RemoveBackground();
            
            await animationElement.Hide();
            gameObject.SetActive(false);
            
            _tcs?.TrySetResult(true);
            RunPopupDestroy();
        }

        public Task<bool> WaitForCompletion()
        {
            _tcs = new TaskCompletionSource<bool>();
            return _tcs.Task;
        }

        //TODO Рефакторинг
        private void AddBackground()
        {
            var bgTex = new Texture2D(1, 1);
            bgTex.SetPixel(0, 0, backgroundColor);
            bgTex.Apply();

            _popupBackground = new GameObject("PopupBackground");
            var image = _popupBackground.AddComponent<Image>();
            var rect = new Rect(0, 0, bgTex.width, bgTex.height);
            var sprite = Sprite.Create(bgTex, rect, new Vector2(0.5f, 0.5f), 1);
            image.material.mainTexture = bgTex;                                                                 
            image.sprite = sprite;
            var newColor = image.color;
            image.color = newColor;
            image.canvasRenderer.SetAlpha(0.0f);
            image.CrossFadeAlpha(1.0f, bgShowHideTime, false);

            _popupBackground.transform.localScale = new Vector3(1, 1, 1);
            _popupBackground.GetComponent<RectTransform>().sizeDelta = _rootCanvas.GetComponent<RectTransform>().sizeDelta;
            _popupBackground.transform.SetParent(_rootCanvas.transform, false);
            _popupBackground.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }

        private void RemoveBackground()
        {
            var image = _popupBackground.GetComponent<Image>();
            if (image != null)
                image.CrossFadeAlpha(0.0f, bgShowHideTime, false);
        }

        private void RunPopupDestroy()
        {
            Destroy(_popupBackground);
            Destroy(gameObject);
        }
    }
}