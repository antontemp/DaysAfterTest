using System;
using Script.Characters.PlayerSpace;
using Script.Core;
using Script.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Script.Bullet
{
    public class FireBullet : BulletBase
    {
        private const float Speed = 2.2f;
        public FireBulletData settings;
        private IMainManager _mainManager;

        [Inject]
        private void Construct(IMainManager mainManager)
        {
            _mainManager = mainManager;
        }

        void Start()
        {
            settings.FirstPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("FireBullet OnTriggerEnter");
                var player = other.gameObject.GetComponent<Player>();
                _mainManager.Shot(player, settings.Damage);
                Destroy();
            }
            
        }
        

        private void Update()
        {
            if (settings.IsActive)
            {
                settings.StartTime += Time.deltaTime;
                var t = settings.StartTime / settings.MovingTime;
                transform.position =
                    Vector3.Lerp(settings.FirstPosition, settings.LastPosition, t);
                if (t > 1)
                {
                    Destroy();
                }
            }
        }

        public override void SetTargetPosition(Vector3 position, float distance)
        {
            var transformPosition = transform.position;
            settings.LastPosition = (position - transformPosition).normalized * (distance * 1.5f) + transformPosition;
            settings.StartTime = 0;
            settings.MovingTime = (distance * 1.5f) / Speed;
            settings.IsActive = true;
        }

        private void Destroy()
        {
            settings.IsActive = false;
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<FireBullet>
        {
        }
    }
}