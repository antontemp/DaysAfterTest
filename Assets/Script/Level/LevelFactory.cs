using System.Collections.Generic;
using Zenject;

namespace Script.Level
{
    public class LevelFactory : ILevelFactory
    {
        private readonly Dictionary<int, PlaceholderFactory<LevelBase>> _factories;

        LevelFactory(LevelBase.LevelFactory0 factory0,
            LevelBase.LevelFactory1 factory1,
            LevelBase.LevelFactory2 factory2)
        {
            _factories = new Dictionary<int, PlaceholderFactory<LevelBase>>()
                { { 0, factory0 }, { 1, factory1 }, { 2, factory2 } };
        }

        public void Create(int id)
        {
            _factories[id].Create();
        }
    }
}