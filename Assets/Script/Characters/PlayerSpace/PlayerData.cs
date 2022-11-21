using System;
using UnityEngine;

namespace Script.Characters.PlayerSpace
{
    [Serializable]
    public class PlayerData
    {
        public PlayerSettings Settings;
        public Vector3 Position;
        public Vector3 Rotation;
        public int Id;
        public bool IsActive;
    }
}