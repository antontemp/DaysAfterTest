using Script.Characters.Enemy;
using UnityEngine;

namespace Script.Characters
{
  public interface ICharacterFactory
  {
    GameObject Create(CharacterType type, Vector3[] points, Transform transform, object externalData);
  }
}