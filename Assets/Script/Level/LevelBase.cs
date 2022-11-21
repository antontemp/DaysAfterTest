using UnityEngine;
using Zenject;

namespace Script.Level
{
    public class LevelBase : MonoBehaviour
    {
        public class LevelFactory0 : PlaceholderFactory<LevelBase>
        {
        }

        public class LevelFactory1 : PlaceholderFactory<LevelBase>
        {
        }

        public class LevelFactory2 : PlaceholderFactory<LevelBase>
        {
        }
    }
}