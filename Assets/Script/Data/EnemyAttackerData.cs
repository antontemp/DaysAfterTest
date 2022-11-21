using System;

namespace Script.Data
{
    [Serializable]
    public class EnemyAttackerData : EnemyDataBase
    {
        public int[] PlayerIds;
        public float LeftTime;
        public FireBulletData[] FireBullet;
    }
}