using CodeBase.Core.UI.Popup;
using CodeBase.Implementation.Popup.Popups;
using Zenject;

namespace CodeBase.Implementation.Popup.PopupFactories
{
    public class AdsDialogBoxPopupFactory : BasePopupFactory<AdsDialogBoxPopup>
    {
        protected AdsDialogBoxPopupFactory(DiContainer diContainer, AdsDialogBoxPopup basePopupFactoryPrefab) 
            : base(diContainer, basePopupFactoryPrefab)
        {
            
        }
    }
}