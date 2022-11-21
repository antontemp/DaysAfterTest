using UnityEngine;

namespace Script.Characters.Enemy.AI
{
    public interface IEnemyLogic
    {
        Vector3 PositionUpdate(Vector3 position);
    }
}