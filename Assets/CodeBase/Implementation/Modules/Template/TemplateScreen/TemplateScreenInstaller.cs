using CodeBase.Core.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Implementation.Modules.Template.TemplateScreen
{
    public class TemplateScreenInstaller : MonoInstaller<TemplateScreenInstaller>
    {
        [Inject] private RootCanvas _rootCanvas;
        [SerializeField] private TemplateScreenView templateScreenView;
        
        public override void InstallBindings()
        {
            Container
                .Bind<TemplateScreenModel>()
                .AsTransient();
            
            Container
                .Bind<TemplateScreenViewModel>()
                .AsTransient();
            
            Container
                .Bind<TemplateScreenView>()
                .FromComponentInNewPrefab(templateScreenView)
                .UnderTransform(_rootCanvas.SaveZoneParent)
                .AsTransient();
        }
    }
}