using System;
using System.Collections;
using System.Collections.Generic;
using Script.Characters.Enemy;
using Script.Characters.PlayerSpace;
using Script.Data;
using UnityEngine;

namespace Script.Core.Implementation
{
    public class PlayerPrefsService : ISaveService
    {
        private const string SlotNameTemplate = "Slot_{0}";
        private GameData _lastLoadData;

        public IEnumerator Load(int slotId)
        {
            var key = String.Format(SlotNameTemplate, slotId);
            _lastLoadData = null;
            if (PlayerPrefs.HasKey(key))
            {
                var dataJson = PlayerPrefs.GetString(key);
                _lastLoadData = JsonUtility.FromJson<GameData>(dataJson);
            }

            yield break;
        }

        public GameData GetLastLoadedData()
        {
            return _lastLoadData;
        }

        public IEnumerator Save(int slotId, int levelId, List<Player> players, List<EnemyBase> enemies)
        {
            var dataSnapshot = new GameData()
            {
                LevelId = levelId
            };

            SavePlayers(dataSnapshot, players);
            SaveEnemies(dataSnapshot, enemies);

            var dataJson = JsonUtility.ToJson(dataSnapshot);
            PlayerPrefs.SetString(String.Format(SlotNameTemplate, slotId), dataJson);
            PlayerPrefs.Save();
            yield break;
        }

        private void SaveEnemies(GameData gameData, List<EnemyBase> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.CreateDataSnapshot(gameData);
            }
        }

        private void SavePlayers(GameData gameData, List<Player> players)
        {
            foreach (var player in players)
            {
                player.CreateDataSnapshot(gameData);
            }
        }
    }
}