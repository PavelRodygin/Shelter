using System;
using System.Collections.Generic;
using CodeBase.Core.UI;
using CodeBase.Core.UI.Popup;
using CodeBase.Implementation.Popup.PopupFactories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Systems
{
    public class PopupHub : MonoBehaviour
    {
        [NonSerialized] public BasePopup CurrentPopup;
        
        [Inject] private LoadingQuitDialogPopupFactory _loadingQuitDialogPopupFactory;
        [Inject] private AdsDialogBoxPopupFactory _adsDialogBoxPopupFactory;
        [Inject] private AddonVersionsPopupFactory _addonVersionsPopupFactory;
        [Inject] private LoadingErrorPopupFactory _loadingErrorPopupFactory;
        [Inject] private RootCanvas _rootCanvas;
        
        // private readonly PopupsPriorityQueue _popupQueue = new PopupsPriorityQueue(); 
        private readonly Stack<BasePopup> _popups = new Stack<BasePopup>();

        public async UniTask OpenPopup<T>(IFactory<Transform, BasePopup> basePopupFactory, T param = default)
        {
            CurrentPopup = basePopupFactory.Create(_rootCanvas.transform);
            _popups.Push(CurrentPopup);
            CurrentPopup.gameObject.SetActive(true);
            await CurrentPopup.Open(param);
        }

        public async UniTask CloseCurrentPopup()
        {
            if (_popups.Count > 0)
            {
                CurrentPopup = _popups?.Pop();
                if (CurrentPopup != null) 
                    await CurrentPopup.Close();
            }
        }

        public async UniTask OpenAddonVersionsPopup(string versions) => 
            await OpenPopup<dynamic>(_addonVersionsPopupFactory, versions);
        public async UniTask OpenLoadingErrorPopup() => await OpenPopup<dynamic>(_loadingErrorPopupFactory);
        public async UniTask OpenLoadingQuitDialogPopup() => await OpenPopup<dynamic>(_loadingQuitDialogPopupFactory);
        public async UniTask OpenAdsDialogBoxPopup() => await OpenPopup<dynamic>(_adsDialogBoxPopupFactory);
        //  Пример работы с параметрами
        // public async UniTask OpenTooltipPopup(ItemData itemData) => await OpenPopup(_tooltipPopupFactory, itemData); 
        
        private void OpenCurrentPopup<T>(T param) 
        {
            _popups.Push(CurrentPopup);
            CurrentPopup.gameObject.SetActive(true);
            if (CurrentPopup != null) CurrentPopup.Open<T>(param);
        }
    }
}