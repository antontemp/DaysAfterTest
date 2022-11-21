using System;
using Script.Characters.Enemy;
using UnityEngine;

namespace Script.Data
{
    [Serializable]
    public class EnemyDataBase
    {
        public CharacterType Type; 
        public EnemySettings Settings;
        public Vector3 Position;
        public Vector3 Rotation;
    }
}