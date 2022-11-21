using Script.Characters.Enemy;
using Script.Characters.Enemy.AI;
using Script.Data;
using UnityEngine;
using Zenject;

namespace Script.Enemy
{
    public class EnemyMover : EnemyBase
    {
        public Vector3[] points;

        private MoverLogic _logic;

        private void Start()
        {
            if (_logic == null)
            {
                CreateLogic(0);
            }
        }

        private void CreateLogic(int index)
        {
            _logic = new MoverLogic(points, index);
        }

        private void Update()
        {
            var position = transform.position;
            transform.position = position +
                                 _logic.PositionUpdate(position) * (Time.deltaTime * settings.speed);
        }

        public override void CreateDataSnapshot(GameData gameData)
        {
            var data = new EnemyMoverData()
            {
                Settings = settings,
                Position = transform.position,
                Rotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z),
                Points = points,
                CurrentActivePoint = _logic.CurrentActivePoint
            };
            gameData.Mover.Add(data);
        }

        public override void ApplyDataSnapshot(object obj)
        {
            if (obj is EnemyMoverData data)
            {
                points = data.Points;
                settings = data.Settings;
                CreateLogic(data.CurrentActivePoint);
            }
        }

        public class Factory : PlaceholderFactory<EnemyMover>
        {
        }
    }
}