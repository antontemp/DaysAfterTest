using Script.Characters.Enemy;

namespace Script.Characters.Markers
{
    public class EnemyMoverMarker : MarkerBase
    {
        private void Awake()
        {
            CurrentCharacterType = CharacterType.Mover;
            CharacterRegistration.Add(this);
        }
    }
}