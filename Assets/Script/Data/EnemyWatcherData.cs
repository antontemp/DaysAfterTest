using System;
using UnityEngine;

namespace Script.Data
{
    [Serializable]
    public class EnemyWatcherData : EnemyDataBase
    {
        public Vector3 Point;
        public int TargetId;
    }
}