using System.Collections;
using System.Collections.Generic;
using Script.Characters.Enemy;
using Script.Characters.PlayerSpace;
using Script.Data;
using Unity.VisualScripting;

namespace Script.Core
{
    public interface ISaveService
    {
        IEnumerator Save(int slotId, int levelId, List<Player> players, List<EnemyBase> enemies);
        IEnumerator Load(int slotId);
        GameData GetLastLoadedData();
    }
}