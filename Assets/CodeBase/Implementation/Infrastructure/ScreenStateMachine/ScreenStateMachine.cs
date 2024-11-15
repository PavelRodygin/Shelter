using System.Threading;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.MVVM;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Implementation.Infrastructure
{
    public class ScreenStateMachine : MonoBehaviour, IScreenStateMachine
    {
        [Inject] private ScreenTypeMapper _screenTypeMapper;
        
        // SemaphoreSlim to ensure only one thread can execute the RunPresenter method at a time
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        
        public IScreenViewModel CurrentViewModel { get; private set; }
        public ScreenViewModelMap? CurrentMapScreen { get; private set; } = null;

        private void Start() => RunScreen(ScreenViewModelMap.Bootstrap).Forget();
        
        //TODO Add Execute(), following the StateMachine pattern
        public async UniTaskVoid RunScreen(ScreenViewModelMap mapScreenViewModel, object param = null)
        {
            // Wait until the semaphore is available (only one thread can pass)
            await _semaphoreSlim.WaitAsync();
            
            try
            {
                if (mapScreenViewModel != CurrentMapScreen)
                {
                    CurrentMapScreen = mapScreenViewModel;
                    CurrentViewModel = _screenTypeMapper.Resolve(mapScreenViewModel);
                    await CurrentViewModel.Enter(param);
                    await CurrentViewModel.Exit();
                    CurrentViewModel.Dispose();
                }
                else
                    Debug.LogWarning("This screen has already been entered.");
            }
            finally
            {
                // Release the semaphore so another thread can enter the RunPresenter method
                _semaphoreSlim.Release();
            }
        }
    }
}