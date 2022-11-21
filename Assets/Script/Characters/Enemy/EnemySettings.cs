using System;

namespace Script.Characters.Enemy
{
    [Serializable]
    public struct  EnemySettings
    {
        public CharacterType type;
        public int damage;
        public float speed;
        public float attackRange;
    }
}
