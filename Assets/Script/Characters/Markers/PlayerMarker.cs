using Script.Characters.Enemy;

namespace Script.Characters.Markers
{
    public class PlayerMarker : MarkerBase
    {
        private void Awake()
        {
            CurrentCharacterType = CharacterType.Player;
            CharacterRegistration.Add(this);
        }
    }
}