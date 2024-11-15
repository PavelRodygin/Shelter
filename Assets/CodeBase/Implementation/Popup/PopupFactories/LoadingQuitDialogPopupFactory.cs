using CodeBase.Core.UI.Popup;
using CodeBase.Implementation.Popup.Popups;
using Zenject;

namespace CodeBase.Implementation.Popup.PopupFactories
{
    public class LoadingQuitDialogPopupFactory : BasePopupFactory<LoadingQuitDialogPopup>
    {
        protected LoadingQuitDialogPopupFactory(DiContainer diContainer, LoadingQuitDialogPopup basePopupFactoryPrefab) 
            : base(diContainer, basePopupFactoryPrefab)
        {
            
        }
    }
}