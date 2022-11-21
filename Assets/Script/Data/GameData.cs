using System;
using System.Collections.Generic;
using Script.Characters.PlayerSpace;

namespace Script.Data
{
    [Serializable]
    public class GameData
    {
        public int LevelId;
        public List<PlayerData> Players = new List<PlayerData>();
        public List<EnemyAttackerData> Attacker = new List<EnemyAttackerData>();
        public List<EnemyMoverData> Mover = new List<EnemyMoverData>();
        public List<EnemyWatcherData> Watcher = new List<EnemyWatcherData>();
    }
}