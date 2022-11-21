using UnityEngine;

namespace Script.Characters.Enemy.AI
{
    public class WatcherLogic : IEnemyLogic
    {
        private Transform _targetTransform;

        public WatcherLogic(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }

        public Vector3 PositionUpdate(Vector3 position)
        {
            if (!_targetTransform)
            {
                return Vector3.zero;
            }

            var vector = _targetTransform.position - position;
            return vector.normalized;
        }

        public bool HasTarget()
        {
            return _targetTransform != null;
        }

        public bool EqualsTarget(Transform transform)
        {
            return _targetTransform == transform;
        }

        public void SetTransform(Transform transform)
        {
            _targetTransform = transform;
        }

        public void Interrupt()
        {
            _targetTransform = null;
        }
    }
}