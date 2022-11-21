using Script.Characters.Enemy;

namespace Script.Characters.Markers
{
    public class EnemyAttackerMarker : MarkerBase
    {
        private void Awake()
        {
            CurrentCharacterType = CharacterType.Attacker;
            CharacterRegistration.Add(this);
        }
    }
}