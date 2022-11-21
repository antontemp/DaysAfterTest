using System;
using UnityEngine;

namespace Script.Data
{
    [Serializable]
    public class EnemyMoverData  : EnemyDataBase
    {
        public Vector3[] Points;
        public int CurrentActivePoint;
    }
}