using UnityEngine;
using Zenject;

namespace CodeBase.Core.UI.Popup
{
    public interface IBasePopupFactory<out T> : IFactory<Transform, T> where T : BasePopup { }

    public class BasePopupFactory<T> : IBasePopupFactory<T> where T : BasePopup
    {
        private readonly DiContainer _diContainer;
        private readonly BasePopup _basePopupFactoryPrefab;
        
        protected BasePopupFactory(DiContainer diContainer, BasePopup basePopupFactoryPrefab)
        {
            _diContainer = diContainer;
            _basePopupFactoryPrefab = basePopupFactoryPrefab;
        }
        
        public T Create(Transform param)
        {
            var popup = _diContainer.InstantiatePrefabForComponent<T>(_basePopupFactoryPrefab, param);
            popup.gameObject.SetActive(false);
            return popup;
        }
    }
}