using System;
using UnityEngine;

namespace Script.Data
{
    [Serializable]
    public class FireBulletData
    {
        public int Damage;
        public Vector3 FirstPosition;
        public Vector3 LastPosition;
        public float StartTime;
        public bool IsActive;

        public float MovingTime;
    }
}