using UnityEngine;

namespace Script.Characters.Enemy.AI
{
    public class MoverLogic : IEnemyLogic
    {
        public int CurrentActivePoint;
        private const float Epsilon = 0.0001f;
        private readonly Vector3[] _points;

        public MoverLogic(Vector3[] points, int index)
        {
            _points = points;
            CurrentActivePoint = index;
        }

        public Vector3 PositionUpdate(Vector3 position)
        {
            if (_points.Length == 0)
            {
                return Vector3.zero;
            }

            var vector = _points[CurrentActivePoint] - position;
            if (vector.magnitude <= Epsilon)
            {
                CurrentActivePoint++;
                if (CurrentActivePoint >= _points.Length) CurrentActivePoint = 0;
            }

            return vector.normalized;
        }
    }
}