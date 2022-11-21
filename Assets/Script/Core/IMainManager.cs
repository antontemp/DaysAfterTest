using Script.Characters.Enemy;
using Script.Characters.PlayerSpace;

namespace Script.Core
{
    public interface IMainManager
    {
        Player ActivePlayer { get; }
        Player GetPlayerById(int id);
        void Shot(Player target, int damage);
        void AddPlayer(Player player);
        void AddEnemy(EnemyBase enemyBase);
        void RemoveEnemy(EnemyBase enemyBase);
        void RemovePlayer(Player player);
        void UpdatedFinishingAmount(int playersCount);
    }
}