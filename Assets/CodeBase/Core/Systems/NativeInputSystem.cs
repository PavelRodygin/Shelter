using CodeBase.Core.Infrastructure;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Systems
{
    public class NativeInputSystem : MonoBehaviour
    {
        [Inject] private IScreenStateMachine _screenStateMachine;
        
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            var currentScreen = _screenStateMachine.CurrentViewModel;
            if (currentScreen is { CanExit: true })
            {
                currentScreen.NativeExit();
            }
        }
    }
}