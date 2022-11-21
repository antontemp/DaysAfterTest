using Script.Core;
using Script.Core.Implementation;
using Script.Installer;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Script
{
    public class MainInstaller : MonoInstaller
    {
        [FormerlySerializedAs("GameSettings")] [SerializeField]
        private GameSettings _gameSettings;

        [FormerlySerializedAs("preLoaderSettings")]
        [FormerlySerializedAs("preLoader")]
        [FormerlySerializedAs("PreLoader")]
        [SerializeField]
        private Boot boot;


        public override void InstallBindings()
        {
            Container
                .BindInstance(_gameSettings)
                .AsSingle();

            Container
                .Bind<IBoot>()
                .FromComponentInNewPrefab(boot)
                .AsSingle();
        }
    }
}