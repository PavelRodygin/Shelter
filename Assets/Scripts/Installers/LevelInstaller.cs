using System;
using Doors;
using GameScripts.Level;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Level levelPrefab;
        public override void InstallBindings()
        {
            Container.Bind<Level>().FromInstance(levelPrefab).AsSingle();
        }
    }
}