using Script.Data;
using Script.Installer;

namespace Script.Core
{
    public interface IBoot
    {
        GameData GameData { get; }
        GameSettings Settings { get; }
        bool IsLoadByLevelId { get; }
        void LoadByGameData(GameData gameData);
        void LoadByLevelId(int levelIndex);
        void ReLoad();
    }
}