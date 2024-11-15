using CodeBase.Core.UI.Popup;
using CodeBase.Implementation.Popup.Popups;
using Zenject;

namespace CodeBase.Implementation.Popup.PopupFactories
{
    public class LoadingErrorPopupFactory : BasePopupFactory<LoadingErrorPopup>
    {
        protected LoadingErrorPopupFactory(DiContainer diContainer, LoadingErrorPopup basePopupFactoryPrefab) 
            : base(diContainer, basePopupFactoryPrefab)
        {
            
        }
    }
}