using CodeBase.Core.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Implementation.Modules.Base.PrivacyPolicy
{
    public class PrivacyPolicyScreenInstaller : MonoInstaller<PrivacyPolicyScreenInstaller>
    {
        [Inject] private RootCanvas _rootCanvas;
        [SerializeField] private PrivacyPolicyScreenView privacyPolicyScreenView;
        
        public override void InstallBindings()
        {
            Container
                .Bind<PrivacyPolicyScreenViewModel>()
                .AsTransient();
            
            Container
                .Bind<PrivacyPolicyScreenView>()
                .FromComponentInNewPrefab(privacyPolicyScreenView)
                .UnderTransform(_rootCanvas.SaveZoneParent)
                .AsTransient();
        }
    }
}