using Script.Core;
using Script.Data;
using Script.InputSpace;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Script.Characters.PlayerSpace
{
    public class Player : MonoBehaviour
    {
        [FormerlySerializedAs("PlayerSettings")] public PlayerSettings Settings;

        private IInput _input;
        private IMainManager _mainManager;
        [HideInInspector]
        public bool IsActive;
        [HideInInspector]
        public int Id = -1;

        [Inject]
        private void Construct(IMainManager manager, IInput input)
        {
            _mainManager = manager;
            _input = input;
            _mainManager.AddPlayer(this);
        }

        public void  Active()
        {
            IsActive = true;
            Subscribe();
        }
        public void Deactivate()
        {
            IsActive = false;
            Unsubscribe();
        }
        private void Subscribe()
        {
            _input.OnLeftEvent += OnLeftEvent;
            _input.OnRightEvent += OnRightEvent;
            _input.OnUpEvent += OnUpEvent;
            _input.OnDownEvent += OnDownEvent;
        }

        private void Unsubscribe()
        {
            _input.OnLeftEvent -= OnLeftEvent;
            _input.OnRightEvent -= OnRightEvent;
            _input.OnUpEvent -= OnUpEvent;
            _input.OnDownEvent -= OnDownEvent;
        }
        private void OnLeftEvent(float value)
        {
            var position = transform.position;
            transform.position = new Vector3(position.x + value * Settings.speed * Time.deltaTime, position.y,
                position.z);
        }

        private void OnRightEvent(float value)
        {
            var position = transform.position;
            transform.position = new Vector3(position.x - value * Settings.speed * Time.deltaTime, position.y,
                position.z);
        }

        private void OnUpEvent(float value)
        {  
            var position = transform.position;
            transform.position = new Vector3(position.x, position.y,
                position.z - value * Settings.speed * Time.deltaTime);
        }

        private void OnDownEvent(float value)
        {
            var position = transform.position;
            transform.position = new Vector3(position.x, position.y,
                position.z + value * Settings.speed * Time.deltaTime);
        }


        private void Update()
        {
            transform.rotation = Quaternion.identity;
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (Settings.health<=0)
            {
                Destroy();
            }
        }

        public void CreateDataSnapshot(GameData gameData)
        {
            var data = new PlayerData
            {
                Settings = Settings,
                Position = transform.position,
                Rotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z),
                Id = Id,
                IsActive = IsActive
            };
            gameData.Players.Add(data);
        }
        
        public void ApplyDataSnapshot(object obj)
        {
            if (obj is PlayerData data)
            {
                Settings = data.Settings;
                Id = data.Id;
                IsActive = data.IsActive;
            }
        }
        
        protected void Destroy()
        {
            _mainManager.RemovePlayer(this);
        }

        public class Factory : PlaceholderFactory<Player>
        {
        }


    }
}