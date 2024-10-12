using System;
using System.Collections.Generic;
using Core.Controllers;
using Modules.GameScreen.Scripts;
using UIModules.MainMenu.Scripts;
using Zenject;

namespace StartUp
{
    public class ScreenTypeMapper
    {
        private readonly Dictionary<ControllerMap, Type> _map;
        private readonly DiContainer _diContainer;

        public ScreenTypeMapper(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _map = new Dictionary<ControllerMap, Type>
            {
                { ControllerMap.MainMenu, typeof(MainMenuController)},
                { ControllerMap.GameScreen, typeof(GameScreenController)}
            };
        }

        public IController Resolve(ControllerMap controllerMap)
        {
            return (IController)_diContainer.Resolve(_map[controllerMap]);
        }
    }
}