using Script.Characters.PlayerSpace;
using Script.Core;
using Script.Data;
using UnityEngine;
using Zenject;

namespace Script.Characters.Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField]
        protected EnemySettings settings;

        protected IMainManager _mainManager;

        [Inject]
        private void Construct(IMainManager mainManager)
        {
            _mainManager = mainManager;
            _mainManager.AddEnemy(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var player = collision.gameObject.GetComponent<Player>();
                _mainManager.Shot(player, settings.damage);
                Destroy();
            }
        }

        protected void Destroy()
        {
            _mainManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
        
        public virtual void CreateDataSnapshot(GameData gameData)
        {
        }

        public virtual void ApplyDataSnapshot(object obj)
        {
            
        }

    }
}

