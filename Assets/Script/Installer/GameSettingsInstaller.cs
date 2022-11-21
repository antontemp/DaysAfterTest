using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Script.Installer
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [FormerlySerializedAs("Prefabs")] [SerializeField]
        private Prefabs prefabs;
        [SerializeField]
        private ControllerSettings _controllerSettings;
        [SerializeField]
        private LevelSettings _levelSettings;
 
        public override void InstallBindings()
        {
            Container.BindInstance(prefabs);
            Container.BindInstance(_controllerSettings);
            Container.BindInstance(_levelSettings);
        }
    }
}