using System;
using Script.Characters.Enemy.AI;
using Script.Characters.PlayerSpace;
using Script.Data;
using UnityEngine;
using Zenject;

namespace Script.Characters.Enemy
{
    public class EnemyWatcher : EnemyBase
    {
        public float angle;
        public Vector3 point;

        private WatcherLogic _logic;
        private SphereCollider _collider;
        private Player _playerTarget;

        private void Start()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.radius = settings.attackRange;
            if (_logic == null)
            {
                CreateLogic(null);
            }
        }

        private void CreateLogic(Transform transform)
        {
            _logic = new WatcherLogic(transform);
        }

        private void Update()
        {
            var position = transform.position;
            transform.position = position +
                                 _logic.PositionUpdate(position) * (Time.deltaTime * settings.speed);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_logic.HasTarget() && other.gameObject.CompareTag("Player"))
            {
                var position = transform.position;
                var vectorA = other.gameObject.transform.position - transform.position;
                var vectorB = point - transform.position;
                var currentAngle = Math.Abs(Vector3.Angle(vectorA, vectorB));
                if (currentAngle <= angle / 2)
                {
                    _playerTarget = other.gameObject.GetComponent<Player>();
                    Debug.Log($"EnemyWatcher OnTriggerEnter angle:{currentAngle}");
                    _logic.SetTransform(other.gameObject.transform);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_logic.EqualsTarget(other.gameObject.transform))
                {
                    _playerTarget = null;
                    _logic.Interrupt();
                }
            }
        }

        private void OnDrawGizmos()
        {
            var tempColor = Gizmos.color;
            Gizmos.color = new Color(1f, 0.0f, 0.0f, 0.2f);
            var position = transform.position;
            var direction = (point - position).normalized;
            var limitA = Quaternion.AngleAxis(-angle / 2, Vector3.up) * direction;
            var limitB = Quaternion.AngleAxis(angle / 2, Vector3.up) * direction;
            Gizmos.DrawRay(position, direction * settings.attackRange);
            Gizmos.DrawRay(position, limitA * settings.attackRange);
            Gizmos.DrawRay(position, limitB * settings.attackRange);
            Gizmos.color = tempColor;
        }

        public override void CreateDataSnapshot(GameData gameData)
        {
            var data = new EnemyWatcherData()
            {
                Settings = settings,
                Position = transform.position,
                Rotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z),
                Point = point,
                TargetId = _playerTarget == null ? -1 : _playerTarget.Id
            };
            gameData.Watcher.Add(data);
        }

        public override void ApplyDataSnapshot(object obj)
        {
            if (obj is EnemyWatcherData data)
            {
                settings = data.Settings;
                point = data.Point;
                if (data.TargetId != -1)
                {
                    _playerTarget = _mainManager.GetPlayerById(data.TargetId);
                    CreateLogic(_playerTarget.transform);
                }
            }
        }

        public class Factory : PlaceholderFactory<EnemyWatcher>
        {
        }
    }
}