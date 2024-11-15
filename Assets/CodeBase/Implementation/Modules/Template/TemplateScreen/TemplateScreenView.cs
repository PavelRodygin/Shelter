using CodeBase.Core.MVVM;

namespace CodeBase.Implementation.Modules.Template.TemplateScreen
{
    public class TemplateScreenView : BaseScreenView
    {
        public void SetupEventListeners()
        {
          
        }

        private void RemoveEventListeners()
        {
            
        }

        public override void Dispose()
        {
            RemoveEventListeners();
            base.Dispose();
        }
    }
}