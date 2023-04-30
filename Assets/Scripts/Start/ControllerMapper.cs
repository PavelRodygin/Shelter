using System;
using System.Collections.Generic;
using Core;
using Core.Controllers;
using UIModules.GameScreen.Scripts;
using UIModules.MainMenu.Scripts;
using UIModules.SettingsScreen.Scripts;
using Zenject;

namespace Start
{
    public class ControllerMapper
    {
        private readonly Dictionary<ControllerMap, Type> _map;
        private readonly DiContainer _diContainer;

        public ControllerMapper(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _map = new Dictionary<ControllerMap, Type>
            {
                { ControllerMap.MainMenu, typeof(MainMenuController)},
                { ControllerMap.Settings, typeof(SettingsController)},
                { ControllerMap.GameScreen, typeof(GameScreenController)},
            };
        }

        public IController Resolve(ControllerMap controllerMap)
        {
            return (IController)_diContainer.Resolve(_map[controllerMap]);
        }
    }
}