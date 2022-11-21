using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Characters.Enemy;
using Script.Characters.PlayerSpace;
using Script.InputSpace;
using Script.Popup;
using UnityEngine;
using Zenject;

namespace Script.Core.Implementation
{
    public class MainManager : MonoBehaviour, IMainManager
    {
        private IMainUIController _mainUIController;
        private readonly List<Player> _players = new List<Player>();
        private readonly List<EnemyBase> _enemies = new List<EnemyBase>();
        private IBoot _boot;
        private ISaveService _saveService;
        private GeneratorId _generatorId;
        private IInput _input;
        public Player ActivePlayer { get; private set; }
        private bool _isFinishLevel;
        private bool _isGameOver;

        [Inject]
        private void Construct(IBoot boot, ILoader loader, ISaveService saveService, IInput input,
            IMainUIController mainUIController)
        {
            _boot = boot;
            _saveService = saveService;
            _mainUIController = mainUIController;
            _generatorId = new GeneratorId();
            _input = input;
            _isFinishLevel = false;
            _isGameOver = false;
        }

        private void OnEnable()
        {
            _mainUIController.SaveGame += SaveGame;
            _mainUIController.LoadGame += LoadGame;
            _mainUIController.ReLoadGame += ReLoad;
            _mainUIController.ClosedPopup += OnClosedPopup;
            _input.OnSwitchEvent += SwitchPlayer;
        }

        private void OnClosedPopup()
        {
            if (_isFinishLevel)
            {
                _boot.LoadByLevelId(_boot.Settings.StartLevelindex + 1);
            }

            if (_isGameOver)
            {
                _boot.ReLoad();
            }
        }

        private void OnDisable()
        {
            _mainUIController.SaveGame -= SaveGame;
            _mainUIController.LoadGame -= LoadGame;
            _mainUIController.ReLoadGame -= ReLoad;
            _mainUIController.ClosedPopup -= OnClosedPopup;
            _input.OnSwitchEvent -= SwitchPlayer;
        }

        private void SaveGame(int slotId)
        {
            StartCoroutine(Save(slotId, _boot.Settings.StartLevelindex));
        }

        private void LoadGame(int slotId)
        {
            StartCoroutine(Load(slotId));
        }

        public void ReLoad()
        {
            _boot.ReLoad();
        }

        void Update()
        {
            CheckPause();
        }

        private void CheckPause()
        {
            var button = Input.GetButtonDown("Pause");
            if (button)
            {
                _mainUIController.OpenPopup(PopupType.Pause);
            }
        }

        public void UpdatedFinishingAmount(int playersCount)
        {
            if (_players.Count == playersCount)
            {
                _isFinishLevel = true;
                _mainUIController.OpenPopup(PopupType.Win);
            }
        }

        public void Shot(Player target, int damage)
        {
            if (_players.Contains(target))
            {
                target.Settings.health -= damage;
                if (target.Settings.health < 0)
                {
                    target.Settings.health = 0;
                }

                CheckPlayers();
            }
        }

        private void CheckPlayers()
        {
            var isAnyKilled = _players.Any(p => p.Settings.health == 0);
            if (isAnyKilled)
            {
                _isGameOver = true;
                _mainUIController.OpenPopup(PopupType.GameOver);
            }
        }

        public IEnumerator Save(int slotId, int levelId)
        {
            _mainUIController.OpenPopup(PopupType.Save);
            yield return _saveService.Save(slotId, levelId, _players, _enemies);
            //emulate a call server
            yield return new WaitForSecondsRealtime(1);
            _mainUIController.CloseAllPopup();
        }

        public IEnumerator Load(int slotId)
        {
            _mainUIController.OpenPopup(PopupType.Load);
            yield return _saveService.Load(slotId);
            var data = _saveService.GetLastLoadedData();
            if (data == null)
            {
                _mainUIController.OpenPopup(PopupType.Error);
                yield break;
            }
            //emulate a call server
            yield return new WaitForSecondsRealtime(1);
            _mainUIController.CloseAllPopup();
            _boot.LoadByGameData(data);
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
            if (player.Id == -1)
            {
                player.Id = _generatorId.NextId();
                if (_players.Count == 1)
                {
                    player.Active();
                    ActivePlayer = player;
                }
            }
            else
            {
                if (player.IsActive)
                {
                    ActivePlayer = player;
                }
            }
        }

        public void AddEnemy(EnemyBase enemyBase)
        {
            _enemies.Add(enemyBase);
        }

        public void RemoveEnemy(EnemyBase enemyBase)
        {
            _enemies.Remove(enemyBase);
        }

        public void RemovePlayer(Player player)
        {
            _players.Remove(player);
            if (player.IsActive)
            {
                SwitchPlayer();
            }
        }

        public void SwitchPlayer()
        {
            if (_players.Count <= 1)
            {
                return;
            }

            var index = _players.IndexOf(ActivePlayer);
            _players[index].Deactivate();

            index = (index + 1) >= _players.Count ? 0 : index + 1;
            _players[index].Active();
            ActivePlayer = _players[index];
        }

        public Player GetPlayerById(int id)
        {
            return _players.FirstOrDefault(p => p.Id == id);
        }
    }
}