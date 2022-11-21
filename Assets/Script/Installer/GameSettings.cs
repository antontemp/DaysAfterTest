using System;
using UnityEngine.Serialization;

namespace Script.Installer
{
    [Serializable]
    public class GameSettings
    {
        [FormerlySerializedAs("StartLevel")] public int StartLevelindex;
        public int LevelAmount;
    }
}