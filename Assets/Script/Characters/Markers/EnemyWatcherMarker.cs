using Script.Characters.Enemy;

namespace Script.Characters.Markers
{
    public class EnemyWatcherMarker : MarkerBase
    {
        private void Awake()
        {
            CurrentCharacterType = CharacterType.Watcher;
            CharacterRegistration.Add(this);
        }
    }
}