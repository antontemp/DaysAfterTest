using System.Collections.Generic;
using Script.Characters.Enemy;
using UnityEngine;

namespace Script.Characters.Markers
{
    public class EnemyMarker : MonoBehaviour
    {
        public CharacterType type;
        public List<Transform> points;
    }
}