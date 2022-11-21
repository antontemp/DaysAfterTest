using System.Collections.Generic;
using Script.Characters;
using Script.Characters.Enemy;
using Script.Characters.Markers;
using Script.Data;
using Script.Level;
using UnityEngine;
using Zenject;

namespace Script.Core.Implementation
{
    public class Loader : MonoBehaviour, ILoader
    {
        private readonly List<ICharacterMarker> _characterList = new List<ICharacterMarker>();
        private ICharacterFactory _factory;
        private ILevelFactory _levelFactory;
        private IBoot _boot;

        [Inject]
        private void Construct(IBoot boot, ICharacterFactory factory, ILevelFactory levelFactory)
        {
            _boot = boot;
            _factory = factory;
            _levelFactory = levelFactory;
        }

        void Start()
        {
            CreateLevel();
            if (_boot.IsLoadByLevelId)
            {
                CreateCharacter();
            }
            else
            {
                CreateCharacterByData(_boot.GameData);
            }
        }

        private void CreateLevel()
        {
            _levelFactory.Create(_boot.Settings.StartLevelindex);
        }

        private void CreateCharacterByData(GameData bootGameData)
        {
            var vector = new Vector3[] { };
            foreach (var character in bootGameData.Players)
            {
                var obj = _factory.Create(CharacterType.Player, vector, transform, character);
                obj.transform.position = character.Position;
                obj.transform.rotation =
                    Quaternion.Euler(character.Rotation.x, character.Rotation.y, character.Rotation.z);
            }

            foreach (var enemy in bootGameData.Attacker)
            {
                var obj = _factory.Create(CharacterType.Attacker, vector, transform, enemy);
                obj.transform.position = enemy.Position;
                obj.transform.rotation = Quaternion.Euler(enemy.Rotation.x, enemy.Rotation.y, enemy.Rotation.z);
            }

            foreach (var enemy in bootGameData.Mover)
            {
                var obj = _factory.Create(CharacterType.Mover, vector, transform, enemy);
                obj.transform.position = enemy.Position;
                obj.transform.rotation = Quaternion.Euler(enemy.Rotation.x, enemy.Rotation.y, enemy.Rotation.z);
            }

            foreach (var enemy in bootGameData.Watcher)
            {
                var obj = _factory.Create(CharacterType.Watcher, new[] { enemy.Point }, transform, enemy);
                obj.transform.position = enemy.Position;
                obj.transform.rotation = Quaternion.Euler(enemy.Rotation.x, enemy.Rotation.y, enemy.Rotation.z);
            }
        }

        private void CreateCharacter()
        {
            foreach (var character in _characterList)
            {
                var characterType = character.GetCharacterType();
                _factory.Create(characterType, character.GetPoints(), character.Transform, null);
            }
        }

        public void Add(ICharacterMarker characterMarker)
        {
            _characterList.Add(characterMarker);
        }
    }
}