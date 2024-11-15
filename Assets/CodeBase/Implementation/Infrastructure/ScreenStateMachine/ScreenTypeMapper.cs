using System;
using System.Collections.Generic;
using CodeBase.Core.Infrastructure;
using CodeBase.Core.MVVM;
using CodeBase.Implementation.Modules.Base.MainMenu;
using CodeBase.Implementation.Modules.Base.PrivacyPolicy;
using CodeBase.Implementation.Modules.Base.StartScreen.Scripts;
using Zenject;

namespace CodeBase.Implementation.Infrastructure
    {
        public class ScreenTypeMapper
        {
            private readonly Dictionary<ScreenViewModelMap, Type> _map;
            private readonly DiContainer _diContainer;
            
            public ScreenTypeMapper(DiContainer diContainer)
            {
                _diContainer = diContainer;
                _map = new Dictionary<ScreenViewModelMap, Type>
                {
                    { ScreenViewModelMap.Bootstrap, typeof(StartGameScreenViewModel)},
                    { ScreenViewModelMap.MainMenu, typeof(MainMenuScreenViewModel)},
                    { ScreenViewModelMap.PrivacyPolicy, typeof(PrivacyPolicyScreenViewModel)},
                };
            }

            public IScreenViewModel Resolve(ScreenViewModelMap screenViewModelMap)
            {
                return (IScreenViewModel)_diContainer.Resolve(_map[screenViewModelMap]);
            }
        }
    }