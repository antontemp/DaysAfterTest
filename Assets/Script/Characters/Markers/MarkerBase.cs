using System.Collections.Generic;
using System.Linq;
using Script.Characters.Enemy;
using Script.Core;
using UnityEngine;
using Zenject;

namespace Script.Characters.Markers
{
    public class MarkerBase : MonoBehaviour, ICharacterMarker
    {
        [SerializeField] protected List<Transform> points;
        protected ICharacterRegistration CharacterRegistration;
        protected CharacterType CurrentCharacterType;
        public Transform Transform => transform;

        [Inject]
        private void Construct(ILoader loader)
        {
            CharacterRegistration = loader;
        }


        public virtual CharacterType GetCharacterType()
        {
            return CurrentCharacterType;
        }

        public virtual Vector3[] GetPoints()
        {
            return points.Select(p => p.position).ToArray();
        }
    }
}