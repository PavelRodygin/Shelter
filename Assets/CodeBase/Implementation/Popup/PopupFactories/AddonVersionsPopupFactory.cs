using CodeBase.Core.UI.Popup;
using CodeBase.Implementation.Popup.Popups;
using Zenject;

namespace CodeBase.Implementation.Popup.PopupFactories
{
    public class AddonVersionsPopupFactory : BasePopupFactory<AddonVersionsPopup>
    {
        protected AddonVersionsPopupFactory(DiContainer diContainer, AddonVersionsPopup basePopupFactoryPrefab) 
            : base(diContainer, basePopupFactoryPrefab)
        {
            
        }
    }
}