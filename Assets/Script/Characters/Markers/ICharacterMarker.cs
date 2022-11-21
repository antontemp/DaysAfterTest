using Script.Characters.Enemy;
using UnityEngine;

namespace Script.Characters.Markers
{
    public interface ICharacterMarker
    {
        Transform Transform { get; }
        CharacterType GetCharacterType();
        Vector3[] GetPoints();
    }
}