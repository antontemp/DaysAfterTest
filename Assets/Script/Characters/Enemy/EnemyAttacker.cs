using System.Collections.Generic;
using System.Linq;
using Script.Bullet;
using Script.Characters.PlayerSpace;
using Script.Data;
using Script.Tools;
using UnityEngine;
using Zenject;

namespace Script.Characters.Enemy
{
    public class EnemyAttacker : EnemyBase
    {
        public float reloadTime;
        private SphereCollider _collider;
        private List<Player> _targets;
        private TimeWatcher _timeWatcher;
        private FireBullet.Factory _fireBulletFactory;
        private Queue<FireBullet> _bullets;


        [Inject]
        private void Construct(FireBullet.Factory factory)
        {
            _fireBulletFactory = factory;
            _timeWatcher = new TimeWatcher(reloadTime);
            _bullets = new Queue<FireBullet>();
        }

        private void Start()
        {
            _collider = GetComponent<SphereCollider>();
            _targets = new List<Player>();
            _collider.radius = settings.attackRange;
        }

        private void Update()
        {
            _timeWatcher.Update(Time.deltaTime);
            if (_targets.Count > 0 && _timeWatcher.IsSpendTime)
            {
                Fire();
                _timeWatcher.ResetTime();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherGameObject = other.gameObject;
            if (otherGameObject.CompareTag("Player"))
            {
                var player = otherGameObject.GetComponent<Player>();
                _targets.Add(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var component = other.gameObject.GetComponent<Player>();
                _targets.Remove(component);
            }
        }

        private void Fire()
        {
            var bullet = _fireBulletFactory.Create();
            bullet.transform.position = transform.position;
            bullet.SetTargetPosition(_targets[0].transform.position, settings.attackRange);

            _bullets.Enqueue(bullet);
            if (!_bullets.Peek().settings.IsActive)
            {
                _bullets.Dequeue();
            }
        }

        private void OnDrawGizmos()
        {
            var tempColor = Gizmos.color;
            Gizmos.color = new Color(1f, 0.0f, 0.0f, 0.2f);
            Gizmos.DrawSphere(transform.position, settings.attackRange);
            Gizmos.color = tempColor;
        }

        public override void CreateDataSnapshot(GameData gameData)
        {
            var data = new EnemyAttackerData()
            {
                Settings = settings,
                Position = transform.position,
                Rotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z),
                PlayerIds = _targets.Select(p => p.Id).ToArray(),
                LeftTime = _timeWatcher.LeftTime,
                FireBullet = _bullets.Select(p=>p.settings).ToArray()
            };
            gameData.Attacker.Add(data);
        }

        public override void ApplyDataSnapshot(object obj)
        {
            if (obj is EnemyAttackerData data)
            {
                settings = data.Settings;
                _targets = data.PlayerIds.Select(id => _mainManager.GetPlayerById(id)).ToList();
                _timeWatcher.StartWithDelay(data.LeftTime);
                foreach (var bulletData in data.FireBullet)
                {
                    if (bulletData.IsActive)
                    {
                        var bullet = _fireBulletFactory.Create();
                        bullet.settings = bulletData;
                        bullet.transform.position = bulletData.FirstPosition;
                        _bullets.Enqueue(bullet);
                    }                    
                } 

            }
        }

        public class Factory : PlaceholderFactory<EnemyAttacker>
        {
        }
    }
}