using Script.Bullet;
using Script.Characters;
using Script.Characters.Enemy;
using Script.Characters.PlayerSpace;
using Script.Core;
using Script.Core.Implementation;
using Script.Enemy;
using Script.InputSpace;
using Script.Level;
using Zenject;

namespace Script.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [Inject] private Prefabs _prefabs;
        [Inject] private ControllerSettings _controllerSettings;
        [Inject] private LevelSettings _levelSettings;

        public override void InstallBindings()
        {
            BindController();
            BindMainManager();
            BindMainUIController();
            BindLoader();
            BindSaveServer();
            BindFactory();
            BindEnemyFactory();
            BindLevel();
        }

        private void BindSaveServer()
        {
            Container
                .Bind<ISaveService>()
                .To<PlayerPrefsService>()
                .AsSingle();
        }

        private void BindMainManager()
        {
            Container
                .Bind<IMainManager>()
                .FromComponentInNewPrefab(_controllerSettings.mainManager)
                .AsSingle();
        }

        private void BindFactory()
        {
            Container.BindFactory<Player, Player.Factory>()
                .FromComponentInNewPrefab(_prefabs.player)
                .AsSingle();
            Container.BindFactory<EnemyMover, EnemyMover.Factory>()
                .FromComponentInNewPrefab(_prefabs.moverEnemy)
                .AsSingle();
            Container.BindFactory<EnemyAttacker, EnemyAttacker.Factory>()
                .FromComponentInNewPrefab(_prefabs.attackerEnemy)
                .AsSingle();
            Container.BindFactory<EnemyWatcher, EnemyWatcher.Factory>()
                .FromComponentInNewPrefab(_prefabs.watcherEnemy)
                .AsSingle();

            Container
                .Bind<ICharacterFactory>()
                .To<CharacterFactory>()
                .AsSingle();
        }

        private void BindLoader()
        {
            Container
                .Bind<ILoader>()
                .FromComponentInNewPrefab(_controllerSettings.loader)
                .AsSingle();
        }

        private void BindMainUIController()
        {
            Container
                .Bind<IMainUIController>()
                .FromComponentInNewPrefab(_controllerSettings.mainUIController)
                .AsSingle();
        }

        private void BindController()
        {
            Container
                .Bind<IInput>()
                .FromComponentInNewPrefab(_controllerSettings.controllerBase)
                .AsSingle();
        }

        private void BindLevel()
        {
            Container.BindFactory<LevelBase, LevelBase.LevelFactory0>()
                .FromComponentInNewPrefab(_levelSettings.level0)
                .AsSingle();
            Container.BindFactory<LevelBase, LevelBase.LevelFactory1>()
                .FromComponentInNewPrefab(_levelSettings.level1)
                .AsSingle();
            Container.BindFactory<LevelBase, LevelBase.LevelFactory2>()
                .FromComponentInNewPrefab(_levelSettings.level2)
                .AsSingle();

            Container
                .Bind<ILevelFactory>()
                .To<LevelFactory>()
                .AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.BindFactory<FireBullet, FireBullet.Factory>()
                .FromComponentInNewPrefab(_prefabs.fireBullet);
        }
    }
}