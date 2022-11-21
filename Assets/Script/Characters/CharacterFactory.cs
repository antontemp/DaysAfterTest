using System;
using Script.Characters.Enemy;
using Script.Characters.PlayerSpace;
using Script.Enemy;
using UnityEngine;

namespace Script.Characters
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly EnemyMover.Factory _moverFactory;
        private readonly EnemyAttacker.Factory _attackerFactory;
        private readonly EnemyWatcher.Factory _watcherFactory;
        private readonly Player.Factory _playerFactory;


        CharacterFactory(EnemyMover.Factory moverFactory, EnemyAttacker.Factory attackerFactory,
            EnemyWatcher.Factory watcherFactory, Player.Factory playerFactory)
        {
            _moverFactory = moverFactory;
            _attackerFactory = attackerFactory;
            _watcherFactory = watcherFactory;
            _playerFactory = playerFactory;
        }

        public GameObject Create(CharacterType type, Vector3[] points, Transform transform,  object externalData)
        {
            switch (type)
            {
                case CharacterType.Mover:
                    var mover = _moverFactory.Create();
                    mover.transform.position = transform.position;
                    mover.points = points;
                    mover.ApplyDataSnapshot(externalData);
                    return mover.gameObject;
                case CharacterType.Watcher:
                    var watcher = _watcherFactory.Create();
                    watcher.transform.position = transform.position;
                    watcher.point = points[0];
                    watcher.ApplyDataSnapshot(externalData);
                    return watcher.gameObject;
                case CharacterType.Attacker:
                    var attacker = _attackerFactory.Create();
                    attacker.transform.position = transform.position;
                    attacker.ApplyDataSnapshot(externalData);
                    return attacker.gameObject;
                case CharacterType.Player:
                    var player = _playerFactory.Create();
                    player.transform.position = transform.position;
                    player.ApplyDataSnapshot(externalData);
                    return player.gameObject;
                default:
                    Debug.LogError("Invalid type of Enemy " + type);
                    break;
            }

            throw new Exception("Invalid type of Enemy " + type);
        }
    }
}