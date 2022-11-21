using Script.Characters.Markers;

namespace Script.Core
{
    public interface ICharacterRegistration
    {
        void Add(ICharacterMarker characterMarker);
    }
}