using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.Core.MVVM;
using CodeBase.Core.Systems;
using CodeBase.Services;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core.Enums;

namespace CodeBase.Implementation.Modules.Base.StartScreen.Scripts
{
    //It is Bootstrap state at the moment
    //TODO Однозначно рефакторинг системы загрузки
    public class StartGameScreenModel : IScreenModel
    {
        private readonly Dictionary<string, Func<Task>> _commands;
        private readonly TestLongInitializationService _serviceForExtraDelay;
        // private int _currentTooltipIndex;
        
        public event Action<float> OnProgressUpdated;
        
        public StartGameScreenModel(TestLongInitializationService serviceForExtraDelay)
        {
            _serviceForExtraDelay = serviceForExtraDelay;
            _commands = new Dictionary<string, Func<Task>>();
            
            RegisterCommands();
        }

        private void DoTweenInit()
        {
            DOTween.Init().SetCapacity(240, 30);
            DOTween.safeModeLogBehaviour = SafeModeLogBehaviour.None;
            DOTween.defaultAutoKill = true;
            DOTween.defaultRecyclable = true;
            DOTween.useSmoothDeltaTime = true;
        }
        
        private void RegisterCommands()
        {
            _commands.Add("First Service", _serviceForExtraDelay.Init);
            // _commands.Add("AdService", _adService.Init);
            // _commands.Add("EnergyService", _energyBarService.Init);
        }
        
        public async UniTask InitializeAppServices()
        {
            DoTweenInit();

            var timing = 1f / _commands.Count;
            var currentTiming = timing;

            foreach (var initFunction in from command in _commands let serviceName = command.Key select command.Value)
            {
                await initFunction.Invoke();
                
                OnProgressUpdated?.Invoke(currentTiming); 
                currentTiming += timing;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
