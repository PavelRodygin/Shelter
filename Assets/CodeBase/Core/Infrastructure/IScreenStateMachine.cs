using CodeBase.Core.MVVM;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure
{
    public interface  IScreenStateMachine
    {
        public IScreenViewModel CurrentViewModel { get; }
        public ScreenViewModelMap? CurrentMapScreen { get; }

        UniTaskVoid RunScreen(ScreenViewModelMap mapScreenViewModel, object param = null);
    }
    
    public static class ScreenControllerExtension
    {
        public static UniTaskVoid RunController(this IScreenStateMachine self, ScreenViewModelMap screenViewModelMap)
        {
            return self.RunScreen(screenViewModelMap, null);
        }
    }
        
    public enum ScreenViewModelMap
    {
        Bootstrap,
        MainMenu,
        Instructions,
        Settings,
        PrivacyPolicy,
        AddonList,
        CategoryAddonList,  //SERVER_ENABLED
        FavoriteAddonList,  //SERVER_ENABLED
        AddonPage,
        OpenAddonPage,
    }
}