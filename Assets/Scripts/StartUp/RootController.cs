using System.Threading;
using Core.Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace StartUp
{
    public class RootController : MonoBehaviour, IRootController
    {
        [Inject] private ScreenTypeMapper _screenTypeMapper;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        private IController _currentController;

        private void Start()
        {
            Application.targetFrameRate = 30;
            RunController(ControllerMap.MainMenu, null).Forget();
        }

        public async UniTaskVoid RunController(ControllerMap controllerMap, object param)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                _currentController = _screenTypeMapper.Resolve(controllerMap);
                await _currentController.Run(param);
                await _currentController.Stop();
                _currentController.Dispose();
            }
            finally { _semaphoreSlim.Release(); }
        }
    }
}